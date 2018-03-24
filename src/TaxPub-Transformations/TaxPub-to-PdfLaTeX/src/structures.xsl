<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:tp="http://www.plazi.org/taxpub"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:mml="http://www.w3.org/1998/Math/MathML">

	<xsl:include href="inline-format.xsl" />

	<!-- <xsl:text><![CDATA[]]></xsl:text> -->

	<xsl:template match="name">
		<xsl:if test="normalize-space(prefix)!=''">
			<xsl:apply-templates select="prefix" />
			<xsl:text> </xsl:text>
		</xsl:if>

		<xsl:if test="normalize-space(given-names)!=''">
			<xsl:apply-templates select="given-names" />
			<xsl:text> </xsl:text>
		</xsl:if>

		<xsl:apply-templates select="surname" />

		<xsl:if test="normalize-space(suffix)!=''">
			<xsl:text> </xsl:text>
			<xsl:apply-templates select="suffix" />
		</xsl:if>

	</xsl:template>

	<xsl:template match="email">
		<xsl:text><![CDATA[\href{mailto:]]></xsl:text>
		<xsl:value-of select="normalize-space(.)" />
		<xsl:text><![CDATA[}{]]></xsl:text>
		<xsl:apply-templates />
		<xsl:text><![CDATA[}]]></xsl:text>
	</xsl:template>

	<xsl:template match="p">
		<xsl:text><![CDATA[
\par ]]></xsl:text>
		<xsl:apply-templates />
		<xsl:text><![CDATA[

]]></xsl:text>
	</xsl:template>


	<xsl:template match="abstract | trans-abstract">
		<xsl:text><![CDATA[
\section*{]]></xsl:text>
		<xsl:apply-templates select="label" />
		<xsl:text><![CDATA[}]]></xsl:text>
		<xsl:apply-templates select="node()[name()!='label']" />
	</xsl:template>

	<!-- First-level sections -->
	<xsl:template match="sec[name(..)!='sec' and name(..)!='tp:treatment-sec']">
		<xsl:text><![CDATA[
\section*{]]></xsl:text>
		<xsl:apply-templates select="title" />
		<xsl:text><![CDATA[}]]></xsl:text>
		<xsl:apply-templates select="node()[name()!='label'][name()!='title']" />
	</xsl:template>

	<!-- Second-level sections -->
	<xsl:template match="sec[name(..)='sec' and name(..)!='tp:treatment-sec']">
		<xsl:text><![CDATA[
\subsection*{]]></xsl:text>
		<xsl:apply-templates select="title" />
		<xsl:text><![CDATA[}]]></xsl:text>
		<xsl:apply-templates select="node()[name()!='label'][name()!='title']" />
	</xsl:template>

	<!-- Taxon treatments -->
	<xsl:template match="tp:treatment-meta" />

	<xsl:template match="tp:taxon-treatment">
		<xsl:apply-templates />
	</xsl:template>

	<xsl:template match="tp:taxon-name/object-id" />

	<xsl:template match="tp:taxon-name">
		<xsl:value-of select="normalize-space(.)" />
	</xsl:template>

	<xsl:template match="tp:nomenclature">
		<xsl:text><![CDATA[
\subsection*{]]></xsl:text>
		<xsl:if test="normalize-space(label)!=''">
			<xsl:apply-templates select="label" />
			<xsl:text> </xsl:text>
		</xsl:if>
		<xsl:apply-templates select="tp:taxon-name" />
		<xsl:if test="normalize-space(tp:taxon-authority)!=''">
			<xsl:text> </xsl:text>
			<xsl:apply-templates select="tp:taxon-authority" />
			<xsl:if test="normalize-space(tp:taxon-status)!=''">
				<xsl:text>,</xsl:text>
			</xsl:if>
		</xsl:if>
		<xsl:if test="normalize-space(tp:taxon-status)!=''">
			<xsl:text> </xsl:text>
			<xsl:apply-templates select="tp:taxon-status" />
		</xsl:if>
		<xsl:text><![CDATA[}]]></xsl:text>

		<xsl:for-each select="tp:taxon-name/object-id">
			<xsl:text><![CDATA[

]]></xsl:text>
			<xsl:call-template name="format-text">
				<xsl:with-param name="string">
					<xsl:value-of select="normalize-space(.)" />
				</xsl:with-param>
			</xsl:call-template>
			<xsl:text><![CDATA[

]]></xsl:text>
		</xsl:for-each>

		<xsl:for-each select="xref-group | xref">
			<xsl:text><![CDATA[

]]></xsl:text>
			<xsl:apply-templates />
			<xsl:text><![CDATA[

]]></xsl:text>
		</xsl:for-each>
	</xsl:template>

	<xsl:template match="tp:treatment-sec">
		<xsl:text><![CDATA[
\paragraph{]]></xsl:text>
		<xsl:apply-templates select="title" />
		<xsl:text><![CDATA[} ]]></xsl:text>
		<xsl:apply-templates select="*[name()!='title'][position()=1]/node()" />
		<xsl:text><![CDATA[

]]></xsl:text>
		<xsl:apply-templates select="*[name()!='title'][position() &gt; 1]" />
	</xsl:template>

	<xsl:template match="tp:treatment-sec/sec">
		<xsl:text><![CDATA[
\subparagraph{]]></xsl:text>
		<xsl:apply-templates select="title" />
		<xsl:text><![CDATA[} ]]></xsl:text>
		<xsl:apply-templates select="*[name()!='title'][position()=1]/node()" />
		<xsl:text><![CDATA[

]]></xsl:text>
		<xsl:apply-templates select="*[name()!='title'][position() &gt; 1]" />
	</xsl:template>

	<!-- Floats -->
	<xsl:template match="fig">
		<xsl:text><![CDATA[
\begin{figure}[ht]
\caption*{\textbf{]]></xsl:text>
		<xsl:apply-templates select="label" />
		<xsl:text><![CDATA[} ]]></xsl:text>
		<xsl:apply-templates select="caption/*[position()=1]/node()" />
		<xsl:text><![CDATA[

]]></xsl:text>
		<xsl:apply-templates select="caption/*[position() &gt; 1]" />
		<xsl:text><![CDATA[}
\label{]]></xsl:text>
		<xsl:value-of select="@id" />
		<xsl:text><![CDATA[}
\end{figure}

]]></xsl:text>
	</xsl:template>


	<!-- References -->
	<xsl:template match="ref">
		<xsl:text><![CDATA[
\bibitem{]]></xsl:text>
		<xsl:value-of select="@id" />
		<xsl:text><![CDATA[}
]]></xsl:text>
		<xsl:text><![CDATA[\label{]]></xsl:text>
		<xsl:value-of select="@id" />
		<xsl:text><![CDATA[}]]></xsl:text>
		<xsl:apply-templates />
		<xsl:text><![CDATA[

]]></xsl:text>
	</xsl:template>


</xsl:stylesheet>