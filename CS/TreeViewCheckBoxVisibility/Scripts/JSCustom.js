function OnRadioButtonValueChanged(s, e) {
    SynchronizeState(null);
    $('#mainForm').submit();
}
function OnTreeViewCheckedChanged(s, e) {
    if (treeView.cpCheckedNodes == null)
        treeView.cpCheckedNodes = GetCheckedNodes();

    if (!e.node.GetChecked()) {
        var ndx = treeView.cpCheckedNodes.indexOf(e.node.name);
        treeView.cpCheckedNodes.splice(ndx, 1);
    }
    else
        treeView.cpCheckedNodes[treeView.cpCheckedNodes.length] = e.node.name;
}
function GetCheckedNodes() {
    var res = null;
    var nodes = $('#tvhSelectedNodes').val();
    if (nodes == '')
        res = [];
    else {
        res = nodes.split(',');
    }
    return res;
}
function SynchronizeState(state) {
    if (treeView.cpCheckedNodes != null)
        $('#tvhSelectedNodes').val(treeView.cpCheckedNodes.join());
    if (state != null)
        $('#tvhCurrentState').val(state);
}
function OnDisableButtonClick(s, e) {
    SynchronizeState("Disable");
}
function OnEnableButtonClick(s, e) {
    SynchronizeState("Enable");
}