<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  xmlns:msxsl="urn:schemas-microsoft-com:xslt"
  exclude-result-prefixes="xs tp xlink mml xsi">

  <xsl:output method="xml" indent="yes" encoding="utf-8" omit-xml-declaration="yes"/>
  <xsl:key name="distinct-taxa" match="taxon" use="normalize-space(.)"/>
  <xsl:key name="distinct-taxa-value" match="taxon" use="normalize-space(part/value)"/>

  <xsl:template match="/">
    <taxa>
      <xsl:variable name="taxa">
        <xsl:for-each select="//tp:taxon-name-part|//tn-part|//nom[normalize-space(.)!='']/name[normalize-space(.)!=''][normalize-space(.)!='?']">
          <xsl:sort/>
          <taxon>
            <part>
              <value>
                <xsl:value-of select="normalize-space(.)"/>
              </value>
              <rank>
                <value>
                  <xsl:value-of select="@taxon-name-part-type"/>
                  <xsl:value-of select="@type"/>
                  <xsl:value-of select="@class"/>
                </value>
              </rank>
            </part>
          </taxon>
        </xsl:for-each>
      </xsl:variable>

      <xsl:variable name="distinct_taxa">
        <xsl:for-each select="msxsl:node-set($taxa)/taxon[generate-id() = generate-id(key('distinct-taxa', normalize-space(.))[1])]">
          <xsl:copy-of select="."/>
        </xsl:for-each>
      </xsl:variable>

      <xsl:for-each select="msxsl:node-set($distinct_taxa)/taxon[generate-id() = generate-id(key('distinct-taxa-value', normalize-space(part/value))[1])]">
        <taxon>
          <part>
            <value>
              <xsl:value-of select="part/value"/>
            </value>
            <rank>
              <xsl:for-each select="key('distinct-taxa-value', normalize-space(part/value))">
                <value>
                  <xsl:value-of select=".//rank/value"/>
                </value>
              </xsl:for-each>
            </rank>
          </part>
        </taxon>
      </xsl:for-each>
    </taxa>
  </xsl:template>

</xsl:stylesheet>
