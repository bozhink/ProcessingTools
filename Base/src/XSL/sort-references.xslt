<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="xml" indent="yes"/>
    <xsl:template match="/">
        <references>
            <article>
                <xsl:value-of select="/references/article"/>
            </article>
            <xsl:for-each select="//reference-list">
                <reference-list title="{@title}">
                    <xsl:for-each select="reference">
                        <xsl:sort/>
                        <reference id="{@id}" year="{@year}" authors="{@authors}"/>
                    </xsl:for-each>
                </reference-list>
            </xsl:for-each>
        </references>
    </xsl:template>
</xsl:stylesheet>
