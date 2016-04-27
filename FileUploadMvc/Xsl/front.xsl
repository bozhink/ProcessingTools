<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="front">
    <header>
      <xsl:apply-templates />
    </header>
  </xsl:template>

  <xsl:template match="front/article-meta/title-group/article-title">
    <h2>
      <xsl:apply-templates />
    </h2>
  </xsl:template>

  <xsl:template match="front/article-meta/contrib-group/contrib">
    <address>
      <strong>
        <xsl:value-of select="name/prefix"/>
        <xsl:text> </xsl:text>
        <xsl:value-of select="name/given-names"/>
        <xsl:text> </xsl:text>
        <xsl:value-of select="name/surname"/>
        <xsl:text> </xsl:text>
        <xsl:value-of select="name/suffix"/>
      </strong>
      <br/>
      <xsl:for-each select="xref[@ref-type='aff']">
        <xsl:variable name="rid" select="@rid" />
        <xsl:apply-templates select="//front//aff[@id=$rid]/node()" />
      </xsl:for-each>
    </address>
  </xsl:template>

  <xsl:template match="front/article-meta/aff" />

</xsl:stylesheet>
