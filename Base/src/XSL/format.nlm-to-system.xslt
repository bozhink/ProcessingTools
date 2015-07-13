<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
                exclude-result-prefixes="xs"
                xmlns:mml="http://www.w3.org/1998/Math/MathML"
                xmlns:xlink="http://www.w3.org/1999/xlink"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:tp="http://www.plazi.org/taxpub">

	<xsl:output method="xml" indent="no" encoding="utf-8" />

	<xsl:template match="@* | node()">
		<xsl:copy>
			<xsl:apply-templates select="@* | node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="i | em | italic | Italic">
		<i>
			<xsl:apply-templates select="@* | node()"/>
		</i>
	</xsl:template>

	<xsl:template match="b | strong | bold | Bold">
		<b>
			<xsl:apply-templates select="@* | node()"/>
		</b>
	</xsl:template>

	<xsl:template match="bold-italic | Bold-Italic">
		<b>
			<i>
				<xsl:apply-templates select="@* | node()"/>
			</i>
		</b>
	</xsl:template>

	<xsl:template match="tp:taxon-name | tn">
		<tn>
			<xsl:apply-templates select="@* | node()"/>
		</tn>
	</xsl:template>

	<xsl:template match="tp:taxon-name-part | tn-part">
		<tn-part>
			<xsl:apply-templates select="@* | node()"/>
		</tn-part>
	</xsl:template>

	<xsl:template match="tp:taxon-name-part/@taxon-name-part-type">
		<xsl:attribute name="type">
			<xsl:value-of select="."/>
		</xsl:attribute>
	</xsl:template>

	<!-- Remove invalid markup -->
	<xsl:template match="//tn//tn|//tp:taxon-name//tn|//a//tn|//ext-link//tn|//tp:treatment-meta/kwd-group/kwd/named-content//tn|//*[@object_id='82']//tn|//*[@id='41']//tn|//article/front/notes/sec//tn|//*[@id='236' or @id='436' or @id='435' or @id='418' or @id='49' or @id='417' or @id='48' or @id='434' or @id='433' or @id='432' or @id='431' or @id='430' or @id='429' or @id='428' or @id='427' or @id='426' or @id='425' or @id='424' or @id='423' or @id='422' or @id='421' or @id='420' or @id='419' or @id='475' or @id='414']/value//tn|//xref//tn|//tn//xref">
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template match="//tn//tp:taxon-name|//tp:taxon-name//tp:taxon-name|//a//tp:taxon-name|//ext-link//tp:taxon-name|//tp:treatment-meta/kwd-group/kwd/named-content//tp:taxon-name|//*[@object_id='82']//tp:taxon-name|//*[@id='41']//tp:taxon-name|//article/front/notes/sec//tp:taxon-name|//*[@id='236' or @id='436' or @id='435' or @id='418' or @id='49' or @id='417' or @id='48' or @id='434' or @id='433' or @id='432' or @id='431' or @id='430' or @id='429' or @id='428' or @id='427' or @id='426' or @id='425' or @id='424' or @id='423' or @id='422' or @id='421' or @id='420' or @id='419' or @id='475' or @id='414']/value//tp:taxon-name|//xref//tp:taxon-name|//tp:taxon-name//xref">
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template match="//tn-part//tn-part|//tn-part//tp:taxon-name-part|//tp:taxon-name-part//tp:taxon-name-part">
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template match="//a//a|//a//ext-link|//ext-link//a|//ext-link//ext-link|//a//xref|//ext-link//xref|//xref//xref|//xref//a|//xref//ext-link">
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template match="surname|given-names|prefix|suffix|anonymous|etal">
		<xsl:element name="{name()}">
			<xsl:value-of select="normalize-space()"/>
		</xsl:element>
	</xsl:template>
</xsl:stylesheet>
