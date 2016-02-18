<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs">
  <xsl:output method="xml" indent="yes" encoding="utf-8" omit-xml-declaration="no" />

  <xsl:template match="/">
    <external-links>
      <xsl:for-each select="//ext-link">
        <xsl:variable name="base-address">
          <xsl:call-template name="get-base-address">
            <xsl:with-param name="uri" select="@xlink:href" />
            <xsl:with-param name="type" select="@ext-link-type" />
          </xsl:call-template>
        </xsl:variable>

        <xsl:variable name="uri">
          <xsl:call-template name="get-uri">
            <xsl:with-param name="uri" select="@xlink:href" />
            <xsl:with-param name="type" select="@ext-link-type" />
            <xsl:with-param name="base-address" select="$base-address" />
          </xsl:call-template>
        </xsl:variable>

        <external-link base-address="{$base-address}" uri="{$uri}">
          <xsl:value-of select="string(.)" />
        </external-link>
      </xsl:for-each>
    </external-links>
  </xsl:template>

  <xsl:template name="get-base-address">
    <xsl:param name="uri" />
    <xsl:param name="type" />
    <xsl:choose>
      <xsl:when test="$type = 'doi'">
        <xsl:text>http://dx.doi.org</xsl:text>
      </xsl:when>
      <xsl:otherwise>
        <xsl:variable name="protocol" select="normalize-space(substring-before($uri, '://'))" />
        <xsl:variable name="address" select="normalize-space(substring-before(substring-after($uri, '://'), '/'))" />
        <xsl:choose>
          <xsl:when test="$protocol = ''">
            <!-- There is no valid base address. Do nothing. -->
          </xsl:when>
          <xsl:when test="$address = ''">
            <xsl:value-of select="$uri" />
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="$protocol" />
            <xsl:text>://</xsl:text>
            <xsl:value-of select="$address" />
          </xsl:otherwise>
        </xsl:choose>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="get-uri">
    <xsl:param name="uri" />
    <xsl:param name="type" />
    <xsl:param name="base-address" />
    <xsl:choose>
      <xsl:when test="$type = 'uri'">
        <xsl:variable name="result" select="normalize-space(substring-after($uri, $base-address))" />
        <xsl:choose>
          <xsl:when test="$result = ''">
            <xsl:text>/</xsl:text>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="$result" />
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$uri" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>
