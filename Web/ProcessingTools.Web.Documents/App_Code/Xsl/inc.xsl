<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

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
    <xsl:copy-of select="." />
  </xsl:template>

  <xsl:template match="*">
    <div>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </div>
  </xsl:template>

  <xsl:template match="@* | text() | comment()" mode="inline">
    <xsl:copy-of select="." />
  </xsl:template>

  <xsl:template match="*" mode="inline">
    <div>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()"  mode="inline" />
    </div>
  </xsl:template>
</xsl:stylesheet>
