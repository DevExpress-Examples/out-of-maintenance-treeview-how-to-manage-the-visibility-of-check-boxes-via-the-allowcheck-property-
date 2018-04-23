Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports DevExpress.Web.Mvc
Imports DevExpress.Web.ASPxTreeView
Imports System.Collections

Namespace TreeViewCheckBoxVisibility.Helpers
	Public Class MyHelper
		Private Sub New()
		End Sub
        Private Shared Sub PerformActionOnNodesRecursive(ByVal nodes As TreeViewNodeCollection, ByVal action As Action(Of TreeViewNode))
            For Each node As TreeViewNode In nodes
                action(node)
                If node.Nodes.Count > 0 Then
                    PerformActionOnNodesRecursive(node.Nodes, action)
                End If
            Next node
        End Sub
		Public Shared Sub AllowCheckNodes(ByVal treeView As MVCxTreeView, ByVal selectedIndex As Integer)
			Select Case selectedIndex
				Case 0
					treeView.AllowCheckNodes = False
				Case 1
					treeView.AllowCheckNodes = True
                    PerformActionOnNodesRecursive(treeView.Nodes, Sub(node)
                                                                      node.AllowCheck = (node.Nodes.Count = 0)
                                                                  End Sub)
				Case 2
					treeView.AllowCheckNodes = True
                    PerformActionOnNodesRecursive(treeView.Nodes, Sub(node)
                                                                      node.AllowCheck = True
                                                                  End Sub)
			End Select
		End Sub
		Public Shared Function ChangeAccessibility(ByVal treeView As MVCxTreeView, ByVal enable As Boolean, ByVal disabledList As String) As String
			If (Not enable) Then
				Dim lst As ArrayList = New ArrayList()
                PerformActionOnNodesRecursive(treeView.Nodes, Sub(node)
                                                                  node.Enabled = Not node.Checked
                                                                  If (Not node.Enabled) Then
                                                                      lst.Add(node.Name)
                                                                  End If
                                                              End Sub)
                disabledList = String.Join(",", lst.ToArray())
            Else
                PerformActionOnNodesRecursive(treeView.Nodes, Sub(node)
                                                                  node.Enabled = True
                                                              End Sub)
                disabledList = String.Empty
            End If
                Return disabledList
        End Function
		Public Shared Sub RestoreSelection(ByVal treeView As MVCxTreeView, ByVal selectedNodes As String)
			If String.IsNullOrEmpty(selectedNodes) Then
				Return
			End If
			Dim strNodes As String() = selectedNodes.Split(","c)
			For i As Integer = 0 To strNodes.Length - 1
                Dim node As TreeViewNode = treeView.Nodes.FindRecursive(Function(n) n.Name = strNodes(i))
				If Not node Is Nothing Then
					node.Checked = True
				End If
			Next i
		End Sub
		Public Shared Sub RestoreAccessibility(ByVal treeView As MVCxTreeView, ByVal disabledList As String)
			If String.IsNullOrEmpty(disabledList) Then
				Return
			End If
			Dim strNodes As String() = disabledList.Split(","c)
			For i As Integer = 0 To strNodes.Length - 1
                Dim node As TreeViewNode = treeView.Nodes.FindRecursive(Function(n) n.Name = strNodes(i))
				If Not node Is Nothing Then
					node.Enabled = False
				End If
			Next i
		End Sub
	End Class
End Namespace