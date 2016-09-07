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
    return <abbreviation content-type="{data($a/@content-type)}">
		<value>
		  {
			for $val in $a/node()[name(.)!='def']/local:clean-content(string(.))
			where string($val) != ''
			return data($val)
		  }
		</value>
		<definition>
		  {
			if ($a/def)
			then for $def in $a/def/p/local:clean-content(string(.))
			  where string($def) != ''
			  return data($def)
			else for $def in $a/@xlink:title/local:clean-content(string(.))
			  where string($def) != ''
			  return data($def)
		  }
		</definition>
    </abbreviation>
}
</abbreviations>