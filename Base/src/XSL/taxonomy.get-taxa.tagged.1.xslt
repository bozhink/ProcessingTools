<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:tp="http://www.plazi.org/taxpub" exclude-result-prefixes="tp msxsl" xmlns:msxsl="urn:schemas-microsoft-com:xslt">

    <xsl:output encoding="utf-8" indent="yes" method="xml"/>

    <xsl:key name="taxon-name" match="taxon" use="@name"/>
    <xsl:key name="taxon-part" match="part" use="@name"/>
    <xsl:key name="taxon-type" match="type" use="."/>

    <xsl:template match="/">
        <taxa>
            <xsl:for-each select="//taxon[generate-id() = generate-id(key('taxon-name', @name)[1])]">
                <taxon>
                    <xsl:apply-templates select="@*"/>
                    <xsl:for-each select="part">
                        <xsl:variable name="part-name" select="@name"/>
                        <xsl:variable name="types">
                            <xsl:for-each select="key('taxon-part', $part-name)[@type!='']">
                                <type>
                                    <xsl:value-of select="@type"/>
                                </type>
                            </xsl:for-each>
                        </xsl:variable>
                        <xsl:variable name="type">
                            <xsl:for-each select="msxsl:node-set($types)/type[generate-id() = generate-id(key('taxon-type', .)[1])]">
                                <type>
                                    <xsl:value-of select="."/>
                                </type>
                            </xsl:for-each>
                        </xsl:variable>
                        <part>
                            <xsl:apply-templates select="@*"/>
                            <xsl:choose>
                                <xsl:when test="count(msxsl:node-set($type)/type)=1">
                                    <xsl:attribute name="type">
                                        <xsl:value-of select="msxsl:node-set($type)/type"/>
                                    </xsl:attribute>
                                </xsl:when>
                                <xsl:otherwise>
                                    <xsl:for-each select="msxsl:node-set($type)/type">
                                        <type>
                                            <xsl:value-of select="."/>
                                        </type>
                                    </xsl:for-each>
                                </xsl:otherwise>
                            </xsl:choose>
                        </part>
                    </xsl:for-each>
                </taxon>
            </xsl:for-each>
        </taxa>
    </xsl:template>

    <xsl:template match="@*|*">
        <xsl:copy>
            <xsl:apply-templates select="@*"/>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="@type"/>

</xsl:stylesheet>
