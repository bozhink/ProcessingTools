<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:output method="xml" indent="no"/>

    <xsl:template match="/">
        <items>
            <xsl:apply-templates select="//item"/>
        </items>
    </xsl:template>

    <xsl:template match="item">
        <envo>
            <xsl:for-each select="entities/entity">
                <xsl:variable name="currentNumber">
                    <xsl:number/>
                </xsl:variable>
                <xsl:attribute name="type{$currentNumber}">
                    <xsl:value-of select="type"/>
                </xsl:attribute>
                <xsl:attribute name="identifier{$currentNumber}">
                    <xsl:value-of select="identifier"/>
                </xsl:attribute>
            </xsl:for-each>
            <xsl:attribute name="count">
                <xsl:value-of select="count"/>
            </xsl:attribute>
            <xsl:attribute name="full-string">
                <xsl:value-of select="name"/>
            </xsl:attribute>
            <xsl:value-of select="name"/>
        </envo>
    </xsl:template>
</xsl:stylesheet>
