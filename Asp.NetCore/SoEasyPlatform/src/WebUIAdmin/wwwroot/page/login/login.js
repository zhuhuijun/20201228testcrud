layui.config({
    base: "js/"
}).use(['form', 'layer'], function () {
    var form = layui.form(),
        layer = parent.layer === undefined ? layui.layer : parent.layer,
        $ = layui.jquery;
    //登录按钮事件
    form.on("submit(login)", function (data) {
        $("#loginForm").submit();
    })
    $(function () {
        var msg = $("#errorMsg").text();
        if (msg.length > 0) {
            layer.msg(msg, { icon: 5 });
        }
    })
})
