(:
let $reordered := for $x in distinct-values(doc("pensoft.taxonomy.rankList.xml")//taxon/part/value)
  return <taxon><part>
    <value>{$x}</value>
    <rank>
      {for $r in doc("pensoft.taxonomy.rankList.xml")//taxon/part[value = $x]/rank/value
        return $r}
    </rank>
  </part></taxon>

for $z in $reordered[count(.//rank/value) > 1]
return $z
:)
let $reordered := for $x in distinct-values(doc("pensoft.taxonomy.rankList.xml")//taxon/part/value)
  return <taxon-rank taxon-name="{$x}">
      {for $r in doc("pensoft.taxonomy.rankList.xml")//taxon/part[value = $x]/rank/value
        return <rank value="{data($r)}"></rank>}
  </taxon-rank>

for $z in $reordered[count(.//rank) > 1]
return $z