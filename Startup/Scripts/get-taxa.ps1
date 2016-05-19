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

    $XLinkNamespace = @{xlink="http://www.w3.org/1999/xlink";link="http://www.w3.org/1999/xlink";mml="http://www.w3.org/1998/Math/MathML";tp="http://www.plazi.org/taxpub"}

    $XPath = "//tp:taxon-name | //tn"
    #$XPath = "//tp:taxon-name"
    #$XPath = "//tp:treatment-sec"
    #$XPath = "//tp:treatment-sec/@sec-type"
    #Add-Content c:\scripts\test.txt "`nThe End" https://technet.microsoft.com/en-us/library/ee156791.aspx

    $ExternalFiles = Select-Xml -Path $path -XPath $xpath -Namespace $XLinkNamespace | Select-Object -ExpandProperty Node

    $ExternalFiles.OuterXml | Set-Content C:\temp\taxa.xml
    $ExternalFiles.OuterXml | Sort-Object | Set-Content C:\temp\taxa_s.xml

} else {
    Write-Warning "You have entered path '$Dirr' in which there are no XML files";
}