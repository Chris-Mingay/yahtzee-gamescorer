param
(
    [Parameter(Mandatory=$true)]
    [string]$Name,
    [Parameter(Mandatory=$true)]
    [string]$Context
)

Write-Host

$AcceptedContexts = @("All","Postgres","Sqlite","SqlServer")

If (-not ($AcceptedContexts -contains $Context))
{
    Write-Warning "'$Context' is not a valid context  must be one of 'All', 'Postgres', 'Sqlite' or 'SqlServer'"
    Write-Host
    exit
}

switch ($Context) {
    "All" {  
        Foreach ($i in $AcceptedContexts)
        {
            if($i -ne "All")
            {
                Write-Host
                Write-Host "Generating migration for $i context"
                Write-Host
                $Command = dotnet ef migrations add $Name --startup-project src/WebApi/WebApi.csproj --project src/Infrastructure/Infrastructure.csproj --context "$($i)ApplicationDbContext"
                Write-Output $Command
                Write-Host
            }
        }
        
    }
    Default {
        Write-Host
        Write-Host "Generating migration for $Context context"
        Write-Host
        $Command = dotnet ef migrations add $Name --startup-project src/WebApi/WebApi.csproj --project src/Infrastructure/Infrastructure.csproj --context "$($Context)ApplicationDbContext"
        Write-Output $Command
        Write-Host
    }
}



