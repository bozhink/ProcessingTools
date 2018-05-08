<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:tp="http://www.plazi.org/taxpub">

  <xsl:output method="xml" indent="no" encoding="utf-8" cdata-section-elements="tex-math" />

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="tp:taxon-name | tn">
    <tn>
      <xsl:apply-templates select="@*" />

      <xsl:attribute name="id">
        <xsl:text>TN</xsl:text>
        <xsl:value-of select="generate-id()" />
      </xsl:attribute>

      <xsl:apply-templates select="node()" />
    </tn>
  </xsl:template>

  <xsl:template match="tp:taxon-name-part | tn-part">
    <tn-part>
      <xsl:apply-templates select="@*" />

      <xsl:attribute name="id">
        <xsl:text>TNP</xsl:text>
        <xsl:value-of select="generate-id()" />
      </xsl:attribute>

      <xsl:variable name="is-abbreviated">
        <xsl:choose>
          <xsl:when test="contains(string(.), '.') or normalize-space(.) = ''">
            <xsl:value-of select="true()" />
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="false()" />
          </xsl:otherwise>
        </xsl:choose>
      </xsl:variable>

      <xsl:if test="normalize-space(@abbreviated)=''">
        <xsl:attribute name="abbreviated">
          <xsl:value-of select="$is-abbreviated" />
        </xsl:attribute>
      </xsl:if>

      <xsl:if test="normalize-space(@full-name)=''">
        <xsl:attribute name="full-name">
          <xsl:choose>
            <xsl:when test="$is-abbreviated = true()">
              <xsl:text></xsl:text>
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="normalize-space(.)" />
            </xsl:otherwise>
          </xsl:choose>
        </xsl:attribute>
      </xsl:if>

      <xsl:apply-templates select="node()" />
    </tn-part>
  </xsl:template>

  <xsl:template match="tp:taxon-name-part/@taxon-name-part-type">
    <xsl:attribute name="type">
      <xsl:value-of select="." />
    </xsl:attribute>
  </xsl:template>
</xsl:stylesheet>
