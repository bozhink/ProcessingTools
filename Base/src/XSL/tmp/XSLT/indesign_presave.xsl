<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs">

	<xsl:output encoding="UTF-8" indent="yes" />

	<xsl:preserve-space elements="*" />

	<xsl:template match="/">
		<article>
			<xsl:apply-templates select="/article/@*" />
			<front>
				<xsl:apply-templates select="/article/front/journal-meta" />
				<article-meta>
					<xsl:apply-templates select="/article/front/article-meta/article-id" />
					<article-categories>
						<subj-group subj-group-type="heading">
							<subject>Research article</subject>
						</subj-group>
						<xsl:apply-templates select="/article/front/article-meta/article-categories/*" />
					</article-categories>
					<xsl:apply-templates select="/article/front/article-meta/title-group" />
					<xsl:apply-templates select="/article/front/article-meta/contrib-group" />
					<xsl:apply-templates select="/article/front/article-meta/aff" />
					<xsl:apply-templates select="/article/front/article-meta/author-notes" />
					<xsl:apply-templates select="/article/front/article-meta/pub-date" />
					<xsl:apply-templates select="/article/front/article-meta/note/volume" />
					<xsl:apply-templates select="/article/front/article-meta/volume-id" />
					<xsl:apply-templates select="/article/front/article-meta/volume-series" />
					<xsl:apply-templates select="/article/front/article-meta/issue" />
					<xsl:apply-templates select="/article/front/article-meta/issue-id" />
					<xsl:apply-templates select="/article/front/article-meta/issue-title" />
					<xsl:apply-templates select="/article/front/article-meta/issue-sponsor" />
					<xsl:apply-templates select="/article/front/article-meta/issue-part" />
					<xsl:apply-templates select="/article/front/article-meta/isbn" />
					<xsl:apply-templates select="/article/front/article-meta/supplement" />
					<xsl:apply-templates select="/article/front/article-meta/fpage" />
					<xsl:apply-templates select="/article/front/article-meta/lpage" />
					<xsl:apply-templates select="/article/front/article-meta/page-range" />
					<xsl:apply-templates select="/article/front/article-meta/elocation-id" />
					<xsl:apply-templates select="/article/front/article-meta/email" />
					<xsl:apply-templates select="/article/front/article-meta/ext-link" />
					<xsl:apply-templates select="/article/front/article-meta/uri" />
					<xsl:apply-templates select="/article/front/article-meta/product" />
					<xsl:apply-templates select="/article/front/article-meta/supplementary-material" />
					<xsl:apply-templates select="/article/front/article-meta/history" />
					<xsl:apply-templates select="/article/front/article-meta/permissions" />
					<xsl:apply-templates select="/article/front/article-meta/self-uri" />
					<xsl:apply-templates select="/article/front/article-meta/related-article" />
					<xsl:apply-templates select="/article/front/article-meta/abstract" />
					<xsl:apply-templates select="/article/front/article-meta/trans-abstract" />
					<xsl:apply-templates select="/article/front/article-meta/kwd-group" />
					<xsl:apply-templates select="/article/front/article-meta/funding-group" />
					<xsl:apply-templates select="/article/front/article-meta/conference" />
					<xsl:apply-templates select="/article/front/article-meta/counts" />
					<xsl:apply-templates select="/article/front/article-meta/custom-meta-group" />
				</article-meta>
				<xsl:if test="normalize-space(/article/front/journal-meta/issn[@pub-type='epub'])!='1860-0743'">
					<!-- Not a ZSE article -->
					<xsl:apply-templates select="/article/front/article-meta" mode="article-citations" />
				</xsl:if>
			</front>
			<xsl:apply-templates select="/article/body" />
			<xsl:apply-templates select="/article/back" />
			<xsl:if test="count(/article/floats-group[normalize-space(.)])!=0">
				<floats-group>
					<xsl:for-each select="/article/floats-group">
						<xsl:for-each select="supplementary-material">
							<xsl:variable name="suppMatAuthors" select="normalize-space(attrib)" />
							<xsl:variable name="suppMatLink" select="normalize-space(ext-link)" />
							<xsl:variable name="suppMatCopyright" select="normalize-space(permissions/license/license-p)" />
							<xsl:variable name="suppMatAttrib">
								<xsl:choose>
									<xsl:when test="contains($suppMatAuthors,'Authors:')">
										<xsl:value-of select="substring-after($suppMatAuthors,'Authors:')" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="$suppMatAuthors" />
									</xsl:otherwise>
								</xsl:choose>
							</xsl:variable>
							<xsl:variable name="suppMatExtLink">
								<xsl:choose>
									<xsl:when test="contains($suppMatLink,'Link: ')">
										<xsl:value-of select="substring-after($suppMatLink,'Link: ')" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="$suppMatLink" />
									</xsl:otherwise>
								</xsl:choose>
							</xsl:variable>
							<xsl:variable name="suppMatPermissions">
								<xsl:choose>
									<xsl:when test="contains($suppMatCopyright,'Copyright notice:')">
										<xsl:value-of select="substring-after($suppMatCopyright,'Copyright notice:')" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="$suppMatCopyright" />
									</xsl:otherwise>
								</xsl:choose>
							</xsl:variable>
							<xsl:element name="{name(.)}">
								<xsl:attribute name="id"><xsl:text>S</xsl:text><xsl:value-of select="position()" /></xsl:attribute>
								<xsl:attribute name="position"><xsl:text>float</xsl:text></xsl:attribute>
								<xsl:attribute name="orientation"><xsl:text>portrait</xsl:text></xsl:attribute>
								<xsl:attribute name="xlink:type"><xsl:text>simple</xsl:text></xsl:attribute>
								<xsl:apply-templates mode="unbold" select="label" />
								<xsl:apply-templates mode="unbold" select="caption" />
								<ext-link ext-link-type="uri" xlink:type="simple">
									<xsl:attribute name="xlink:href"><xsl:value-of select="$suppMatExtLink" /></xsl:attribute>
									<xsl:value-of select="$suppMatExtLink" />
								</ext-link>
								<xsl:apply-templates select="p" />
								<xsl:apply-templates select="dataType" />
								<xsl:apply-templates select="note" />
								<xsl:apply-templates select="notes" />
								<media xlink:href="" mimetype="" mime-subtype="" position="float" orientation="portrait" />
								<permissions>
									<license>
										<license-p>
											<xsl:value-of select="$suppMatPermissions" />
										</license-p>
									</license>
								</permissions>
								<attrib specific-use="authors">
									<xsl:value-of select="$suppMatAttrib" />
								</attrib>
							</xsl:element>
						</xsl:for-each>
					</xsl:for-each>
				</floats-group>
			</xsl:if>
			<xsl:if test="normalize-space(/article/article_figs_and_tables)!=''">
				<article_figs_and_tables xmlns:aid="http://ns.adobe.com/AdobeInDesign/4.0/">
					<xsl:apply-templates select="/article/article_figs_and_tables/*" />
				</article_figs_and_tables>
			</xsl:if>
		</article>
	</xsl:template>
	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*" />
			<xsl:apply-templates />
		</xsl:copy>
	</xsl:template>
	<xsl:template match="self-uri | contrib/uri | object-id">
		<xsl:element name="{name()}">
			<xsl:apply-templates select="@*" mode="uri-attributes" />
			<xsl:choose>
				<xsl:when test="contains(normalize-space(.), 'zoobank.org')">
					<xsl:attribute name="content-type">zoobank</xsl:attribute>
				</xsl:when>
				<xsl:otherwise>
					<xsl:if test="normalize-space(@content-type)!=''">
						<xsl:attribute name="content-type">
							<xsl:value-of select="@content-type" />
						</xsl:attribute>
					</xsl:if>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:choose>
				<xsl:when test="normalize-space(@xlink:type)=''">
					<xsl:attribute name="xlink:type">simple</xsl:attribute>
				</xsl:when>
				<xsl:otherwise>
					<xsl:attribute name="xlink:type">
						<xsl:value-of select="normalize-space(@xlink:type)" />
					</xsl:attribute>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:apply-templates />
		</xsl:element>
	</xsl:template>

	<xsl:template mode="uri-attributes" match="@*|*" />

	<xsl:template mode="uri-attributes" match="@xlink:actuate | @xlink:href | @xlink:role | @xlink:show | @xlink:title | @pub-id-type">
		<xsl:attribute name="{name()}">
			<xsl:value-of select="normalize-space(.)" />
		</xsl:attribute>
	</xsl:template>

	<xsl:template match="note" />

	<xsl:template match="*" mode="article-citations">
		<notes>
			<xsl:for-each select="note">
				<xsl:variable name="title">
					<xsl:choose>
						<xsl:when test="contains(string(bold),':')">
							<xsl:value-of select="normalize-space(substring-before(bold,':'))" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="normalize-space(bold)" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<sec>
					<xsl:attribute name="sec-type">
						<xsl:value-of select="$title" />
					</xsl:attribute>
					<title>
						<xsl:value-of select="$title" />
					</title>
					<p>
						<xsl:apply-templates select="." mode="citation-format" />
					</p>
				</sec>
			</xsl:for-each>
		</notes>
	</xsl:template>

	<xsl:template match="p" mode="citation-format">
		<xsl:apply-templates mode="citation-format" />
	</xsl:template>

	<xsl:template match="@*" mode="citation-format" />

	<xsl:template match="bold" mode="citation-format" />

	<xsl:template match="italic" mode="citation-format">
		<italic>
			<xsl:apply-templates mode="citation-format" />
		</italic>
	</xsl:template>

	<xsl:template match="text()" mode="citation-format">
		<xsl:value-of select="." />
	</xsl:template>

	<xsl:template match="ext-link|uri" mode="citation-format">
		<xsl:copy-of select="." />
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

	<xsl:template match="tr/td[position()!=3]" mode="unbold">
		<xsl:apply-templates select="." />
	</xsl:template>

	<xsl:template match="title | label | article-title | th">
		<xsl:apply-templates select="." mode="unbold" />
	</xsl:template>

	<xsl:template match="table-wrap[@content-type='key']">
		<table-wrap content-type="key" position="anchor" orientation="portrait">
			<xsl:apply-templates mode="unbold" />
		</table-wrap>
	</xsl:template>

	<xsl:template match="table[@id!='']">
		<table id="{@id}">
			<xsl:apply-templates />
		</table>
	</xsl:template>

	<xsl:template match="sec | tp:treatment-sec">
		<xsl:variable name="title">
			<xsl:variable name="full" select="normalize-space(title[1])" />
			<xsl:variable name="length" select="string-length($full)" />
			<xsl:variable name="last" select="substring($full, $length)" />
			<xsl:choose>
				<xsl:when test="$last='.' or $last=',' or $last=':'">
					<xsl:value-of select="substring($full, 1, $length - 1)" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$full" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:element name="{name()}">
			<xsl:attribute name="sec-type">
				<xsl:value-of select="$title" />
			</xsl:attribute>
			<xsl:apply-templates />
		</xsl:element>
	</xsl:template>
</xsl:stylesheet>