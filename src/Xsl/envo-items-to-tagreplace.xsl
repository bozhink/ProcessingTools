<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:reflect="Reflect" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:output method="xml" indent="no"/>

  <xsl:key name="distinct" match="reflect:item" use="translate(normalize-space(.), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')"/>

  <xsl:template match="/">
    <items>
      <xsl:for-each select="//reflect:item[generate-id() = generate-id(key('distinct', translate(normalize-space(.), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'))[1])]">
        <xsl:sort case-order="lower-first" order="descending" select="reflect:name"/>
        <xsl:message terminate="no">
          <xsl:value-of select="reflect:name"/>
        </xsl:message>
        <xsl:apply-templates select="."/>
      </xsl:for-each>
    </items>
  </xsl:template>

  <xsl:template match="reflect:item">
    <envo>
      <xsl:for-each select="reflect:entities/reflect:entity">
        <xsl:variable name="currentNumber">
          <xsl:number/>
        </xsl:variable>

        <xsl:attribute name="type{$currentNumber}">
          <xsl:value-of select="reflect:type"/>
        </xsl:attribute>

        <xsl:attribute name="identifier{$currentNumber}">
          <xsl:value-of select="reflect:identifier"/>
        </xsl:attribute>
      </xsl:for-each>

      <!--<xsl:attribute name="count">
        <xsl:value-of select="reflect:count"/>
      </xsl:attribute>-->

      <!--<xsl:attribute name="full-string">
        <xsl:value-of select="reflect:name"/>
      </xsl:attribute>-->

      <xsl:value-of select="reflect:name"/>
    </envo>
  </xsl:template>
</xsl:stylesheet>