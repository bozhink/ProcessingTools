<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	exclude-result-prefixes="xs" xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub">
	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>
	<xsl:template match="/">
		<xsl:apply-templates />
	</xsl:template>
	<!-- standard copy template -->
	<xsl:template match="@*|node()[name()!='bold-italic' and name()!='bold' and name()!='italic']">
		<xsl:copy>
			<xsl:apply-templates select="@*" />
			<xsl:apply-templates />
		</xsl:copy>
	</xsl:template>

	<xsl:template match="bold-italic">
		<bold><italic><xsl:apply-templates select="node()"/></italic></bold>
	</xsl:template>

	<xsl:template match="bold">
		<bold>
			<xsl:apply-templates select="node()"/>
		</bold>
	</xsl:template>

	<xsl:template match="italic">
		<xsl:choose>
			<xsl:when test="count(node())=1">
				<xsl:choose>
					<xsl:when test="name(node())=''"><!-- This italic contains only text -->
						<xsl:choose>
							<xsl:when test="matches(normalize-space(node()),'[A-Z][a-z\-\.]+.*')">
								<italic><tp:taxon-name>
									<xsl:value-of select="normalize-space(node())"/>
								</tp:taxon-name></italic>
							</xsl:when>
							<xsl:otherwise>
								<italic><xsl:value-of select="normalize-space(node())"/></italic>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>
						<italic><xsl:apply-templates select="node()"/></italic>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<italic><xsl:apply-templates select="node()"/></italic>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>