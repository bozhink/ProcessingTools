<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:aid="http://ns.adobe.com/AdobeInDesign/4.0/">
	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>

	<xsl:template match="/">
		<table-wrap position="float" orientation="portrait">
			<xsl:apply-templates/>
		</table-wrap>
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="@*[contains(name(),'aid')]"/>

	<xsl:template match="Root|Story">
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template match="Table|table">
		<xsl:variable name="rows" select="number(@aid:trows)"/>
		<xsl:variable name="cols" select="number(@aid:tcols)"/>
		<xsl:variable name="tds" select="Cell[not(@aid:theader)]"/>
		<xsl:variable name="ths" select="Cell[@aid:theader]"/>
		<xsl:variable name="thcells" select="number(count($ths))"/>
		<xsl:variable name="tdcells" select="number(count($tds))"/>
		<xsl:variable name="trcount" select="$tdcells div $cols"/>

		<table id="T1">
			<x><xsl:value-of select="$rows"/></x>
			<x><xsl:value-of select="$cols"/></x>
			<x><xsl:value-of select="$tdcells"/></x>
			<x><xsl:value-of select="$trcount"/></x>
			
			<xsl:for-each select="Cell[not(@aid:theader)][position() mod $cols = 0]">
				<xsl:variable name="pos" select="position()"/>
				<tr>
					<xsl:for-each select="../Cell[not(@aid:theader)][(position() &gt; $pos) and (position() &lt; $pos + $cols + 1)]">
						<td>
							<xsl:apply-templates/>
						</td>
					</xsl:for-each>
				</tr>
			</xsl:for-each>
		</table>
	</xsl:template>

	<xsl:template match="Cell">
		<td>
			<xsl:attribute name="rowspan">1</xsl:attribute>
			<xsl:attribute name="colspan">1</xsl:attribute>
			<xsl:apply-templates/>
		</td>
	</xsl:template>
</xsl:stylesheet>