<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs">

	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>

	<xsl:template match="/">
		<xsl:apply-templates />
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:choose>
			<xsl:when test="name()='bold-italic'">
				<bold><italic><xsl:apply-templates select="node()"/></italic></bold>
			</xsl:when>
			<xsl:when test="name()='fake_tag'">
				<source><xsl:apply-templates select="node()"/></source>
			</xsl:when>
			<xsl:when test="name()='article_figs_and_tables'">
				<floats-group><xsl:apply-templates select="node()"/></floats-group>
			</xsl:when>
			<xsl:when test="name()='fig'">
				<fig>
					<xsl:attribute name="id"><xsl:value-of select="@id"/></xsl:attribute>
					<xsl:attribute name="position"><xsl:value-of select="@position"/></xsl:attribute>
					<xsl:attribute name="orientation"><xsl:value-of select="@orientation"/></xsl:attribute>
					<xsl:apply-templates select="node()"/>
				</fig>
			</xsl:when>
			<xsl:when test="name()='label' or name()='tite' or name()='article-title' or name()='caption' or name()='p'">
				<xsl:element name="{name()}"><xsl:apply-templates select="node()"/></xsl:element>
			</xsl:when>
			<xsl:when test="name()='bold' or name()='italic' or name()='underline' or name()='sc'">
				<xsl:element name="{name()}"><xsl:apply-templates select="node()"/></xsl:element>
			</xsl:when>
			<xsl:when test="name()='graphic'">
				<graphic>
					<xsl:attribute name="xlink:href"><xsl:value-of select="@xlink:href"/></xsl:attribute>
					<xsl:attribute name="position"><xsl:value-of select="@position"/></xsl:attribute>
					<xsl:attribute name="orientation"><xsl:value-of select="@orientation"/></xsl:attribute>
				</graphic>
			</xsl:when>
			<xsl:when test="name()='table-wrap'">
				<table-wrap>
					<xsl:if test="@content-type!=''">
						<xsl:attribute name="content-type"><xsl:value-of select="@content-type"/></xsl:attribute>
					</xsl:if>
					<xsl:attribute name="position"><xsl:value-of select="@position"/></xsl:attribute>
					<xsl:attribute name="orientation"><xsl:value-of select="@orientation"/></xsl:attribute>
					<xsl:apply-templates select="node()"/>
				</table-wrap>
			</xsl:when>
			<xsl:when test="name()='table'">
				<table>
					<xsl:if test="@id!=''">
						<xsl:attribute name="id"><xsl:value-of select="@id"/></xsl:attribute>
					</xsl:if>
					<xsl:apply-templates select="node()"/>
				</table>
			</xsl:when>
			<xsl:when test="name()='tbody' or name()='thead' or name()='tr'">
				<xsl:element name="{name()}"><xsl:apply-templates select="node()"/></xsl:element>
			</xsl:when>
			<xsl:when test="name()='td' or name()='th'">
				<xsl:element name="{name()}">
					<xsl:attribute name="rowspan"><xsl:value-of select="@rowspan"/></xsl:attribute>
					<xsl:attribute name="colspan"><xsl:value-of select="@colspan"/></xsl:attribute>
					<xsl:apply-templates select="node()"/>
				</xsl:element>
			</xsl:when>
			<xsl:otherwise>
				<xsl:copy>
					<xsl:apply-templates select="@*|node()" />
				</xsl:copy>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>