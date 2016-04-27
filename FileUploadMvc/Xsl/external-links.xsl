<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xlink="http://www.w3.org/1999/xlink">
  <xsl:template match="ext-link[@ext-link-type='uri']">
    <a target="_blank" href="{@xlink:href}">
      <xsl:apply-templates />
    </a>
  </xsl:template>

  <xsl:template match="ext-link[@ext-link-type='doi']">
    <a target="_blank" href="http://dx.doi.org/{@xlink:href}">
      <xsl:apply-templates />
    </a>
  </xsl:template>
</xsl:stylesheet>
