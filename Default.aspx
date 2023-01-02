<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
<div class="widget-main">
                                                            <asp:DataList ID="dlAssign_Add" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                Width="100%">
                                                                <HeaderTemplate>
                                                                    <b>Module</b> </th>
                                                                    <th style="width: 10%; text-align: center">
                                                                        Product Content Name
                                                                    </th>
                                                                    <th style="width: 10%; text-align: center">
                                                                        Display Name
                                                                    </th>
                                                                    <th>
                                                                        Content Location
                                                                    </th>
                                                                    <th style="width: 10%; text-align: center">
                                                                        Content Type
                                                                    </th>
                                                                    <th style="width: 10%; text-align: center">
                                                                        Test
                                                                    </th>
                                                                    <th style="width: 10%; text-align: center">
                                                                        No.of Question
                                                                    </th>
                                                                    <th style="width: 10%; text-align: center">
                                                                        Duration (in Min)
                                                                    </th>
                                                                    <th style="width: 10%; text-align: center">
                                                                        Is Active
                                                                    </th>
                                                                    <th style="width: 10%; text-align: center">
                                                                    Action
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlModule" runat="server" Width="142px" ToolTip="Module" data-placeholder="Select Module"
                                                                        CssClass="chzn-select">
                                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="Module 1"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="Module 2"></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="Module 3"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:TextBox ID="txt" runat="server" Text="" Width="120px" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:TextBox ID="TextBox2" runat="server" Text="" Width="120px" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlLocation" runat="server" ToolTip="Location" data-placeholder="Select Location"
                                                                            CssClass="chzn-select" Width="120px">
                                                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                                            <asp:ListItem Value="1" Text="Home"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Class"></asp:ListItem>
                                                                            <asp:ListItem Value="3" Text="Course"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlContentType" runat="server" ToolTip="Content Type" data-placeholder="Select Content Type"
                                                                            CssClass="chzn-select" Width="120px">
                                                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                                            <asp:ListItem Value="1" Text="EOC Test Class"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="EOC Test Class"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlTest" runat="server" Width="120px" ToolTip="Test" data-placeholder="Select Test"
                                                                            CssClass="chzn-select">
                                                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                                            <asp:ListItem Value="1" Text="Test 1"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Test 2"></asp:ListItem>
                                                                            <asp:ListItem Value="3" Text="Test 3"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:TextBox ID="TextBox3" runat="server" Text="" Width="30px" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:TextBox ID="TextBox1" runat="server" Text="" Width="30px" />
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:CheckBox ID="ChkActive" runat="server" />
                                                                        <span class="lbl"></span>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:LinkButton ID="lnkDLSave" ToolTip="Save" class="btn-small btn-success icon-save"
                                                                            runat="server" CommandName="Save" Height="25px" />
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </div>
</asp:Content>

