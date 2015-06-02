<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="xml" indent="yes"/>
    <xsl:template match="/">
        <references>
            <article>
                <xsl:value-of select="/article/front/article-meta/article-id[@pub-id-type='doi']"/>
            </article>
            <xsl:for-each select="//ref-list[ref]">
                <reference-list title="{normalize-space(title)}">
                    <xsl:for-each select="ref">
                        <xsl:variable name="authors">
                            <xsl:for-each select=".//person-group[1]/name">
                                <xsl:value-of select="surname"/>
                                <xsl:if test="normalize-space(given-names)!=''">
                                    <xsl:text>_</xsl:text>
                                    <xsl:value-of select="given-names"/>
                                </xsl:if>
                                <xsl:if test="position()!=last()">
                                    <xsl:text>, </xsl:text>
                                </xsl:if>
                            </xsl:for-each>
                            <xsl:for-each select=".//person-group[1]/anonymous">
                                <xsl:value-of select="."/>
                                <xsl:if test="position()!=last()">
                                    <xsl:text>, </xsl:text>
                                </xsl:if>
                            </xsl:for-each>
                            <xsl:for-each select=".//named-content[@content-type='project']">
                                <xsl:text> «</xsl:text>
                                <xsl:value-of select="normalize-space(.)"/>
                                <xsl:text>» </xsl:text>
                            </xsl:for-each>
                            <xsl:if test="name(./*/*[1])='institution'">
                                <xsl:value-of select="normalize-space(./*/*[1])"/>
                            </xsl:if>
                        </xsl:variable>
                        <xsl:variable name="year">
                            <xsl:value-of select="normalize-space(.//year[1])"/>
                        </xsl:variable>
                        <reference id="{@id}" year="{$year}" authors="{$authors}"/>
                    </xsl:for-each>
                </reference-list>
            </xsl:for-each>
        </references>
    </xsl:template>
</xsl:stylesheet>
