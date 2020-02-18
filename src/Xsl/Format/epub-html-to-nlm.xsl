<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs">

  <xsl:output method="xml" encoding="UTF-8" omit-xml-declaration="no" indent="no" cdata-section-elements="tex-math" />

  <xsl:preserve-space elements="*" />

  <xsl:strip-space elements="addr-line aff article-id" />

  <xsl:template match="/">
    <article article-type="research-article" dtd-version="3.0" xml:lang="en">
      <front>
        <journal-meta>
          <journal-id journal-id-type="publisher-id"></journal-id>
          <journal-title-group>
            <journal-title xml:lang="en"></journal-title>
            <abbrev-journal-title xml:lang="en"></abbrev-journal-title>
          </journal-title-group>
          <issn pub-type="ppub"></issn>
          <issn pub-type="epub"></issn>
          <publisher>
            <publisher-name></publisher-name>
          </publisher>
        </journal-meta>
        <article-meta>
          <article-id pub-id-type="doi"></article-id>
          <article-categories>
            <subj-group subj-group-type="heading">
              <subject>Research article</subject>
            </subj-group>
          </article-categories>
          <title-group>
            <article-title>
              <xsl:apply-templates select="//h1/node()" />
            </article-title>
          </title-group>
          <contrib-group>
            <contrib contrib-type="author" xlink:type="simple">
              <name name-style="western">
                <surname></surname>
                <given-names></given-names>
              </name>
              <xref ref-type="aff" rid="A1">1</xref>
            </contrib>
          </contrib-group>
          <aff id="A1">
            <label>1</label>
            <addr-line></addr-line>
          </aff>
          <author-notes>
            <fn fn-type="corresp">
              <p>
                Corresponding author:  (<email xlink:type="simple"></email>)
              </p>
            </fn>
            <fn fn-type="edited-by">
              <p>Academic editor: </p>
            </fn>
          </author-notes>
          <pub-date pub-type="collection">
            <year></year>
          </pub-date>
          <pub-date pub-type="epub">
            <day></day>
            <month></month>
            <year></year>
          </pub-date>
          <issue></issue>
          <fpage></fpage>
          <lpage></lpage>
          <history>
            <date date-type="received">
              <day></day>
              <month></month>
              <year></year>
            </date>
            <date date-type="accepted">
              <day></day>
              <month></month>
              <year></year>
            </date>
          </history>
          <permissions>
            <copyright-statement></copyright-statement>
            <license license-type="creative-commons-attribution" xlink:href="http://creativecommons.org/licenses/by/4.0" xlink:type="simple">
              <license-p>This is an open access article distributed under the terms of the Creative Commons Attribution License (CC BY 4.0), which permits unrestricted use, distribution, and reproduction in any medium, provided the original author and source are credited.</license-p>
            </license>
          </permissions>
          <self-uri content-type="zoobank" xlink:type="simple"></self-uri>
          <abstract>
            <label>Abstract</label>
            <p></p>
          </abstract>
          <kwd-group>
            <label>Keywords</label>
            <kwd></kwd>
          </kwd-group>
        </article-meta>
        <notes>
          <sec sec-type="Citation">
            <title>Citation</title>
            <p></p>
          </sec>
        </notes>
      </front>
      <body>
        <xsl:apply-templates select="node()" />
      </body>
    </article>
  </xsl:template>

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="h1/b | h2/b | h3/b">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="h2 | h3">
    <title>
      <xsl:apply-templates />
    </title>
  </xsl:template>

  <xsl:template match="section">
    <sec>
      <xsl:attribute name="sec-type">
        <xsl:value-of select="normalize-space(h2)" />
        <xsl:value-of select="normalize-space(h3)" />
      </xsl:attribute>
      <xsl:apply-templates />
    </sec>
  </xsl:template>

  <xsl:template match="section[@class='ack']">
    <ack>
      <xsl:apply-templates />
    </ack>
  </xsl:template>

  <xsl:template match="section[@class='ref']">
    <ref-list>
      <xsl:apply-templates />
    </ref-list>
  </xsl:template>

  <xsl:template match="ref">
    <ref>
      <xsl:attribute name="id">
        <xsl:text>B</xsl:text>
        <xsl:number />
      </xsl:attribute>
      <mixed-citation xlink:type="simple">
        <xsl:apply-templates />
      </mixed-citation>
    </ref>
  </xsl:template>

  <xsl:template match="td/p | th/p">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="a[not(@href)]"></xsl:template>
</xsl:stylesheet>
