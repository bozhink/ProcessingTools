<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:include href="front.xsl" />
  <xsl:include href="inline.xsl" />
  <xsl:include href="taxonomy.xsl" />
  <xsl:include href="block-elements.xsl" />
  <xsl:include href="external-links.xsl" />
  <xsl:include href="figures.xsl" />

  <xsl:output encoding="utf-8" indent="yes" method="html" omit-xml-declaration="yes" version="5.0" media-type="text/html" standalone="yes" />

  <xsl:preserve-space elements="*" />

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="article">
    <article>
      <xsl:attribute name="id">
        <xsl:value-of select="generate-id()" />
      </xsl:attribute>
      <xsl:apply-templates />
    </article>
  </xsl:template>
</xsl:stylesheet>
