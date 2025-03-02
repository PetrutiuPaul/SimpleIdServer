properties {
	$base_dir = resolve-path .
	$build_dir = "$base_dir\build"
	$source_dir = "$base_dir\src"
	$result_dir = "$build_dir\results"
	$global:config = "debug"
	$tag = $(git tag -l --points-at HEAD)
	$revision = @{ $true = "{0:00000}" -f [convert]::ToInt32("0" + $env:APPVEYOR_BUILD_NUMBER, 10); $false = "local" }[$env:APPVEYOR_BUILD_NUMBER -ne $NULL];
	$suffix = @{ $true = ""; $false = "ci-$revision"}[$tag -ne $NULL -and $revision -ne "local"]
	$commitHash = $(git rev-parse --short HEAD)
	$buildSuffix = @{ $true = "$($suffix)-$($commitHash)"; $false = "$($branch)-$($commitHash)" }[$suffix -ne ""]
    $versionSuffix = @{ $true = "--version-suffix=$($suffix)"; $false = ""}[$suffix -ne ""]
}


task default -depends local
task local -depends compile, test
task ci -depends clean, release, local, pack, publish

task publish {
	exec { dotnet publish $source_dir\OpenID\SimpleIdServer.OpenID.Startup\SimpleIdServer.OpenID.Startup.csproj -c $config -o $result_dir\services\OpenID }
	exec { dotnet publish $source_dir\OAuth\SimpleIdServer.OAuth.Startup\SimpleIdServer.OAuth.Startup.csproj -c $config -o $result_dir\services\OAuth }
	exec { dotnet publish $source_dir\Website\SimpleIdServer.Gateway.Host\SimpleIdServer.Gateway.Host.csproj -c $config -o $result_dir\services\SimpleIdServerApi }
	exec { dotnet publish $source_dir\OpenBankingApi\SimpleIdServer.OpenBankingApi.Startup\SimpleIdServer.OpenBankingApi.Startup.csproj -c $config -o $result_dir\services\OpenBankingApi }
}

task publishDocker -depends clean {
	exec { dotnet publish $source_dir\OpenID\SimpleIdServer.OpenID.SqlServer.Startup\SimpleIdServer.OpenID.SqlServer.Startup.csproj -c $config -o $result_dir\docker\OpenID }
	exec { dotnet publish $source_dir\Website\SimpleIdServer.Gateway.Host\SimpleIdServer.Gateway.Host.csproj -c $config -o $result_dir\docker\Gateway }
	exec { dotnet publish $source_dir\Scim\SimpleIdServer.Scim.SqlServer.Startup\SimpleIdServer.Scim.SqlServer.Startup.csproj -c $config -o $result_dir\docker\Scim }
	exec { npm run docker --prefix $source_dir\Website\SimpleIdServer.Website }
	exec { dotnet publish $source_dir\CaseManagement\CaseManagement.BPMN.Host\CaseManagement.BPMN.Host.csproj -c $config -o $result_dir\docker\Bpmn }
	exec { dotnet publish $source_dir\CaseManagement\CaseManagement.HumanTask.Host\CaseManagement.HumanTask.Host.csproj -c $config -o $result_dir\docker\HumanTask }
	exec { dotnet publish $source_dir\Saml\SimpleIdServer.Saml.Idp.EF.Startup\SimpleIdServer.Saml.Idp.EF.Startup.csproj -c $config -o $result_dir\docker\SamlIdp }
	exec { dotnet publish $source_dir\Scim\SimpleIdServer.Scim.Provisioning\SimpleIdServer.Scim.Provisioning.csproj -c $config -o $result_dir\docker\Provisioning }
	exec { docker-compose build }
	exec { docker-compose push }
}

task clean {
	rd "$source_dir\artifacts" -recurse -force  -ErrorAction SilentlyContinue | out-null
	rd "$base_dir\build" -recurse -force  -ErrorAction SilentlyContinue | out-null
}

task release {
    $global:config = "release"
}

