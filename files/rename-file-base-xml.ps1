#  $env:PSExecutionPolicyPreference

# Set-ExecutionPolicy -ExecutionPolicy Restricted -Scope CurrentUser
# Set-ExecutionPolicy -ExecutionPolicy Restricted -Scope MachinePolicy
# Set-ExecutionPolicy -ExecutionPolicy Restricted -Scope UserPolicy
# Set-ExecutionPolicy -ExecutionPolicy Restricted -Scope Process
# Set-ExecutionPolicy -ExecutionPolicy Restricted -Scope LocalMachine
# Set-ExecutionPolicy -ExecutionPolicy Restricted

# Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
# Get-ExecutionPolicy -List


if ($args.Count) {
    $Dirr = $args[0];
} else {
    $Dirr = ".";
}

$Path = "$Dirr\*.xml";

# Write-Warning $Path;
# Write-Warning $args.Count;

if (Test-Path $Path) {

    $XLinkNamespace = @{xlink="http://www.w3.org/1999/xlink";mml="http://www.w3.org/1998/Math/MathML";tp="http://www.plazi.org/taxpub"}

    $XPath = "//article/front/article-meta/fpage"

#    $ExternalFiles = Select-Xml -Path $path -XPath $xpath -Namespace $XLinkNamespace | Select-Object -ExpandProperty Node
    $firstPage = Select-Xml -Path $path -XPath $xpath -Namespace $XLinkNamespace | Select-Object -ExpandProperty Node

    foreach ($i in $firstPage.'#text') {
        Write-Output "$i";
#        if (Test-Path "$Dirr\$i") {
#            Write-Output "$i";
#        } else {
#            Write-Warning "There is no fpage";
#        }
    }

} else {
    Write-Warning "You have entered path '$Dirr' in which there are no XML files";
}