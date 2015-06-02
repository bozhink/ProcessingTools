<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  xmlns:msxsl="urn:schemas-microsoft-com:xslt"
  exclude-result-prefixes="xs msxsl tp xlink mml xsi">

    <xsl:output method="xml" indent="yes" encoding="utf-8" omit-xml-declaration="yes"/>

    <xsl:key name="distinct-taxa" match="taxon" use="normalize-space(.)"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="/">
        <taxa>
            <xsl:variable name="taxaList">
                <xsl:call-template name="generateTaxaList"/>
            </xsl:variable>

            <xsl:for-each select="msxsl:node-set($taxaList)//taxon[generate-id() = generate-id(key('distinct-taxa', normalize-space(.))[1])]">
                <xsl:sort/>
                <xsl:apply-templates select="."/>
            </xsl:for-each>
        </taxa>
    </xsl:template>

    <xsl:template name="generateTaxaList">
        <!--
                Flora part
            -->
        <xsl:for-each select="//taxon/taxontitle[normalize-space(.)!='']">
            <xsl:sort/>
            <taxon>
                <xsl:for-each select="tp:taxon-name-part">
                    <part>
                        <value>
                            <xsl:value-of select="normalize-space(.)"/>
                        </value>
                        <rank>
                            <value>
                                <xsl:value-of select="@taxon-name-part-type"/>
                            </value>
                        </rank>
                    </part>
                </xsl:for-each>
            </taxon>
        </xsl:for-each>
        <xsl:for-each select="//nom[normalize-space(.)!='']">
            <taxon>
                <xsl:for-each select="name[normalize-space(.)!=''][normalize-space(.)!='?']">
                    <part>
                        <value>
                            <xsl:value-of select="."/>
                        </value>
                        <rank>
                            <value>
                                <xsl:value-of select="@class"/>
                            </value>
                        </rank>
                    </part>
                </xsl:for-each>
            </taxon>
        </xsl:for-each>

        <!--
                TaxPub-like xml-s
            -->
        <xsl:for-each select="//tn[*]|//tp:taxon-name[*]">
            <taxon>
                <xsl:for-each select="tn-part|tp:taxon-name-part">
                    <part>
                        <value>
                            <xsl:value-of select="normalize-space(.)"/>
                        </value>
                        <xsl:if test="normalize-space(@full-name)!=''">
                            <full-name>
                                <value>
                                    <xsl:value-of select="@full-name"/>
                                </value>
                            </full-name>
                        </xsl:if>
                        <rank>
                            <value>
                                <xsl:value-of select="@type"/>
                                <xsl:value-of select="@taxon-name-part-type"/>
                            </value>
                        </rank>
                    </part>
                </xsl:for-each>
            </taxon>
        </xsl:for-each>
    </xsl:template>
</xsl:stylesheet>