task compile -depends clean {
	echo "build: Tag is $tag"
	echo "build: Package version suffix is $suffix"
	echo "build: Build version suffix is $buildSuffix" 
	
	exec { dotnet --version }
	exec { dotnet --info }

	exec { msbuild -version }
	
    # exec { msbuild .\SimpleIdServer.Mobile.sln /p:Configuration=$config /p:VersionSuffix=$buildSuffix }
    exec { msbuild .\SimpleIdServer.MSBuild.sln /p:Configuration=$config /p:VersionSuffix=$buildSuffix }
    exec { dotnet build .\SimpleIdServer.OAuth.Host.sln -c $config --version-suffix=$buildSuffix }
    exec { dotnet build .\SimpleIdServer.OpenId.Host.sln -c $config --version-suffix=$buildSuffix }
    exec { dotnet build .\SimpleIdServer.Scim.Host.sln -c $config --version-suffix=$buildSuffix }
    exec { dotnet build .\SimpleIdServer.Uma.Host.sln -c $config --version-suffix=$buildSuffix }
    exec { dotnet build .\SimpleIdServer.OpenBanking.Host.sln -c $config --version-suffix=$buildSuffix }
    exec { dotnet build .\SimpleIdServer.Saml.Host.sln -c $config --version-suffix=$buildSuffix }
    exec { dotnet build .\CaseManagement.sln -c $config --version-suffix=$buildSuffix }
    exec { dotnet build .\SimpleIdServer.Templates.sln -c $config --version-suffix=$buildSuffix }
}
 
task pack -depends release, compile {
	exec { dotnet publish $source_dir\MSBuild\Nuget.Transform.MSBuild.Task\Nuget.Transform.MSBuild.Task.csproj -c $config -f netcoreapp2.2 }
	exec { dotnet publish $source_dir\MSBuild\Nuget.Transform.MSBuild.Task\Nuget.Transform.MSBuild.Task.csproj -c $config -f net472 }
	exec { dotnet pack $source_dir\OAuth\SimpleIdServer.Jwt\SimpleIdServer.Jwt.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\OAuth\SimpleIdServer.OAuth\SimpleIdServer.OAuth.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\OAuth\SimpleIdServer.Common\SimpleIdServer.Common.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\OpenID\SimpleIdServer.OpenID\SimpleIdServer.OpenID.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\OpenID\SimpleIdServer.OpenID.Bootstrap4\SimpleIdServer.OpenID.Bootstrap4.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\OpenID\SimpleIdServer.OpenID.EF\SimpleIdServer.OpenID.EF.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\UI\SimpleIdServer.UI.Authenticate.LoginPassword\SimpleIdServer.UI.Authenticate.LoginPassword.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\UI\SimpleIdServer.UI.Authenticate.LoginPassword.Bootstrap4\SimpleIdServer.UI.Authenticate.LoginPassword.Bootstrap4.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\UI\SimpleIdServer.UI.Authenticate.Email\SimpleIdServer.UI.Authenticate.Email.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\UI\SimpleIdServer.UI.Authenticate.Email.Bootstrap4\SimpleIdServer.UI.Authenticate.Email.Bootstrap4.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\UI\SimpleIdServer.UI.Authenticate.Sms\SimpleIdServer.UI.Authenticate.Sms.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\UI\SimpleIdServer.UI.Authenticate.Sms.Bootstrap4\SimpleIdServer.UI.Authenticate.Sms.Bootstrap4.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\UI\SimpleIdServer.Saml.UI.Authenticate.LoginPassword\SimpleIdServer.Saml.UI.Authenticate.LoginPassword.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\UI\SimpleIdServer.UI.Bootstrap4\SimpleIdServer.UI.Bootstrap4.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\UI\SimpleIdServer.Saml.UI.Bootstrap4\SimpleIdServer.Saml.UI.Bootstrap4.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\UI\SimpleIdServer.Saml.UI.Authenticate.LoginPassword.Bootstrap4\SimpleIdServer.Saml.UI.Authenticate.LoginPassword.Bootstrap4.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\Scim\SimpleIdServer.Scim.Persistence.EF\SimpleIdServer.Scim.Persistence.EF.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\Scim\SimpleIdServer.Scim\SimpleIdServer.Scim.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\Scim\SimpleIdServer.Scim.Persistence.MongoDB\SimpleIdServer.Scim.Persistence.MongoDB.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\Scim\SimpleIdServer.Scim.SqlServer\SimpleIdServer.Scim.SqlServer.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\Scim\SimpleIdServer.Scim.Swashbuckle\SimpleIdServer.Scim.Swashbuckle.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\Uma\SimpleIdServer.Uma\SimpleIdServer.Uma.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\Uma\SimpleIdServer.Uma.Bootstrap4\SimpleIdServer.Uma.Bootstrap4.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\OpenBankingApi\SimpleIdServer.OpenBankingApi\SimpleIdServer.OpenBankingApi.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\OpenBankingApi\SimpleIdServer.OpenBankingApi.Domains\SimpleIdServer.OpenBankingApi.Domains.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\Saml\SimpleIdServer.Saml\SimpleIdServer.Saml.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\Saml\SimpleIdServer.Saml.Idp\SimpleIdServer.Saml.Idp.csproj -c $config --no-build $versionSuffix --output $result_dir }
	exec { dotnet pack $source_dir\Saml\SimpleIdServer.Saml.Sp\SimpleIdServer.Saml.Sp.csproj -c $config --no-build $versionSuffix --output $result_dir }
	# exec { dotnet pack $source_dir\Templates\SimpleIdServer.Templates.csproj -c $config --no-build $versionSuffix --output $result_dir }
}

