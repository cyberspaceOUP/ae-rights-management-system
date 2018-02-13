<%@ Page Language="C#" AutoEventWireup="false" Inherits="System.Web.UI.Page" Title="Send Email" %>

<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.Net.Mail" %>

<script runat="server">

//-----------------------------------------------

// Copyright 2008 to present by Bronze Inc.

// Visit www.Programmer.bz

// You may use and modify this code anyway you like

// as long as you keep this copyright notice in the file.

// You may add your own copyright to your modifications.

//-----------------------------------------------

protected void Button_Send_Click(object sender, EventArgs e)

{

//----- Mail address

MailAddress emailfrom = new MailAddress(Text_From_Email.Text);

//----- Mail object

MailMessage mm = new MailMessage();

mm.Body = Text_Body.Text;

mm.From = emailfrom;

mm.Sender = emailfrom;

mm.Subject = Text_Subject.Text;

mm.To.Add(Text_To.Text);

//----- Sending object

SmtpClient smtp = new SmtpClient();

smtp.Host = Text_Host.Text;

smtp.Port = 25;

//----- Delivery Method

if (Delivery_Method2.Checked) smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;

else if (Delivery_Method2.Checked)

{

smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;

smtp.PickupDirectoryLocation = Delivery_Folder.Text;

}

else smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

//----- Credentials

string password = Text_Host_Password.Text;

string username = Text_Host_Username.Text;

if (string.IsNullOrEmpty(username)) smtp.UseDefaultCredentials = true;

else

{

smtp.Credentials = new NetworkCredential(username, password);

smtp.UseDefaultCredentials = false;

}

//----- Send

StringBuilder sberror = new StringBuilder();

try

{

smtp.Send(mm);

}

catch (SmtpFailedRecipientsException ex)

{

for (int i = 0; i < ex.InnerExceptions.Length; i++)

{

SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;

if (status == SmtpStatusCode.MailboxBusy ||

status == SmtpStatusCode.MailboxUnavailable)

{

sberror.Append("Mailbox busy or unavailable");

}

else

{

sberror.Append("Failed to deliver message to "

+ ex.InnerExceptions[i].FailedRecipient);

}

}

}

catch (SmtpException ex)

{

sberror.Append(ex.Message);

}

if (sberror.Length > 0) Label_Message.Text = "11111: " + sberror.ToString();

else Label_Message.Text = "Message sent, hopefully";

}

//-----------------------------------------------

</script>

<html>
<head>
</head>
<body>
    <form runat="server">
    <asp:Label ID="Label_Message" runat="server" Text="" EnableViewState="false" />
    <div class="form">
        <asp:Button ID="Button_Send_1" runat="server" Text="Send" EnableViewState="false"
            OnClick="Button_Send_Click" />
        <table cellspacing="0" cellpadding="3" class="form">
            <tr class="form">
                <td class="form_prompt">
                    Email Host:
                </td>
                <td runat="server">
                    <asp:TextBox ID="Text_Host" runat="server" Columns="60" MaxLength="120" EnableViewState="false"></asp:TextBox>
                </td>
            </tr>
            <tr class="form">
                <td class="form_prompt">
                    Email Host Username:
                </td>
                <td>
                    <asp:TextBox ID="Text_Host_Username" runat="server" Columns="60" MaxLength="120"
                        EnableViewState="false"></asp:TextBox>
                </td>
            </tr>
            <tr class="form">
                <td class="form_prompt">
                    Email Host Password:
                </td>
                <td>
                    <asp:TextBox ID="Text_Host_Password" runat="server" Columns="60" MaxLength="120"
                        EnableViewState="false"></asp:TextBox>
                </td>
            </tr>
            <tr class="form">
                <td class="form_prompt">
                    From Email:
                </td>
                <td>
                    <asp:TextBox ID="Text_From_Email" runat="server" Columns="60" MaxLength="120" EnableViewState="false"></asp:TextBox>
                </td>
            </tr>
            <tr class="form">
                <td class="form_prompt">
                    To:
                </td>
                <td>
                    <asp:TextBox ID="Text_To" runat="server" Columns="60" MaxLength="2000" Rows="2" TextMode="MultiLine"
                        EnableViewState="false"></asp:TextBox>
                </td>
            </tr>
            <tr class="form">
                <td class="form_prompt">
                    Subject:
                </td>
                <td>
                    <asp:TextBox ID="Text_Subject" runat="server" Columns="60" MaxLength="120" EnableViewState="false"></asp:TextBox>
                </td>
            </tr>
            <tr class="form">
                <td class="form_prompt">
                    Body:
                </td>
                <td>
                    <asp:TextBox ID="Text_Body" runat="server" Columns="60" MaxLength="10000" TextMode="MultiLine"
                        Rows="15" EnableViewState="false"></asp:TextBox>
                </td>
            </tr>
            <tr class="form">
                <td class="form_prompt">
                    Delivery:
                </td>
                <td>
                    <asp:RadioButton ID="Delivery_Method1" GroupName="Delivery_Method" Checked Text="Network"
                        runat="server" EnableViewState="false" />
                    <asp:RadioButton ID="Delivery_Method2" GroupName="Delivery_Method" Text="IIS Folder"
                        runat="server" EnableViewState="false" />
                    <asp:RadioButton ID="Delivery_Method3" GroupName="Delivery_Method" Text="Specify Folder"
                        runat="server" EnableViewState="false" />
                    <br />
                    <asp:TextBox ID="Delivery_Folder" runat="server" Columns="60" MaxLength="2000" Rows="2"
                        TextMode="MultiLine" EnableViewState="false"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:Button ID="Button_Send_2" runat="server" Text="Send" EnableViewState="false"
            OnClick="Button_Send_Click" />
    </div>
    <p>
        Copyright 2008 to present by Bronze Inc. Visit <a href="http://www.programmer.bz">Programmer.bz</a>.</p>
    </form>
</body>
</html>
