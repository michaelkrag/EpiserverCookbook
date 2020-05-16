<%@ control language="C#" autoeventwireup="true" codebehind="ConfigurePayment.ascx.cs" inherits="DankortPaymentProvider.ConfigurePayment" %>
<table id="GenericTable" runat="server">
    <tr>
        <td class="FormLabelCell" colspan="2">
            <b>
                <asp:literal id="litLabel" text="Enter custom parameter value" runat="server"></asp:literal>
            </b>
        </td>
    </tr>
    <tr>
        <td class="FormLabelCell" colspan="2">
            <b>
                <asp:textbox id="txtSecretKey" text="Enter some key example." runat="server"></asp:textbox>
            </b>
        </td>
    </tr>
</table>