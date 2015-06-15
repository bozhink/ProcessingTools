<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema" exclude-result-prefixes="xs"
  xmlns:mml="http://www.w3.org/1998/Math/MathML" xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:tp="http://www.plazi.org/taxpub">

    <xsl:output method="xml" encoding="UTF-8" omit-xml-declaration="yes" indent="no"/>
    <xsl:preserve-space elements="*"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template mode="unstyle" match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template mode="unstyle" match="@style"/>

    <xsl:template mode="links" match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template mode="links" match="@target | @style"/>

    <xsl:template match="em | i | italic">
        <i>
            <xsl:apply-templates/>
        </i>
    </xsl:template>

    <xsl:template match="strong | b | bold">
        <b>
            <xsl:apply-templates />
        </b>
    </xsl:template>

    <xsl:template match="bold-italic | Bold-Italic">
        <b>
            <i>
                <xsl:apply-templates />
            </i>
        </b>
    </xsl:template>

    <xsl:template match="u">
        <u>
            <xsl:apply-templates />
        </u>
    </xsl:template>

    <xsl:template match="sup">
        <sup>
            <xsl:apply-templates/>
        </sup>
    </xsl:template>

    <xsl:template match="sub">
        <sub>
            <xsl:apply-templates/>
        </sub>
    </xsl:template>

    <xsl:template match="span">
        <xsl:apply-templates />
    </xsl:template>

    <xsl:template match="@align | @cellspacing | @cellpadding"/>

    <xsl:template match="a">
        <a>
            <xsl:attribute name="target">_blank</xsl:attribute>
            <xsl:apply-templates mode="links" select="@*"/>
            <xsl:apply-templates />
        </a>
    </xsl:template>

    <xsl:template match="node()[@id='90']/value[normalize-space(.)!='']">
        <value>
            <tn>
                <tn-part type="kingdom">
                    <xsl:apply-templates />
                </tn-part>
            </tn>
        </value>
    </xsl:template>
    <xsl:template match="node()[@id='91']/value[normalize-space(.)!='']">
        <value>
            <tn>
                <tn-part type="phylum">
                    <xsl:apply-templates />
                </tn-part>
            </tn>
        </value>
    </xsl:template>
    <xsl:template match="node()[@id='92']/value[normalize-space(.)!='']">
        <value>
            <tn>
                <tn-part type="class">
                    <xsl:apply-templates />
                </tn-part>
            </tn>
        </value>
    </xsl:template>
    <xsl:template match="node()[@id='93']/value[normalize-space(.)!='']">
        <value>
            <tn>
                <tn-part type="order">
                    <xsl:apply-templates />
                </tn-part>
            </tn>
        </value>
    </xsl:template>
    <xsl:template match="node()[@id='94']/value[normalize-space(.)!='']">
        <value>
            <tn>
                <tn-part type="family">
                    <xsl:apply-templates />
                </tn-part>
            </tn>
        </value>
    </xsl:template>
    <xsl:template match="node()[@id='95']/value[normalize-space(.)!='']">
        <value>
            <tn>
                <tn-part type="genus">
                    <xsl:apply-templates />
                </tn-part>
            </tn>
        </value>
    </xsl:template>
    <xsl:template match="node()[@id='96']/value[normalize-space(.)!='']">
        <value>
            <tn>
                <tn-part type="subgenus">
                    <xsl:apply-templates />
                </tn-part>
            </tn>
        </value>
    </xsl:template>
</xsl:stylesheet>
