<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output encoding="UTF-8" method="text"/>
	<xsl:preserve-space elements="*"/>
<xsl:template match="/"><![CDATA[% !TEX TS-program = pdflatex
% !TEX encoding = UTF-8 Unicode

% This is a simple template for a LaTeX document using the "article" class.
% See "book", "report", "letter" for other types of document.

\documentclass[11pt]{article} % use larger type; default would be 10pt

\usepackage[utf8]{inputenc} % set input encoding (not needed with XeLaTeX)

%%% PAGE DIMENSIONS
\usepackage{geometry} % to change the page dimensions
\geometry{b5paper} % or letterpaper (US) or a5paper or....
% \geometry{margin=2in} % for example, change the margins to 2 inches all round
% \geometry{landscape} % set up the page for landscape
%   read geometry.pdf for detailed page layout information

\usepackage{graphicx} % support the \includegraphics command and options

% \usepackage[parfill]{parskip} % Activate to begin paragraphs with an empty line rather than an indent

%%% PACKAGES
\usepackage{booktabs} % for much better looking tables
\usepackage{array} % for better arrays (eg matrices) in maths
\usepackage{paralist} % very flexible & customisable lists (eg. enumerate/itemize, etc.)
\usepackage{verbatim} % adds environment for commenting out blocks of text & for better verbatim
\usepackage{subfig} % make it possible to include more than one captioned figure/table in a single float
% These packages are all incorporated in the memoir class to one degree or another...

\usepackage[breaklinks]{hyperref}
\hypersetup{
     bookmarks=true,      % show bookmarks bar?
     unicode=false,       % non-Latin characters in Acrobat’s bookmarks
     pdftoolbar=true,     % show Acrobat’s toolbar?
     pdfmenubar=true,     % show Acrobat’s menu?
     pdffitwindow=false,  % window fit to page when opened
     pdfstartview={FitH}, % fits the width of the page to the window
     pdftitle={]]><xsl:value-of select="/document/pdf/meta/title"/><![CDATA[}, % title
     pdfauthor=]]><xsl:call-template name="pdfauthors"/><![CDATA[, % author(s)
     pdfsubject=]]><xsl:call-template name="pdfsubjects"/><![CDATA[, % subject(s) of the document
     pdfcreator={PdfLaTeX}, % creator of the document
     pdfproducer={PP}, % producer of the document
     pdfkeywords=]]><xsl:call-template name="pdfkeywords"/><![CDATA[, % list of keywords
     pdfnewwindow=true,   % links in new window
     colorlinks=true,     % false: boxed links; true: colored links
     linkcolor=red,       % color of internal links (change box color with linkbordercolor)
     citecolor=green,     % color of links to bibliography
     filecolor=magenta,   % color of file links
     urlcolor=cyan        % color of external links
}

\usepackage{amssymb}
\usepackage{amsmath}

\usepackage{seqsplit}



%%% HEADERS & FOOTERS
\usepackage{fancyhdr} % This should be set AFTER setting up the page geometry
\pagestyle{fancy} % options: empty , plain , fancy
\renewcommand{\headrulewidth}{0pt} % customise the layout...
\lhead{}\chead{}\rhead{}
\lfoot{}\cfoot{\thepage}\rfoot{}

%%% SECTION TITLE APPEARANCE
\usepackage{sectsty}
\allsectionsfont{\sffamily\mdseries\upshape} % (See the fntguide.pdf for font help)
% (This matches ConTeXt defaults)

%%% ToC (table of contents) APPEARANCE
\usepackage[nottoc,notlof,notlot]{tocbibind} % Put the bibliography in the ToC
\usepackage[titles,subfigure]{tocloft} % Alter the style of the Table of Contents
\renewcommand{\cftsecfont}{\rmfamily\mdseries\upshape}
\renewcommand{\cftsecpagefont}{\rmfamily\mdseries\upshape} % No bold!

%%% END Article customizations

%%% The "real" document content comes below...
]]>
<!-- Article title -->
<xsl:text>\title{</xsl:text>
<xsl:for-each select="/document/objects/article_metadata/title_and_authors/fields/title/value/p">
	<xsl:apply-templates/>
</xsl:for-each>
<xsl:text>}</xsl:text>
<xsl:text>&#xa;</xsl:text>
<xsl:text>&#xa;</xsl:text>
<!-- Authors -->
<xsl:text>\author{The Author}</xsl:text>
<xsl:text>&#xa;</xsl:text>
<xsl:text>&#xa;</xsl:text>
<!-- Dates -->
<xsl:text>%\date{}</xsl:text>
<![CDATA[
\begin{document}
\maketitle
]]>

<!-- Introduction -->
<xsl:apply-templates mode="section" select="/document/objects/introduction"/>

<!-- Materials and methods -->
<xsl:apply-templates mode="section" select="/document/objects/materials_and_methods"/>

<!-- Data resources -->
<xsl:apply-templates mode="section" select="/document/objects/data_resources"/>

<!-- Results -->
<xsl:apply-templates mode="section" select="/document/objects/results"/>

<!-- Discussion -->
<xsl:apply-templates mode="section" select="/document/objects/discussion"/>

<!-- Acknowledgments -->
<xsl:apply-templates mode="section" select="/document/objects/acknowledgements"/>

<!-- Author contributions -->
<xsl:apply-templates mode="section" select="/document/objects/author_contributions"/>
<![CDATA[
\end{document}
]]>
</xsl:template>

<xsl:template match="@*|node()">
	<xsl:copy>
		<xsl:apply-templates select="@*|node()"/>
	</xsl:copy>
</xsl:template>

<xsl:template match="p">
<!-- <xsl:text>\par </xsl:text> -->
<xsl:apply-templates />
<!-- <xsl:text>\\</xsl:text> -->
<!-- <xsl:text>&#xa;</xsl:text> -->
<xsl:text>&#xa;</xsl:text>
</xsl:template>

<xsl:template mode="section" match="*">
<xsl:if test="normalize-space(.)!=''">
<xsl:text>\section{</xsl:text>
<xsl:value-of select="fields/*/@field_name"/>
<xsl:text>}</xsl:text>
<xsl:text>&#xa;</xsl:text>
<xsl:apply-templates select="fields/*/value"/>
<xsl:text>&#xa;&#xa;</xsl:text>
</xsl:if>
</xsl:template>

<xsl:template name="pdfkeywords">
<xsl:for-each select="/document/pdf/meta/keywords/kwd">
<xsl:text>{</xsl:text><xsl:value-of select="normalize-space(.)"/><xsl:text>}</xsl:text>
<xsl:if test="position()!=last()">
<xsl:text> </xsl:text>
</xsl:if>
</xsl:for-each>
</xsl:template>

<xsl:template name="pdfauthors">
<xsl:for-each select="/document/pdf/meta/authors/author">
<xsl:text>{</xsl:text><xsl:value-of select="full-name"/><xsl:text>}</xsl:text>
<xsl:if test="position()!=last()">
<xsl:text> </xsl:text>
</xsl:if>
</xsl:for-each>
</xsl:template>

<xsl:template name="pdfsubjects">
<xsl:for-each select="/document/pdf/meta/subjects/subject">
<xsl:text>{</xsl:text><xsl:value-of select="."/><xsl:text>}</xsl:text>
<xsl:if test="position()!=last()">
<xsl:text> </xsl:text>
</xsl:if>
</xsl:for-each>
</xsl:template>
</xsl:stylesheet>