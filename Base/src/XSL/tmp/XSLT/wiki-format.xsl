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

	<!-- WIKI FORMAT -->
	<xsl:template match="italic">
		<xsl:text>''</xsl:text><xsl:apply-templates/><xsl:text>''</xsl:text>
	</xsl:template>
	<xsl:template match="bold">
		<xsl:text>'''</xsl:text><xsl:apply-templates/><xsl:text>'''</xsl:text>
	</xsl:template>

	<xsl:template match="ref//tp:taxon-name | title-group/article-title//tp:taxon-name | article-meta//kwd//tp:taxon-name">
		<xsl:choose>
			<xsl:when test="name(..)='italic'">
				<xsl:value-of select="normalize-space(.)"/>
			</xsl:when>
			<xsl:when test="count(tp:taxon-name-part[@taxon-name-part-type='genus'])!=0">
				<xsl:text>''</xsl:text><xsl:value-of select="normalize-space(.)"/><xsl:text>''</xsl:text>
			</xsl:when>
			<xsl:when test="count(tp:taxon-name-part[@taxon-name-part-type='subgenus'])!=0">
				<xsl:text>''</xsl:text><xsl:value-of select="normalize-space(.)"/><xsl:text>''</xsl:text>
			</xsl:when>
			<xsl:when test="count(tp:taxon-name-part[@taxon-name-part-type='species'])!=0">
				<xsl:text>''</xsl:text><xsl:value-of select="normalize-space(.)"/><xsl:text>''</xsl:text>
			</xsl:when>
			<xsl:when test="count(tp:taxon-name-part[@taxon-name-part-type='subspecies'])!=0">
				<xsl:text>''</xsl:text><xsl:value-of select="normalize-space(.)"/><xsl:text>''</xsl:text>
			</xsl:when>
			<xsl:when test="count(tp:taxon-name-part[@taxon-name-part-type='forma'])!=0">
				<xsl:text>''</xsl:text><xsl:value-of select="normalize-space(.)"/><xsl:text>''</xsl:text>
			</xsl:when>
			<xsl:when test="count(tp:taxon-name-part[@taxon-name-part-type='variety'])!=0">
				<xsl:text>''</xsl:text><xsl:value-of select="normalize-space(.)"/><xsl:text>''</xsl:text>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="normalize-space(.)"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template match="tp:taxon-name">
		<xsl:choose>
			<xsl:when test="name(..)='italic'">
				<xsl:call-template name="taxon-name"/>
			</xsl:when>
			<xsl:when test="count(tp:taxon-name-part[@taxon-name-part-type='genus'])!=0">
				<xsl:text>''</xsl:text><xsl:call-template name="taxon-name"/><xsl:text>''</xsl:text>
			</xsl:when>
			<xsl:when test="count(tp:taxon-name-part[@taxon-name-part-type='subgenus'])!=0">
				<xsl:text>''</xsl:text><xsl:call-template name="taxon-name"/><xsl:text>''</xsl:text>
			</xsl:when>
			<xsl:when test="count(tp:taxon-name-part[@taxon-name-part-type='species'])!=0">
				<xsl:text>''</xsl:text><xsl:call-template name="taxon-name"/><xsl:text>''</xsl:text>
			</xsl:when>
			<xsl:when test="count(tp:taxon-name-part[@taxon-name-part-type='subspecies'])!=0">
				<xsl:text>''</xsl:text><xsl:call-template name="taxon-name"/><xsl:text>''</xsl:text>
			</xsl:when>
			<xsl:when test="count(tp:taxon-name-part[@taxon-name-part-type='forma'])!=0">
				<xsl:text>''</xsl:text><xsl:call-template name="taxon-name"/><xsl:text>''</xsl:text>
			</xsl:when>
			<xsl:when test="count(tp:taxon-name-part[@taxon-name-part-type='variety'])!=0">
				<xsl:text>''</xsl:text><xsl:call-template name="taxon-name"/><xsl:text>''</xsl:text>
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="taxon-name"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<xsl:template name="taxon-name">
		<xsl:text>{{Taxon name|</xsl:text>
		<xsl:for-each select="node()[normalize-space(.)!='']">
			<xsl:choose>
				<xsl:when test="name()=''">
					<xsl:value-of select="normalize-space(.)"/>
				</xsl:when>
				<xsl:when test="name()='tp:taxon-name-part'">
					<xsl:choose>
						<xsl:when test="normalize-space(@full-name)!=''">
							<xsl:value-of select="@full-name"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="normalize-space(.)"/>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="normalize-space(.)"/>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:if test="position()!=last()">
				<xsl:text> </xsl:text>
			</xsl:if>
		</xsl:for-each>
		<xsl:text>}}</xsl:text>
	</xsl:template>

	<!-- END OF WIKI FORMAT -->

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