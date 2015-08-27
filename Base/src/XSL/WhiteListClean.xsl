<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="no" omit-xml-declaration="yes" encoding="utf-8"/>
  <xsl:strip-space elements="taxa taxon part rank full-name"/>
  <xsl:key name="clean" match="item" use="normalize-space(.)"/>
  <xsl:template match="/">
    <list>
      <xsl:for-each select="//item[generate-id() = generate-id(key('clean', normalize-space(.))[1])]">
        <xsl:sort/>
        <item>
          <xsl:value-of select="normalize-space(.)"/>
        </item>
      </xsl:for-each>
    </list>
  </xsl:template>
</xsl:stylesheet>