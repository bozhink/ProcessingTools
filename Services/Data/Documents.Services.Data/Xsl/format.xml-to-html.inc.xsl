<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:tp="http://www.plazi.org/taxpub" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:mml="http://www.w3.org/1998/Math/MathML">

  <xsl:template name="set-elem-name">
    <xsl:attribute name="elem-name">
      <xsl:value-of select="name()" />
    </xsl:attribute>
  </xsl:template>

  <xsl:template name="set-default-class">
    <xsl:attribute name="class">
      <xsl:value-of select="local-name()" />
    </xsl:attribute>
  </xsl:template>

  <xsl:template match="@* | text() | comment()">
    <xsl:param name="output-node-name" select="'div'" />
    <xsl:copy-of select="." />
  </xsl:template>

  <xsl:template match="@xlink:*">
    <xsl:param name="output-node-name" select="'div'" />
    <xsl:copy-of select="." />
    <xsl:attribute name="{local-name()}">
      <xsl:value-of select="string(.)" />
    </xsl:attribute>
  </xsl:template>

  <xsl:template match="@xlink:href">
    <xsl:param name="output-node-name" select="'div'" />
    <xsl:copy-of select="." />
    <xsl:attribute name="{local-name()}">
      <xsl:variable name="type" select="string(../@ext-link-type)" />
      <xsl:variable name="content" select="normalize-space(.)" />
      <xsl:choose>
        <xsl:when test="$type = 'doi'">
          <xsl:text>http://dx.doi.org/</xsl:text>
          <xsl:value-of select="$content" />
        </xsl:when>
        <xsl:when test="$type = 'gen'">
          <xsl:text>http://www.ncbi.nlm.nih.gov/nuccore/</xsl:text>
          <xsl:value-of select="$content" />
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="$content" />
        </xsl:otherwise>
      </xsl:choose>
    </xsl:attribute>
  </xsl:template>

  <xsl:template match="*">
    <xsl:param name="output-node-name" select="'div'" />
    <xsl:call-template name="process-node">
      <xsl:with-param name="output-node-name" select="$output-node-name" />
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="process-node">
    <xsl:param name="output-node-name" select="'div'" />
    <xsl:element name="{$output-node-name}">
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()">
        <xsl:with-param name="output-node-name" select="$output-node-name" />
      </xsl:apply-templates>
    </xsl:element>
  </xsl:template>

  <xsl:template name="process-inner-node">
    <xsl:param name="output-node-name" select="'div'" />
    <xsl:call-template name="set-elem-name" />
    <xsl:call-template name="set-default-class" />
    <xsl:apply-templates select="@* | node()">
      <xsl:with-param name="output-node-name" select="$output-node-name" />
    </xsl:apply-templates>
  </xsl:template>
</xsl:stylesheet>
