<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
                exclude-result-prefixes="xs mml xlink tp xsi"
                xmlns:mml="http://www.w3.org/1998/Math/MathML"
                xmlns:xlink="http://www.w3.org/1999/xlink"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:tp="http://www.plazi.org/taxpub" >

	<xsl:output method="xml" encoding="utf-8" indent="yes" omit-xml-declaration="yes" standalone="yes" version="1.0" media-type="text/xml" />

	<xsl:key name="original-name" match="taxon" use="@original-name"/>

	<xsl:template mode="simple-copy" match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates mode="simple-copy" select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="/">
		<taxa>
			<xsl:apply-templates mode="simple-copy" select="/taxa/splitted"/>
			<unsplitted>
				<xsl:apply-templates mode="split-taxon" select="/taxa/unsplitted/*"/>
			</unsplitted>
		</taxa>
	</xsl:template>

	<xsl:template mode="split-taxon" match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates mode="split-taxon" select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template mode="split-taxon" match="taxon">
		<xsl:variable name="string" select="normalize-space(.)"/>
		<xsl:element name="{name()}">
			<xsl:apply-templates mode="simple-copy" select="@*"/>
			<xsl:choose>
				<xsl:when test="@parent='general'">
					<xsl:call-template name="split-general">
						<xsl:with-param name="string" select="$string"/>
					</xsl:call-template>
				</xsl:when>
				<xsl:when test="@parent='italic'">
					<xsl:call-template name="split-italic">
						<xsl:with-param name="string" select="$string"/>
					</xsl:call-template>
				</xsl:when>
			</xsl:choose>
		</xsl:element>
	</xsl:template>

	<xsl:template name="split-general">
		<xsl:param name="string" select="''"/>
		<xsl:choose>
			<xsl:when test="contains($string, ' ')">
				<xsl:variable name="part" select="translate(substring-before($string, ' '), '()', '')"/>
				<part>
					<xsl:call-template name="get-taxon-type-general">
						<xsl:with-param name="part" select="$part"/>
					</xsl:call-template>
					<xsl:value-of select="$part"/>
				</part>
				<xsl:call-template name="split-general">
					<xsl:with-param name="string" select="substring-after($string, ' ')"/>
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="part" select="translate(normalize-space($string), '()', '')"/>
				<part>
					<xsl:call-template name="get-taxon-type-general">
						<xsl:with-param name="part" select="$part"/>
					</xsl:call-template>
					<xsl:value-of select="$part"/>
				</part>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="get-taxon-type-general">
		<xsl:param name="part" select="''"/>
		<xsl:variable name="node" select="/taxa/splitted/taxon/part[normalize-space(.)=$part]"/>
		<!--xsl:value-of select="//part[generate-id() = generate-id(key('original-name', .)[1])]/@type"/-->
		<!--xsl:value-of select="normalize-space(key('original-name', .))"/-->
		<xsl:attribute name="type">
			<xsl:value-of select="$node/@type"/>
		</xsl:attribute>
	</xsl:template>

	<xsl:template name="split-italic">
		<xsl:param name="string" select="''"/>
		<xsl:param name="previous-part" select="''"/>
		<xsl:param name="level" select="1"/>
		<xsl:choose>
			<xsl:when test="contains($string, ' ')">
				<xsl:variable name="part" select="substring-before($string, ' ')"/>
				<xsl:call-template name="split-italic-part">
					<xsl:with-param name="part" select="$part"/>
					<xsl:with-param name="previous-part" select="$previous-part"/>
					<xsl:with-param name="level" select="$level"/>
				</xsl:call-template>
				<xsl:call-template name="split-italic">
					<xsl:with-param name="string" select="substring-after($string, ' ')"/>
					<xsl:with-param name="previous-part" select="$part"/>
					<xsl:with-param name="level" select="$level+1"/>
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="part" select="normalize-space($string)"/>
				<xsl:call-template name="split-italic-part">
					<xsl:with-param name="part" select="$part"/>
					<xsl:with-param name="previous-part" select="$previous-part"/>
					<xsl:with-param name="level" select="$level"/>
				</xsl:call-template>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template name="split-italic-part">
		<xsl:param name="part" select="''"/>
		<xsl:param name="previous-part" select="''"/>
		<xsl:param name="level"/>

		<xsl:variable name="test1" select="contains(translate($part, 'QWERTYUIOPASDFGHJKLZXCVBNM', '##########################'), '#')"/>
		<xsl:variable name="test2" select="contains($part, '(') or contains($part, ')')"/>
		<xsl:variable name="test3" select="contains($previous-part, '(') or contains($part, ')')"/>
		<xsl:variable name="test4" select="(contains($part, 'var.') or contains($part, 'subsp.') or contains($part, 'f.'))"/>
		<xsl:variable name="test5" select="(contains($part, 'cf.') or contains($part, 'aff.') or contains($part, 'spp.') or contains($part, 'sp.')) or contains($part, 'ssp.') or contains($part, 'gen.') or $part='?'"/>

		<part>
			<xsl:attribute name="type">
				<xsl:choose>
					<xsl:when test="$test1 and not($test2) and ($level=1)">
						<xsl:text>genus</xsl:text>
					</xsl:when>
					<xsl:when test="$test1 and $test2">
						<xsl:text>subgenus</xsl:text>
					</xsl:when>
					<xsl:when test="$test4 and (($level &gt; 1 and not($test3)) or $level &gt; 2)">
						<xsl:text>infraspecific-rank</xsl:text>
					</xsl:when>
					<xsl:when test="$test5 and (($level &gt; 1 and not($test3)) or $level &gt; 2)">
						<xsl:text>uncertainty-rank</xsl:text>
					</xsl:when>
					<xsl:when test="contains($previous-part, 'var.')">
						<xsl:text>variety</xsl:text>
					</xsl:when>
					<xsl:when test="contains($previous-part, 'subsp.')">
						<xsl:text>subspecies</xsl:text>
					</xsl:when>
					<xsl:when test="contains($previous-part, 'f.') and $level &gt; 3">
						<xsl:text>form</xsl:text>
					</xsl:when>
					<xsl:when test="(not($test1) and ($level &gt; 1)) or ($test1 and (($level &gt; 2 and not($test3)) or $level &gt; 3))">
						<xsl:text>species</xsl:text>
					</xsl:when>
					<xsl:when test="(not($test1) and ($level &gt; 2)) or ($test1 and $level &gt; 4)">
						<xsl:text>subspecies</xsl:text>
					</xsl:when>
				</xsl:choose>
			</xsl:attribute>
			<xsl:value-of select="translate($part, '()', '')"/>
		</part>
	</xsl:template>
</xsl:stylesheet>