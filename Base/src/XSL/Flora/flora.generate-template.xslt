<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  xmlns:msxsl="urn:schemas-microsoft-com:xslt"
  exclude-result-prefixes="xs xlink mml xsi msxsl tp">

    <xsl:output method="xml" indent="yes" encoding="utf-8" omit-xml-declaration="yes"/>
    <xsl:param name="short-genus" select="true()"/>
    <xsl:param name="full-genus" select="true()"/>
    <xsl:key name="distinct-taxa" match="taxon" use="normalize-space(.)"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="/">
        <taxa>
            <xsl:variable name="templateList">
                <xsl:call-template name="generate-template-list"/>
            </xsl:variable>

            <xsl:for-each select="msxsl:node-set($templateList)//taxon[generate-id() = generate-id(key('distinct-taxa', normalize-space(.))[1])]">
                <xsl:sort select="@length" data-type="number" order="ascending"/>
                <xsl:copy-of select="."/>
            </xsl:for-each>
        </taxa>
    </xsl:template>

    <xsl:template name="generate-template-list">
        <xsl:for-each select="//taxon[normalize-space(.)!='']">
            <xsl:variable name="taxon" select="."/>
            <xsl:for-each select="part">
                <xsl:variable name="position" select="position()"/>
                <xsl:choose>
                    <xsl:when test="rank/value='infrank'"></xsl:when>
                    <xsl:when test="rank/value='infraspecific-rank'"></xsl:when>
                    <xsl:otherwise>
                        <!-- 
                                Taxa without genus and species
                             -->
                        <xsl:choose>
                            <xsl:when test="contains(string(rank/value),'aut')"></xsl:when>
                            <xsl:otherwise>
                                <xsl:variable name="find-string">
                                    <xsl:for-each select="msxsl:node-set($taxon)//part[position() &lt; $position]">
                                        <xsl:choose>
                                            <xsl:when test="rank/value='genus'"></xsl:when>
                                            <xsl:when test=" rank/value='species'"></xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="value"/>
                                                <xsl:text> </xsl:text>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                    <xsl:value-of select="value"/>
                                </xsl:variable>
                                <xsl:variable name="tagged">
                                    <xsl:for-each select="msxsl:node-set($taxon)//part[position() &lt; $position]">
                                        <xsl:choose>
                                            <xsl:when test="rank/value='genus' or rank/value='species'">
                                                <part>
                                                    <value></value>
                                                    <full-name>
                                                        <value>
                                                            <xsl:value-of select="value"/>
                                                        </value>
                                                    </full-name>
                                                    <xsl:copy-of select="rank"/>
                                                </part>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:copy-of select="."/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                    <xsl:copy-of select="."/>
                                </xsl:variable>
                                <xsl:call-template name="create-taxa-record">
                                    <xsl:with-param name="find-string" select="$find-string"/>
                                    <xsl:with-param name="tagged" select="$tagged"/>
                                </xsl:call-template>
                            </xsl:otherwise>
                        </xsl:choose>

                        <!-- 
                                Taxa without genus
                             -->
                        <xsl:choose>
                            <xsl:when test="contains(string(rank/value),'aut')"></xsl:when>
                            <xsl:otherwise>
                                <xsl:variable name="find-string">
                                    <xsl:for-each select="msxsl:node-set($taxon)//part[position() &lt; $position]">
                                        <xsl:choose>
                                            <xsl:when test="rank/value='genus'"></xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="value"/>
                                                <xsl:text> </xsl:text>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                    <xsl:value-of select="value"/>
                                </xsl:variable>
                                <xsl:variable name="tagged">
                                    <xsl:for-each select="msxsl:node-set($taxon)//part[position() &lt; $position]">
                                        <xsl:choose>
                                            <xsl:when test="rank/value='genus'">
                                                <part>
                                                    <value></value>
                                                    <full-name>
                                                        <value>
                                                            <xsl:value-of select="value"/>
                                                        </value>
                                                    </full-name>
                                                    <xsl:copy-of select="rank"/>
                                                </part>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:copy-of select="."/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                    <xsl:copy-of select="."/>
                                </xsl:variable>
                                <xsl:call-template name="create-taxa-record">
                                    <xsl:with-param name="find-string" select="$find-string"/>
                                    <xsl:with-param name="tagged" select="$tagged"/>
                                </xsl:call-template>
                            </xsl:otherwise>
                        </xsl:choose>

                        <!-- 
                                Shortened genus
                             -->
                        <xsl:if test="$short-genus">
                            <xsl:variable name="find-string">
                                <xsl:for-each select="msxsl:node-set($taxon)//part[position() &lt; $position]">
                                    <xsl:choose>
                                        <xsl:when test="rank/value='genus'">
                                            <xsl:value-of select="substring(string(value),1,1)"/>
                                            <xsl:text>.</xsl:text>
                                        </xsl:when>
                                        <xsl:otherwise>
                                            <xsl:value-of select="value"/>
                                        </xsl:otherwise>
                                    </xsl:choose>
                                    <xsl:text> </xsl:text>
                                </xsl:for-each>
                                <xsl:value-of select="value"/>
                            </xsl:variable>
                            <xsl:variable name="tagged">
                                <xsl:for-each select="msxsl:node-set($taxon)//part[position() &lt; $position]">
                                    <xsl:choose>
                                        <xsl:when test="rank/value='genus'">
                                            <part>
                                                <value>
                                                    <xsl:value-of select="substring(string(value),1,1)"/>
                                                    <xsl:text>.</xsl:text>
                                                </value>
                                                <full-name>
                                                    <value>
                                                        <xsl:value-of select="value"/>
                                                    </value>
                                                </full-name>
                                                <xsl:copy-of select="rank"/>
                                            </part>
                                        </xsl:when>
                                        <xsl:otherwise>
                                            <xsl:copy-of select="."/>
                                        </xsl:otherwise>
                                    </xsl:choose>
                                </xsl:for-each>
                                <xsl:copy-of select="."/>
                            </xsl:variable>
                            <xsl:call-template name="create-taxa-record">
                                <xsl:with-param name="find-string" select="$find-string"/>
                                <xsl:with-param name="tagged" select="$tagged"/>
                            </xsl:call-template>
                        </xsl:if>

                        <!-- 
                                Full name
                            -->
                        <xsl:if test="$full-genus">
                            <xsl:variable name="find-string">
                                <xsl:for-each select="msxsl:node-set($taxon)//part[position() &lt; $position]">
                                    <xsl:value-of select="value"/>
                                    <xsl:text> </xsl:text>
                                </xsl:for-each>
                                <xsl:value-of select="value"/>
                            </xsl:variable>
                            <xsl:variable name="tagged">
                                <xsl:for-each select="msxsl:node-set($taxon)//part[position() &lt; $position]">
                                    <xsl:copy-of select="."/>
                                </xsl:for-each>
                                <xsl:copy-of select="."/>
                            </xsl:variable>
                            <xsl:call-template name="create-taxa-record">
                                <xsl:with-param name="find-string" select="$find-string"/>
                                <xsl:with-param name="tagged" select="$tagged"/>
                            </xsl:call-template>
                        </xsl:if>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:for-each>
        </xsl:for-each>
    </xsl:template>

    <xsl:template name="create-taxa-record">
        <xsl:param name="find-string" select="''"/>
        <xsl:param name="tagged"/>
        <!--<xsl:variable name="find-string-length" select="string-length($find-string)"/>-->
        <xsl:if test="string-length($find-string) &gt; 4 or not(contains($find-string, '.'))">
            <taxon length="{string-length($find-string)}">
                <find>
                    <xsl:value-of select="$find-string"/>
                </find>
                <replace>
                    <tn>
                        <xsl:apply-templates mode="taxpub" select="msxsl:node-set($tagged)"/>
                    </tn>
                </replace>
            </taxon>
        </xsl:if>
    </xsl:template>

    <xsl:template match="@* | node()" mode="taxpub">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()" mode="taxpub"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="part" mode="taxpub">
        <tn-part>
            <xsl:attribute name="type">
                <xsl:value-of select="rank/value"/>
            </xsl:attribute>
            <xsl:if test="normalize-space(full-name)!=''">
                <xsl:attribute name="full-name">
                    <xsl:variable name="fullName" select="string(full-name/value)"/>
                    <xsl:if test="not(contains($fullName, '.'))">
                        <xsl:value-of select="$fullName"/>
                    </xsl:if>
                </xsl:attribute>
            </xsl:if>
            <xsl:value-of select="value"/>
        </tn-part>
    </xsl:template>

</xsl:stylesheet>
