<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output encoding="UTF-8" method="xml"/>
	<xsl:preserve-space elements="*"/>

	<xsl:template match="/document">
		<document>
			<pdf>
				<meta>
					<title>
						<xsl:apply-templates mode="untag-all" select="/document/objects/article_metadata/title_and_authors/fields/title/value"/>
					</title>
					<keywords>
						<xsl:apply-templates mode="untag-all" select="/document/objects/article_metadata/abstract_and_keywords/fields/keywords/value"/>
					</keywords>
					<authors>
						<xsl:apply-templates mode="pdf-meta" select="/document/objects/article_metadata/title_and_authors/author"/>
					</authors>
					<subjects>
						<xsl:for-each select="/document/objects/article_metadata/classifications/fields/*/value[normalize-space()!='']">
							<subject>
								<xsl:value-of select="."/>
							</subject>
						</xsl:for-each>
					</subjects>
				</meta>
			</pdf>
			<xsl:apply-templates />
		</document>
	</xsl:template>

	<xsl:template mode="pdf-meta" match="@*|*"/>

	<xsl:template mode="pdf-meta" match="author">
		<xsl:variable name="first_name" select="normalize-space(fields/node()[@id='6'])"/>
		<xsl:variable name="middle_name" select="normalize-space(fields/node()[@id='7'])"/>
		<xsl:variable name="last_name" select="normalize-space(fields/node()[@id='8'])"/>
		<author>
			<first-name>
				<xsl:value-of select="$first_name"/>
			</first-name>
			<middle-name>
				<xsl:value-of select="$middle_name"/>
			</middle-name>
			<last-name>
				<xsl:value-of select="$last_name"/>
			</last-name>
			<full-name>
				<xsl:if test="string($first_name)!=''">
					<xsl:value-of select="$first_name"/>
					<xsl:if test="string($middle_name)!='' or string(@last_name)!=''">
						<xsl:text> </xsl:text>
					</xsl:if>
				</xsl:if>
				<xsl:if test="string($middle_name)!=''">
					<xsl:value-of select="$middle_name"/>
					<xsl:if test="string($last_name)!=''">
						<xsl:text> </xsl:text>
					</xsl:if>
				</xsl:if>
				<xsl:value-of select="$last_name"/>
			</full-name>
		</author>
	</xsl:template>

	<!-- Remove empty fields -->
	<xsl:template match="@class"/>

	<xsl:template match="node()[count(node())=1][value][value[count(node())=0]]"/>

	<!-- Remove all internal tags and attributes -->
	<xsl:template mode="untag-all" match="@*"/>

	<xsl:template mode="untag-all" match="*">
		<xsl:apply-templates mode="untag-all"/>
	</xsl:template>

	<xsl:template match="@*|node()[name()!='']">
		<xsl:copy>
			<xsl:apply-templates select="@*"/>
			<xsl:apply-templates/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="node()[name()='']">
		<xsl:variable name="text1">
			<xsl:call-template name="replace">
				<xsl:with-param name="string" select="string(.)"/>
				<xsl:with-param name="pattern" select="'%'"/>
				<xsl:with-param name="replace" select="'\%'"/>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="text2">
			<xsl:call-template name="replace">
				<xsl:with-param name="string" select="$text1"/>
				<xsl:with-param name="pattern" select="'&amp;'"/>
				<xsl:with-param name="replace" select="'\&amp;'"/>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="text3">
			<xsl:call-template name="replace">
				<xsl:with-param name="string" select="$text2"/>
				<xsl:with-param name="pattern" select="'_'"/>
				<xsl:with-param name="replace" select="'\_'"/>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="text4">
			<xsl:call-template name="replace">
				<xsl:with-param name="string" select="$text3"/>
				<xsl:with-param name="pattern" select="'^'"/>
				<xsl:with-param name="replace" select="'\^'"/>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="text5">
			<xsl:call-template name="replace">
				<xsl:with-param name="string" select="$text4"/>
				<xsl:with-param name="pattern" select="'$'"/>
				<xsl:with-param name="replace" select="'\$'"/>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="text6">
			<xsl:call-template name="replace">
				<xsl:with-param name="string" select="$text5"/>
				<xsl:with-param name="pattern" select="'°'"/>
				<xsl:with-param name="replace" select="'$^\circ$'"/>
			</xsl:call-template>
		</xsl:variable>
		<xsl:value-of select="$text6"/>
	</xsl:template>

	<xsl:template mode="text-escape" match="*"/>

	<xsl:template name="replace">
		<xsl:param name="string"/>
		<xsl:param name="pattern"/>
		<xsl:param name="replace" select="''"/>
		<xsl:choose>
			<xsl:when test="contains($string, $pattern)">
				<xsl:value-of select="substring-before($string, $pattern)"/>
				<xsl:value-of select="$replace"/>
				<xsl:call-template name="replace">
					<xsl:with-param name="string" select="substring-after($string, $pattern)"/>
					<xsl:with-param name="pattern" select="$pattern"/>
					<xsl:with-param name="replace" select="$replace"/>
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$string"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<!-- Remove italics around tn -->
	<xsl:template match="i[count(node())=1][tn]">
		<xsl:apply-templates />
	</xsl:template>

	<!-- Paste italics around lower taxonomic names -->
	<xsl:template match="tn-part[@type='genus']">
		<tn-part>
			<xsl:apply-templates select="@*"/>
			<i>
				<xsl:apply-templates/>
			</i>
		</tn-part>
	</xsl:template>

	<xsl:template match="tn-part[@type='subgenus']">
		<tn-part>
			<xsl:apply-templates select="@*"/>
			<i>
				<xsl:apply-templates/>
			</i>
		</tn-part>
	</xsl:template>

	<xsl:template match="tn-part[@type='species']">
		<tn-part>
			<xsl:apply-templates select="@*"/>
			<i>
				<xsl:apply-templates/>
			</i>
		</tn-part>
	</xsl:template>

	<xsl:template match="tn-part[@type='subspecies']">
		<tn-part>
			<xsl:apply-templates select="@*"/>
			<i>
				<xsl:apply-templates/>
			</i>
		</tn-part>
	</xsl:template>

	<xsl:template match="tn-part[@type='variaty']">
		<tn-part>
			<xsl:apply-templates select="@*"/>
			<i>
				<xsl:apply-templates/>
			</i>
		</tn-part>
	</xsl:template>

	<xsl:template match="tn-part[@type='form']">
		<tn-part>
			<xsl:apply-templates select="@*"/>
			<i>
				<xsl:apply-templates/>
			</i>
		</tn-part>
	</xsl:template>
</xsl:stylesheet>