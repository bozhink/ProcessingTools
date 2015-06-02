<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:tp="http://www.plazi.org/taxpub" exclude-result-prefixes="tp msxsl" xmlns:msxsl="urn:schemas-microsoft-com:xslt">

    <xsl:output encoding="utf-8" indent="yes" method="xml"/>

    <xsl:key name="match-species" match="taxon" use="part[@type='species']/@name"/>
    <xsl:key name="match-species-full" match="taxon[not(contains(part[@type='genus']/@name,'.'))][not(contains(part[@type='species']/@name,'.'))]" use="part[@type='species']/@name"/>
    <xsl:key name="match-subspecies" match="taxon" use="part[@type='subspecies']/@name"/>
    <xsl:key name="full-name" match="full-name" use="."/>

    <xsl:template match="/">
        <taxa>
            <!-- Select taxa with shortened genus and non-shortened species name -->
            <xsl:for-each select="//taxon[contains(part[@type='genus']/@name,'.')][not(contains(normalize-space(part[@type='species']/@name),'.'))]">
                <taxon name="{@name}">

                    <xsl:variable name="replaces">
                        <xsl:for-each select="key('match-species-full',part[@type='species']/@name)">
                            <xsl:copy-of select="."/>
                        </xsl:for-each>
                    </xsl:variable>

                    <xsl:for-each select="part">

                        <xsl:variable name="clear_name">
                            <xsl:choose>
                                <xsl:when test="contains(@name,'.')">
                                    <xsl:value-of select="substring-before(@name,'.')"/>
                                </xsl:when>
                                <xsl:otherwise>
                                    <xsl:value-of select="@name"/>
                                </xsl:otherwise>
                            </xsl:choose>
                        </xsl:variable>

                        <xsl:variable name="name_length" select="string-length($clear_name)"/>

                        <xsl:variable name="type" select="normalize-space(@type)"/>

                        <xsl:variable name="full_names">
                            <xsl:for-each select="msxsl:node-set($replaces)/taxon/part[normalize-space(@type)=$type][substring(@name,1,$name_length)=$clear_name]">
                                <full-name>
                                    <xsl:value-of select="@name"/>
                                </full-name>
                            </xsl:for-each>
                        </xsl:variable>

                        <xsl:variable name="full_name">
                            <xsl:for-each select="msxsl:node-set($full_names)/full-name[generate-id() = generate-id(key('full-name',.)[1])]">
                                <full-name>
                                    <xsl:value-of select="."/>
                                </full-name>
                            </xsl:for-each>
                        </xsl:variable>

                        <part>
                            <xsl:attribute name="name">
                                <xsl:value-of select="@name"/>
                            </xsl:attribute>
                            <xsl:choose>
                                <xsl:when test="count(msxsl:node-set($full_name)/full-name)=1">
                                    <xsl:attribute name="full-name">
                                        <xsl:value-of select="msxsl:node-set($full_name)/full-name"/>
                                    </xsl:attribute>
                                </xsl:when>
                                <xsl:otherwise>
                                    <xsl:apply-templates select="msxsl:node-set($full_name)/full-name"/>
                                </xsl:otherwise>
                            </xsl:choose>

                            <xsl:apply-templates select="type"/>
                                    
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
