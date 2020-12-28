var $;
layui.config({
    base: "js/"
}).use(['form', 'layer', 'jquery'], function () {
    var form = layui.form(),
        layer = parent.layer === undefined ? layui.layer : parent.layer,
        laypage = layui.laypage;
    $ = layui.jquery;
    form.on("submit(editui)", function (data) {
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.ajax({
            type: 'POST',
            url: '/api/Role',
            dataType: 'json',
            contentType: 'application/json;charset=utf-8', //设置请求头信息
            data: JSON.stringify(data.field),
            success: function (e) {
                //e为后台返回的数据
                if (e.code === 200) {
                    //当你在iframe页面关闭自身时
                    //var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
                    //parent.layer.close(index); //再执行关闭
                    top.layer.close(index);
                    top.layer.msg("数据编辑成功！");
                    setTimeout(function () {
                        layer.closeAll("iframe");
                        //刷新父页面 
                        parent.location.reload();
                    }, 1000);

                } else {
                    parent.window.ColHelper.ErrorMsg(e.msg, "error");
                    top.layer.close(index);
                    setTimeout(function () {
                        layer.closeAll("iframe");
                    }, 1000);
                }
            },
            error: function (msg) {
                top.layer.close(index);
                var code = ColHelper.CanRedirectLogin(msg);
                if (401 !== code) {
                    layer.msg("服务器开小差了，请稍后再试...", { icon: 5 });
                }
            }
        });
        return false
    });
    /*为用户分配角色*/
    form.on("submit(assignUser)", function (data) {
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        var rowid = $('#rowid').val();
        var $checked = $('.usercontainer input[type="checkbox"][name="checked"]:checked');
        if ($checked.length < 1) {
            top.layer.msg("暂未选中任何数据！");
            return false;
        }
        var RowIdArr = new Array();
        $checked.each(function (ind, ele) {
            var rowid = $(ele).attr("data-id");
            RowIdArr.push(rowid);
        });

        $.ajax({
            type: 'put',
            url: '/api/Role/' + rowid + "?type=assignUser",
            dataType: 'json',
            contentType: 'application/json;charset=utf-8', //设置请求头信息
            data: JSON.stringify(RowIdArr),
            success: function (e) {
                //e为后台返回的数据
                if (e.code === 200) {
                    //当你在iframe页面关闭自身时
                    //var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
                    //parent.layer.close(index); //再执行关闭
                    top.layer.close(index);
                    top.layer.msg("为用户分配角色成功！");
                    setTimeout(function () {
                        layer.closeAll("iframe");
                    }, 1000);

                } else {
                    parent.window.ColHelper.ErrorMsg(e.msg, "error");
                    top.layer.close(index);
                    setTimeout(function () {
                        layer.closeAll("iframe");
                    }, 1000);
                }
            },
            error: function (msg) {
                top.layer.close(index);
                var code = ColHelper.CanRedirectLogin(msg);
                if (401 !== code) {
                    layer.msg("服务器开小差了，请稍后再试...", { icon: 5 });
                }
            }
        });
        return false
    });
    /*为角色勾选菜单*/
    form.on("submit(assignMenu)", function (data) {
        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
        var checkNodes = zTree.getCheckedNodes(true);
        var MenuIdArr = new Array();
        if (checkNodes.length > 0) {
            for (var i = 0; i < checkNodes.length; i++) {
                MenuIdArr.push(checkNodes[i].id.toString());
            }
        }
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        var rowid = $('#rowid').val();
        if (MenuIdArr.length < 1) {
            top.layer.msg("暂未选中任何数据！");
            return false;
        }
        $.ajax({
            type: 'put',
            url: '/api/Role/' + rowid + "?type=assignMenu",
            dataType: 'json',
            contentType: 'application/json;charset=utf-8', //设置请求头信息
            data: JSON.stringify(MenuIdArr),
            success: function (e) {
                //e为后台返回的数据
                if (e.code === 200) {
                    //当你在iframe页面关闭自身时
                    //var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
                    //parent.layer.close(index); //再执行关闭
                    top.layer.close(index);
                    top.layer.msg("为角色分配菜单成功！");
                    setTimeout(function () {
                        layer.closeAll("iframe");
                    }, 1000);

                } else {
                    parent.window.ColHelper.ErrorMsg(e.msg, "error");
                    top.layer.close(index);
                    setTimeout(function () {
                        layer.closeAll("iframe");
                    }, 1000);
                }
            },
            error: function (msg) {
                top.layer.close(index);
                var code = ColHelper.CanRedirectLogin(msg);
                if (401 !== code) {
                    layer.msg("服务器开小差了，请稍后再试...", { icon: 5 });
                }
            }
        });
        return false
    });
})
