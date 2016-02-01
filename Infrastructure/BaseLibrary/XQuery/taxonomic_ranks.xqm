for $x in distinct-values(doc("taxonomy.rankList.xml")//taxon/part/rank/value)
order by $x
return data($x)