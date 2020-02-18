<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs">
  <xsl:output method="html" indent="yes" version="5.0"/>

  <xsl:template match="/">
    <html lang="en">
      <head>
        <meta charset="utf-8" />
        <title></title>
      </head>
      <body>
        <xsl:apply-templates />
      </body>
    </html>
  </xsl:template>

  <xsl:template match="p">
    <p>
      <xsl:apply-templates />
    </p>
  </xsl:template>

  <xsl:template match="italic|Italic|i|em">
    <i>
      <xsl:apply-templates />
    </i>
  </xsl:template>

  <xsl:template match="bold|Bold|b|strong">
    <b>
      <xsl:apply-templates />
    </b>
  </xsl:template>

  <xsl:template match="styled-content">
    <span>
      <xsl:apply-templates select="@style"/>
      <xsl:apply-templates />
    </span>
  </xsl:template>

  <xsl:template match="named-content">
    <span>
      <xsl:attribute name="class">
        <xsl:value-of select="content-type"/>
      </xsl:attribute>
      <xsl:apply-templates />
    </span>
  </xsl:template>

  <xsl:template match="ref">
    <div>
      <xsl:attribute name="id">
        <xsl:value-of select="@id"/>
      </xsl:attribute>
      <xsl:apply-templates/>
    </div>
  </xsl:template>

  <xsl:template match="sec|tp:treatment|tp:treatment-sec">
    <section>
      <xsl:apply-templates/>
    </section>
  </xsl:template>

  <xsl:template match="title">
    <h3>
      <xsl:apply-templates/>
    </h3>
  </xsl:template>

  <xsl:template match="label">
    <h4>
      <xsl:apply-templates/>
    </h4>
  </xsl:template>

  <xsl:template match="tp:taxon-name|tp:taxon-name-part|tn|tn-part">
    <em>
      <xsl:apply-templates/>
    </em>
  </xsl:template>

  <xsl:template match="xref">
    <span class="xref">
      <xsl:apply-templates/>
    </span>
  </xsl:template>

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>
</xsl:stylesheet>
