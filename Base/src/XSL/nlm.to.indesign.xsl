<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema" exclude-result-prefixes="xs"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub">

	<xsl:output method="xml" indent="no" standalone="yes" encoding="utf-8" version="1.0"/>
	<xsl:preserve-space elements="italic bold"/>
	<xsl:strip-space elements="front body back sec p td th tr"/>

	<xsl:template match="@* | node()">
		<xsl:copy>
			<xsl:apply-templates select="@* | node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="italic | Italic | i">
		<Italic>
			<xsl:apply-templates/>
		</Italic>
	</xsl:template>

	<xsl:template match="bold | Bold | b">
		<Bold>
			<xsl:apply-templates/>
		</Bold>
	</xsl:template>

	<xsl:template match="bold/italic | Bold/Italic | b/i">
		<Bold-Italic>
			<xsl:apply-templates/>
		</Bold-Italic>
	</xsl:template>

	<xsl:template match="tp:treatment-meta"/>

	<xsl:template match="tp:treatment-sec/title">
		<TreatmentSecTitle>
			<xsl:apply-templates/>
		</TreatmentSecTitle>
	</xsl:template>

	<xsl:template match="abstract/label | trans-abstract/label">
		<AbstractLabel>
			<xsl:apply-templates/>
		</AbstractLabel>
	</xsl:template>

	<xsl:template match="abstract/p | trans-abstract/p">
		<AbstractP>
			<xsl:apply-templates/>
		</AbstractP>
	</xsl:template>

	<xsl:template match="sec[name(..)!='sec' and name(..)!='tp:treatment-sec']/title">
		<Title1>
			<xsl:apply-templates/>
		</Title1>
	</xsl:template>

	<xsl:template match="sec[name(..)='sec' and name(..)='tp:treatment-sec']/title">
		<Title2>
			<xsl:apply-templates/>
		</Title2>
	</xsl:template>

	<xsl:template match="@sec-type | tp:taxon-name/@type | tp:taxon-name-part/@full-name"/>

	<xsl:template match="tp:taxon-name-part[normalize-space()='']"/>

	<xsl:template match="front/article-meta/kwd-group">
		<kwd-group>
			<kwd-label>
				<xsl:apply-templates select="label"/>
			</kwd-label>
			<kwd-p>
				<xsl:for-each select="kwd">
					<xsl:apply-templates/>
					<xsl:if test="position()!=last()">
						<xsl:text>, </xsl:text>
					</xsl:if>
				</xsl:for-each>
			</kwd-p>
		</kwd-group>
	</xsl:template>

	<xsl:template match="tp:taxon-name">
		<tp:taxon-name>
			<xsl:for-each select="node()">
				<xsl:choose>
					<xsl:when test="name()='tp:taxon-name-part'">
						<xsl:apply-templates/>
					</xsl:when>
					<xsl:when test="name()='' and normalize-space(.)!=''">
						<xsl:value-of select="normalize-space()"/>
					</xsl:when>
					<xsl:when test="name()='' and normalize-space(.)=''"></xsl:when>
					<xsl:otherwise>
						<xsl:element name="{name()}">
							<xsl:apply-templates/>
						</xsl:element>
					</xsl:otherwise>
				</xsl:choose>
				<xsl:if test="position()!=last()">
					<xsl:text> </xsl:text>
				</xsl:if>
			</xsl:for-each>
		</tp:taxon-name>
	</xsl:template>

</xsl:stylesheet>
