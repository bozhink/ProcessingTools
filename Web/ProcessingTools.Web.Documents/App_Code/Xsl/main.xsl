<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:tp="http://www.plazi.org/taxpub" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:mml="http://www.w3.org/1998/Math/MathML">

  <xsl:include href="inc.xsl" />

  <xsl:output encoding="utf-8" indent="no" method="html" omit-xml-declaration="yes" version="5.0" media-type="text/html" standalone="yes" />

  <xsl:preserve-space elements="*" />

  <xsl:template match="/">
    <article>
      <xsl:attribute name="id">
        <xsl:value-of select="generate-id()" />
      </xsl:attribute>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </article>
  </xsl:template>

  <!--
Block elements
-->

  <xsl:template match="front">
    <header>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </header>
  </xsl:template>

  <xsl:template match="front/article-meta/title-group/article-title">
    <h1>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </h1>
  </xsl:template>

  <xsl:template match="front/article-meta/contrib-group/contrib">
    <div>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </div>
  </xsl:template>

  <xsl:template match="p">
    <p>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </p>
  </xsl:template>

  <xsl:template match="sec | ack | abstract | trans-abstract">
    <section>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </section>
  </xsl:template>

  <xsl:template match="*[count(ancestor::sec) = 0]/title">
    <h2>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </h2>
  </xsl:template>

  <xsl:template match="*[count(ancestor::sec) &gt; 0]/title">
    <h3>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </h3>
  </xsl:template>

  <xsl:template match="label">
    <h4>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </h4>
  </xsl:template>

  <xsl:template match="ref-list">
    <section>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@*" />
      <xsl:apply-templates select="label | title" />
      <xsl:apply-templates select="address | alternatives | array | boxed-text | chem-struct-wrap | fig | fig-group | graphic | media | preformat | supplementary-material | table-wrap | table-wrap-group | disp-formula | disp-formula-group | p | def-list | list | tex-math | related-article | related-object | disp-quote | speech | statement | verse-group | comment()" />
      <ul>
        <xsl:apply-templates select="ref" />
      </ul>
      <xsl:apply-templates select="ref-list" />
    </section>
  </xsl:template>

  <xsl:template match="ref-list/ref">
    <li>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </li>
  </xsl:template>

  <xsl:template match="nlm-citation | mixed-citation | element-citation">
    <div>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </div>
  </xsl:template>

  <xsl:template match="app-group">
    <section>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </section>
  </xsl:template>

  <xsl:template match="app">
    <section>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </section>
  </xsl:template>

  <xsl:template match="list">
    <xsl:choose>
      <xsl:when test="@list-type='order'">
        <ol type="1">
          <xsl:call-template name="set-elem-name" />
          <xsl:call-template name="set-default-class" />
          <xsl:apply-templates select="@* | node()" />
        </ol>
      </xsl:when>
      <xsl:otherwise>
        <ul>
          <xsl:call-template name="set-elem-name" />
          <xsl:call-template name="set-default-class" />
          <xsl:apply-templates select="@* | node()" />
        </ul>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="li | list-item">
    <li>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </li>
  </xsl:template>

  <xsl:template match="fig">
    <figure>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </figure>
  </xsl:template>

  <xsl:template match="fig/caption">
    <figcaption>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </figcaption>
  </xsl:template>

  <!--<xsl:template match="fig/graphic">
    <img src="{@xlink:href}">
      <xsl:apply-templates select="@*" />
      <xsl:apply-templates />
    </img>
  </xsl:template>-->

  <!--
In-line elements
-->

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

  <xsl:template match="i | em | italic | Italic">
    <em>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </em>
  </xsl:template>

  <xsl:template match="b | bold | Bold | strong">
    <strong>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </strong>
  </xsl:template>

  <xsl:template match="u | underline">
    <u>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </u>
  </xsl:template>

  <xsl:template match="sub">
    <sub>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </sub>
  </xsl:template>

  <xsl:template match="sup">
    <sup>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </sup>
  </xsl:template>

  <xsl:template match="monospace">
    <kbd>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </kbd>
  </xsl:template>

  <xsl:template match="s | strike">
    <s>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </s>
  </xsl:template>

  <xsl:template match="named-content">
    <span>
      <xsl:call-template name="set-elem-name" />
      <xsl:apply-templates select="@*" />
      <xsl:attribute name="class">
        <xsl:value-of select="local-name()" />
        <xsl:if test="normalize-space(@content-type)!=''">
          <xsl:text> </xsl:text>
          <xsl:value-of select="normalize-space(@content-type)" />
        </xsl:if>
      </xsl:attribute>
      <xsl:apply-templates select="node()" mode="inline" />
    </span>
  </xsl:template>

  <xsl:template match="styled-content">
    <span>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </span>
  </xsl:template>

  <xsl:template match="br | break">
    <span>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
    </span>
  </xsl:template>

  <xsl:template match="xref">
    <a href="#{@rid}" class="xref {@ref-type}">
      <xsl:call-template name="set-elem-name" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </a>
  </xsl:template>

  <xsl:template match="abbrev">
    <abbr>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </abbr>
  </xsl:template>

  <xsl:template match="def | def/p">
    <span>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </span>
  </xsl:template>

  <xsl:template match="ext-link[@ext-link-type='uri']">
    <a target="_blank" href="{@xlink:href}">
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </a>
  </xsl:template>

  <xsl:template match="ext-link[@ext-link-type='doi']">
    <a target="_blank" href="http://dx.doi.org/{@xlink:href}">
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </a>
  </xsl:template>

  <!--
Taxonomy
-->
  <xsl:template match="tn | tp:taxon-name">
    <em class="taxon-name">
      <xsl:call-template name="set-elem-name" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </em>
  </xsl:template>

  <xsl:template match="tn-part | tp:taxon-name-part">
    <em>
      <xsl:attribute name="class">
        <xsl:text>taxon-name-part </xsl:text>
        <xsl:value-of select="@type" />
        <xsl:value-of select="@taxon-name-part-type" />
      </xsl:attribute>
      <xsl:call-template name="set-elem-name" />
      <xsl:apply-templates select="@* | node()" mode="inline" />
    </em>
  </xsl:template>

  <xsl:template match="tp:taxon-treatment">
    <section id="{generate-id()}">
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </section>
  </xsl:template>

  <xsl:template match="tp:nomenclature">
    <header>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </header>
  </xsl:template>

  <xsl:template match="tp:treatment-sec">
    <section>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </section>
  </xsl:template>


  <xsl:template match="tp:nomenclature-citation-list">
    <ul>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </ul>
  </xsl:template>

  <xsl:template match="tp:nomenclature-citation">
    <li>
      <xsl:call-template name="set-elem-name" />
      <xsl:call-template name="set-default-class" />
      <xsl:apply-templates select="@* | node()" />
    </li>
  </xsl:template>
</xsl:stylesheet>
