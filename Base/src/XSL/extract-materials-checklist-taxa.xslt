<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:template match="/">
		<taxa>
			<xsl:apply-templates/>
		</taxa>
	</xsl:template>

	<xsl:template match="@*"/>

	<xsl:template match="*">
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template match="node()[name()='']"/>

	<xsl:template match="node()[@id='90']/value | node()[@id='419']/value">
		<taxon name="{normalize-space(.)}" type="kingdom">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='420']/value">
		<taxon name="{normalize-space(.)}" type="subkingdom">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='91']/value | node()[@id='421']/value">
		<taxon name="{normalize-space(.)}" type="phylum">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='422']/value">
		<taxon name="{normalize-space(.)}" type="subphylum">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='423']/value">
		<taxon name="{normalize-space(.)}" type="superclass">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='92']/value | node()[@id='424']/value">
		<taxon name="{normalize-space(.)}" type="class">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='425']/value">
		<taxon name="{normalize-space(.)}" type="subclass">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='426']/value">
		<taxon name="{normalize-space(.)}" type="superorder">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='93']/value | node()[@id='427']/value">
		<taxon name="{normalize-space(.)}" type="order">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='428']/value">
		<taxon name="{normalize-space(.)}" type="suborder">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='429']/value">
		<taxon name="{normalize-space(.)}" type="infraorder">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='430']/value">
		<taxon name="{normalize-space(.)}" type="superfamily">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='94']/value | node()[@id='431']/value">
		<taxon name="{normalize-space(.)}" type="family">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='432']/value">
		<taxon name="{normalize-space(.)}" type="subfamily">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='433']/value">
		<taxon name="{normalize-space(.)}" type="tribe">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='434']/value">
		<taxon name="{normalize-space(.)}" type="subtribe">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='95']/value | node()[@id='48']/value">
		<taxon name="{normalize-space(.)}" type="genus">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='96']/value | node()[@id='417']/value">
		<taxon name="{normalize-space(.)}" type="subgenus">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='49']/value">
		<taxon name="{normalize-space(.)}" type="species">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='418']/value">
		<taxon name="{normalize-space(.)}" type="subspecies">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='435']/value">
		<taxon name="{normalize-space(.)}" type="variety">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

	<xsl:template match="node()[@id='436']/value">
		<taxon name="{normalize-space(.)}" type="form">
			<xsl:apply-templates />
		</taxon>
	</xsl:template>

</xsl:stylesheet>