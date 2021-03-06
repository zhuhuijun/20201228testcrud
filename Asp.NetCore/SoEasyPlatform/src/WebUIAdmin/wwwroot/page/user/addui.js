var $;
layui.config({
    base: "js/"
}).use(['form', 'layer', 'jquery'], function () {
    var form = layui.form(),
        layer = parent.layer === undefined ? layui.layer : parent.layer,
        laypage = layui.laypage;
    $ = layui.jquery;
    //密码框验证规则
    form.verify({
        pass: [/^[\S]{6,12}$/, '密码必须6到12位，且不能出现空格'],
        repass: function (value) {
            var repassvalue = $('#Password').val();
            if (value !== repassvalue) {
                return '两次输入的密码不一致!';
            }
        }
    });

    form.on("submit(addUser)", function (data) {
        if (!data.field.Password) {
            data.field.Password = null;
        }
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.ajax({
            type: 'POST',
            url: '/api/User',
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
                    top.layer.msg("数据添加成功！");
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
    })
})
