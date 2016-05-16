<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template  match="/*">
        <table>
      <tr>
        <th>Business Code</th>
        <th>Name</th>
        <th>Description</th>
        <th>Price</th>        
        <th>Category</th>
        <th>Subcategory</th>
        </tr>
          
          <!--iterate through xml nodes-->
       <xsl:for-each select="Product">
         
         <!--depending on the position number of node set background for the table rows-->
          <xsl:choose>

            <!--if the position is odd, create a row with white background-->
            <xsl:when test="position() mod 2 = 1">
              <tr style="background-color: #ffffff">
                <td>
                  <xsl:value-of select="BusinessCode"></xsl:value-of>
                </td>
                <td>
                  <xsl:value-of select="Name"></xsl:value-of>
                </td>
                <td>
                  <xsl:value-of select="Desctiption"></xsl:value-of>
                </td>
                <td>
                  <xsl:value-of select="Price"></xsl:value-of>
                </td>
                <td>
                  <xsl:value-of select="Category"></xsl:value-of>
                </td>
                <td>
                  <xsl:value-of select="Subcategory"></xsl:value-of>
                </td>

              </tr>
            </xsl:when>

            <!--if the position is even, create a row with grey background-->
            <xsl:otherwise>
              <tr style="background-color: #f5f5f5">
                <td>
                  <xsl:value-of select="BusinessCode"></xsl:value-of>
                </td>
                <td>
                  <xsl:value-of select="Name"></xsl:value-of>
                </td>
                <td>
                  <xsl:value-of select="Desctiption"></xsl:value-of>
                </td>
                <td>
                  <xsl:value-of select="Price"></xsl:value-of>
                </td>
                <td>
                  <xsl:value-of select="Category"></xsl:value-of>
                </td>
                <td>
                  <xsl:value-of select="Subcategory"></xsl:value-of>
                </td>

              </tr>
            </xsl:otherwise>
          </xsl:choose>
         </xsl:for-each>
    </table></xsl:template>
</xsl:stylesheet>