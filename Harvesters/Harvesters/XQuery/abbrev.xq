declare namespace xlink = "http://www.w3.org/1999/xlink";

declare function local:trim($string as xs:string)
as xs:string
{
  let $result := replace(replace($string, '\s+', ' '), '^\s+|\s+$', '')
  return $result
};

declare function local:clean-content($string as xs:string)
as xs:string
{
  let $result := replace(replace(replace(local:trim($string), '^\((.*?)\)$', '$1'), '^\[(.*?)\]$', '$1'), '^\{(.*?)\}$', '$1')
  return replace($result, '^[=,;: –—−\-]+|[=,;: –—−\-]+$', '')
};

<abbreviations>
{
  for $a in //abbrev
    return <abbreviation>
      {
        for $val in local:clean-content(string($a/node()[name(.)!='def']))
        return <value>{$val}</value>
      }
      {
        for $def in local:clean-content(string($a/def/p|$a/@xlink:title))
        return <definition>{data($def)}</definition>
      }
      {
        for $type in $a/@content-type
        return <content-type>{data($type)}</content-type>
      }
    </abbreviation>
}
</abbreviations>