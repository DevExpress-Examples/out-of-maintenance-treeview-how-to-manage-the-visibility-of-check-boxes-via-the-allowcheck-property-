Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc

Namespace TreeViewContextMenu.Controllers
	Public Class HomeController
		Inherits Controller
		Public Function Index() As ActionResult
			Session("disabledList") = String.Empty
			ViewBag.SelectedIndex = 0
			Return View()
		End Function
		<HttpPost> _
		Public Function FormAction(<ModelBinder(GetType(DevExpressEditorsBinder))> ByVal rbList As Integer, ByVal tvhSelectedNodes As String, ByVal tvhCurrentState As String) As ActionResult
			ViewBag.SelectedIndex = rbList
			ViewBag.CurrentState = tvhCurrentState
			ViewBag.SelectedNodeNames = tvhSelectedNodes
			Return View("Index")
		End Function
	End Class
End Namespace
