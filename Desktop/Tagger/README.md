# Tagger

## Tagging Process

* Initial format [-i]
  * correct invalid punctuation in italics. Search with **&lt;italic&gt;(\s\*\.|[^<>]\*[”“&/,;-])|\.\s\*&lt;/italic&gt;|&lt;/italic&gt;\.?\s\*&lt;italic&gt;**
<!--
<italic>(\s*\.|[^<>]*[”“&/,;-])|\.\s*</italic>|</italic>\.?\s*<italic>
-->

* Tag external links (web links) [-w, -d]
  * URL
  * DOI
  * PMID

* Process static content
  * Tag coordinates: [-c] or manual search with **\[º°˚\]|(?&lt;!\[\u\l\])\[NSWE\](?!\[\u\l\])**.
    * Wrapping tag name should be *locality-coordinates*
    * If a coordinate is split in columns of a **table** use *locality-coordinates\[@type="latitude"\]* or *locality-coordinates\[@type="longitude"\]* for corresponding part. Different part will be combined automatically if in a *tr* there is only one *locality-coordinates* of each type.
  * Parse coordinates [-k]
    * Attributes *@latitude* and *@longitude* of the *locality-coordinates* will be set.
    * If these attributes are set, in the final XSLT execution, child element *named-content\[@content-type="geo-json"\]* will be created in each *locality-coordinates* element.
  * Other: tag dates [+TagDates], tag altitudes, quantities, etc.

* Tag internal cross-references (x-ref)
  * Floating objects (figures, tables, supplementary materials, etc.) [-f]
    * Manually move figures and tables to their corresponding place in the document.
  * Bibliographic references [-R]
    * Check for malformed references in the file *\*-references.xml* with **""|,_|_\[^&lt;&gt;,"\]\*\s**.
    * Correct errors and re-run [-R] if needed.
    * Manually check for missed citations.

* Taxonomy
  * Tag lower taxa [-A]
  * Tag higher taxa [-B]
  * Parse lower taxa ranks [-C]
  * Parse higher taxa ranks [-D]
  * Resolve abbreviated taxa names [-E]
  * Format treatment nomenclatures [-t]
  * Resolve taxonomic classifications in nomenclatural acts [parse treatment meta]

* Tag abbreviations [--abbrev]

* Bio-codes
  * Tag institutional codes and instituional names
  * Tag collection codes and collection names
  * Tag known specimen codes (e.g., OSUC, CASENT, EMEC)
  * Tag other specimen codes

* Data validation
  * Validate cross-references [+validatec]
  * Validate taxa names [+validatet] [Redis cache is optional]
  * Validate external links [+validatee] [Redis cache is optional]
