<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="text" omit-xml-declaration="yes" encoding="utf-8" standalone="yes" indent="yes" />

  <xsl:template match="/">
    <xsl:variable name="article-id" select="/article/front/article-meta/article-id[@pub-id-type='doi'][1]" />
    <xsl:variable name="article-title" select="/article/front/article-meta/title-group/article-title[1]" />

    <xsl:text>{</xsl:text>
    <xsl:text>"articleId": "</xsl:text>
    <xsl:value-of select="normalize-space($article-id)" />
    <xsl:text>",</xsl:text>
    <xsl:text>"articleTitle": "</xsl:text>
    <xsl:value-of select="string($article-title)" />
    <xsl:text>",</xsl:text>

    <xsl:text>"references":[</xsl:text>
    <xsl:for-each select=".//ref/mixed-citation | .//ref/element-citation | .//ref/nlm-citation">
      <xsl:text>{"id":"</xsl:text>
      <xsl:value-of select="../@id"/>
      <xsl:text>",</xsl:text>
      <xsl:text>"content":"</xsl:text>
      <xsl:variable name="content">
        <xsl:apply-templates mode="string" />
      </xsl:variable>
      <xsl:value-of select="normalize-space($content)" />
      <xsl:text>"}</xsl:text>
      <xsl:if test="position() != last()">
        <xsl:text>,</xsl:text>
      </xsl:if>
    </xsl:for-each>
    <xsl:text>]</xsl:text>
    <xsl:text>}</xsl:text>
  </xsl:template>

  <xsl:template match="@* | comment()" mode="string"></xsl:template>

  <xsl:template match="*" mode="string">
    <xsl:apply-templates  mode="string" />
  </xsl:template>

  <xsl:template match="text()" mode="string">
    <xsl:copy-of select="." />
  </xsl:template>

  <xsl:template match="volume | fpage | comment | series | conf-name | conf-date | conf-loc | source | article-title | puslisher-name | puslisher-loc" mode="string">
    <xsl:text> </xsl:text>
    <xsl:apply-templates mode="string" />
  </xsl:template>

  <xsl:template match="person-group/name" mode="string">
    <xsl:apply-templates mode="string" />
    <xsl:if test="position() != last()">
      <xsl:text>, </xsl:text>
    </xsl:if>
  </xsl:template>

  <xsl:template match="name/*" mode="string">
    <xsl:apply-templates  mode="string" />
    <xsl:if test="position() != last()">
      <xsl:text> </xsl:text>
    </xsl:if>
  </xsl:template>
</xsl:stylesheet>
