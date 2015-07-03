<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs">

	<xsl:output method="xml" encoding="UTF-8" indent="yes" />
	<xsl:preserve-space elements="*"/>

	<xsl:key use="name()" name="xkey" match="family_header"/>

	<xsl:template match="/root">
		<article article-type="research-article">
			<body>
				<xsl:apply-templates/>
			</body>
		</article>
	</xsl:template>

	<xsl:template match="@*|*">
		<xsl:copy>
			<xsl:apply-templates select="@*|*"/>
		</xsl:copy>
	</xsl:template>
	
	<xsl:template match="small">
		<sc>
			<xsl:apply-templates/>
		</sc>
	</xsl:template>
	
	<xsl:template match="em">
		<italic>
			<xsl:apply-templates/>
		</italic>
	</xsl:template>

	<xsl:template match="page">
		<xsl:comment>
			<xsl:text> Page #</xsl:text>
			<xsl:value-of select="."/>
			<xsl:text>: url=</xsl:text>
			<xsl:value-of select="@url"/>
			<xsl:text> </xsl:text>
		</xsl:comment>
		<div>
			<a target="_blank" href="{@url}"><xsl:text>Page </xsl:text><xsl:value-of select="."/></a>
		</div>
	</xsl:template>
	
	<xsl:template match="rule">
		<xsl:comment><xsl:text> Rule </xsl:text></xsl:comment>
	</xsl:template>

	<xsl:template match="sec[@type='family']">
		<sec sec-type="{normalize-space(family_header)}">
			<xsl:apply-templates/>
		</sec>
	</xsl:template>

	<xsl:template match="sec[@type='genus']">
		<sec sec-type="{normalize-space(genus_header)}">
			<xsl:apply-templates/>
		</sec>
	</xsl:template>

	<xsl:template match="sec">
		<sec>
			<xsl:attribute name="sec-type">
				<xsl:choose>
					<xsl:when test="normalize-space(header1)">
						<xsl:value-of select="normalize-space(header1)"/>
					</xsl:when>
					<xsl:when test="normalize-space(header2)">
						<xsl:value-of select="normalize-space(header2)"/>
					</xsl:when>
				</xsl:choose>
			</xsl:attribute>
			<xsl:apply-templates/>
		</sec>
	</xsl:template>

	<xsl:template match="header1 | header2">
		<title><xsl:apply-templates/></title>
	</xsl:template>

	<xsl:template match="family_header">
		<title><xsl:apply-templates/></title>
	</xsl:template>

	<xsl:template match="genus_header">
		<title><xsl:apply-templates/></title>
	</xsl:template>

	<xsl:template match="genus_header/gnum">
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template match="genus_header/g">
		<tp:taxon-name>
			<tp:taxon-name-part taxon-name-part-type="genus">
				<xsl:apply-templates/>
			</tp:taxon-name-part>
		</tp:taxon-name>
	</xsl:template>

	<xsl:template match="treatment">
		<tp:taxon-treatment>
			<xsl:attribute name="id">
				<xsl:value-of select="generate-id()"/>
			</xsl:attribute>
			<xsl:attribute name="xid">
				<xsl:value-of select="position()"/>
			</xsl:attribute>
			<xsl:if test="count(@*[normalize-space(.)!=''])!=0">
				<tp:treatment-meta>
					<kwd-group>
						<xsl:for-each select="@*">
							<kwd><xsl:value-of select="name()"/><xsl:text>: </xsl:text><xsl:value-of select="."/></kwd>
						</xsl:for-each>
					</kwd-group>
				</tp:treatment-meta>
			</xsl:if>
			<xsl:apply-templates select="treatment_header"/>
			<xsl:if test="count(citations)+count(location)+count(person)+count(latinname)+count(vernacular)!=0">
				<tp:treatment-sec sec-type="citations">
					<p><xsl:apply-templates select="citations | location | person | latinname | vernacular"/></p>
				</tp:treatment-sec>
			</xsl:if>
			<xsl:if test="count(notes)!=0">
				<tp:treatment-sec sec-type="notes">
					<xsl:apply-templates select="notes"/>
				</tp:treatment-sec>
			</xsl:if>
		</tp:taxon-treatment>
	</xsl:template>

	<xsl:template match="treatment_header">
		<tp:nomenclature>
			<xsl:apply-templates select="num"/>
			<xsl:apply-templates select="latinname"/>
			<xsl:if test="normalize-space(../@lsid)!=''">
				<object-id content-type="lsid" xlink:type="simple">
					<xsl:value-of select="../@lsid"/>
				</object-id>
			</xsl:if>
			<xsl:apply-templates select="vernacular"/>
		</tp:nomenclature>
	</xsl:template>
	
	<xsl:template match="treatment_header/num">
		<label><xsl:value-of select="."/></label>
	</xsl:template>

	<xsl:template match="treatment_header/vernacular">
		<object-id content-type="vernacular" xlink:type="simple">
			<xsl:apply-templates select="@*"/>
			<xsl:apply-templates/>
		</object-id>
	</xsl:template>

	<xsl:template match="latinname">
		<tp:taxon-name>
			<xsl:apply-templates mode="parse-taxon-names"/>
		</tp:taxon-name>
	</xsl:template>
	
	<xsl:template mode="parse-taxon-names" match="@*|node()[name()!='']">
		<tp:taxon-name-part taxon-name-part-type="{name()}">
			<xsl:apply-templates mode="parse-taxon-names-attributes" select="@*"/>
			<xsl:apply-templates/>
		</tp:taxon-name-part>
	</xsl:template>
	
	<xsl:template mode="parse-taxon-names" match="node()[name()='']">
		<xsl:value-of select="."/>
	</xsl:template>

	<xsl:template mode="parse-taxon-names-attributes" match="@*|*">
		<xsl:copy>
			<xsl:apply-templates mode="parse-taxon-names-attributes" select="@*"/>
			<xsl:apply-templates mode="parse-taxon-names-attributes"/>
		</xsl:copy>
	</xsl:template>
	
	<xsl:template mode="parse-taxon-names-attributes" match="@fullname">
		<xsl:attribute name="full-name"><xsl:value-of select="."/></xsl:attribute>
	</xsl:template>

	<xsl:template match="person">
		<xsl:choose>
			<xsl:when test="count(@*[normalize-space(.)!=''])!=0">
				<ballon_wrapper type="{name()}">
					<content>
						<xsl:apply-templates/>
					</content>
					<ballon_content>
						<xsl:if test="normalize-space(@name)!=''">
							<span class="person-name"><xsl:text>Name: </xsl:text><xsl:value-of select="@name"/></span><br/>
						</xsl:if>
						<xsl:if test="normalize-space(@born)!=''">
							<span class="peson-born"><xsl:text>Born: </xsl:text><xsl:value-of select="@born"/></span><br/>
						</xsl:if>
						<xsl:if test="normalize-space(@died)!=''">
							<span class="peson-died"><xsl:text>Died: </xsl:text><xsl:value-of select="@died"/></span><br/>
						</xsl:if>
					</ballon_content>
				</ballon_wrapper>
			</xsl:when>
			<xsl:otherwise>
				<xsl:apply-templates/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template match="location">
		<xsl:choose>
			<xsl:when test="count(@*[normalize-space(.)!=''])!=0">
				<ballon_wrapper type="{name()}">
					<content>
						<xsl:choose>
							<xsl:when test="normalize-space(@decimalLatitude)!='' and normalize-space(@decimalLongitude)!=''">
								<locality-coordinates latitude="{@decimalLatitude}" longitude="{@decimalLongitude}">
									<xsl:apply-templates/>
								</locality-coordinates>
							</xsl:when>
							<xsl:otherwise>
								<xsl:apply-templates/>
							</xsl:otherwise>
						</xsl:choose>
					</content>
					<ballon_content>
						<xsl:if test="normalize-space(@localGridReference)!=''">
							<span class="location-localGridReference"><xsl:text>Local Grid Reference: </xsl:text><xsl:value-of select="@localGridReference"/></span><br/>
						</xsl:if>
						<xsl:if test="normalize-space(@countryCode)!=''">
							<span class="location-countryCode"><xsl:text>Country Code: </xsl:text><xsl:value-of select="@countryCode"/></span><br/>
						</xsl:if>
						<xsl:if test="normalize-space(@higherGeography)!=''">
							<span class="location-higherGeography"><xsl:text>Higher Geography: </xsl:text><xsl:value-of select="@higherGeography"/></span><br/>
						</xsl:if>
						<xsl:if test="normalize-space(@decimalLatitude)!=''">
							<span class="location-decimalLatitude"><xsl:text>Decimal Latitude: </xsl:text><xsl:value-of select="@decimalLatitude"/></span><br/>
						</xsl:if>
						<xsl:if test="normalize-space(@decimalLongitude)!=''">
							<span class="location-decimalLongitude"><xsl:text>Decimal Longitude: </xsl:text><xsl:value-of select="@decimalLongitude"/></span><br/>
						</xsl:if>
						<xsl:if test="normalize-space(@geodeticDatum)!=''">
							<span class="location-geodeticDatum"><xsl:text>Geodetic Datum: </xsl:text><xsl:value-of select="@geodeticDatum"/></span><br/>
						</xsl:if>
						<xsl:if test="normalize-space(@coordinateUncertaintyInMeters)!=''">
							<span class="location-coordinateUncertaintyInMeters"><xsl:text>Coordinate Uncertainty In Meters: </xsl:text><xsl:value-of select="@coordinateUncertaintyInMeters"/></span><br/>
						</xsl:if>
						<xsl:if test="normalize-space(@date)!=''">
							<span class="location-date"><xsl:text>Date: </xsl:text><xsl:value-of select="@date"/></span><br/>
						</xsl:if>
					</ballon_content>
				</ballon_wrapper>
			</xsl:when>
			<xsl:otherwise>
				<xsl:apply-templates/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template match="citation">
		<xsl:variable name="author" select="normalize-space(@author)!=''"/>
		<xsl:variable name="year" select="normalize-space(@year)!=''"/>
		<xsl:variable name="title" select="normalize-space(@title)!=''"/>
		<xsl:variable name="journal" select="normalize-space(@journal)!=''"/>
		<xsl:variable name="volume" select="normalize-space(@volume)!=''"/>
		<xsl:variable name="page" select="normalize-space(@page)!=''"/>
		<xsl:variable name="figure" select="normalize-space(@figure)!=''"/>
		<xsl:variable name="table" select="normalize-space(@table)!=''"/>
		<xsl:variable name="url" select="normalize-space(@url)!=''"/>
		<xsl:choose>
			<xsl:when test="count(@*[normalize-space(.)!=''])!=0">
				<ballon_wrapper type="{name()}">
					<content>
						<xsl:choose>
							<xsl:when test="$url">
								<a target="_blank" href="{@url}"><xsl:apply-templates/></a>
							</xsl:when>
							<xsl:otherwise><xsl:apply-templates/></xsl:otherwise>
						</xsl:choose>
					</content>
					<ballon_content>
						<!-- 
							Author
						 -->
						<xsl:if test="$author">
							<span class="citation-author"><xsl:value-of select="@author"/></span>
						</xsl:if>
						<!-- 
							Year
						 -->
						<xsl:choose>
							<xsl:when test="$year">
								<xsl:choose>
									<xsl:when test="$author">
										<xsl:text> </xsl:text>
										<span class="citation-year"><xsl:text>(</xsl:text><xsl:value-of select="@year"/><xsl:text>)</xsl:text></span>
									</xsl:when>
									<xsl:otherwise>
										<span class="citation-year"><xsl:value-of select="@year"/></span>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:when>
							<xsl:when test="count(@*[normalize-space(.)!=''][name()!='author'])=0"></xsl:when>
							<xsl:otherwise>
								<xsl:choose>
									<xsl:when test="$author">
										<xsl:text> – </xsl:text>
									</xsl:when>
								</xsl:choose>
							</xsl:otherwise>
						</xsl:choose>
						<!-- 
							Title
						 -->
						<xsl:if test="$title">
							<xsl:if test="$year"><xsl:text> </xsl:text></xsl:if>
							<span class="citation-title"><xsl:value-of select="@title"/></span>
							<xsl:text>.</xsl:text>
						</xsl:if>
						<!-- 
							Journal
						 -->
						<xsl:if test="$journal">
							<xsl:choose>
								<xsl:when test="$title">
									<xsl:text> </xsl:text>
								</xsl:when>
								<xsl:when test="$year">
									<xsl:text> </xsl:text>
								</xsl:when>
							</xsl:choose>
							<span class="citation-journal"><xsl:value-of select="@journal"/></span>
						</xsl:if>
						<!-- 
							Volume
						 -->
						<xsl:if test="$volume">
							<xsl:choose>
								<xsl:when test="$journal">
									<xsl:text> </xsl:text>
								</xsl:when>
								<xsl:when test="$title">
									<xsl:text> </xsl:text>
								</xsl:when>
								<xsl:when test="$year">
									<xsl:text> </xsl:text>
								</xsl:when>
							</xsl:choose>
							<span class="citation-volume"><xsl:value-of select="@volume"/></span>
							<xsl:text>.</xsl:text>
						</xsl:if>
						<!-- 
							Page
						 -->
						<xsl:if test="$page">
							<xsl:choose>
								<xsl:when test="$volume">
									<xsl:text> </xsl:text>
								</xsl:when>
								<xsl:when test="$journal">
									<xsl:text> </xsl:text>
								</xsl:when>
								<xsl:when test="$title">
									<xsl:text> </xsl:text>
								</xsl:when>
								<xsl:when test="$year">
									<xsl:text> </xsl:text>
								</xsl:when>
							</xsl:choose>
							<span class="citation-page"><xsl:text>p. </xsl:text><xsl:value-of select="@page"/></span>
						</xsl:if>
						<!-- 
							Figure
						 -->
						<xsl:if test="$figure">
							<xsl:choose>
								<xsl:when test="$page">
									<xsl:text>, </xsl:text>
								</xsl:when>
								<xsl:when test="$volume">
									<xsl:text> </xsl:text>
								</xsl:when>
								<xsl:when test="$journal">
									<xsl:text> </xsl:text>
								</xsl:when>
								<xsl:when test="$title">
									<xsl:text> </xsl:text>
								</xsl:when>
								<xsl:when test="$year">
									<xsl:text> </xsl:text>
								</xsl:when>
							</xsl:choose>
							<span class="citation-figure"><xsl:text>fig. </xsl:text><xsl:value-of select="@figure"/></span>
						</xsl:if>
						<!-- 
							Table
						 -->
						<xsl:if test="$table">
							<xsl:choose>
								<xsl:when test="$figure">
									<xsl:text>, </xsl:text>
								</xsl:when>
								<xsl:when test="$page">
									<xsl:text>, </xsl:text>
								</xsl:when>
								<xsl:when test="$volume">
									<xsl:text> </xsl:text>
								</xsl:when>
								<xsl:when test="$journal">
									<xsl:text> </xsl:text>
								</xsl:when>
								<xsl:when test="$title">
									<xsl:text> </xsl:text>
								</xsl:when>
								<xsl:when test="$year">
									<xsl:text> </xsl:text>
								</xsl:when>
							</xsl:choose>
							<span class="citation-table"><xsl:text>tabl. </xsl:text><xsl:value-of select="@table"/></span>
						</xsl:if>
						<!--  -->
						<xsl:choose>
							<xsl:when test="$table">
								<xsl:text>.</xsl:text>
							</xsl:when>
							<xsl:when test="$figure">
								<xsl:text>.</xsl:text>
							</xsl:when>
							<xsl:when test="$page">
								<xsl:text>.</xsl:text>
							</xsl:when>
						</xsl:choose>
						<xsl:if test="$url">
							<xsl:text> url: </xsl:text>
							<a target="_blank" href="{@url}"><xsl:value-of select="@url"/></a>
						</xsl:if>
					</ballon_content>
				</ballon_wrapper>
			</xsl:when>
			<xsl:otherwise>
				<xsl:apply-templates/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template match="citations">
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template match="notes">
		<xsl:apply-templates/>
	</xsl:template>
</xsl:stylesheet>
