<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>
  <!--declare a template for every "Product" node in xml -->
    <xsl:template match="/Products/Product">
      <!-- select the child node and insert comma-->
        <xsl:value-of select="BusinessCode"/>,
        <xsl:value-of select="Name"/>,
        <xsl:value-of select="Price"/>,
        <xsl:value-of select="Desctiption"/>,
        <xsl:value-of select="Category"/>,
        <xsl:value-of select="Subcategory"/>
      <!--applicable only to html documents separate item with break-->
        <br></br>
    </xsl:template>
</xsl:stylesheet>
