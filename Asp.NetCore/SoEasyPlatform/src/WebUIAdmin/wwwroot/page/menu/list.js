layui.config({
    base: '../js/'
}).use(['layer', 'util', 'treeTable'], function () {
    var $ = layui.jquery;
    var layer = layui.layer;
    var util = layui.util;
    var treeTable = layui.treeTable;
    var table = layui.table;
    var InitParam = {
        ListUrl: '/api/Menu'
    };
    /**
     * 
     * @param {any} options
     */
    var laytoaster = function (message, type) {
        toastr.options = { progressBar: true, positionClass: 'toast-bottom-right', hideDuration: 1000 };
        toastr[type](message);
    };
    // 渲染表格
    var tarbal = '#tbBar';
    var tarcon = $("#tbBar").html();
    if ($.trim(tarcon)==='') {
        tarbal = '#tbBarempty';
    }
    var insTb = treeTable.render({
        elem: '#demoTreeTb',
        url: '/api/Menu',
        height: 'full',
        tree: {
            iconIndex: 2,
            isPidData: true,
            idName: 'authorityId',
            pidName: 'parentId'
        },
        cols: [
            [
                { type: 'numbers' },
                { type: 'checkbox' },
                { field: 'authorityName', title: '菜单名称', minWidth: 165 },
                {
                    title: '菜单图标', align: 'center', hide: false,
                    templet: '<p><i class="iconfont {{d.Icon}}"></i></p>'
                },
                { field: 'Url', title: '链接地址' },
                { title: '类型', templet: '<p>{{d.IsResource==0?"菜单":"按钮"}}</p>', align: 'center', width: 60 },
                { field: 'Order', title: '菜单序号' },
                { align: 'center', toolbar: tarbal, title: '操作', width: 260 }
            ]
        ],
        style: 'margin-top:0;'
    });

    // 全部展开
    $('#btnExpandAll').click(function () {
        insTb.expandAll();
    });

    // 全部折叠
    $('#btnFoldAll').click(function () {
        insTb.foldAll();
    });

    // 展开指定
    $('#btnExpand').click(function () {
        insTb.expand(2);
    });

    // 折叠指定
    $('#btnFold').click(function () {
        insTb.fold(2);
    });

    // 设置选中
    $('#btnChecked').click(function () {
        insTb.expand(4);
        insTb.setChecked([4]);
    });

    // 搜索
    $('#btnSearch').click(function () {
        var keywords = $('#edtSearch').val();
        if (keywords) {
            insTb.filterData(keywords);
        } else {
            insTb.clearFilter();
        }
    });

    // 清除搜索
    $('#btnClearSearch').click(function () {
        insTb.clearFilter();
    });

    // 重载
    $('#btnReload').click(function () {
        insTb.reload();
    });
    $('#btnRefresh').click(function () {
        insTb.refresh();
    });
    // 工具列点击事件
    treeTable.on('tool(demoTreeTb)', function (obj) {
        var event = obj.event;
        var rowdataId = obj.data.authorityId;
        if (event === 'del') {
            var RowIdArr = new Array();
            RowIdArr.push(rowdataId);
            MyCrudHelper.BathDelServer(RowIdArr);
        } else if (event === 'edit') {
            /*console.info(JSON.stringify(obj.data));*/
            MyCrudHelper.ShowEditUI(rowdataId);
        }
    });
    // 获取选中
    $('#btnGetChecked').click(function () {
        layer.alert('<pre>' + JSON.stringify(insTb.checkStatus().map(function (d) {
            return {
                authorityName: d.authorityName,
                authorityId: d.authorityId,
                LAY_INDETERMINATE: d.LAY_INDETERMINATE
            };
        }), null, 3) + '</pre>');
    });
    var MyCrudHelper = (function () {
        /**
         *
         * */
        function Crud() { }
        /**相关的常量 */
        Crud.ConstField = {
            AddTitle: '新增菜单',
            AddUIUrl: "/Menu/AddUI",
            EditTitle: '编辑菜单',
            EditUIUrl: "/Menu/EditUI/",
            RestfulServer: InitParam.ListUrl
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
                            laytoaster('数据删除成功', "success");
                            insTb.refresh();
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
                area: ['600px', '500px'],
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
        /**添加界面 */
        Crud.ShowAddUI = function () {
            var index = layui.layer.open({
                title: Crud.ConstField.AddTitle,
                type: 2,
                area: ['600px', '500px'],
                content: Crud.ConstField.AddUIUrl,
                success: function (layero, index) {
                }
            });
        };
        /**返回相关的方法**/
        return Crud;
    })();
    /**
     * 添加按钮的操作
     */
    $(".usersAdd_btn").click(MyCrudHelper.ShowAddUI);
});