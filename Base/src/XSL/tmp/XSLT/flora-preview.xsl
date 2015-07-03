<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:mods="http://www.loc.gov/mods/v3"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs mods tp xsi xlink mml">

	<xsl:output method="html" version="5.0" encoding="UTF-8" indent="yes" />

	<xsl:template match="/">
		<html>
			<head></head>
			<body>
				<h1>
					<xsl:value-of select="/publication/metaData/mods:mods/mods:titleInfo/mods:title"/>
				</h1>
				<h2>
					<xsl:value-of select="/publication/metaData/mods:mods/mods:titleInfo/mods:subTitle"/>
				</h2>
				<h6>
					<xsl:value-of select="/publication/metaData/mods:mods/mods:titleInfo/mods:partNumber"/>
				</h6>
				<h4>
					<em><xsl:value-of select="/publication/metaData/mods:mods/mods:name/mods:namePart"/></em>
				</h4>
				<h6>
					<xsl:value-of select="/publication/metaData/mods:mods/mods:originInfo/mods:publisher"/><br/>
					<xsl:value-of select="/publication/metaData/mods:mods/mods:originInfo/mods:copyrightDate"/>
				</h6>
				
				<section>
					<h5>Abstract</h5>
					<p>
						<xsl:apply-templates select="/publication/metaData/mods:mods/mods:abstract"/>
					</p>
				</section>
				<xsl:apply-templates/>
			</body>
		</html>
	</xsl:template>
	<!-- standard copy template -->
	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="textSection"></xsl:template>
	<xsl:template match="taxontitle"></xsl:template>

	<xsl:template match="treatment/taxon">
		<section>
			<h4>
				<xsl:apply-templates select="taxontitle/*"/>
			</h4>
			<xsl:for-each select="*[name()!='taxontitle' and name()!='key']">
				<p>
					<xsl:apply-templates/>
				</p>
			</xsl:for-each>
			<xsl:apply-templates select="key"/>
		</section>
	</xsl:template>

	<xsl:template match="tp:taxon-name-part">
		<xsl:text> </xsl:text>
		<span style=" background: rgb(0, 66, 0) transparent; background: rgba(0, 99, 0, 0.3); filter:progid:DXImageTransform.Microsoft.gradient(startColorstr=#99000000, endColorstr=#99000000); -ms-filter: 'progid:DXImageTransform.Microsoft.gradient(startColorstr=#99000000, endColorstr=#99000000)'">
			<xsl:apply-templates/>
		</span>
	</xsl:template>

	<xsl:template match="key">
		<table style=" background: rgb(150, 150, 150) transparent; background: rgba(150, 150, 150, 0.3); filter:progid:DXImageTransform.Microsoft.gradient(startColorstr=#99000000, endColorstr=#99000000); -ms-filter: 'progid:DXImageTransform.Microsoft.gradient(startColorstr=#99000000, endColorstr=#99000000)'">
			<xsl:apply-templates/>
		</table>
	</xsl:template>

	<xsl:template match="couplet">
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template match="question">
		<tr>
			<td colspan="1" rowspan="1" style="width:40px">
				<xsl:choose>
					<xsl:when test="@num='a'">
						<xsl:value-of select="../@num"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:text>–</xsl:text>
					</xsl:otherwise>
				</xsl:choose>
			</td>
			<td colspan="1" rowspan="1">
				<xsl:apply-templates select="text"/>
			</td>
			<td colspan="1" rowspan="1" style="width:25%">
				<xsl:apply-templates select="toTaxon"/>
				<xsl:text> </xsl:text>
				<xsl:apply-templates select="toCouplet"/>
			</td>
		</tr>
	</xsl:template>
</xsl:stylesheet>