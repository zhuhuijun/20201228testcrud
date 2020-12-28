var clickIcon = function (ele) {
    $(".myicon").removeClass("selected");
    $(ele).addClass("selected");
    var mm = $(ele).attr("data-clazz");
    parent.$('#iconid').text(mm);
};
var buildicon = function () {
    var iconTxt = '';
    var iconArr = ['icon-zhanghu', 'icon-lock1', 'icon-erweima', 'icon-xinlangweibo', 'icon-qq', 'icon-icon', 'icon-guanbi',
        'icon-gonggao', 'icon-menu1', 'icon-wenben', 'icon-dengji3', 'icon-dengji1', 'icon-dengji2', 'icon-dengji4', 'icon-dengji5',
        'icon-dengji6', 'icon-new1', 'icon-huanfu', 'icon-link', 'icon-lingsheng', 'icon-star', 'icon-dongtaifensishu', 'icon-prohibit',
        'icon-caozuo', 'icon-weather',
        'icon-edit', 'icon-computer', 'icon-text', 'icon-loginout', 'icon-shuaxin1', 'icon-shezhi1'];
    var oldicon = $('#oldicon').text();
    for (var i = 0; i < iconArr.length; i++) {
        if (oldicon.length > 0 && iconArr[i] === oldicon) {
            iconTxt += '<i  onclick="javascript:window.clickIcon(this)" data-clazz="' + iconArr[i] + '" class="myicon selected iconfont ' + iconArr[i] + '"></i>';
        } else {
            iconTxt += '<i  onclick="javascript:window.clickIcon(this)" data-clazz="' + iconArr[i] + '" class="myicon iconfont ' + iconArr[i] + '"></i>';
        }

    }
    var iconTxtTarget = '<div  class="iconShow" >' + iconTxt + '</div > ';
    $("#iconPage").html(iconTxtTarget);
};


$(function () {
    buildicon();
});