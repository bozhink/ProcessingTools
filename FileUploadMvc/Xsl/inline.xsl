<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <!-- %p-elements -->
  <!-- email | ext-link | uri | inline-supplementary-material | related-article
		| related-object | address | alternatives | array | boxed-text | chem-struct-wrap
		| fig | fig-group | graphic | media | preformat | supplementary-material
		| table-wrap | table-wrap-group | disp-formula | disp-formula-group | element-citation
		| mixed-citation | nlm-citation | bold | italic | monospace | overline |
		roman | sans-serif | sc | strike | underline | award-id | funding-source
		| open-access | chem-struct | inline-formula | inline-graphic | private-char
		| def-list | list | tex-math | mml:math | abbrev | milestone-end | milestone-start
		| named-content | styled-content | disp-quote | speech | statement | verse-group
		| fn | target | xref | sub | sup -->

  <xsl:template match="i | italic">
    <em>
      <xsl:apply-templates />
    </em>
  </xsl:template>

  <xsl:template match="b | bold">
    <strong>
      <xsl:apply-templates />
    </strong>
  </xsl:template>

  <xsl:template match="u | underline">
    <u>
      <xsl:apply-templates />
    </u>
  </xsl:template>

  <xsl:template match="sub">
    <sub>
      <xsl:apply-templates />
    </sub>
  </xsl:template>

  <xsl:template match="sup">
    <sup>
      <xsl:apply-templates />
    </sup>
  </xsl:template>

  <xsl:template match="monospace">
    <kbd>
      <xsl:apply-templates />
    </kbd>
  </xsl:template>

  <xsl:template match="strike">
    <s>
      <xsl:apply-templates />
    </s>
  </xsl:template>

  <xsl:template match="named-content">
    <span>
      <xsl:apply-templates select="@*" />
      <xsl:attribute name="class">
        <xsl:value-of select="local-name()" />
      </xsl:attribute>
      <xsl:apply-templates />
    </span>
  </xsl:template>

  <xsl:template match="br | break">
    <br />
  </xsl:template>

  <xsl:template match="xref">
    <a href="#{@rid}" class="xref {@ref-type}">
      <xsl:apply-templates />
    </a>
  </xsl:template>
</xsl:stylesheet>
