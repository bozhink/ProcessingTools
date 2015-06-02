<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:tp="http://www.plazi.org/taxpub" exclude-result-prefixes="tp msxsl" xmlns:msxsl="urn:schemas-microsoft-com:xslt">

    <xsl:output encoding="utf-8" indent="yes" method="xml"/>

    <xsl:key name="taxon-name" match="taxon" use="@name"/>
    <xsl:key name="taxon-type" match="type" use="."/>

    <xsl:template match="/">
        <taxa>
            <xsl:for-each select="//taxon[generate-id() = generate-id(key('taxon-name', @name)[1])]">
                <taxon>
                    <xsl:attribute name="name">
                        <xsl:value-of select="@name"/>
                    </xsl:attribute>
                    <xsl:variable name="types">
                        <xsl:for-each select="key('taxon-name', @name)[@type!='']">
                            <type>
                                <xsl:value-of select="@type"/>
                            </type>
                        </xsl:for-each>
                    </xsl:variable>
                    <xsl:for-each select="msxsl:node-set($types)/type[generate-id() = generate-id(key('taxon-type',.)[1])]">
                        <type>
                            <xsl:value-of select="."/>
                        </type>
                    </xsl:for-each>
                </taxon>
            </xsl:for-each>
        </taxa>
    </xsl:template>

</xsl:stylesheet>
