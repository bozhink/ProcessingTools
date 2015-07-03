<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
                xmlns:mml="http://www.w3.org/1998/Math/MathML"
                xmlns:xlink="http://www.w3.org/1999/xlink"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:tp="http://www.plazi.org/taxpub"
                exclude-result-prefixes="xs">

	<xsl:output encoding="UTF-8" indent="yes"/>

	<xsl:template match="/">
		<article>
			<xsl:apply-templates select="/article/@*"/>
			<front>
				<xsl:apply-templates select="/article/front/*"/>
				<xsl:apply-templates select="/article/front/article-meta" mode="article-citations"/>
			</front>
			<xsl:apply-templates select="/article/body"/>
			<xsl:apply-templates select="/article/back"/>
			<floats-group xmlns:aid="http://ns.adobe.com/AdobeInDesign/4.0/">
				<xsl:apply-templates select="/article/article_figs_and_tables/*"/>
			</floats-group>
		</article>
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()" />
		</xsl:copy>
	</xsl:template>

	<xsl:template match="note"/>

	<xsl:template match="*" mode="article-citations">
		<notes>
			<xsl:for-each select="note">
				<xsl:variable name="title">
					<xsl:choose>
						<xsl:when test="contains(string(bold),':')">
							<xsl:value-of select="normalize-space(substring-before(bold,':'))"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="normalize-space(bold)"/>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<sec>
					<xsl:attribute name="sec-type">
						<xsl:value-of select="$title"/>
					</xsl:attribute>
					<title>
						<xsl:value-of select="$title"/>
					</title>
					<p>
						<xsl:apply-templates select="." mode="citation-format"/>
					</p>
				</sec>
			</xsl:for-each>
		</notes>
	</xsl:template>

	<xsl:template match="p" mode="citation-format">
		<xsl:apply-templates mode="citation-format"/>
	</xsl:template>
	<xsl:template match="@*" mode="citation-format"/>
	<xsl:template match="bold" mode="citation-format"/>
	<xsl:template match="italic" mode="citation-format">
		<italic>
			<xsl:apply-templates mode="citation-format"/>
		</italic>
	</xsl:template>
	<xsl:template match="text()" mode="citation-format">
		<xsl:value-of select="."/>
	</xsl:template>
	<xsl:template match="ext-link|uri" mode="citation-format">
		<xsl:copy-of select="."/>
	</xsl:template>

	<xsl:template match="bold-italic">
		<bold>
			<italic>
				<xsl:apply-templates />
			</italic>
		</bold>
	</xsl:template>
	<xsl:template match="ref">
		<ref>
			<xsl:attribute name="id">
				<xsl:text>B</xsl:text>
				<xsl:number />
			</xsl:attribute>
			<xsl:apply-templates />
		</ref>
	</xsl:template>
	<xsl:template match="fake_tag">
		<source>
			<xsl:apply-templates />
		</source>
	</xsl:template>
	<xsl:template match="@*|node()" mode="unbold">
		<xsl:copy>
			<xsl:apply-templates select="@*" />
			<xsl:apply-templates mode="unbold" />
		</xsl:copy>
	</xsl:template>
	<xsl:template match="bold" mode="unbold">
		<xsl:apply-templates mode="unbold" />
	</xsl:template>
	<xsl:template match="bold-italic" mode="unbold">
		<italic>
			<xsl:apply-templates mode="unbold" />
		</italic>
	</xsl:template>
	<xsl:template match="title | label | article-title | th">
		<xsl:apply-templates select="." mode="unbold" />
	</xsl:template>
	<xsl:template match="table-wrap[@content-type='key']">
		<table-wrap>
			<xsl:attribute name="content-type">key</xsl:attribute>
			<xsl:attribute name="position">anchor</xsl:attribute>
			<xsl:attribute name="orientation">portrait</xsl:attribute>
			<xsl:apply-templates mode="unbold" />
		</table-wrap>
	</xsl:template>
	<xsl:template match="table[@id!='']">
		<table>
			<xsl:attribute name="id">
				<xsl:value-of select="@id"/>
			</xsl:attribute>
			<xsl:apply-templates />
		</table>
	</xsl:template>

	<xsl:template match="sec">
		<xsl:variable name="title" select="normalize-space(title[1])"/>
		<sec>
			<xsl:attribute name="sec-type">
				<xsl:value-of select="$title"/>
			</xsl:attribute>
			<xsl:apply-templates />
		</sec>
	</xsl:template>
	<xsl:template match="tp:treatment-sec">
		<xsl:variable name="title" select="normalize-space(title[1])"/>
		<tp:treatment-sec>
			<xsl:attribute name="sec-type">
				<xsl:value-of select="$title"/>
			</xsl:attribute>
			<xsl:apply-templates />
		</tp:treatment-sec>
	</xsl:template>
</xsl:stylesheet>