task publishTemplate -depends release, compile {
	exec { dotnet pack $source_dir\Templates\SimpleIdServer.Templates.csproj -c $config --no-build $versionSuffix --output $result_dir }
}

task test {
    Push-Location -Path $base_dir\tests\SimpleIdServer.Jwt.Tests

    try {
        exec { & dotnet test -c $config --no-build --no-restore }
    } finally {
        Pop-Location
    }

    Push-Location -Path $base_dir\tests\SimpleIdServer.OAuth.Host.Acceptance.Tests

    try {
        exec { & dotnet test -c $config --no-build --no-restore }
    } finally {
        Pop-Location
    }

    Push-Location -Path $base_dir\tests\SimpleIdServer.OpenID.Host.Acceptance.Tests

    try {
        exec { & dotnet test -c $config --no-build --no-restore }
    } finally {
        Pop-Location
    }

    Push-Location -Path $base_dir\tests\SimpleIdServer.Scim.Tests

    try {
        exec { & dotnet test -c $config --no-build --no-restore }
    } finally {
        Pop-Location
    }

    Push-Location -Path $base_dir\tests\SimpleIdServer.Scim.Host.Acceptance.Tests

    try {
        exec { & dotnet test -c $config --no-build --no-restore }
    } finally {
        Pop-Location
    }

    Push-Location -Path $base_dir\tests\SimpleIdServer.Uma.Host.Acceptance.Tests

    try {
        exec { & dotnet test -c $config --no-build --no-restore }
    } finally {
        Pop-Location
    }

    Push-Location -Path $base_dir\tests\SimpleIdServer.OpenID.Tests

    try {
        exec { & dotnet test -c $config --no-build --no-restore }
    } finally {
        Pop-Location
    }

    Push-Location -Path $base_dir\tests\SimpleIdServer.OpenBankingApi.Host.Acceptance.Tests

    try {
        exec { & dotnet test -c $config --no-build --no-restore }
    } finally {
        Pop-Location
    }

    Push-Location -Path $base_dir\tests\SimpleIdServer.Saml.Tests

    try {
        exec { & dotnet test -c $config --no-build --no-restore }
    } finally {
        Pop-Location
    }

    Push-Location -Path $base_dir\tests\SimpleIdServer.Saml.Acceptance.Tests

    try {
        exec { & dotnet test -c $config --no-build --no-restore }
    } finally {
        Pop-Location
    }
}

task publishWebsite {
	exec { git checkout gh-pages }
	exec { git rm -r . }
	exec { git checkout HEAD -- .gitignore }
	exec { git add . }
	exec { git commit -m "Remove" }
	exec { git checkout master }
	exec { docfx ./docs/docfx.json }
	exec { Copy-item -Force -Recurse -Verbose "./docs/_site/*" -Destination "." }
	exec { git checkout gh-pages --merge }
	exec { git add . }
	exec { git commit -m "Update Documentation" }
	exec { git rebase -i HEAD~2 }
	exec { git push origin gh-pages }
	exec { git checkout master }
}