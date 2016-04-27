<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="p">
    <p>
      <xsl:apply-templates />
    </p>
  </xsl:template>

  <xsl:template match="sec | abstract | trans-abstract">
    <section>
      <xsl:apply-templates />
    </section>
  </xsl:template>

  <xsl:template match="sec[count(ancestor::node()[name()='sec'])=0]/title">
    <h3>
      <xsl:apply-templates />
    </h3>
  </xsl:template>

  <xsl:template match="sec[count(ancestor::node()[name()='sec'])!=0]/title">
    <h4>
      <xsl:apply-templates />
    </h4>
  </xsl:template>

  <xsl:template match="abstract/title | abstract/label | trans-abstract/title | trans-abstract/label">
    <h4>
      <xsl:apply-templates />
    </h4>
  </xsl:template>

  <xsl:template match="ack">
    <section>
      <xsl:apply-templates />
    </section>
  </xsl:template>

  <xsl:template match="ack/title">
    <h4>
      <xsl:apply-templates />
    </h4>
  </xsl:template>

  <xsl:template match="ref-list">
    <section>
      <xsl:apply-templates select="label | title" />
      <xsl:apply-templates select="address | alternatives | array | boxed-text | chem-struct-wrap | fig | fig-group | graphic | media | preformat | supplementary-material | table-wrap | table-wrap-group | disp-formula | disp-formula-group | p | def-list | list | tex-math | related-article | related-object | disp-quote | speech | statement | verse-group" />
      <ul>
        <xsl:apply-templates select="ref" />
      </ul>
      <xsl:apply-templates select="ref-list" />
    </section>
  </xsl:template>

  <xsl:template match="ref-list/title">
    <h4>
      <xsl:apply-templates />
    </h4>
  </xsl:template>

  <xsl:template match="ref-list/ref">
    <li>
      <xsl:apply-templates select="@id" />
      <xsl:apply-templates />
    </li>
  </xsl:template>

  <xsl:template match="app-group">
    <section>
      <xsl:apply-templates />
    </section>
  </xsl:template>

  <xsl:template match="app">
    <section>
      <xsl:apply-templates />
    </section>
  </xsl:template>

  <xsl:template match="app/title">
    <h4>
      <xsl:apply-templates />
    </h4>
  </xsl:template>
</xsl:stylesheet>
