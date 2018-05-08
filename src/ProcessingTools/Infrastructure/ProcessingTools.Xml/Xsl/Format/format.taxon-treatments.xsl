<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs">

  <xsl:output method="xml" encoding="UTF-8" omit-xml-declaration="no" indent="no" cdata-section-elements="tex-math" />

  <xsl:preserve-space elements="*" />

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="tp:treatment-sec/p">
    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <xsl:attribute name="id">
        <xsl:text>P</xsl:text>
        <xsl:value-of select="generate-id()" />
      </xsl:attribute>
      <xsl:attribute name="content-type">
        <xsl:text>taxon-treatment</xsl:text>
      </xsl:attribute>
      <xsl:attribute name="specific-use">
        <xsl:choose>
          <xsl:when test="normalize-space(../@sec-type)=''">
            <xsl:value-of select="local-name(..)" />
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="translate(normalize-space(../@sec-type), 'QWERTYUIOPASDFGHJKLZXCVBNM', 'qwertyuiopasdfghjklzxcvbnm')" />
          </xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:apply-templates />
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>