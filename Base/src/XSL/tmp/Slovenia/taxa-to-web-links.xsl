<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs mml xsi tp xlink"
	default-validation="strip">
	<xsl:output method="xhtml" encoding="UTF-8" indent="yes"
		doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"
		doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN"
		omit-xml-declaration="yes"/>
	<xsl:template match="/">
		<html>
			<head>
				<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
				<title>Taxa to web links</title>
				<link rel="stylesheet" href="style.css" media="all" title="default"/>
			</head>
			<body>
				<xsl:for-each select="//taxon">
					<div class="taxon">
						<span class="title">
							<xsl:value-of select="."/>
						</span>
						<ul>
							<xsl:apply-templates mode="search" select="document('search-engines.xml')">
								<xsl:with-param name="taxon" select="normalize-space(.)"/>
							</xsl:apply-templates>
						</ul>
					</div>
				</xsl:for-each>
			</body>
		</html>
	</xsl:template>

	<xsl:template mode="search" match="*">
		<xsl:param name="taxon" select="''"/>
		<xsl:for-each select="//search">
			<li>
				<a target="_blank">
					<xsl:attribute name="href">
						<xsl:value-of select="@uri"/>
						<xsl:value-of select="encode-for-uri($taxon)"/>
					</xsl:attribute>
					<xsl:value-of select="@label"/>
				</a>
			</li>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>