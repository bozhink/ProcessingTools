<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs">

  <xsl:output method="xml" indent="yes" encoding="utf-8"  cdata-section-elements="tex-math" />

  <xsl:strip-space elements="*" />
  <xsl:preserve-space elements="p label title article-title td th kwd email ext-link uri preformat bold b italic i monospace overline roman sans-serif sc strike s underline u award-id funding-source private-char tex-math abbrev def milestone-end milestone-start named-content styled-content disp-quote speech statement target xref xref-group sub sup tp:taxon-name source institution institutional_code envo mixed-citation" />

  <xsl:include href="format.inc.xsl" />

  <!--<xsl:template match="i[count(text()[normalize-space()!=''])=0][(count(*) = count(tn[@type='lower'])) or (count(*) = count(tp:taxon-name[@type='lower']))]">
    <xsl:apply-templates />
  </xsl:template>-->

  <xsl:template match="i | em | italic | Italic">
    <italic>
      <xsl:apply-templates select="@* | node()" />
    </italic>
  </xsl:template>

  <xsl:template match="b | strong | bold | Bold">
    <bold>
      <xsl:apply-templates select="@* | node()" />
    </bold>
  </xsl:template>

  <xsl:template match="u | underline">
    <underline>
      <xsl:apply-templates select="@* | node()" />
    </underline>
  </xsl:template>

  <xsl:template match="bold-italic | Bold-Italic">
    <bold>
      <italic>
        <xsl:apply-templates select="@* | node()" />
      </italic>
    </bold>
  </xsl:template>
  
  <xsl:template match="sec[title][@sec-type][normalize-space(@sec-type)='']/@sec-type">
    <xsl:attribute name="sec-type">
      <xsl:value-of select="normalize-space(../title[position() = 1])"/>
    </xsl:attribute>
  </xsl:template>

  <xsl:template match="tp:taxon-name | tn">
    <tp:taxon-name>
      <xsl:apply-templates select="@* | node()" />
    </tp:taxon-name>
  </xsl:template>

  <xsl:template match="tp:taxon-name-part | tn-part">
    <tp:taxon-name-part>
      <xsl:apply-templates select="@* | node()" />
    </tp:taxon-name-part>
    <xsl:if test="name(../..) != 'tp:nomenclature'">
      <xsl:if test="name(following-sibling::node()) = name() and normalize-space() != ''">
        <xsl:text> </xsl:text>
      </xsl:if>
    </xsl:if>
  </xsl:template>

  <xsl:template match="tn-part/@type">
    <xsl:attribute name="taxon-name-part-type">
      <xsl:value-of select="." />
    </xsl:attribute>
  </xsl:template>

  <xsl:template match="tp:nomenclature-citation[not(comment)]">
    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <comment>
        <xsl:apply-templates select="node()" />
      </comment>
    </xsl:element>
  </xsl:template>

  <xsl:template match="addr-line[count(node()) = count(text())]">
    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <xsl:value-of select="normalize-space(.)" />
    </xsl:element>
  </xsl:template>

  <xsl:template match="abbrev/def/p">
    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <xsl:if test="name(../../node()[position()=1]) != 'def'">
        <xsl:if test="name(node()[position()=1])='' and not(starts-with(normalize-space(node()[position()=1]),':')) and not(starts-with(normalize-space(node()[position()=1]),','))">
          <xsl:text> </xsl:text>
        </xsl:if>
      </xsl:if>

      <xsl:apply-templates select="node()" />

      <xsl:if test="name(../../node()[position()=last()]) != 'def'">
        <xsl:variable name="last-string" select="string(node()[position()=last()])" />
        <xsl:if test="substring($last-string, string-length($last-string) - 1, 1) != ' '">
          <xsl:text> </xsl:text>
        </xsl:if>
      </xsl:if>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>