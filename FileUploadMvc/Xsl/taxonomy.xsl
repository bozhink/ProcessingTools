<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:tp="http://www.plazi.org/taxpub">
  <xsl:template match="tn | tp:taxon-name">
    <em class="taxon-name">
      <xsl:apply-templates />
    </em>
  </xsl:template>

  <xsl:template match="tn-part | tp:taxon-name-part">
    <em>
      <xsl:attribute name="class">
        <xsl:text>taxon-name-part </xsl:text>
        <xsl:value-of select="@type" />
        <xsl:value-of select="@taxon-name-part-type" />
      </xsl:attribute>
      <xsl:apply-templates />
    </em>
  </xsl:template>

  <xsl:template match="tp:taxon-treatment">
    <article id="{generate-id()}" class="{local-name()}">
      <xsl:apply-templates />
    </article>
  </xsl:template>

  <xsl:template match="tp:nomenclature">
    <header class="{local-name()}">
      <xsl:apply-templates />
    </header>
  </xsl:template>

  <xsl:template match="tp:treatment-sec">
    <section class="{local-name()}">
      <xsl:apply-templates />
    </section>
  </xsl:template>

  <xsl:template match="tp:treatment-sec/title">
    <h3>
      <xsl:apply-templates />
    </h3>
  </xsl:template>

  <xsl:template match="tp:nomenclature-citation-list">
    <ul class="{local-name()}">
      <xsl:apply-templates />
    </ul>
  </xsl:template>

  <xsl:template match="tp:nomenclature-citation">
    <li class="{local-name()}">
      <xsl:apply-templates />
    </li>
  </xsl:template>
</xsl:stylesheet>
