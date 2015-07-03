<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs xsi mml xlink tp">

	<xsl:output method="xml" encoding="UTF-8" indent="yes" />
	<xsl:strip-space elements="taxon"/>

	<xsl:key name="taxa" match="tp:taxon-name" use="normalize-space(.)"/>

	<xsl:template match="/">
		<taxa>
			<xsl:for-each select="//tp:taxon-name[generate-id() = generate-id(key('taxa', .)[1])]">
				<xsl:sort />

				<xsl:variable name="genus" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='genus'])"/>
				<xsl:variable name="subgenus" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='subgenus'])"/>
				<xsl:variable name="species" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='species'])"/>
				<xsl:variable name="subspecies" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='subspecies'])"/>
				<xsl:variable name="fullName" select="normalize-space(.)"/>

				<taxon full-name="{$fullName}">

					<xsl:for-each select="tp:taxon-name-part[normalize-space(.)!='']">
						<xsl:attribute name="{@taxon-name-part-type}">
							<xsl:value-of select="normalize-space(.)"/>
						</xsl:attribute>
					</xsl:for-each>

					<xsl:if test="contains($fullName,'.')">
						<!-- <xsl:attribute name="short">true</xsl:attribute> -->
						<xsl:if test="contains($genus,'.')">
							<xsl:variable name="expandedGenus">
								<xsl:call-template name="expandGenus">
									<xsl:with-param name="genus" select="$genus"/>
									<xsl:with-param name="subgenus" select="$subgenus"/>
									<xsl:with-param name="species" select="$species"/>
									<xsl:with-param name="subspecies" select="$subspecies"/>
								</xsl:call-template>
							</xsl:variable>
							<xsl:if test="$expandedGenus!=''">
								<xsl:attribute name="expanded-genus">
									<xsl:value-of select="$expandedGenus"/>
								</xsl:attribute>
							</xsl:if>
							<xsl:variable name="expandedGenusNssp">
								<xsl:call-template name="expandGenusNoSubspecies">
									<xsl:with-param name="genus" select="$genus"/>
									<xsl:with-param name="subgenus" select="$subgenus"/>
									<xsl:with-param name="species" select="$species"/>
									<xsl:with-param name="subspecies" select="$subspecies"/>
								</xsl:call-template>
							</xsl:variable>
							<xsl:if test="$expandedGenusNssp!=''">
								<xsl:attribute name="expanded-genus-nssp">
									<xsl:value-of select="$expandedGenusNssp"/>
								</xsl:attribute>
							</xsl:if>
						</xsl:if>
						<xsl:if test="contains($subgenus,'.')">
							<xsl:variable name="expandedSubgenus">
								<xsl:call-template name="expandSubgenus">
									<xsl:with-param name="genus" select="$genus"/>
									<xsl:with-param name="subgenus" select="$subgenus"/>
									<xsl:with-param name="species" select="$species"/>
									<xsl:with-param name="subspecies" select="$subspecies"/>
								</xsl:call-template>
							</xsl:variable>
							<xsl:if test="$expandedSubgenus!=''">
								<xsl:attribute name="expanded-subgenus">
									<xsl:value-of select="$expandedSubgenus"/>
								</xsl:attribute>
							</xsl:if>
							<xsl:variable name="expandedSubgenusNssp">
								<xsl:call-template name="expandSubgenusNoSubspecies">
									<xsl:with-param name="genus" select="$genus"/>
									<xsl:with-param name="subgenus" select="$subgenus"/>
									<xsl:with-param name="species" select="$species"/>
									<xsl:with-param name="subspecies" select="$subspecies"/>
								</xsl:call-template>
							</xsl:variable>
							<xsl:if test="$expandedSubgenusNssp!=''">
								<xsl:attribute name="expanded-subgenus-nssp">
									<xsl:value-of select="$expandedSubgenusNssp"/>
								</xsl:attribute>
							</xsl:if>
						</xsl:if>
						<xsl:if test="contains($species,'.')">
							<xsl:variable name="expandedSpecies">
								<xsl:call-template name="expandSpecies">
									<xsl:with-param name="genus" select="$genus"/>
									<xsl:with-param name="subgenus" select="$subgenus"/>
									<xsl:with-param name="species" select="$species"/>
									<xsl:with-param name="subspecies" select="$subspecies"/>
								</xsl:call-template>
							</xsl:variable>
							<xsl:if test="$expandedSpecies!=''">
								<xsl:attribute name="expanded-species">
									<xsl:value-of select="$expandedSpecies"/>
								</xsl:attribute>
							</xsl:if>
							<xsl:variable name="expandedSpeciesNssp">
								<xsl:call-template name="expandSpeciesNoSubspecies">
									<xsl:with-param name="genus" select="$genus"/>
									<xsl:with-param name="subgenus" select="$subgenus"/>
									<xsl:with-param name="species" select="$species"/>
									<xsl:with-param name="subspecies" select="$subspecies"/>
								</xsl:call-template>
							</xsl:variable>
							<xsl:if test="$expandedSpeciesNssp!=''">
								<xsl:attribute name="expanded-species-nssp">
									<xsl:value-of select="$expandedSpeciesNssp"/>
								</xsl:attribute>
							</xsl:if>
						</xsl:if>
					</xsl:if>

					<xsl:for-each select="tp:taxon-name-part">
						<part type="{@taxon-name-part-type}">
							<xsl:value-of select="normalize-space(.)"/>
						</part>
					</xsl:for-each>

				</taxon>

			</xsl:for-each>
		</taxa>
	</xsl:template>
	
	<xsl:template name="expandGenus">
		<xsl:param name="genus"/>
		<xsl:param name="subgenus"/>
		<xsl:param name="species"/>
		<xsl:param name="subspecies"/>
		<xsl:variable name="genusPattern" select="substring-before($genus,'.')"/>
		<xsl:variable name="speciesPattern" select="substring-before($species,'.')"/>

		<xsl:if test="$genusPattern!=''">
			<xsl:for-each select="//tp:taxon-name[generate-id() = generate-id(key('taxa', .)[1])]">
				<xsl:variable name="lGenus" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='genus'])"/>
				<!-- <xsl:variable name="lSubgenus" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='subgenus'])"/> -->
				<xsl:variable name="lSpecies" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='species'])"/>
				<xsl:variable name="lSubspecies" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='subspecies'])"/>
				<xsl:variable name="lGenusMatch" select="not(contains($lGenus,'.')) and contains($lGenus,$genusPattern)"/>
	
				<xsl:if test="$subspecies=$lSubspecies">
					<xsl:choose>
						<xsl:when test="$speciesPattern!=''">
							<xsl:if test="contains($lSpecies,$speciesPattern) and $lGenusMatch">
								<xsl:value-of select="$lGenus"/>
								<xsl:text>;</xsl:text>
							</xsl:if>
						</xsl:when>
						<xsl:otherwise>
							<xsl:if test="($lSpecies=$species) and $lGenusMatch">
								<xsl:value-of select="$lGenus"/>
								<xsl:text>;</xsl:text>
							</xsl:if>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:if>
			</xsl:for-each>
		</xsl:if>
	</xsl:template>
	
	<xsl:template name="expandGenusNoSubspecies">
		<xsl:param name="genus"/>
		<xsl:param name="subgenus"/>
		<xsl:param name="species"/>
		<xsl:param name="subspecies"/>
		<xsl:variable name="genusPattern" select="substring-before($genus,'.')"/>
		<xsl:variable name="speciesPattern" select="substring-before($species,'.')"/>

		<xsl:if test="$genusPattern!=''">
			<xsl:for-each select="//tp:taxon-name[generate-id() = generate-id(key('taxa', .)[1])]">
				<xsl:variable name="lGenus" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='genus'])"/>
				<!-- <xsl:variable name="lSubgenus" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='subgenus'])"/> -->
				<xsl:variable name="lSpecies" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='species'])"/>
				<xsl:variable name="lSubspecies" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='subspecies'])"/>
				<xsl:variable name="lGenusMatch" select="not(contains($lGenus,'.')) and contains($lGenus,$genusPattern)"/>
	
				<xsl:choose>
					<xsl:when test="$speciesPattern!=''">
						<xsl:if test="contains($lSpecies,$speciesPattern) and $lGenusMatch">
							<xsl:value-of select="$lGenus"/>
							<xsl:text>;</xsl:text>
						</xsl:if>
					</xsl:when>
					<xsl:otherwise>
						<xsl:if test="($lSpecies=$species) and $lGenusMatch">
							<xsl:value-of select="$lGenus"/>
							<xsl:text>;</xsl:text>
						</xsl:if>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:for-each>
		</xsl:if>
	</xsl:template>

	<xsl:template name="expandSubgenus">
		<xsl:param name="genus"/>
		<xsl:param name="subgenus"/>
		<xsl:param name="species"/>
		<xsl:param name="subspecies"/>
		<xsl:variable name="genusPattern" select="substring-before($genus,'.')"/>
		<xsl:variable name="subgenusPattern" select="substring-before($subgenus,'.')"/>
		<xsl:variable name="speciesPattern" select="substring-before($species,'.')"/>

		<xsl:if test="$subgenusPattern!=''">
			<xsl:for-each select="//tp:taxon-name[generate-id() = generate-id(key('taxa', .)[1])]">
				<xsl:variable name="lGenus" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='genus'])"/>
				<xsl:variable name="lSubgenus" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='subgenus'])"/>
				<xsl:variable name="lSpecies" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='species'])"/>
				<xsl:variable name="lSubspecies" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='subspecies'])"/>
				<xsl:variable name="lSubgenusMatch" select="not(contains($lSubgenus,'.')) and contains($lSubgenus,$subgenusPattern)"/>

				<xsl:if test="$subspecies=$lSubspecies">
					<xsl:choose>
						<xsl:when test="$genusPattern!=''">
							<xsl:if test="contains($lGenus,$genusPattern)">
								<xsl:choose>
									<xsl:when test="$speciesPattern!=''">
										<xsl:if test="contains($lSpecies,$speciesPattern) and $lSubgenusMatch">
											<xsl:value-of select="$lSubgenus"/>
											<xsl:text>;</xsl:text>
										</xsl:if>
									</xsl:when>
									<xsl:otherwise>
										<xsl:if test="($lSpecies=$species) and $lSubgenusMatch">
											<xsl:value-of select="$lSubgenus"/>
											<xsl:text>;</xsl:text>
										</xsl:if>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:if>
						</xsl:when>
						<xsl:otherwise><!-- genus is not shorten but lGenus could be -->
							<xsl:choose>
								<xsl:when test="not(contains($lGenus,'.'))">
									<xsl:if test="$lGenus=$genus">
										<xsl:choose>
											<xsl:when test="$speciesPattern!=''">
												<xsl:if test="contains($lSpecies,$speciesPattern) and $lSubgenusMatch">
													<xsl:value-of select="$lSubgenus"/>
													<xsl:text>;</xsl:text>
												</xsl:if>
											</xsl:when>
											<xsl:otherwise>
												<xsl:if test="($lSpecies=$species) and $lSubgenusMatch">
													<xsl:value-of select="$lSubgenus"/>
													<xsl:text>;</xsl:text>
												</xsl:if>
											</xsl:otherwise>
										</xsl:choose>
									</xsl:if>
								</xsl:when>
								<xsl:otherwise>
									<xsl:if test="contains($genus,substring-before($lGenus,'.'))">
										<xsl:choose>
											<xsl:when test="$speciesPattern!=''">
												<xsl:if test="contains($lSpecies,$speciesPattern) and $lSubgenusMatch">
													<xsl:value-of select="$lSubgenus"/>
													<xsl:text>;</xsl:text>
												</xsl:if>
											</xsl:when>
											<xsl:otherwise>
												<xsl:if test="($lSpecies=$species) and $lSubgenusMatch">
													<xsl:value-of select="$lSubgenus"/>
													<xsl:text>;</xsl:text>
												</xsl:if>
											</xsl:otherwise>
										</xsl:choose>
									</xsl:if>
								</xsl:otherwise>
							</xsl:choose>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:if>
			</xsl:for-each>
		</xsl:if>
	</xsl:template>
	
	<xsl:template name="expandSubgenusNoSubspecies">
		<xsl:param name="genus"/>
		<xsl:param name="subgenus"/>
		<xsl:param name="species"/>
		<xsl:param name="subspecies"/>
		<xsl:variable name="genusPattern" select="substring-before($genus,'.')"/>
		<xsl:variable name="subgenusPattern" select="substring-before($subgenus,'.')"/>
		<xsl:variable name="speciesPattern" select="substring-before($species,'.')"/>

		<xsl:if test="$subgenusPattern!=''">
			<xsl:for-each select="//tp:taxon-name[generate-id() = generate-id(key('taxa', .)[1])]">
				<xsl:variable name="lGenus" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='genus'])"/>
				<xsl:variable name="lSubgenus" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='subgenus'])"/>
				<xsl:variable name="lSpecies" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='species'])"/>
				<xsl:variable name="lSubspecies" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='subspecies'])"/>
				<xsl:variable name="lSubgenusMatch" select="not(contains($lSubgenus,'.')) and contains($lSubgenus,$subgenusPattern)"/>

				<xsl:choose>
					<xsl:when test="$genusPattern!=''">
						<xsl:if test="contains($lGenus,$genusPattern)">
							<xsl:choose>
								<xsl:when test="$speciesPattern!=''">
									<xsl:if test="contains($lSpecies,$speciesPattern) and $lSubgenusMatch">
										<xsl:value-of select="$lSubgenus"/>
										<xsl:text>;</xsl:text>
									</xsl:if>
								</xsl:when>
								<xsl:otherwise>
									<xsl:if test="($lSpecies=$species) and $lSubgenusMatch">
										<xsl:value-of select="$lSubgenus"/>
										<xsl:text>;</xsl:text>
									</xsl:if>
								</xsl:otherwise>
							</xsl:choose>
						</xsl:if>
					</xsl:when>
					<xsl:otherwise><!-- genus is not shorten but lGenus could be -->
						<xsl:choose>
							<xsl:when test="not(contains($lGenus,'.'))">
								<xsl:if test="$lGenus=$genus">
									<xsl:choose>
										<xsl:when test="$speciesPattern!=''">
											<xsl:if test="contains($lSpecies,$speciesPattern) and $lSubgenusMatch">
												<xsl:value-of select="$lSubgenus"/>
												<xsl:text>;</xsl:text>
											</xsl:if>
										</xsl:when>
										<xsl:otherwise>
											<xsl:if test="($lSpecies=$species) and $lSubgenusMatch">
												<xsl:value-of select="$lSubgenus"/>
												<xsl:text>;</xsl:text>
											</xsl:if>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:if test="contains($genus,substring-before($lGenus,'.'))">
									<xsl:choose>
										<xsl:when test="$speciesPattern!=''">
											<xsl:if test="contains($lSpecies,$speciesPattern) and $lSubgenusMatch">
												<xsl:value-of select="$lSubgenus"/>
												<xsl:text>;</xsl:text>
											</xsl:if>
										</xsl:when>
										<xsl:otherwise>
											<xsl:if test="($lSpecies=$species) and $lSubgenusMatch">
												<xsl:value-of select="$lSubgenus"/>
												<xsl:text>;</xsl:text>
											</xsl:if>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:if>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:for-each>
		</xsl:if>
	</xsl:template>

	<!-- TODO: Problem with contains for species - lowercase letters make extensive matches -->

	<xsl:template name="expandSpecies">
		<xsl:param name="genus"/>
		<xsl:param name="subgenus"/>
		<xsl:param name="species"/>
		<xsl:param name="subspecies"/>
		<xsl:variable name="genusPattern" select="substring-before($genus,'.')"/>
		<xsl:variable name="speciesPattern" select="substring-before($species,'.')"/>

		<xsl:if test="$speciesPattern!=''">
			<xsl:for-each select="//tp:taxon-name[generate-id() = generate-id(key('taxa', .)[1])]">
				<xsl:variable name="lGenus" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='genus'])"/>
				<xsl:variable name="lSubgenus" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='subgenus'])"/>
				<xsl:variable name="lSpecies" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='species'])"/>
				<xsl:variable name="lSubspecies" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='subspecies'])"/>
				<xsl:variable name="lSpeciesMatch" select="not(contains($lSpecies,'.')) and contains($lSpecies,speciesPattern)"/>

				<xsl:if test="$subspecies=$lSubspecies">
					<xsl:choose>
						<xsl:when test="$genusPattern!=''">
							<xsl:if test="contains($lGenus,$genusPattern) and $lSpeciesMatch">
								<xsl:value-of select="$lSpecies"/>
								<xsl:text>;</xsl:text>
							</xsl:if>
						</xsl:when>
						<xsl:otherwise><!-- genus is not shorten but lGenus could be -->
							<xsl:choose>
								<xsl:when test="not(contains($lGenus,'.'))">
									<xsl:if test="($lGenus=$genus) and $lSpeciesMatch">
										<xsl:value-of select="$lSpecies"/>
										<xsl:text>;</xsl:text>
									</xsl:if>
								</xsl:when>
								<xsl:otherwise>
									<xsl:if test="contains($genus,substring-before($lGenus,'.')) and $lSpeciesMatch">
										<xsl:value-of select="$lSpecies"/>
										<xsl:text>;</xsl:text>
									</xsl:if>
								</xsl:otherwise>
							</xsl:choose>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:if>
			</xsl:for-each>
		</xsl:if>
	</xsl:template>

	<xsl:template name="expandSpeciesNoSubspecies">
		<xsl:param name="genus"/>
		<xsl:param name="subgenus"/>
		<xsl:param name="species"/>
		<xsl:param name="subspecies"/>
		<xsl:variable name="genusPattern" select="substring-before($genus,'.')"/>
		<xsl:variable name="speciesPattern" select="substring-before($species,'.')"/>

		<xsl:if test="$speciesPattern!=''">
			<xsl:for-each select="//tp:taxon-name[generate-id() = generate-id(key('taxa', .)[1])]">
				<xsl:variable name="lGenus" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='genus'])"/>
				<xsl:variable name="lSubgenus" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='subgenus'])"/>
				<xsl:variable name="lSpecies" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='species'])"/>
				<xsl:variable name="lSubspecies" select="normalize-space(tp:taxon-name-part[@taxon-name-part-type='subspecies'])"/>
				<xsl:variable name="lSpeciesMatch" select="not(contains($lSpecies,'.')) and contains($lSpecies,speciesPattern)"/>

				<xsl:choose>
					<xsl:when test="$genusPattern!=''">
						<xsl:if test="contains($lGenus,$genusPattern) and $lSpeciesMatch">
							<xsl:value-of select="$lSpecies"/>
							<xsl:text>;</xsl:text>
						</xsl:if>
					</xsl:when>
					<xsl:otherwise><!-- genus is not shorten but lGenus could be -->
						<xsl:choose>
							<xsl:when test="not(contains($lGenus,'.'))">
								<xsl:if test="($lGenus=$genus) and $lSpeciesMatch">
									<xsl:value-of select="$lSpecies"/>
									<xsl:text>;</xsl:text>
								</xsl:if>
							</xsl:when>
							<xsl:otherwise>
								<xsl:if test="contains($genus,substring-before($lGenus,'.')) and $lSpeciesMatch">
									<xsl:value-of select="$lSpecies"/>
									<xsl:text>;</xsl:text>
								</xsl:if>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:for-each>
		</xsl:if>
	</xsl:template>
</xsl:stylesheet>