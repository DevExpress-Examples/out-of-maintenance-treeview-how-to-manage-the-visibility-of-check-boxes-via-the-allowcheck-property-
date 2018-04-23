@Code
    ViewBag.Title = "How to manage the visibility of check boxes via the AllowCheck property"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
@Using (Html.BeginForm("FormAction", "Home", FormMethod.Post, New With { Key. id = "mainForm" })) 

@Html.DevExpress().RadioButtonList(
    Sub(settings)
    settings.Name = "rbList"
    Dim item1 As ListEditItem = New ListEditItem
    item1.Value = "0"
    item1.Text = "Hide all check boxes"    
    settings.Properties.Items.Add(item1)
    Dim item2 As ListEditItem = New ListEditItem
    item2.Value = "1"
    item2.Text = "Hide all check boxes"    
    settings.Properties.Items.Add(item2)
    Dim item3 As ListEditItem = New ListEditItem
    item3.Value = "2"
    item3.Text = "Show all check boxes"    
    settings.Properties.Items.Add(item3)    

    settings.Properties.ClientSideEvents.ValueChanged = "OnRadioButtonValueChanged"
    settings.PreRender = Function(sender, e) 
        Dim rbList As MVCxRadioButtonList = TryCast(sender, MVCxRadioButtonList)
        rbList.SelectedIndex = ViewBag.SelectedIndex
    End Function
End Sub).GetHtml()
    
@Html.DevExpress().TreeView(
    Sub(settings)
        settings.Name = "treeView"
        settings.DataBound = Sub(sender, e)
                                 Dim treeView As MVCxTreeView = TryCast(sender, MVCxTreeView)
                                 treeView.ExpandAll()
                                 TreeViewCheckBoxVisibility_vb.TreeViewCheckBoxVisibility.Helpers.MyHelper.RestoreSelection(treeView, ViewBag.SelectedNodeNames)
                                 TreeViewCheckBoxVisibility_vb.TreeViewCheckBoxVisibility.Helpers.MyHelper.AllowCheckNodes(treeView, ViewBag.SelectedIndex)
                                 Session("disabledList") = TreeViewCheckBoxVisibility_vb.TreeViewCheckBoxVisibility.Helpers.MyHelper.ChangeAccessibility(treeView, If(ViewBag.CurrentState = "Disable", False, True), Session("disabledList").ToString())
                                 TreeViewCheckBoxVisibility_vb.TreeViewCheckBoxVisibility.Helpers.MyHelper.RestoreAccessibility(treeView, ViewBag.DisabledList)
                             End Sub
        settings.ClientSideEvents.CheckedChanged = "OnTreeViewCheckedChanged"
    End Sub).BindToSiteMap("~/App_Data/Feature.sitemap", False).GetHtml()
  
@Html.Hidden("tvhSelectedNodes")
@Html.Hidden("tvhCurrentState")
    @<table>
        <tr>
            <td>
@Html.DevExpress().Button(
Sub(settings)
    settings.Name = "btnDisable"
    settings.Text = "Disable checked nodes"
    settings.RouteValues = New With { Key.  Controller = "Home", Key.Action = "FormAction", Key. actionName = "Disable" }
    settings.UseSubmitBehavior = True
    settings.ClientSideEvents.Click = "OnDisableButtonClick"
End Sub).GetHtml()
             </td>
             <td>
@Html.DevExpress().Button(
Sub(settings)
    settings.Name = "btnEnable"
    settings.Text = "Enable checked nodes"
    settings.RouteValues = New With { Key. Controller = "Home", Key. Action = "FormAction", Key. actionName = "Enable" }
    settings.UseSubmitBehavior = True
    settings.ClientSideEvents.Click = "OnEnableButtonClick"
End Sub).GetHtml()  
            </td>
        </tr>
    </table>  
End Using
