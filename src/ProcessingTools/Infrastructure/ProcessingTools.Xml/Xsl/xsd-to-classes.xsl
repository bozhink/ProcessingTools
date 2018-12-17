<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xs="http://www.w3.org/2001/XMLSchema">

  <xsl:output method="text" indent="no" encoding="utf-8" omit-xml-declaration="yes" />
  <xsl:preserve-space elements="*" />

  <xsl:template match="xs:schema">
    <xsl:text>namespace Models
{
    using Newtonsoft.Json;

    public class SerializationModels
    {
</xsl:text>
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
    <xsl:text>
    }
}
</xsl:text>
  </xsl:template>

  <xsl:template match="xs:complexType">
    <xsl:text>public class </xsl:text>
    <xsl:value-of select="@name"/>
    <xsl:text>
{
</xsl:text>
    <xsl:apply-templates/>
    <xsl:text>}
</xsl:text>
  </xsl:template>

  <xsl:template match="xs:sequence">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="xs:schema/xs:element"></xsl:template>

  <xsl:template match="xs:element">
    <xsl:text>
</xsl:text>
    <xsl:if test="@minOccurs='1'">
      <xsl:text>[JsonRequired]</xsl:text>
    </xsl:if>
    <xsl:text>
</xsl:text>
    <xsl:text>[JsonProperty("</xsl:text>
    <xsl:value-of select="@name"/>
    <xsl:text>")]</xsl:text>
    <xsl:text>
</xsl:text>
    <xsl:text>public </xsl:text>
    <xsl:choose>
      <xsl:when test="string-length(substring-after(@type, ':')) &gt; 0">
        <xsl:value-of select="substring-after(@type, ':')"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="@type"/>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:text>[] </xsl:text>
    <xsl:value-of select="@name"/>
    <xsl:text> { get; set; }</xsl:text>
  </xsl:template>

  <xsl:template match="xs:element[@maxOccurs='1']">
    <xsl:text>
</xsl:text>
    <xsl:if test="@minOccurs='1'">
      <xsl:text>[JsonRequired]</xsl:text>
    </xsl:if>
    <xsl:text>
</xsl:text>
    <xsl:text>[JsonProperty("</xsl:text>
    <xsl:value-of select="@name"/>
    <xsl:text>")]</xsl:text>
    <xsl:text>
</xsl:text>
    <xsl:text>public </xsl:text>
    <xsl:choose>
      <xsl:when test="string-length(substring-after(@type, ':')) &gt; 0">
        <xsl:value-of select="substring-after(@type, ':')"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="@type"/>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:text> </xsl:text>
    <xsl:value-of select="@name"/>
    <xsl:text> { get; set; }</xsl:text>
  </xsl:template>

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

</xsl:stylesheet>