<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output byte-order-mark="no" encoding="UTF-8" method="xml" indent="yes" include-content-type="yes"/>
	<xsl:template match="/">
		<taxa>
			<xsl:for-each select="//tr[normalize-space(.)!='']">
				<xsl:for-each select="td">
					<xsl:if test="normalize-space(.)!=''">
						<taxon>
							<part>
								<value>
									<xsl:value-of select="normalize-space(.)"/>
								</value>
								<rank>
									<value>
										<xsl:choose>
											<xsl:when test="position()=1">
												<xsl:text>class</xsl:text>
											</xsl:when>
											<xsl:when test="position()=2">
												<xsl:text>order</xsl:text>
											</xsl:when>
											<xsl:when test="position()=3">
												<xsl:text>family</xsl:text>
											</xsl:when>
											<xsl:when test="position()=4">
												<xsl:text>subfamily</xsl:text>
											</xsl:when>
										</xsl:choose>
									</value>
								</rank>
							</part>
						</taxon>
					</xsl:if>
				</xsl:for-each>
			</xsl:for-each>
		</taxa>
	</xsl:template>
</xsl:stylesheet>