<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	
	<xsl:output indent="yes" encoding="utf-8" method="text" omit-xml-declaration="yes"/>
	
	<xsl:preserve-space elements="*"/>
	
	<xsl:include href="latex-preambule.xsl"/>
	<!-- <xsl:include href="inline-format.xsl"/> -->
	<xsl:include href="structures.xsl"/>
	
	
	<xsl:template name="make-title">
<xsl:text><![CDATA[% Title must be 250 characters or less.
% Please capitalize all terms in the title except conjunctions, prepositions, and articles.
{\Large
\textbf\newline
{]]></xsl:text>
<xsl:apply-templates select="/article/front/article-meta/title-group/article-title"/>
<xsl:text><![CDATA[}
\newline}]]></xsl:text>
	</xsl:template>
	
	<xsl:template name="make-authors">
<xsl:text><![CDATA[% Insert author names, affiliations and corresponding author email (do not include titles, positions, or degrees).
\\
]]></xsl:text>

	<!-- Author's names -->
	<xsl:for-each select="/article/front/article-meta/contrib-group/contrib">
		<xsl:if test="position()!=1">
			<xsl:text>,
</xsl:text>
		</xsl:if>
		<xsl:apply-templates select="name"/>
		<xsl:text><![CDATA[\textsuperscript{]]></xsl:text>
		<xsl:for-each select="xref">
			<xsl:if test="position()!=1">
				<xsl:text>, </xsl:text>
			</xsl:if>
			<xsl:apply-templates/>
		</xsl:for-each>
		<xsl:text><![CDATA[}]]></xsl:text>
	</xsl:for-each>
	
<xsl:text><![CDATA[
\\
\bigskip
]]></xsl:text>
	
	<!-- Affiliations -->
	<xsl:for-each select="/article/front/article-meta/aff">
		<xsl:if test="position()!=1">
<xsl:text>
\\
</xsl:text>
		</xsl:if>
		<xsl:text><![CDATA[\bf{]]></xsl:text>
		<xsl:apply-templates select="label"/>
		<xsl:text><![CDATA[} ]]></xsl:text>
		<xsl:apply-templates select="node()[name()!='label']"/>
	</xsl:for-each>
	
<xsl:text><![CDATA[
\\
\bigskip
]]></xsl:text>

<xsl:text><![CDATA[
% Insert additional author notes using the symbols described below. Insert symbol callouts after author names as necessary.
% 
% Remove or comment out the author notes below if they aren't used.
%
% Primary Equal Contribution Note
% \Yinyang These authors contributed equally to this work.

% Additional Equal Contribution Note
% Also use this double-dagger symbol for special authorship notes, such as senior authorship.
% \ddag These authors also contributed equally to this work.

% Current address notes
% \textcurrency a Insert current address of first author with an address update
% \textcurrency b Insert current address of second author with an address update
% \textcurrency c Insert current address of third author with an address update

% Deceased author note
% \dag Deceased

% Group/Consortium Author Note
% \textpilcrow Membership list can be found in the Acknowledgments section.

% Use the asterisk to denote corresponding authorship and provide email address in note below.
% * CorrespondingAuthor@institute.edu
]]></xsl:text>

<xsl:text>
</xsl:text>

	<xsl:for-each select="/article/front/article-meta/author-notes/fn/p">
		<xsl:if test="position()!=1">
<xsl:text>

</xsl:text>
		</xsl:if>
		<xsl:apply-templates/>
	</xsl:for-each>
<xsl:text>
</xsl:text>

	</xsl:template>
	
	<xsl:template name="make-abstract">
<xsl:text><![CDATA[
% Please keep the abstract below 300 words
]]></xsl:text>
	<xsl:apply-templates select="/article/front/article-meta/abstract | /article/front/article-meta/trans-abstract"/>
	</xsl:template>
	
	
	
	<xsl:template match="/">
<xsl:value-of select="$latex-preambule"/>
<xsl:text><![CDATA[
\begin{document}
\vspace*{0.35in}

\begin{flushleft}
]]></xsl:text>

<xsl:call-template name="make-title"/>

<xsl:call-template name="make-authors"/>

<xsl:text><![CDATA[
\end{flushleft}


]]></xsl:text>

<xsl:call-template name="make-abstract"/>

<xsl:apply-templates select="/article/body"/>

