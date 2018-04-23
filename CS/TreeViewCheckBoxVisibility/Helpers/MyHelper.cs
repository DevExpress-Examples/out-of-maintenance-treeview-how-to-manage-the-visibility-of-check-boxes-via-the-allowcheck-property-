using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxTreeView;
using System.Collections;

namespace TreeViewCheckBoxVisibility.Helpers {
    public static class MyHelper {
        private static void PerformActionOnNodesRecursive(TreeViewNodeCollection nodes, Action<TreeViewNode> action) {
            foreach (TreeViewNode node in nodes) {
                action(node);
                if (node.Nodes.Count > 0)
                    PerformActionOnNodesRecursive(node.Nodes, action);
            }
        }
        public static void AllowCheckNodes(MVCxTreeView treeView, int selectedIndex) {
            switch (selectedIndex) {
                case 0:
                    treeView.AllowCheckNodes = false;
                    break;
                case 1:
                    treeView.AllowCheckNodes = true;
                    PerformActionOnNodesRecursive(treeView.Nodes, delegate(TreeViewNode node) { node.AllowCheck = node.Nodes.Count == 0; });
                    break;
                case 2:
                    treeView.AllowCheckNodes = true;
                    PerformActionOnNodesRecursive(treeView.Nodes, delegate(TreeViewNode node) { node.AllowCheck = true; });
                    break;
            }
        }
        public static string ChangeAccessibility(MVCxTreeView treeView, bool enable, string disabledList) {
            if (!enable) {
                ArrayList lst = new ArrayList();
                PerformActionOnNodesRecursive(treeView.Nodes, delegate(TreeViewNode node) { 
                    node.Enabled = !node.Checked;
                    if (!node.Enabled)
                       lst.Add(node.Name);
                });
                disabledList = string.Join(",", lst.ToArray());
            }
            else {
                PerformActionOnNodesRecursive(treeView.Nodes, delegate(TreeViewNode node) { node.Enabled = true; });
                disabledList = string.Empty;
            }
            return disabledList;
        }
        public static void RestoreSelection(MVCxTreeView treeView, string selectedNodes) {
            if (string.IsNullOrEmpty(selectedNodes))
                return;
            string[] strNodes = selectedNodes.Split(',');
            for (int i = 0; i < strNodes.Length; i++) {
                TreeViewNode node = treeView.Nodes.FindRecursive(n => n.Name == strNodes[i]);
                if (node != null)
                    node.Checked = true;
            }
        }
        public static void RestoreAccessibility(MVCxTreeView treeView, string disabledList) {
            if (string.IsNullOrEmpty(disabledList))
                return;
            string[] strNodes = disabledList.Split(',');
            for (int i = 0; i < strNodes.Length; i++) {
                TreeViewNode node = treeView.Nodes.FindRecursive(n => n.Name == strNodes[i]);
                if (node != null)
                    node.Enabled = false;
            }
        }
    }
}