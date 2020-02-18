<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs mml tp xlink xsi">

  <xsl:output method="xml" indent="yes" />

  <xsl:param name="doi"
             select="/article/front/article-meta/article-id[@pub-id-type='doi']" />

  <xsl:param name="article-id"
             select="substring-after(substring-after(substring-after($doi, '/'), '.'), '.')" />

  <xsl:param name="journal-name"
             select="/article/front/journal-meta/journal-title-group/journal-title" />

  <xsl:param name="publication-year"
             select="/article/front/article-meta/pub-date/year" />

  <xsl:param name="article-title"
             select="normalize-space(/article/front/article-meta/title-group/article-title)" />

  <xsl:param name="issue"
             select="/article/front/article-meta/issue" />

  <xsl:param name="start-page"
             select="/article/front/article-meta/fpage" />

  <xsl:param name="end-page"
             select="/article/front/article-meta/lpage" />

  <xsl:param name="publication-date">
    <xsl:value-of select="/article/front/article-meta/pub-date[@pub-type='epub']/day" />
    <xsl:text>-</xsl:text>
    <xsl:value-of select="/article/front/article-meta/pub-date[@pub-type='epub']/month" />
    <xsl:text>-</xsl:text>
    <xsl:value-of select="/article/front/article-meta/pub-date[@pub-type='epub']/year" />
  </xsl:param>

  <xsl:template match="/">
    <ipni-query>
      <taxon-acts>
        <xsl:for-each select="//tp:taxon-treatment[tp:nomenclature/tp:taxon-name/object-id[@content-type='ipni']]">
          <taxon-act>
            <xsl:attribute name="ID">
              <!--<xsl:number count="tp:taxon-treatment" />-->
              <!--<xsl:value-of select="generate-id()" />-->
              <xsl:value-of select="position()" />
            </xsl:attribute>

            <xsl:attribute name="article_id">
              <xsl:value-of select="$article-id" />
            </xsl:attribute>

            <!-- TODO -->
            <xsl:attribute name="type">
              <xsl:text>comb_nov</xsl:text>
            </xsl:attribute>

            <!-- TODO -->
            <taxon-rank>specific</taxon-rank>
            <xsl:call-template name="taxon-parent" />

            <xsl:variable name="genus"
                          select="tp:nomenclature/tp:taxon-name/tp:taxon-name-part[@taxon-name-part-type='genus']" />

            <xsl:variable name="species"
                          select="tp:nomenclature/tp:taxon-name/tp:taxon-name-part[@taxon-name-part-type='species']" />

            <xsl:variable name="basionym-author"
                          select="tp:nomenclature/tp:taxon-name/tp:taxon-name-part[@taxon-name-part-type='basionym-authority']" />

            <xsl:variable name="author"
                          select="tp:nomenclature/tp:taxon-name/tp:taxon-name-part[@taxon-name-part-type='authority']" />

            <!--<FULL-TAXON-NAME>
              <xsl:value-of select="normalize-space(tp:nomenclature/tp:taxon-name)" />
            </FULL-TAXON-NAME>-->

            <taxon-name>
              <taxon-name-part part="genus">
                <xsl:value-of select="$genus" />
              </taxon-name-part>
              <taxon-name-part part="species">
                <xsl:value-of select="$species" />
              </taxon-name-part>
            </taxon-name>
            <basionym-author>
              <xsl:value-of select="$basionym-author" />
            </basionym-author>
            <publishing-author>
              <xsl:value-of select="$author" />
            </publishing-author>

            <hybrid type="no" />

            <basionym rank="specific">
              <xsl:message terminate="no">TODO</xsl:message>

              <xsl:variable name="basionym-genus"
                            select="tp:nomenclature/tp:nomenclature-citation-list/tp:nomenclature-citation//tp:taxon-name[normalize-space(tp:taxon-name-part[@taxon-name-part-type='authority']) = $basionym-author]/tp:taxon-name-part[@taxon-name-part-type='genus']" />

              <xsl:variable name="basionym-species"
                            select="tp:nomenclature/tp:nomenclature-citation-list/tp:nomenclature-citation//tp:taxon-name[normalize-space(tp:taxon-name-part[@taxon-name-part-type='authority']) = $basionym-author]/tp:taxon-name-part[@taxon-name-part-type='species']" />

              <xsl:variable name="basionym-basionym-author"
                            select="tp:nomenclature/tp:nomenclature-citation-list/tp:nomenclature-citation//tp:taxon-name[normalize-space(tp:taxon-name-part[@taxon-name-part-type='authority']) = $basionym-author]/tp:taxon-name-part[@taxon-name-part-type='authority']" />

              <taxon-name>
                <xsl:message terminate="no">
                </xsl:message>
                <taxon-name-part part="genus">
                  <xsl:value-of select="$basionym-genus" />
                </taxon-name-part>
                <taxon-name-part part="species">
                  <xsl:value-of select="$basionym-species" />
                </taxon-name-part>
              </taxon-name>
              <basionym-author>
                <xsl:value-of select="$basionym-basionym-author" />
              </basionym-author>
              <publication>
                <journal-name>
                  <xsl:value-of select="tp:nomenclature/tp:nomenclature-citation-list/tp:nomenclature-citation[.//tp:taxon-name[normalize-space(tp:taxon-name-part[@taxon-name-part-type='authority']) = $basionym-author]]/mixed-citation" />
                </journal-name>
                <year>
                  <xsl:value-of select="tp:nomenclature/tp:nomenclature-citation-list/tp:nomenclature-citation[.//tp:taxon-name[normalize-space(tp:taxon-name-part[@taxon-name-part-type='authority']) = $basionym-author]]/mixed-citation/year" />
                </year>
                <volume></volume>
                <page></page>
              </publication>
            </basionym>

            <xsl:call-template name="published-in">
              <xsl:with-param name="taxon-page">XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX</xsl:with-param>
            </xsl:call-template>
          </taxon-act>
        </xsl:for-each>
      </taxon-acts>
    </ipni-query>
  </xsl:template>

  <xsl:template name="taxon-parent">
    <taxon-parent>
      <kingdom>
        <xsl:value-of select="tp:treatment-meta/kwd-group/kwd/named-content[@content-type='kingdom']" />
      </kingdom>
      <order>
        <xsl:value-of select="tp:treatment-meta/kwd-group/kwd/named-content[@content-type='order']" />
      </order>
      <family>
        <xsl:value-of select="tp:treatment-meta/kwd-group/kwd/named-content[@content-type='family']" />
      </family>
    </taxon-parent>
  </xsl:template>

  <xsl:template name="published-in">
    <xsl:param name="taxon-page" />
    <published-in>
      <journal-name>
        <xsl:value-of select="$journal-name" />
      </journal-name>
      <year>
        <xsl:value-of select="$publication-year" />
      </year>
      <title>
        <xsl:value-of select="$article-title" />
      </title>
      <issue>
        <xsl:value-of select="$issue" />
      </issue>
      <taxon-page>
        <xsl:value-of select="$taxon-page" />
      </taxon-page>
      <start-page>
        <xsl:value-of select="$start-page" />
      </start-page>
      <end-page>
        <xsl:value-of select="$end-page" />
      </end-page>
      <publication-date>
        <xsl:value-of select="$publication-date" />
      </publication-date>
      <doi>
        <xsl:value-of select="$doi" />
      </doi>
    </published-in>
  </xsl:template>
</xsl:stylesheet>