<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs">

	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>

	<xsl:preserve-space elements="*"/>

	<xsl:template match="/article">
		<article>
			<xsl:apply-templates select="@*"/>
			<xsl:attribute name="article-type">research-article</xsl:attribute>
			<front>
				<xsl:for-each select="front[normalize-space(.)!='']">
					<xsl:apply-templates select="*"/>
				</xsl:for-each>
			</front>
			<xsl:if test="count(body[normalize-space(.)!=''])!=0">
				<body>
					<xsl:for-each select="body[normalize-space(.)!='']">
						<xsl:apply-templates select="*"/>
					</xsl:for-each>
				</body>
			</xsl:if>
			<xsl:if test="count(back[normalize-space(.)!=''])!=0">
				<back>
					<xsl:for-each select="back[normalize-space(.)!='']">
						<xsl:apply-templates select="*"/>
					</xsl:for-each>
				</back>
			</xsl:if>
			<xsl:if test="count(floats-group[normalize-space(.)!=''])!=0">
				<floats-group>
					<xsl:for-each select="floats-group[normalize-space(.)!='']">
						<xsl:apply-templates select="*"/>
					</xsl:for-each>
				</floats-group>
			</xsl:if>
			<xsl:apply-templates select="sub-article"/>
			<xsl:apply-templates select="response"/>
		</article>
	</xsl:template>

	<xsl:template match="@*|*|comment()">
		<xsl:copy>
			<xsl:apply-templates select="@*" />
			<xsl:apply-templates />
		</xsl:copy>
	</xsl:template>

	<xsl:template match="@class"/>

	<xsl:template match="table-wrap">
		<xsl:variable name="id" select="normalize-space(table[1]/@id)"/>
		<table-wrap>
			<xsl:apply-templates select="@*"/>
			<xsl:if test="$id!=''">
				<xsl:attribute name="id">
					<xsl:value-of select="$id"/>
				</xsl:attribute>
			</xsl:if>
			<xsl:apply-templates/>
		</table-wrap>
	</xsl:template>

	<xsl:template match="table">
		<table>
			<xsl:apply-templates select="node()"/>
		</table>
	</xsl:template>
	
	<xsl:template match="person-group">
		<person-group person-group-type="author">
			<xsl:apply-templates select="anonymous | collab | name | aff | etal | string-name"/>
		</person-group>
	</xsl:template>

	<xsl:template match="person-group1">
		<person-group person-group-type="editor">
			<xsl:apply-templates select="anonymous | collab | name | aff | etal | string-name"/>
		</person-group>
	</xsl:template>
	
	<xsl:template match="tp:nomenclature">
		<tp:nomenclature>
			<xsl:apply-templates select="sec-meta"/>
			<xsl:apply-templates select="label"/>
			<tp:taxon-name>
				<xsl:apply-templates select="tp:taxon-name/*"/>
				<xsl:apply-templates select="object-id"/>
			</tp:taxon-name>
			<xsl:apply-templates select="tp:taxon-authority"/>
			<xsl:apply-templates select="tp:taxon-status"/>
			<xsl:apply-templates select="xref | xref-group/xref"/>
			<xsl:apply-templates select="tp:nomenclature-citation-list"/>
			<xsl:apply-templates select="tp:type-genus|tp:type-species"/>
			<xsl:apply-templates select="tp:taxon-type-location"/>
		</tp:nomenclature>
	</xsl:template>

	<xsl:template match="tp:taxon-name">
		<tp:taxon-name>
			<xsl:apply-templates/>
		</tp:taxon-name>
	</xsl:template>

	<xsl:template match="tp:taxon-name-part">
		<xsl:if test="normalize-space(.)!=''">
			<tp:taxon-name-part>
				<xsl:apply-templates select="@taxon-name-part-type"/>
				<xsl:apply-templates/>
			</tp:taxon-name-part>
		</xsl:if>
	</xsl:template>

	<xsl:template match="media/@xlink:href | graphic/@xlink:href">
		<xsl:attribute name="xlink:href">
			<xsl:call-template name="cut-href-to-file-name">
				<xsl:with-param name="href" select="normalize-space(.)"/>
			</xsl:call-template>
		</xsl:attribute>
	</xsl:template>

	<xsl:template name="cut-href-to-file-name">
		<xsl:param name="href"/>
		<xsl:choose>
			<xsl:when test="contains($href, '/')">
				<xsl:call-template name="cut-href-to-file-name">
					<xsl:with-param name="href" select="substring-after($href,'/')"/>
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="normalize-space($href)"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template match="supplementary-material">
		<supplementary-material>
			<xsl:apply-templates select="@*"/>
			<xsl:apply-templates select="object-id"/>
			<xsl:apply-templates select="label"/>
			<xsl:apply-templates select="caption"/>
			<xsl:apply-templates select="alt-text | long-desc | email | ext-link | uri"/>
			<xsl:apply-templates select="disp-formula | disp-formula-group | chem-struct-wrap | disp-quote | speech | statement | verse-group | table-wrap | p | dataType/p | notes/p | def-list | list | alternatives | array | graphic | preformat"/>
			<xsl:apply-templates select="media"/>
			<xsl:apply-templates select="permissions"/>
			<xsl:apply-templates select="attrib"/>
		</supplementary-material>
	</xsl:template>
</xsl:stylesheet>