function Make-Directory
{
    param( [string]$Path );
    if (!(Test-Path $Path)) {
        New-Item -ItemType Directory -Path $Path > $null;
    }
}

if ($args.Count) {
    $dir = $args[0];
} else {
    $dir = '.';
}

$Path = "$dir";
$PathPrefix = "$dir\XML\";
$PathSuffix = '\external-files';

Make-Directory "$PathPrefix\backup";

$matchArticleIdPattern = '^(?:\d+[_\W]\d+|\d+)';
$matchArticleId = [regex] "$matchArticleIdPattern";
$matchArticleFileName = [regex] "$matchArticleIdPattern.*\.(?:indd|xml)$";

$fileNames = Get-ChildItem $Path | Select-Object -Property Name;

foreach ($fileName in $fileNames) {
    if ($matchArticleFileName.IsMatch($fileName.Name)) {
        echo $fileName.Name;

        $currentPath = $PathPrefix + $matchArticleId.Match($fileName.Name).Value + $PathSuffix;

        Make-Directory $currentPath;
    }
}
