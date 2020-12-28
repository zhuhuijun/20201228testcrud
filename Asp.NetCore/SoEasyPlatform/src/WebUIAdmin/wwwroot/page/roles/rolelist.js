layui.config({
    base: "js/"
}).use(['form', 'layer', 'jquery', 'laypage'], function () {
    var form = layui.form(),
        layer = parent.layer === undefined ? layui.layer : parent.layer,
        laypage = layui.laypage,
        $ = layui.jquery;
    PageMaxNum = 1;
    CurrentPage = 1;
    //加载页面数据
    var usersData = '';
    var InitParam = {
        Order: 'desc',
        Limit: 13,
        PageIndex: 1,
        Sort: 'ID',
        ListUrl: '/api/Role'
    };
    var MyCrudHelper = (function () {
        /**
         *
         * */
        function Crud() { }
        /**相关的常量 */
        Crud.ConstField = {
            AddTitle: '新增角色',
            AddUIUrl: "/Role/AddUI",
            EditTitle: '编辑角色',
            EditUIUrl: "/Role/EditUI/",
            AssignUserTitle: '分配用户',
            AssignUser: '/Role/AssignUser/',
            AssignMenuTitle:'菜单授权',
            AssignMenu: '/Role/AssignMenu/',
            RestfulServer: InitParam.ListUrl
        };
        /*导航上的编辑*/
        Crud.NavEdit = function () {
            var $checkbox = $('.users_list tbody input[type="checkbox"][name="checked"]');
            var $checked = $('.users_list tbody input[type="checkbox"][name="checked"]:checked');
            if ($checkbox.is(":checked")) {
                if ($checked.length > 1) {
                    layer.msg("请选择单行记录进行编辑");
                    return;
                }
                var rowid = $($checked[0]).attr("data-id");
                Crud.ShowEditUI(rowid);
            } else {
                layer.msg("请选择需要编辑的记录!");
            }
        };
        /*导航上的为角色授权*/
        Crud.NavAssignUser = function () {
            var $checkbox = $('.users_list tbody input[type="checkbox"][name="checked"]');
            var $checked = $('.users_list tbody input[type="checkbox"][name="checked"]:checked');
            if ($checkbox.is(":checked")) {
                if ($checked.length > 1) {
                    layer.msg("请选择单行记录进行操作");
                    return;
                }
                var rowid = $($checked[0]).attr("data-id");
                Crud.ShowAssignUserUI(rowid);
            } else {
                layer.msg("请选择需要授权的角色!");
            }
        };
        /*导航上的为角色授权*/
        Crud.NavAssignMenu = function () {
            var $checkbox = $('.users_list tbody input[type="checkbox"][name="checked"]');
            var $checked = $('.users_list tbody input[type="checkbox"][name="checked"]:checked');
            if ($checkbox.is(":checked")) {
                if ($checked.length > 1) {
                    layer.msg("请选择单行记录进行操作");
                    return;
                }
                var rowid = $($checked[0]).attr("data-id");
                Crud.ShowAssignMenuUI(rowid);
            } else {
                layer.msg("请选择需要授权的角色!");
            }
        };
        /*导航上的批量删除 */
        Crud.BathDel = function (rowidArr) {
            var $checkbox = $('.users_list tbody input[type="checkbox"][name="checked"]');
            var $checked = $('.users_list tbody input[type="checkbox"][name="checked"]:checked');
            if ($checkbox.is(":checked")) {
                var RowIdArr = new Array();
                $checked.each(function (ind, ele) {
                    var rowid = $(ele).attr("data-id");
                    RowIdArr.push(rowid);
                });
                Crud.BathDelServer(RowIdArr);
            } else {
                layer.msg("请选择需要编辑的记录!");
            }
        };
        /**
         * 删除发送请求
         * @param {any} RowIdArr
         */
        Crud.BathDelServer = function (RowIdArr) {
            layer.confirm('确定删除相关记录？', { icon: 3, title: '提示信息' }, function (index) {
                $.ajax({
                    type: 'delete',
                    url: Crud.ConstField.RestfulServer,
                    dataType: 'json',
                    contentType: 'application/json;charset=utf-8', //设置请求头信息
                    data: JSON.stringify(RowIdArr),
                    success: function (e) {
                        layer.close(index);
                        if (e.code === 200) {
                            form.render();
                            laytoaster('数据删除成功', "success");
                            var cur = CurrentPage;
                            if (CurrentPage > PageMaxNum) {
                                cur = PageMaxNum;
                            }
                            SearchData({ PageIndex: cur });
                        } else {
                            layer.msg(e.msg);
                        }
                    },
                    error: function () {
                        layer.msg("服务器开小差了，请稍后再试...");
                    }
                });
            });
        };
        /**
         * 编辑界面
         * @param {any} rowid
         */
        Crud.ShowEditUI = function (rowid) {
            var index = layui.layer.open({
                title: Crud.ConstField.EditTitle,
                type: 2,
                area: ['430px', '350px'],
                content: Crud.ConstField.EditUIUrl + rowid,
                success: function (layero, index) {
                }
            });
        };
        /**
         * 为角色分配用户
         * @param {any} rowid
         */
        Crud.ShowAssignUserUI = function (rowid) {
            var index = layui.layer.open({
                title: Crud.ConstField.AssignUserTitle,
                type: 2,
                area: ['500px', '400px'],
                content: Crud.ConstField.AssignUser + rowid,
                success: function (layero, index) {
                }
            });
        };
        /**
         * 为角色分配菜单
         * @param {any} rowid
         */
        Crud.ShowAssignMenuUI = function (rowid) {
            var index = layui.layer.open({
                title: Crud.ConstField.AssignMenuTitle,
                type: 2,
                area: ['480px', '520px'],
                content: Crud.ConstField.AssignMenu + rowid,
                success: function (layero, index) {
                }
            });
        };
        /**添加界面 */
        Crud.ShowAddUI = function () {
            var index = layui.layer.open({
                title: Crud.ConstField.AddTitle,
                type: 2,
                area: ['430px', '350px'],
                content: Crud.ConstField.AddUIUrl,
                success: function (layero, index) {
                }
            });
        };
        /**返回相关的方法**/
        return Crud;
    })();
    /**
     * 
     * 
     * @param {any} options
     */
    var laySwal = function (options) {
        if ($.isFunction(swal)) {
            swal($.extend({ showConfirmButton: false, showCancelButton: false, timer: 1000, title: '未设置', type: "success" }, options));
        }
        else {
            window.log('缺少 swal 脚本引用');
        }
    };
    /**
     * 
     * @param {any} options
     */
    var laytoaster = function (message, type) {
        toastr.options = { progressBar: true, positionClass: 'toast-bottom-right', hideDuration: 1000 };
        toastr[type](message);
    };
    /**
     * 初始化页面的方法
     * @param {any} searchData
     */
    var GetTableData = function (searchData) {
        var index = layer.msg('数据加载中，请稍候', { icon: 16, time: false, shade: 0.8 });
        searchData = $.extend(InitParam, searchData);
        $.ajax({
            url: InitParam.ListUrl,
            data: InitParam,
            type: 'get',
            contentType: 'application/json',
            dataType: "json",
            success: function (ServerData) {
                setTimeout(function () {
                    layer.close(index);
                }, 500);
                //执行加载数据的方法
                RenderTableList(ServerData);
            },
            error: function (msg) {
                top.layer.close(index);
                console.info(JSON.stringify(msg));
                var code = ColHelper.CanRedirectLogin(msg);
                if (401 !== code) {
                    layer.msg("服务器开小差了，请稍后再试...", { icon: 5 });
                }
            }
        });
    };
    /*调用加载数据的方法*/
    GetTableData({});
    var SearchModel = function (paramName, paramVal, op) {
        this.ParamName = paramName;
        this.ParamVal = paramVal;
        this.OpType = op;
    }
    /**
     * 获取查询的参数
     */
    var GetSearchForm = function () {
        var SearchArr = [];
        $("#searchForm input").each(function (mm, ele) {
            var searchVal = $(ele).val();
            if (searchVal) {
                var searchName = $(ele).attr("name");
                var searchOp = $(ele).attr("op");
                var searchOne = new SearchModel(searchName, searchVal, searchOp);
                SearchArr.push(searchOne);
            }
        });
        return SearchArr;
    };
    /**
     * 
     * @param {any} options
     */
    var SearchData = function (options) {

        //获取查询的参数
        var searchData = GetSearchForm();
        var searchStr = "";
        if (searchData.length > 0) {
            searchStr = JSON.stringify(searchData);
        }
        var dataSearch = { SearchData: searchStr, PageIndex: 1 };
        dataSearch = $.extend(dataSearch, options);
        GetTableData(dataSearch);
    };
    //查询
    $(".search_btn").click(function () {
        var userArray = [];
        SearchData({});
    });

    /**
     * 添加按钮的操作
     */
    $(".usersAdd_btn").click(MyCrudHelper.ShowAddUI);
    /*导航条上的编辑按钮的事件绑定*/
    $('.usersEdit_btn').click(MyCrudHelper.NavEdit);
    /*导航上的为角色分配菜单的功能*/
    $('.assignMenu').click(MyCrudHelper.NavAssignMenu);
    /*导航上为角色分配用户*/
    $(".assignUser").click(MyCrudHelper.NavAssignUser);
    //操作
    $("body").on("click", ".users_edit", function () {  //编辑
        var rowid = $(this).attr("data-id");
        MyCrudHelper.ShowEditUI(rowid);
    });
    $(".batchDel").click(MyCrudHelper.BathDel);

    //全选
    form.on('checkbox(allChoose)', function (data) {
        var child = $(data.elem).parents('table').find('tbody input[type="checkbox"]:not([name="show"])');
        child.each(function (index, item) {
            item.checked = data.elem.checked;
        });
        form.render('checkbox');
    });

    //通过判断文章是否全部选中来确定全选按钮是否选中
    form.on("checkbox(choose)", function (data) {
        var child = $(data.elem).parents('table').find('tbody input[type="checkbox"]:not([name="show"])');
        var childChecked = $(data.elem).parents('table').find('tbody input[type="checkbox"]:not([name="show"]):checked')
        if (childChecked.length == child.length) {
            $(data.elem).parents('table').find('thead input#allChoose').get(0).checked = true;
        } else {
            $(data.elem).parents('table').find('thead input#allChoose').get(0).checked = false;
        }
        form.render('checkbox');
    })


    /**
     * 删除
     * 
     */
    $("body").on("click", ".users_del", function () {
        var _this = $(this);
        var rowid = _this.attr("data-id");
        var RowIdArr = new Array();
        RowIdArr.push(rowid);
        MyCrudHelper.BathDelServer(RowIdArr);
    });
    /**
     * *
     * @param {any} data
     * @param {any} curr
     */
    var RealRenderTable = function (ServerData) {
        var dataHtml = '';
        var currData = ServerData;
        if (currData.code !== 200) {
            dataHtml = '<tr><td colspan="7">' + currData.msg + '</td></tr>';
            return dataHtml;
        }
        if (currData.rows.length !== 0) {
            var dataArr = currData.rows;
            for (var i = 0; i < dataArr.length; i++) {
                dataHtml += '<tr>'
                    + '<td><input type="checkbox" name="checked" lay-skin="primary" lay-filter="choose" data-id="' + dataArr[i].Id + '"></td>'
                    + '<td>' + dataArr[i].RoleName + '</td>'
                    + '<td>' + dataArr[i].Description + '</td>'
                    + '<td>'
                    + ColHelper.SetColBtn(dataArr[i].Id)
                    + '</td>'
                    + '</tr>';
            }
        } else {
            dataHtml = '<tr><td colspan="7">暂无数据</td></tr>';
        }
        $(".users_content").html(dataHtml);
    };
    /**
     * 渲染页面中的分页功能
     * @param {any} ServerData
     */
    function RenderTableList(ServerData) {
        //分页
        var nums = InitParam.Limit; //每页出现的数据量
        laypage({
            cont: "page",
            pages: Math.ceil(ServerData.total / nums),
            curr: ServerData.pageindex || 1, //当前页
            jump: function (obj, isfirst) {
                CurrentPage = ServerData.pageindex;
                PageMaxNum = ServerData.maxpagesize;
                var pageIndex = obj.curr;
                if (!isfirst) {
                    GetTableData({ PageIndex: pageIndex });
                }
                if (CurrentPage > PageMaxNum) {
                    GetTableData({ PageIndex: PageMaxNum });
                }
                RealRenderTable(ServerData);
                $('.users_list thead input[type="checkbox"]').prop("checked", false);
                form.render();
            }
        })
    }

})