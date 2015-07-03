<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output indent="no" method="xml" encoding="utf-8" omit-xml-declaration="yes"/>
	<xsl:strip-space elements="taxa taxon part rank full-name"/>

	<xsl:key use="normalize-space(.)" name="taxa" match="taxon"/>

	<xsl:template match="/">
		<taxa>
			<xsl:for-each select="/taxa/taxon[generate-id() = generate-id(key('taxa', normalize-space(.))[1])]">
				<xsl:sort/>
				<taxon>
					<xsl:apply-templates/>
				</taxon>
			</xsl:for-each>
		</taxa>
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="text()[string()='familia' or string()='Familia' or string()='Family']">
		<xsl:text>family</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='subfamilia' or string()='fubfamilia' or string()='Subfamilia' or string()='Subfamily' or string()='fubfamily']">
		<xsl:text>subfamily</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='infrafamilia' or string()='Infrafamilia' or string()='Infrafamily']">
		<xsl:text>infrafamily</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='superfamilia' or string()='Superfamilia' or string()='Superfamily']">
		<xsl:text>superfamily</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='Clade' or string()='clade']">
		<xsl:text>clade</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='ordo' or string()='Ordo' or string()='Order']">
		<xsl:text>order</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='subordo' or string()='Subordo' or string()='Suborder']">
		<xsl:text>suborder</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='infraordo' or string()='Infraordo' or string()='Infraorder']">
		<xsl:text>infraorder</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='superordo' or string()='Superordo' or string()='Superorder']">
		<xsl:text>superorder</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='classis' or string()='Classis' or string()='Class']">
		<xsl:text>class</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='subclassis' or string()='Subclassis' or string()='Subclass']">
		<xsl:text>subclass</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='infraclassis' or string()='Infraclassis' or string()='Infraclass']">
		<xsl:text>infraclass</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='superclassis' or string()='Superclassis' or string()='Superclass']">
		<xsl:text>superclass</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='divisio' or string()='Divisio' or string()='Division']">
		<xsl:text>division</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='subdivisio' or string()='Subdivisio' or string()='Subdivision']">
		<xsl:text>subdivision</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='infradivisio' or string()='Infradivisio' or string()='Infradivision']">
		<xsl:text>infradivision</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='regnum' or string()='Regnum' or string()='Kingdom']">
		<xsl:text>kingdom</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='subregnum' or string()='Subregnum' or string()='Subkingdom']">
		<xsl:text>subkingdom</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='infraregnum' or string()='Infraregnum' or string()='Infrakingdom']">
		<xsl:text>infrakingdom</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='tribus' or string()='Tribus' or string()='Tribe']">
		<xsl:text>tribe</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='subtribus' or string()='Subtribus' or string()='Subtribe']">
		<xsl:text>subtribe</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='infratribus' or string()='Infratribus' or string()='Infratribe']">
		<xsl:text>infratribe</xsl:text>
	</xsl:template>

	<xsl:template match="text()[string()='supertribus' or string()='Supertribus' or string()='Supertribe']">
		<xsl:text>supertribe</xsl:text>
	</xsl:template>

</xsl:stylesheet>