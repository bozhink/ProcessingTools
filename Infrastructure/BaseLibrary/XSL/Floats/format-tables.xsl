<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs">

  <xsl:template match="tgroup">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="tgroup/colspec"></xsl:template>

  <xsl:template match="tgroup/thead/row">
    <tr>
      <xsl:apply-templates />
    </tr>
  </xsl:template>

  <xsl:template match="tgroup/tbody/row">
    <tr>
      <xsl:apply-templates />
    </tr>
  </xsl:template>

  <xsl:template match="tgroup/thead/row/entry">
    <th>
      <xsl:apply-templates select="@* | node()" />
    </th>
  </xsl:template>

  <xsl:template match="tgroup/tbody/row/entry">
    <td>
      <xsl:apply-templates select="@* | node()" />
    </td>
  </xsl:template>

  <xsl:template match="tgroup//row/entry/@colsep"></xsl:template>
  
  <xsl:template match="tgroup//row/entry/@align"></xsl:template>

  <xsl:template match="tgroup//row/entry/@valign"></xsl:template>
</xsl:stylesheet>
