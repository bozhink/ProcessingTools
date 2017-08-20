<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="p">
		<p>
			<xsl:apply-templates />
		</p>
	</xsl:template>

	<xsl:template match="sec | abstract | trans-abstract">
		<section>
			<xsl:apply-templates />
		</section>
	</xsl:template>

	<xsl:template match="sec[count(ancestor::node()[name()='sec'])=0]/title">
		<h2>
			<xsl:apply-templates />
		</h2>
	</xsl:template>

	<xsl:template match="sec[count(ancestor::node()[name()='sec'])!=0]/title">
		<h3>
			<xsl:apply-templates />
		</h3>
	</xsl:template>

	<xsl:template
		match="abstract/title | abstract/label | trans-abstract/title | trans-abstract/label">
		<h3>
			<xsl:apply-templates />
		</h3>
	</xsl:template>

	<xsl:template match="ack">
		<section>
			<xsl:apply-templates />
		</section>
	</xsl:template>

	<xsl:template match="ref-list">
		<ul>
			<xsl:apply-templates />
		</ul>
	</xsl:template>

	<xsl:template match="ref-list/ref">
		<li>
			<xsl:apply-templates select="@id" />
			<xsl:apply-templates />
		</li>
	</xsl:template>
</xsl:stylesheet>