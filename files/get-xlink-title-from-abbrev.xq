xquery version "1.0";
declare namespace xlink = "http://www.w3.org/1999/xlink";

<abbreviations  xmlns:mml="http://www.w3.org/1998/Math/MathML" xmlns:tp="http://www.plazi.org/taxpub" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
{
  for $a in distinct-values(//abbrev/@xlink:title)
    return <a>{string($a)}</a>
}
</abbreviations>
