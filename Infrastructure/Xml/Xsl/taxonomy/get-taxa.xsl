<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:tp="http://www.plazi.org/taxpub" exclude-result-prefixes="xs mml xlink tp xsi" xmlns:mml="http://www.w3.org/1998/Math/MathML">

  <xsl:output method="xml" encoding="UTF-8" indent="yes" />

  <xsl:template match="/">
    <taxa>
      <!-- NLM -->
      <xsl:for-each select="distinct-values(//tp:taxon-name)">
        <xsl:sort />
        <taxon>
          <xsl:value-of select="normalize-space(.)" />
        </taxon>
      </xsl:for-each>
      <!-- System -->
      <!-- <xsl:for-each select="distinct-values(//tn[count(tn-part[@type='genus'])=0])"> -->
      <xsl:for-each select="distinct-values(//tn)">
        <xsl:sort />
        <taxon>
          <xsl:value-of select="normalize-space(.)" />
        </taxon>
      </xsl:for-each>
      <!-- This schema -->
      <xsl:for-each select="distinct-values(//taxon[count(@*)=0])">
        <xsl:sort />
        <taxon>
          <xsl:value-of select="normalize-space(.)" />
        </taxon>
      </xsl:for-each>
    </taxa>
  </xsl:template>
</xsl:stylesheet>
