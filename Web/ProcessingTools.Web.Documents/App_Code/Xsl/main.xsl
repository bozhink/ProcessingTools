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
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'div'" />
      </xsl:call-template>
    </article>
  </xsl:template>

  <!--
Block elements
-->

  <xsl:template match="front">
    <header>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'div'" />
      </xsl:call-template>
    </header>
  </xsl:template>

  <xsl:template match="front/article-meta/title-group/article-title">
    <h1>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </h1>
  </xsl:template>

  <xsl:template match="front/article-meta/contrib-group/contrib">
    <xsl:call-template name="process-node">
      <xsl:with-param name="output-node-name" select="'div'" />
    </xsl:call-template>
  </xsl:template>

  <xsl:template match="p">
    <p>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </p>
  </xsl:template>

  <xsl:template match="sec | ack | abstract | trans-abstract">
    <section>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'div'" />
      </xsl:call-template>
    </section>
  </xsl:template>

  <xsl:template match="*[count(ancestor::sec) = 0]/title">
    <h2>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </h2>
  </xsl:template>

  <xsl:template match="*[count(ancestor::sec) &gt; 0]/title">
    <h3>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </h3>
  </xsl:template>

  <xsl:template match="label">
    <h4>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </h4>
  </xsl:template>

  <xsl:template match="ref-list">
    <section>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'div'" />
      </xsl:call-template>
    </section>
  </xsl:template>

  <xsl:template match="ref-list/ref">
    <ul>
      <li>
        <xsl:call-template name="process-inner-node">
          <xsl:with-param name="output-node-name" select="'span'" />
        </xsl:call-template>
      </li>
    </ul>
  </xsl:template>

  <xsl:template match="nlm-citation | mixed-citation | element-citation">
    <div>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'mark'" />
      </xsl:call-template>
    </div>
  </xsl:template>

  <xsl:template match="app-group | app">
    <section>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'div'" />
      </xsl:call-template>
    </section>
  </xsl:template>

  <xsl:template match="list">
    <xsl:choose>
      <xsl:when test="@list-type='order'">
        <ol type="1">
          <xsl:call-template name="process-inner-node">
            <xsl:with-param name="output-node-name" select="'div'" />
          </xsl:call-template>
        </ol>
      </xsl:when>
      <xsl:otherwise>
        <ul>
          <xsl:call-template name="process-inner-node">
            <xsl:with-param name="output-node-name" select="'div'" />
          </xsl:call-template>
        </ul>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="li | list-item">
    <li>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'div'" />
      </xsl:call-template>
    </li>
  </xsl:template>

  <xsl:template match="fig">
    <figure>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'div'" />
      </xsl:call-template>
    </figure>
  </xsl:template>

  <xsl:template match="fig/caption">
    <figcaption>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'div'" />
      </xsl:call-template>
    </figcaption>
  </xsl:template>

  <xsl:template match="table | tr | td | th | thead | tbody | tfoot">
    <xsl:element name="{name()}">
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </xsl:element>
  </xsl:template>

  <!--
In-line elements
-->

  <!--%mixed-citation-elements-->
  <!--(#PCDATA | bold | italic | monospace | overline | roman | sans-serif | sc | strike | underline | alternatives | inline-graphic | private-char | chem-struct | inline-formula | label | abbrev | milestone-end | milestone-start | named-content | styled-content | annotation | article-title | chapter-title | collab | comment | conf-date | conf-loc | conf-name | conf-sponsor | date | date-in-citation | day | edition | email | elocation-id | etal | ext-link | fpage | gov | institution | isbn | issn | issue | issue-id | issue-part | issue-title | lpage | month | name | object-id | page-range | part-title | patent | person-group | pub-id | publisher-loc | publisher-name | role | season | series | size | source | std | string-name | supplement | trans-source | trans-title | uri | volume | volume-id | volume-series | year | sub | sup)*-->

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

  <xsl:template match="graphic | media | inline-graphic">
    <a target="_blank" href="{@xlink:href}">
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </a>
  </xsl:template>

  <xsl:template match="preformat">
    <pre>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </pre>
  </xsl:template>

  <xsl:template match="i | em | italic | Italic">
    <em>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </em>
  </xsl:template>

  <xsl:template match="b | bold | Bold | strong">
    <strong>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </strong>
  </xsl:template>

  <xsl:template match="u | underline">
    <u>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </u>
  </xsl:template>

  <xsl:template match="sub">
    <sub>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </sub>
  </xsl:template>

  <xsl:template match="sup">
    <sup>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </sup>
  </xsl:template>

  <xsl:template match="monospace">
    <kbd>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </kbd>
  </xsl:template>

  <xsl:template match="s | strike">
    <s>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
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
      <xsl:apply-templates select="node()">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:apply-templates>
    </span>
  </xsl:template>

  <xsl:template match="styled-content">
    <xsl:call-template name="process-node">
      <xsl:with-param name="output-node-name" select="'span'" />
    </xsl:call-template>
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
      <xsl:apply-templates select="@* | node()">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:apply-templates>
    </a>
  </xsl:template>

  <xsl:template match="target">
    <a>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </a>
  </xsl:template>

  <xsl:template match="abbrev">
    <abbr>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </abbr>
  </xsl:template>

  <xsl:template match="def | def/p">
    <span>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </span>
  </xsl:template>

  <xsl:template match="ext-link">
    <a target="_blank">
      <xsl:attribute name="href">
        <xsl:choose>
          <xsl:when test="@ext-link-type='doi'">
            <xsl:text>http://dx.doi.org/</xsl:text>
            <xsl:value-of select="@xlink:href"/>
          </xsl:when>
          <xsl:when test="@ext-link-type='gen'">
            <xsl:text>http://www.ncbi.nlm.nih.gov/nuccore/</xsl:text>
            <xsl:value-of select="@xlink:href"/>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="@xlink:href"/>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </a>
  </xsl:template>

  <!--
Taxonomy
-->
  <xsl:template match="tn | tp:taxon-name">
    <em class="taxon-name">
      <xsl:call-template name="set-elem-name" />
      <xsl:apply-templates select="@* | node()">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:apply-templates>
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
      <xsl:apply-templates select="@* | node()" />
    </em>
  </xsl:template>

  <xsl:template match="tp:taxon-treatment">
    <section id="{generate-id()}">
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'div'" />
      </xsl:call-template>
    </section>
  </xsl:template>

  <xsl:template match="tp:nomenclature">
    <header>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'div'" />
      </xsl:call-template>
    </header>
  </xsl:template>

  <xsl:template match="tp:treatment-sec">
    <section>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'div'" />
      </xsl:call-template>
    </section>
  </xsl:template>

  <xsl:template match="tp:nomenclature-citation-list">
    <ul>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'div'" />
      </xsl:call-template>
    </ul>
  </xsl:template>

  <xsl:template match="tp:nomenclature-citation">
    <li>
      <xsl:call-template name="process-inner-node">
        <xsl:with-param name="output-node-name" select="'span'" />
      </xsl:call-template>
    </li>
  </xsl:template>
</xsl:stylesheet>
