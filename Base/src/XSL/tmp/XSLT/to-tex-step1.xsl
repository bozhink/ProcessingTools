<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output encoding="UTF-8" method="xml"/>
	<xsl:preserve-space elements="*"/>

	<xsl:template match="/">
		<xsl:apply-templates />
	</xsl:template>

	<xsl:template match="/document/pdf/meta/keywords">
		<keywords>
			<xsl:call-template name="split-keywords-text">
				<xsl:with-param name="string" select="string(.)"/>
			</xsl:call-template>
		</keywords>
	</xsl:template>

	<xsl:template name="split-keywords-text">
		<xsl:param name="string"/>
		<xsl:choose>
			<xsl:when test="contains($string, ',')">
				<kwd>
					<xsl:value-of select="substring-before($string,',')"/>
				</kwd>
				<xsl:call-template name="split-keywords-text">
					<xsl:with-param name="string" select="substring-after($string,',')"/>
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<kwd>
					<xsl:value-of select="$string"/>
				</kwd>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<!-- Standard copy template -->
	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*"/>
			<xsl:apply-templates/>
		</xsl:copy>
	</xsl:template>

	<!-- Remove taxonomic markers -->
	<xsl:template match="tn|tn-part">
		<xsl:apply-templates />
	</xsl:template>

	<!-- Remove citation wrappers -->
	<xsl:template match="fig-citation|reference-citation|tbls-citation">
		<xsl:apply-templates />
	</xsl:template>

	<xsl:template match="citations"/>

	<!-- Format citations and internal hyperlinks -->
	<xsl:template match="xref[@type='bibr']">
		<xsl:text>\hyperref[</xsl:text>
		<xsl:text>label_B</xsl:text>
		<xsl:value-of select="@rid"/>
		<xsl:text>]{</xsl:text>
		<xsl:apply-templates/>
		<xsl:text>}</xsl:text>
		<xsl:text>\cite{</xsl:text>
		<xsl:text>B</xsl:text>
		<xsl:value-of select="@rid"/>
		<xsl:text>}</xsl:text>
	</xsl:template>

	<xsl:template match="xref[@type='fig']">
		<xsl:text>\hyperref[</xsl:text>
		<xsl:text>label_F</xsl:text>
		<xsl:value-of select="@rid"/>
		<xsl:text>]{</xsl:text>
		<xsl:apply-templates/>
<!-- 		<xsl:text>\ref*{</xsl:text> -->
<!-- 		<xsl:text>label_F</xsl:text> -->
<!-- 		<xsl:value-of select="@rid"/> -->
<!-- 		<xsl:text>}</xsl:text> -->
		<xsl:text>}</xsl:text>
	</xsl:template>

	<xsl:template match="xref[@type='table']">
		<xsl:text>\hyperref[</xsl:text>
		<xsl:text>label_T</xsl:text>
		<xsl:value-of select="@rid"/>
		<xsl:text>]{</xsl:text>
		<xsl:apply-templates/>
<!-- 		<xsl:text>\ref*{</xsl:text> -->
<!-- 		<xsl:text>label_T</xsl:text> -->
<!-- 		<xsl:value-of select="@rid"/> -->
<!-- 		<xsl:text>}</xsl:text> -->
		<xsl:text>}</xsl:text>
	</xsl:template>

	<!-- Remove Priority DarwinCore -->
	<xsl:template match="node()[@object_id='83']"/>

	<!-- Convert formats -->
	<xsl:template match="i|em">
		<xsl:text>{\em </xsl:text>
		<xsl:apply-templates />
		<xsl:text>}</xsl:text>
	</xsl:template>

	<xsl:template match="b|strong">
		<xsl:text>{\bf </xsl:text>
		<xsl:apply-templates />
		<xsl:text>}</xsl:text>
	</xsl:template>

	<xsl:template match="a">
		<xsl:text>\href{</xsl:text>
		<xsl:value-of select="@href"/>
		<xsl:text>}{\seqsplit{</xsl:text>
		<xsl:apply-templates/>
		<xsl:text>}}</xsl:text>
	</xsl:template>
</xsl:stylesheet>