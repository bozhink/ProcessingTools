declare namespace xlink = "http://www.w3.org/1999/xlink";

declare function local:trim($string as xs:string)
as xs:string
{
  let $result := replace(replace($string, '\s+', ' '), '^\s+|\s+$', '')
  return $result
};

declare function local:clean-conent($string as xs:string)
as xs:string
{
  let $result := replace(replace(replace(local:trim($string), '^\((.*?)\)$', '$1'), '^\[(.*?)\]$', '$1'), '^\{(.*?)\}$', '$1')
  return replace($result, '^[=,;: –—−\-]+|[=,;: –—−\-]+$', '')
};

let $abbrev := for $a in document-node()//abbrev
  return <abbrev>
    {for $val in local:clean-conent(string($a/node()[name(.)!='def']))
      return <value>{$val}</value>}
    {for $def in local:clean-conent(string($a/def/p|$a/@xlink:title))
      return <definition>{data($def)}</definition>}
  </abbrev>

<root>
{
    for $a in $abbrev
    return $a
}
</root>