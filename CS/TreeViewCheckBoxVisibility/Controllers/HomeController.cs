using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace TreeViewContextMenu.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            Session["disabledList"] = string.Empty;
            ViewBag.SelectedIndex = 0;
            return View();
        }
        [HttpPost]
        public ActionResult FormAction([ModelBinder(typeof(DevExpressEditorsBinder))] int rbList, string tvhSelectedNodes, string tvhCurrentState) {
            ViewBag.SelectedIndex = rbList;
            ViewBag.CurrentState = tvhCurrentState;
            ViewBag.SelectedNodeNames = tvhSelectedNodes;
            return View("Index");
        }
    }
}
