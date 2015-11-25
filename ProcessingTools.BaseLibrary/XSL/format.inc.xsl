<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs">

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="surname|given-names|prefix|suffix|anonymous|etal">
    <xsl:element name="{name()}">
      <xsl:value-of select="normalize-space()" />
    </xsl:element>
  </xsl:template>

  <xsl:template match="tn//tn | tp:taxon-name//tn | a//tn | ext-link//tn | *[@object_id='82']//tn | *[@id='41']//tn | *[@id='236' or @id='436' or @id='435' or @id='418' or @id='49' or @id='417' or @id='48' or @id='434' or @id='433' or @id='432' or @id='431' or @id='430' or @id='429' or @id='428' or @id='427' or @id='426' or @id='425' or @id='424' or @id='423' or @id='422' or @id='421' or @id='420' or @id='419' or @id='475' or @id='414']/value//tn | xref//tn | tn//xref | tn//abbrev | tp:taxon-name//abbrev | xref//abbrev | aff/abbrev | institution//abbrev | source//abbrev">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="tn//tp:taxon-name | tp:taxon-name//tp:taxon-name | a//tp:* | ext-link//tp:* | *[@object_id='82']//tp:* | *[@id='41']//tp:* | *[@id='236' or @id='436' or @id='435' or @id='418' or @id='49' or @id='417' or @id='48' or @id='434' or @id='433' or @id='432' or @id='431' or @id='430' or @id='429' or @id='428' or @id='427' or @id='426' or @id='425' or @id='424' or @id='423' or @id='422' or @id='421' or @id='420' or @id='419' or @id='475' or @id='414']/value//tp:* | xref//tp:* | tp:taxon-name//xref">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="tp:treatment-meta/kwd-group/kwd/named-content//tn">
    <xsl:value-of select="string(.)" />
  </xsl:template>

  <xsl:template match="article/front/notes/sec//p//*[name()!='i'][name()!='italic'][name()!='sup'][name()!='sub'][name()!='ext-link']">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="tn-part[name(..)!='tn'][name(..)!='tp:taxon-name']|tp:taxon-name-part[name(..)!='tn'][name(..)!='tp:taxon-name']|tn-part//tn-part|tn-part//tp:*|tp:taxon-name-part//tp:*">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="a//a|a//ext-link|ext-link//a|ext-link//ext-link|a//xref|ext-link//xref|xref//xref|xref//a|xref//ext-link">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="date//institutional_code | quantity//quantity | //abbrev[normalize-space(@content-type)!='institution']//institutional_code[name(..)!='p']">
    <xsl:apply-templates />
  </xsl:template>

  <!-- Remove xref/bibr from ref. In some cases this is not needed. -->
  <xsl:template match="ref//xref[@ref-type='bibr' or @ref-type='fig' or @ref-type='table' or @ref-type='table-fn']">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="ref//institutional_code | ref//institution[@url]">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="email|self-uri|uri|ext-link">
    <xsl:element name="{name()}">
      <xsl:attribute name="xlink:type">
        <xsl:text>simple</xsl:text>
      </xsl:attribute>
      <xsl:apply-templates select="@* | node()" />
    </xsl:element>
  </xsl:template>

  <xsl:template match="tex-math">
    <xsl:element name="{name()}">
      <xsl:attribute name="id">
        <xsl:text>Math</xsl:text>
        <xsl:value-of select="generate-id()" />
      </xsl:attribute>
    </xsl:element>
  </xsl:template>

  <!-- license-p model -->
  <xsl:template match="license-p">
    <xsl:apply-templates select="." mode="license-p" />
  </xsl:template>

  <xsl:template match="@*" mode="license-p">
    <xsl:copy>
      <xsl:apply-templates select="@*" mode="license-p" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="text() | comment()" mode="license-p">
    <xsl:copy-of select="." />
  </xsl:template>

  <xsl:template match="license-p | email | ext-link | uri | inline-supplementary-material | related-article | related-object | address | alternatives | array | boxed-text | chem-struct-wrap | fig | fig-group | graphic | media | preformat | supplementary-material | table-wrap | table-wrap-group | disp-formula | disp-formula-group | element-citation | mixed-citation | nlm-citation | bold | italic | monospace | overline | roman | sans-serif | sc | strike | underline | award-id | funding-source | open-access | chem-struct | inline-formula | inline-graphic | private-char | def-list | list | tex-math | mml:math | abbrev | milestone-end | milestone-start | named-content | styled-content | disp-quote | speech | statement | verse-group | fn | target | xref | sub | sup | price" mode="license-p">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" mode="license-p" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="*" mode="license-p">
    <xsl:apply-templates mode="license-p" />
  </xsl:template>

  <xsl:template match="object-id">
    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <xsl:value-of select="string(.)" />
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>