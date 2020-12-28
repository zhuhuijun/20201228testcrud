; (function (exports) {
    var ColHelper = function () { };
    /**
     * 设置列的按钮
     * @param {any} rowid
     */
    ColHelper.SetColBtn = function (rowid) {
        //1.是不是有编辑按钮和删除按钮
        var editBtn = $(".usersEdit_btn").length;
        var delBtn = $(".batchDel").length;
        if (editBtn === 0 && delBtn === 0) {
            return '无';
        }
        var targetBtnStr = "";
        if (editBtn) {
            targetBtnStr += '<a class="layui-btn layui-btn-warm layui-btn-mini users_edit" data-id="' + rowid + '"><i class="iconfont icon-edit"></i> 编辑</a>'
        }
        if (delBtn) {
            targetBtnStr += '<a class="layui-btn layui-btn-danger layui-btn-mini users_del" data-id="' + rowid + '"><i class="layui-icon">&#xe640;</i> 删除</a>'
        }
        return targetBtnStr;
    };
    /**
     * 消息的提示
     * @param {any} msg
     * @param {any} type
     */
    ColHelper.ErrorMsg = function (msg, type) {
        toastr.options = { progressBar: true, positionClass: 'toast-bottom-right', hideDuration: 500 };
        toastr[type](msg);
        setTimeout(function () {
            //刷新父页面 
            location.reload();
        }, 3000);
    };
    /**
     * 重新登录
     * @param {any} msg
     */
    ColHelper.CanRedirectLogin = function (msg) {
        var code = 200;
        if (msg.status) {
            switch (msg.status) {
                case 401:
                    layer.msg("登录已经过期，请重新登录", { icon: 5 });
                    setTimeout(function () {
                        window.location.href = window.basepath;
                    }, 1000);
                    code = 401;
                    break;
            }
        }
        return code;
    };
    exports.ColHelper = ColHelper;
})(window);