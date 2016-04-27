<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xlink="http://www.w3.org/1999/xlink">
  <xsl:template match="fig">
    <figure>
      <xsl:apply-templates select="@*" />
      <xsl:apply-templates />
    </figure>
  </xsl:template>

  <xsl:template match="fig/caption">
    <figcaption>
      <xsl:apply-templates />
    </figcaption>
  </xsl:template>

  <xsl:template match="fig/label">
    <h4 class="figure label">
      <xsl:apply-templates />
    </h4>
  </xsl:template>

  <xsl:template match="fig/graphic">
    <!-- <img src="{@xlink:href}">
			<xsl:apply-templates select="@*" />
			<xsl:apply-templates />
		</img> -->
  </xsl:template>
</xsl:stylesheet>
