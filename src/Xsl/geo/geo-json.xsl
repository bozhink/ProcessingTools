<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="locality-coordinates[normalize-space(@latitude) != '' and normalize-space(@longitude) != ''][not(named-content[@content-type='geo-json'])]">
    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <named-content content-type="geo-json">
        <xsl:attribute name="specific-use">
          <xsl:text>{"type":"Point","coordinates":[</xsl:text>
          <xsl:value-of select="normalize-space(@longitude)" />
          <xsl:text>,</xsl:text>
          <xsl:value-of select="normalize-space(@latitude)" />
          <xsl:text>]}</xsl:text>
        </xsl:attribute>
        <xsl:apply-templates />
      </named-content>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>