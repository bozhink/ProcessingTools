<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
                xmlns:tp="http://www.plazi.org/taxpub"
                exclude-result-prefixes="xs">

    <xsl:output encoding="UTF-8" indent="no" method="xml"/>
    <xsl:preserve-space elements="*" />

    <xsl:template match="/">
        <xsl:apply-templates select="*"/>
    </xsl:template>

    <xsl:template match="p[.//tp:taxon-name]">
        <xsl:variable name="genus">
            <xsl:choose>
                <xsl:when test="count(.//tp:taxon-name-part[@taxon-name-part-type='genus'])!=0">
                    <xsl:value-of select=".//tp:taxon-name-part[@taxon-name-part-type='genus'][1]"/>
                </xsl:when>
            </xsl:choose>
        </xsl:variable>
        <p>
            <xsl:apply-templates select="@*" mode="nom" />
            <xsl:apply-templates mode="nom">
                <xsl:with-param name="genus" select="$genus"/>
            </xsl:apply-templates>
        </p>
    </xsl:template>

    <xsl:template match="tp:taxon-name[@type='lower'][count(tp:taxon-name-part[@taxon-name-part-type='genus']) = 0]" mode="nom">
        <xsl:param name="genus" select="''"/>
        <tp:taxon-name type="lower">
            <tp:taxon-name-part taxon-name-part-type="genus">
                <xsl:attribute name="full-name">
                    <xsl:value-of select="$genus"/>
                </xsl:attribute>
            </tp:taxon-name-part>
            <xsl:apply-templates mode="nom">
                <xsl:with-param name="genus" select="$genus"/>
            </xsl:apply-templates>
        </tp:taxon-name>
    </xsl:template>

    <xsl:template match="@*|node()" mode="nom">
        <xsl:param name="genus" select="''"/>
        <xsl:copy>
            <xsl:apply-templates select="@*" mode="nom"/>
            <xsl:apply-templates mode="nom">
                <xsl:with-param name="genus" select="$genus"/>
            </xsl:apply-templates>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*" />
            <xsl:apply-templates />
        </xsl:copy>
    </xsl:template>

</xsl:stylesheet>
