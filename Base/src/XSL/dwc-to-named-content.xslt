<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  xmlns:dwc="http://rs.tdwg.org/dwc/terms/"
  exclude-result-prefixes="dwc xs">

    <xsl:output method="xml" encoding="UTF-8" omit-xml-declaration="no" indent="no"/>
    <xsl:preserve-space elements="*"/>

    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*"/>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="dwc:*">
        <named-content>
            <xsl:attribute name="content-type">
                <xsl:value-of select="name()"/>
            </xsl:attribute>
            <xsl:apply-templates/>
        </named-content>
    </xsl:template>
</xsl:stylesheet>