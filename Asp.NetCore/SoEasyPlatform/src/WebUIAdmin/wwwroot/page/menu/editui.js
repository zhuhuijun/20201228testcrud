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
            url: '/api/Menu',
            dataType: 'json',
            contentType: 'application/json;charset=utf-8', //设置请求头信息
            data: JSON.stringify(data.field),
            success: function (e) {
                //e为后台返回的数据
                if (e.code === 200) {
                    setTimeout(function () {
                        top.layer.close(index);
                        top.layer.msg("数据编辑成功！");
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
    $("#iconBtn").click(function () {
        var oldiconT = $(this).val();
        layer.open({
            type: 2,
            title: "图标数据",
            closeBtn: false,
            area: ['310px','290px'],
            shade: 0.8,
            id: 'LAY_layuipro',
            btn: ['确定'],
            moveType: 1,
            shadeClose: true,
            content: '/Others/IconPage?oldicon=' + oldiconT,
            success: function (layero, index) {
                var btn = layero.find('.layui-layer-btn');
                btn.css('text-align', 'center');
                btn.on("click", function () {
                    var t = parent.$('#iconid').text();
                    if (t.length>0) {
                        $('#iconBtn').text(t);
                        $('#iconBtn').val(t);
                    }

                })

            }
        });
    });
});
