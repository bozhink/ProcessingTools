<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:output method="xml" indent="no"/>
  
  <xsl:key name="distinct" match="item" use="translate(normalize-space(.), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')"/>

  <xsl:template match="/">
    <items>
      <xsl:for-each select="//item[generate-id() = generate-id(key('distinct', translate(normalize-space(.), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'))[1])]">
        <xsl:sort case-order="lower-first" order="descending" select="name"/>
        <xsl:message terminate="no">
          <xsl:value-of select="name"/>
        </xsl:message>
        <xsl:apply-templates select="."/>
      </xsl:for-each>
    </items>
  </xsl:template>

  <xsl:template match="item">
    <envo>
      <xsl:for-each select="entities/entity">
        <xsl:variable name="currentNumber">
          <xsl:number/>
        </xsl:variable>
        <xsl:attribute name="type{$currentNumber}">
          <xsl:value-of select="type"/>
        </xsl:attribute>
        <xsl:attribute name="identifier{$currentNumber}">
          <xsl:value-of select="identifier"/>
        </xsl:attribute>
      </xsl:for-each>
      <!--<xsl:attribute name="count">
        <xsl:value-of select="count"/>
      </xsl:attribute>-->
      <!--<xsl:attribute name="full-string">
        <xsl:value-of select="name"/>
      </xsl:attribute>-->
      <xsl:value-of select="name"/>
    </envo>
  </xsl:template>
</xsl:stylesheet>