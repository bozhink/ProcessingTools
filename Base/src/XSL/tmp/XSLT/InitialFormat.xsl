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

	<xsl:template match="@*|node()[name()!='bold-italic' and name()!='title' and name()!='label' and name()!='article-title' and name()!='sec' and name()!='tp:treatment-sec' and name()!='table-wrap' and name()!='table' and name()!='fake_tag' and name()!='article_figs_and_tables']">
		<xsl:copy>
			<xsl:apply-templates select="@*" />
			<xsl:apply-templates />
		</xsl:copy>
	</xsl:template>

	<xsl:template match="bold-italic">
		<bold>
			<italic>
				<xsl:apply-templates select="node()"/>
			</italic>
		</bold>
	</xsl:template>

	<xsl:template match="fake_tag">
		<source>
			<xsl:copy-of select="node()"/>
		</source>
	</xsl:template>

	<xsl:template match="title | label | article-title">
		<xsl:element name="{name()}">
			<xsl:choose>
				<xsl:when test="count(node())=1 and name(node())=''">
					<xsl:value-of select="normalize-space(.)"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="unbold">
						<xsl:with-param name="select" select="node()"/>
					</xsl:call-template>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:element>
	</xsl:template>

	<xsl:template name="unbold">
		<xsl:param name="select" select="."/>
		<xsl:for-each select="node()">
			<xsl:choose>
				<xsl:when test="name()='bold-italic'">
					<italic>
						<xsl:call-template name="unbold">
							<xsl:with-param name="select" select="."/>
						</xsl:call-template>
					</italic>
				</xsl:when>
				<xsl:when test="name()=''">
					<xsl:value-of select="."/>
				</xsl:when>
				<xsl:when test="name()='PageBreak'">
					<xsl:comment>PageBreak</xsl:comment>
				</xsl:when>
				<xsl:when test="name()='bold'">
					<xsl:call-template name="unbold">
						<xsl:with-param name="select" select="."/>
					</xsl:call-template>
				</xsl:when>
				<xsl:otherwise>
					<xsl:element name="{name()}">
						<xsl:call-template name="unbold">
							<xsl:with-param name="select" select="."/>
						</xsl:call-template>
					</xsl:element>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>
	</xsl:template>

	<xsl:template match="sec | tp:treatment-sec">
		<xsl:element name="{name()}">
			<xsl:attribute name="sec-type" select="normalize-space(title[1])"/>
			<xsl:apply-templates/>
		</xsl:element>
	</xsl:template>

	<xsl:template match="table-wrap">
		<table-wrap>
			<xsl:choose>
				<xsl:when test="@content-type='key'">
					<xsl:attribute name="content-type" select="'key'"/>
					<xsl:attribute name="position" select="anchor"/>
					<xsl:attribute name="orientation" select="'portrait'"/>
					<xsl:call-template name="unbold">
						<xsl:with-param name="select" select="."/>
					</xsl:call-template>
				</xsl:when>
				<xsl:otherwise>
					<xsl:attribute name="position" select="'float'"/>
					<xsl:attribute name="orientation" select="'portrait'"/>
					<xsl:copy-of select="node()"/>
				</xsl:otherwise>
			</xsl:choose>
		</table-wrap>
	</xsl:template>

	<xsl:template match="table">
		<table>
			<xsl:if test="@id!=''">
				<xsl:attribute name="id" select="@id"/>
			</xsl:if>
			<xsl:apply-templates select="node()"/>
		</table>
	</xsl:template>

	<xsl:template match="article_figs_and_tables">
		<floats-group>
			<xsl:apply-templates select="node()"/>
		</floats-group>
	</xsl:template>
</xsl:stylesheet>