<xsl:text><![CDATA[

\linenumbers



\begin{table}[!ht]
\begin{adjustwidth}{-2.25in}{0in} % Comment out/remove adjustwidth environment if table fits in text column.
\caption{
{\bf Table caption Nulla mi mi, venenatis sed ipsum varius, volutpat euismod diam.}}
\begin{tabular}{|l|l|l|l|l|l|l|l|}
\hline
\multicolumn{4}{|l|}{\bf Heading1} & \multicolumn{4}{|l|}{\bf Heading2}\\ \hline
$cell1 row1$ & cell2 row 1 & cell3 row 1 & cell4 row 1 & cell5 row 1 & cell6 row 1 & cell7 row 1 & cell8 row 1\\ \hline
$cell1 row2$ & cell2 row 2 & cell3 row 2 & cell4 row 2 & cell5 row 2 & cell6 row 2 & cell7 row 2 & cell8 row 2\\ \hline
$cell1 row3$ & cell2 row 3 & cell3 row 3 & cell4 row 3 & cell5 row 3 & cell6 row 3 & cell7 row 3 & cell8 row 3\\ \hline
\end{tabular}
\begin{flushleft} Table notes Phasellus venenatis, tortor nec vestibulum mattis, massa tortor interdum felis, nec pellentesque metus tortor nec nisl. Ut ornare mauris tellus, vel dapibus arcu suscipit sed.
\end{flushleft}
\label{table1}
\end{adjustwidth}
\end{table}


\section*{Supporting Information}

% Include only the SI item label in the subsection heading. Use the \nameref{label} command to cite SI items in the text.
\subsection*{S1 Video}
\label{S1_Video}
{\bf Bold the first sentence.}  Maecenas convallis mauris sit amet sem ultrices gravida. Etiam eget sapien nibh. Sed ac ipsum eget enim egestas ullamcorper nec euismod ligula. Curabitur fringilla pulvinar lectus consectetur pellentesque.

\subsection*{S1 Text}
\label{S1_Text}
{\bf Lorem Ipsum.} Maecenas convallis mauris sit amet sem ultrices gravida. Etiam eget sapien nibh. Sed ac ipsum eget enim egestas ullamcorper nec euismod ligula. Curabitur fringilla pulvinar lectus consectetur pellentesque.

\subsection*{S1 Fig}
\label{S1_Fig}
{\bf Lorem Ipsum.} Maecenas convallis mauris sit amet sem ultrices gravida. Etiam eget sapien nibh. Sed ac ipsum eget enim egestas ullamcorper nec euismod ligula. Curabitur fringilla pulvinar lectus consectetur pellentesque.

\subsection*{S2 Fig}
\label{S2_Fig}
{\bf Lorem Ipsum.} Maecenas convallis mauris sit amet sem ultrices gravida. Etiam eget sapien nibh. Sed ac ipsum eget enim egestas ullamcorper nec euismod ligula. Curabitur fringilla pulvinar lectus consectetur pellentesque.

\subsection*{S1 Table}
\label{S1_Table}
{\bf Lorem Ipsum.} Maecenas convallis mauris sit amet sem ultrices gravida. Etiam eget sapien nibh. Sed ac ipsum eget enim egestas ullamcorper nec euismod ligula. Curabitur fringilla pulvinar lectus consectetur pellentesque.

% \section*{Acknowledgments}
\section*{]]></xsl:text>
<xsl:apply-templates select="/article/back/ack/title"/>
<xsl:text><![CDATA[}]]></xsl:text>
<xsl:apply-templates select="/article/back/ack/node()[name()!='title']"/>
<xsl:text><![CDATA[

\nolinenumbers

% \section*{References}
\section*{]]></xsl:text>
<xsl:apply-templates select="/article/back/ref-list/title"/>
<xsl:text><![CDATA[}
\begin{thebibliography}{0}

]]></xsl:text>

<xsl:apply-templates select="/article/back/ref-list/ref"/>

<xsl:text><![CDATA[

\end{thebibliography}

\end{document}]]>
</xsl:text>
	</xsl:template>

	<xsl:template match="@*" />
	<xsl:template match="comment()" />

	<xsl:template match="*">
		<xsl:apply-templates />
	</xsl:template>

	<xsl:template match="text()">
		<xsl:call-template name="format-text">
			<xsl:with-param name="string"><xsl:value-of select="string(.)"/></xsl:with-param>
		</xsl:call-template>
	</xsl:template>
</xsl:stylesheet>