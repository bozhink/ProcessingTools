<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output encoding="utf-8" indent="yes" method="xml"/>
	<xsl:key name="taxa" use="." match="tn"/>
	<xsl:template match="/">
		<replaces>
			<xsl:for-each select="//tn[count(tn-part)=0][generate-id() = generate-id(key('taxa', normalize-space(.))[1])]">
				<xsl:sort/>
				<replace>
					<A>
						<tn>
							<xsl:value-of select="."/>
						</tn>
					</A>
					<B>
						<tn>
							<tn-part type="">
								<xsl:value-of select="."/>
							</tn-part>
						</tn>
					</B>
				</replace>
			</xsl:for-each>
		</replaces>
	</xsl:template>
</xsl:stylesheet>