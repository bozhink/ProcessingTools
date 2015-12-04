if ($args.Count) {
    $Dirr = $args[0];
} else {
    $Dirr = ".";
}

$OutputFileName = "get-coordinates-out.txt";
$TempOutputFileName = "get-coordinates-out-temp.txt";

$Path = "$Dirr\*.xml";

if (Test-Path $Path) {

    $XLinkNamespace = @{xlink="http://www.w3.org/1999/xlink";link="http://www.w3.org/1999/xlink";mml="http://www.w3.org/1998/Math/MathML";tp="http://www.plazi.org/taxpub"};

    $XPath = "//locality-coordinates|//named-content[@content-type='dwc:verbatimCoordinates']";

    $ExternalFiles = Select-Xml -Path $Path -XPath $xpath -Namespace $XLinkNamespace | Select-Object -ExpandProperty Node;

    foreach ($i in $ExternalFiles) {
        Add-Content -Path $TempOutputFileName ($i.'#text' + " -- " + $i.latitude + " " + $i.longitude);
    }

} else {
    Write-Warning "You have entered path '$Dirr' in which there are no XML files";
}

if (Test-Path $TempOutputFileName) {
    Get-Content $TempOutputFileName | Sort-Object -Unique | Set-Content $OutputFileName;
    Remove-Item $TempOutputFileName;
}