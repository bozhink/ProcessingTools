<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
                exclude-result-prefixes="xs mml xlink tp xsi"
                xmlns:mml="http://www.w3.org/1998/Math/MathML"
                xmlns:xlink="http://www.w3.org/1999/xlink"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:tp="http://www.plazi.org/taxpub">

    <xsl:output method="xml" encoding="utf-8" indent="yes" />

    <xsl:template match="/">
        <taxa>
            <splitted>
                <xsl:for-each select="//tp:taxon-name[count(tp:taxon-name-part)!=0]">
                    <xsl:sort />
                    <taxon>
                        <xsl:attribute name="original-name">
                            <xsl:value-of select="normalize-space(.)"/>
                        </xsl:attribute>
                        <xsl:for-each select="tp:taxon-name-part">
                            <xsl:attribute name="{@taxon-name-part-type}">
                                <xsl:choose>
                                    <xsl:when test="@full-name!=''">
                                        <xsl:value-of select="@full-name"/>
                                    </xsl:when>
                                    <xsl:otherwise>
                                        <xsl:value-of select="translate(normalize-space(.), '(|)', '')"/>
                                    </xsl:otherwise>
                                </xsl:choose>
                            </xsl:attribute>
                        </xsl:for-each>

                        <xsl:apply-templates mode="get-taxon-parts"/>
                    </taxon>
                </xsl:for-each>
            </splitted>
            <unsplitted>
                <xsl:for-each select="//tp:taxon-name[count(tp:taxon-name-part)=0][name(..)!='tp:taxon-name' and name(..)!='tp:taxon-name-part']">
                    <xsl:sort/>
                    <taxon>
                        <xsl:attribute name="original-name">
                            <xsl:value-of select="normalize-space(.)"/>
                        </xsl:attribute>
                        <xsl:attribute name="parent">
                            <xsl:choose>
                                <xsl:when test="name(..)='italic'">
                                    <xsl:value-of select="name(..)"/>
                                </xsl:when>
                                <xsl:otherwise>
                                    <xsl:text>general</xsl:text>
                                </xsl:otherwise>
                            </xsl:choose>
                        </xsl:attribute>
                        <xsl:apply-templates mode="simple-copy"/>
                    </taxon>
                </xsl:for-each>
            </unsplitted>
        </taxa>
    </xsl:template>
    
    <xsl:template mode="simple-copy" match="@*|*">
        <xsl:copy>
            <xsl:apply-templates mode="simple-copy" select="@*"/>
            <xsl:apply-templates mode="simple-copy"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template mode="get-taxon-parts" match="@*|*">
        <xsl:copy>
            <xsl:apply-templates mode="get-taxon-parts" select="@*"/>
            <xsl:apply-templates mode="get-taxon-parts"/>
        </xsl:copy>
    </xsl:template>
    <xsl:template mode="get-taxon-parts" match="tp:taxon-name-part">
        <part type="{@taxon-name-part-type}">
            <xsl:choose>
                <xsl:when test="@full-name!=''">
                    <xsl:value-of select="@full-name"/>
                </xsl:when>
                <xsl:otherwise>
                    <!--xsl:value-of select="normalize-space(.)"/-->
                    <xsl:value-of select="translate(normalize-space(.), '(|)', '')"/>
                </xsl:otherwise>
            </xsl:choose>
        </part>
    </xsl:template>

</xsl:stylesheet>
