<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs">

  <xsl:output method="xml" encoding="UTF-8" indent="yes" omit-xml-declaration="no" />
  <!--<xsl:output method="xml" encoding="UTF-8" indent="yes" omit-xml-declaration="no"
	  doctype-system="tax-treatment-NS0.dtd"
	  doctype-public="-//TaxonX//DTD Taxonomic Treatment Publishing DTD v0 20100105//EN" />-->

  <xsl:preserve-space elements="*" />

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="front/article-meta">
    <article-meta>
      <xsl:apply-templates select="article-id" />
      <xsl:apply-templates select="article-categories" />
      <xsl:apply-templates select="title-group" />
      <xsl:apply-templates select="contrib-group | aff" />
      <xsl:apply-templates select="author-notes" />
      <xsl:apply-templates select="pub-date" />
      <xsl:apply-templates select="volume" />
      <xsl:apply-templates select="volume-id" />
      <xsl:apply-templates select="volume-series" />
      <xsl:apply-templates select="issue" />
      <xsl:apply-templates select="issue-id" />
      <xsl:apply-templates select="issue-title" />
      <xsl:apply-templates select="issue-sponsor" />
      <xsl:apply-templates select="issue-part" />
      <xsl:apply-templates select="isbn" />
      <xsl:apply-templates select="supplement" />
      <xsl:apply-templates select="fpage" />
      <xsl:apply-templates select="lpage" />
      <xsl:apply-templates select="page-range" />
      <xsl:apply-templates select="elocation-id" />
      <xsl:apply-templates select="email | ext-link | uri | product | supplementary-material" />
      <xsl:apply-templates select="history" />
      <xsl:apply-templates select="permissions" />
      <xsl:choose>
        <xsl:when test="count(self-uri[@content-type='zoobank']) != 0">
          <xsl:apply-templates select="self-uri" />
        </xsl:when>
        <xsl:otherwise>
          <self-uri content-type="zoobank" xlink:type="simple"></self-uri>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:apply-templates select="related-article" />
      <xsl:apply-templates select="abstract" />
      <xsl:apply-templates select="trans-abstract" />
      <xsl:apply-templates select="kwd-group" />
      <xsl:apply-templates select="funding-group" />
      <xsl:apply-templates select="conference" />
      <xsl:apply-templates select="counts" />
      <xsl:apply-templates select="custom-meta-group" />
    </article-meta>
  </xsl:template>

  <xsl:template match="front/article-meta/contrib-group/contrib[not(uri[@content-type='zoobank'])]">
    <contrib>
      <xsl:apply-templates select="@*" />
      <xsl:apply-templates />
      <xsl:if test="count(uri[@content-type='zoobank']) = 0">
        <uri content-type="zoobank" xlink:type="simple" />
      </xsl:if>
    </contrib>
  </xsl:template>

  <xsl:template match="table-wrap[not(@id)][table[@id]]">
    <table-wrap id="{table/@id}">
      <xsl:apply-templates select="@*" />
      <xsl:apply-templates />
    </table-wrap>
  </xsl:template>

  <xsl:template match="table">
    <table rules="all">
      <xsl:apply-templates select="@*[name()!='id']" />
      <xsl:apply-templates />
    </table>
  </xsl:template>

  <xsl:template match="graphic/@xlink:href | media/@xlink:href | inline-graphic/@xlink:href | inline-supplementary-material/@xlink:href | supplementary-material/@xlink:href">
    <xsl:attribute name="xlink:href">
      <xsl:call-template name="cut-file-path-to-relative">
        <xsl:with-param name="path" select="normalize-space(.)" />
      </xsl:call-template>
    </xsl:attribute>
  </xsl:template>

  <xsl:template name="cut-file-path-to-relative">
    <xsl:param name="path" />
    <xsl:choose>
      <xsl:when test="contains($path, '/')">
        <xsl:call-template name="cut-file-path-to-relative">
          <xsl:with-param name="path" select="substring-after($path,'/')" />
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$path" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="person-group | person-group1">
    <person-group>
      <xsl:apply-templates select="anonymous | collab | name | aff | etal | string-name" />
    </person-group>
  </xsl:template>

  <xsl:template match="locality-coordinates | named-content | abbrev">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="sec/@sec-type">
    <xsl:variable name="sec-type" select="translate(string(),'QWERTYUIOPASDFGHJKLZXCVBNM','qwertyuiopasdfghjklzxcvbnm')" />
    <xsl:attribute name="sec-type">
      <xsl:choose>
        <xsl:when test="$sec-type='methods' or $sec-type='method'">
          <xsl:text>methods</xsl:text>
        </xsl:when>
        <xsl:when test="$sec-type='materials and methods' or $sec-type='material and methods' or $sec-type='materials and method' or $sec-type='methods and materials' or $sec-type='method and materials' or $sec-type='methods and material'">
          <xsl:text>materials|methods</xsl:text>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="." />
        </xsl:otherwise>
      </xsl:choose>
    </xsl:attribute>
  </xsl:template>

  <xsl:template match="supplementary-material/dataType | supplementary-material/notes">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="institution">
    <xsl:element name="{name()}">
      <xsl:value-of select="string()" />
    </xsl:element>
  </xsl:template>

  <!--
		TAXONOMIC PART
	 -->

  <xsl:template match="front/article-meta/title-group/article-title/tp:taxon-name[@type='lower']|front/article-meta/title-group/article-title/tn[@type='lower']">
    <italic>
      <xsl:call-template name="parse-inline-taxon-name-content" />
    </italic>
  </xsl:template>

  <xsl:template match="front/article-meta/title-group/article-title/tp:taxon-name|front/article-meta/title-group/article-title/italic/tp:taxon-name|front/article-meta/title-group/article-title/tn|front/article-meta/title-group/article-title/i/tn">
    <xsl:call-template name="parse-inline-taxon-name-content" />
  </xsl:template>

  <xsl:template match="tp:treatment-meta"></xsl:template>

  <xsl:template match="tp:nomenclature">
    <tp:nomenclature>
      <xsl:apply-templates select="sec-meta" />
      <xsl:apply-templates select="label" />
      <xsl:apply-templates select="tp:taxon-name|tn" />
      <xsl:apply-templates select="tp:taxon-authority" />
      <xsl:apply-templates select="tp:taxon-status" />
      <xsl:apply-templates select="tp:taxon-identifier" />
      <xsl:apply-templates select="xref" />
      <xsl:apply-templates select="xref-group" />
      <xsl:apply-templates select="tp:nomenclature-citation-list" />
      <xsl:apply-templates select="tp:type-genus" />
      <xsl:apply-templates select="tp:type-species" />
      <xsl:apply-templates select="tp:taxon-type-location" />
    </tp:nomenclature>
  </xsl:template>

  <xsl:template match="supplementary-material/dataType | supplementary-material/notes">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="tp:nomenclature/tp:taxon-name | tp:nomenclature/tn">
    <tp:taxon-name>
      <xsl:apply-templates />
      <xsl:if test="count(object-id[@content-type='zoobank']) + count(../object-id[@content-type='zoobank']) = 0">
        <object-id content-type="gnub" xlink:type="simple"></object-id>
      </xsl:if>
      <xsl:apply-templates select="../object-id" />
    </tp:taxon-name>
  </xsl:template>

  <xsl:template match="tp:taxon-name[name(..)!='tp:nomenclature'][name(../..)!='title-group'][name(../../..)!='title-group']|tn[name(..)!='tp:nomenclature'][name(../..)!='title-group'][name(../../..)!='title-group']">
    <tp:taxon-name>
      <xsl:call-template name="parse-inline-taxon-name-content" />
    </tp:taxon-name>
  </xsl:template>

  <xsl:template name="parse-inline-taxon-name-content">
    <xsl:for-each select="node()">
      <xsl:variable name="fullName" select="normalize-space(@full-name)" />
      <xsl:variable name="value" select="normalize-space(.)" />
      <xsl:variable name="position" select="position()" />
      <xsl:variable name="nextValue">
        <xsl:if test="position()!=last()">
          <xsl:value-of select="normalize-space(../node()[position() = $position + 1])" />
        </xsl:if>
      </xsl:variable>
      <xsl:choose>
        <xsl:when test="$fullName!=''">
          <xsl:value-of select="$fullName" />
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="$value" />
        </xsl:otherwise>
      </xsl:choose>
      <xsl:if test="position()!=last() and ($fullName!='' or $value!='') and not(contains($value,'(')) and not(substring($nextValue,1,1) = ')')">
        <xsl:text> </xsl:text>
      </xsl:if>
    </xsl:for-each>
  </xsl:template>

  <xsl:template match="tp:taxon-name | tn">
    <tp:taxon-name>
      <xsl:apply-templates />
    </tp:taxon-name>
  </xsl:template>

  <xsl:template match="tp:taxon-name-part | tn-part">
    <xsl:variable name="leftBracket" select="contains(string(.),'(')" />
    <xsl:variable name="rightBracket" select="contains(string(.),')')" />
    <xsl:variable name="leftValue">
      <xsl:choose>
        <xsl:when test="$leftBracket">
          <xsl:value-of select="substring-after(normalize-space(.), '(')" />
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="normalize-space(.)" />
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>
    <xsl:variable name="rightValue">
      <xsl:choose>
        <xsl:when test="$rightBracket">
          <xsl:value-of select="substring-before($leftValue, ')')" />
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="$leftValue" />
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>
    <xsl:if test="$leftBracket">
      <xsl:text> (</xsl:text>
    </xsl:if>
    <tp:taxon-name-part>
      <xsl:attribute name="taxon-name-part-type">
        <xsl:value-of select="@taxon-name-part-type" />
        <xsl:value-of select="@type" />
      </xsl:attribute>
      <xsl:choose>
        <xsl:when test="@full-name">
          <xsl:value-of select="@full-name" />
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="$rightValue" />
        </xsl:otherwise>
      </xsl:choose>
    </tp:taxon-name-part>
    <xsl:if test="$rightBracket">
      <xsl:text>) </xsl:text>
    </xsl:if>
  </xsl:template>

  <xsl:template match="xref-group">
    <xsl:for-each select="node()">
      <xsl:variable name="position" select="position()" />
      <xsl:if test="name()='xref'">
        <xref>
          <xsl:apply-templates select="@*" />
          <xsl:if test="name(../node()[$position - 1])!='xref'">
            <xsl:value-of select="normalize-space(../node()[$position - 1])" />
            <xsl:text> </xsl:text>
          </xsl:if>
          <xsl:apply-templates />
        </xref>
      </xsl:if>
    </xsl:for-each>
  </xsl:template>

  <xsl:template match="tp:nomenclature-citation[text()][count(.//tp:taxon-name)+count(.//tn)!=0]">
    <tp:nomenclature-citation>
      <xsl:choose>
        <xsl:when test="name(node()[1])='italic' and node()[1]/tp:taxon-name">
          <xsl:apply-templates select="node()[1]/tp:taxon-name" />
        </xsl:when>
        <xsl:when test="name(node()[1])='i' and node()[1]/tn">
          <xsl:apply-templates select="node()[1]/tn" />
        </xsl:when>
        <xsl:when test="name(node()[1])='tp:taxon-name'">
          <xsl:apply-templates select="node()[1]" />
        </xsl:when>
        <xsl:when test="name(node()[1])='tn'">
          <xsl:apply-templates select="node()[1]" />
        </xsl:when>
        <xsl:when test="name(node()[1])='' and normalize-space(node()[1])='=' and name(node()[2])='italic' and node()[2]/tp:taxon-name">
          <xsl:apply-templates select="node()[2]/tp:taxon-name" />
        </xsl:when>
        <xsl:when test="name(node()[1])='' and normalize-space(node()[1])='=' and name(node()[2])='i' and node()[2]/tn">
          <xsl:apply-templates select="node()[2]/tn" />
        </xsl:when>
        <xsl:when test="name(node()[1])='' and normalize-space(node()[1])='=' and name(node()[2])='tp:taxon-name'">
          <xsl:apply-templates select="node()[2]" />
        </xsl:when>
        <xsl:when test="name(node()[1])='' and normalize-space(node()[1])='=' and name(node()[2])='tn'">
          <xsl:apply-templates select="node()[2]" />
        </xsl:when>
      </xsl:choose>
      <xsl:choose>
        <xsl:when test="count(node()) &gt; 1 and name(node()[1])!=''">
          <comment>
            <xsl:for-each select="node()[position() &gt; 1]">
              <xsl:apply-templates select="." />
            </xsl:for-each>
          </comment>
        </xsl:when>
        <xsl:when test="count(node()) &gt; 2 and name(node()[1])='' and normalize-space(node()[1])='='">
          <comment>
            <xsl:for-each select="node()[position() &gt; 2]">
              <xsl:apply-templates select="." />
            </xsl:for-each>
          </comment>
        </xsl:when>
        <xsl:otherwise>
          <comment>
            <xsl:apply-templates />
          </comment>
        </xsl:otherwise>
      </xsl:choose>
    </tp:nomenclature-citation>
  </xsl:template>

  <xsl:template match="tp:nomenclature-citation[text()][count(.//tp:taxon-name)+count(.//tn)=0]">
    <xsl:choose>
      <xsl:when test="normalize-space(.)!=''">
        <tp:nomenclature-citation>
          <xsl:apply-templates select="../../tp:taxon-name" />
          <xsl:apply-templates select="../../tn" />
          <comment>
            <xsl:apply-templates />
          </comment>
        </tp:nomenclature-citation>
      </xsl:when>
      <xsl:otherwise>
        <xsl:apply-templates />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>
