<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes"/>
  <xsl:template match="/">
    <references>
      <article>
        <xsl:value-of select="/article/front/article-meta/article-id[@pub-id-type='doi']"/>
      </article>
      <reference-list title="{normalize-space(title)}">
        <xsl:for-each select="//ref-list[ref]">
          <xsl:for-each select="ref[normalize-space(.//person-group[1])!='' or normalize-space(.//institution)!='' or normalize-space(.//named-content[@content-type='project'])!='']">
            <xsl:variable name="id" select="@id"/>

            <xsl:variable name="authors">
              <xsl:variable name="cauthors" select="count(.//person-group[1]/name)+count(.//person-group[1]/anonymous)+3*count(.//person-group[1]/etal)"/>
              <xsl:if test="normalize-space(.//person-group[1])!=''">
                <xsl:choose>
                  <xsl:when test="$cauthors=1">
                    <xsl:apply-templates select=".//person-group[1]/name/surname"/>
                    <xsl:apply-templates select=".//person-group[1]/anonymous"/>
                  </xsl:when>
                  <xsl:when test="$cauthors=2">
                    <xsl:apply-templates select=".//person-group[1]/name[1]/surname"/>
                    <xsl:text> and </xsl:text>
                    <xsl:apply-templates select=".//person-group[1]/name[2]/surname"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:apply-templates select=".//person-group[1]/name[1]/surname"/>
                    <xsl:text> et al.</xsl:text>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:if>
            </xsl:variable>

            <xsl:for-each select="//ref[@id=$id]//year">
              <xsl:variable name="year" select="normalize-space(.)"/>

              <xsl:if test="normalize-space($authors)!=''">
                <xsl:call-template name="ref">
                  <xsl:with-param name="id" select="$id"/>
                  <xsl:with-param name="year" select="$year"/>
                  <xsl:with-param name="authors" select="$authors"/>
                </xsl:call-template>
              </xsl:if>

              <xsl:for-each select="//ref[@id=$id]//institution">
                <xsl:variable name="inst" select="normalize-space(.)"/>

                <xsl:call-template name="ref">
                  <xsl:with-param name="id" select="$id"/>
                  <xsl:with-param name="year" select="$year"/>
                  <xsl:with-param name="authors" select="$inst"/>
                </xsl:call-template>

              </xsl:for-each>

            </xsl:for-each>
          </xsl:for-each>
        </xsl:for-each>
      </reference-list>
    </references>
  </xsl:template>

  <xsl:template name="ref">
    <xsl:param name="id"/>
    <xsl:param name="year"/>
    <xsl:param name="authors"/>
    <reference id="{$id}" year="{$year}" authors="{$authors}">
      <xsl:value-of select="$authors"/>
      <xsl:value-of select="$year"/>
      <xsl:value-of select="$id"/>
    </reference>
  </xsl:template>

  <xsl:template match="surname | anonymous">
    <xsl:if test="contains(string(.), ',')">
      <xsl:message terminate="no">
        <xsl:value-of select="."/>
      </xsl:message>
    </xsl:if>
    <xsl:value-of select="."/>
  </xsl:template>
</xsl:stylesheet>
