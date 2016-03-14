; (function (window, document, $) {

    function toggleTab(src, tar) {
        $(src).siblings().removeClass("current");
        $(tar).siblings().hide();
        $(src).addClass("current");
        $(tar).show();
    }

    function getHeaderHeight(width) {
        var height = 45;
        if (width < 330) {
            height = 45;
        } else if (width >= 330 && width < 600) {
            height = 50;
        } else if (width >= 600 && width < 900) {
            height = 55;
        } else if (width >= 900) {
            height = 60;
        }
        return height;
    }

    var clientWidth = document.documentElement.clientWidth || document.body.clientWidth;
    var clientHeight = document.documentElement.clientHeight || document.body.clientHeight;
    var headerHeight = getHeaderHeight(clientWidth);

    $(function () {

        /* 绑定底部导航菜单 */
        var mtabs = new mTabs({
            mtab: "#divOaTab",
            mhead: "#divOaTab #ulOaTabsFooter",
            mcontent: "#divOaTab #sOaTabsContent",
            width: clientWidth
        });

        /* 为返回按钮绑定方法 */
        $("#iFormReturn").click(function () { window.close(); window.opener.location.reload(); });
        $("#divFormReturn").click(function () { window.close(); window.opener.location.reload(); });
        $("#iFastReturn").click(function () { $("#divOaTab").show(); $("#divFastTab").hide(); });
        $("#divFastReturn").click(function () { $("#divOaTab").show(); $("#divFastTab").hide(); });

        /* 为批办意见页面的，批办意见，批办过程，您的意见，3个tab绑定方法 */
        $("#liApprovalOpinionTab").click(function () { toggleTab(this, "#liApprovalOpinion"); });
        $("#liApprovalProcessTab").click(function () { toggleTab(this, "#liApprovalProcess"); });
        $("#liApprovalYourOpinionTab").click(function () { toggleTab(this, "#liApprovalYourOpinion"); });

        /* 为发送页面的，传阅tab中的，“全选”，“取消”按钮绑定方法 */
        $("#btnSelectAllPass").click(function () { $("#divPassUser").find("input").forEach(function (element) { element.checked = "true"; }); });
        $("#btnCancelAllPass").click(function () { $("#divPassUser").find("input").forEach(function (element) { element.checked = ""; }); });

        /* 为主页面的，表单，正文，附件，批办意见，发送，5个页面动态指定高度，避免多重滚动条 */
        $("#sOaForm").css("height", (clientHeight - headerHeight * 2) + "px");
        $("#sOaContent").css("height", (clientHeight - headerHeight * 2) + "px");
        $("#sOaAttachment").css("height", (clientHeight - headerHeight * 2) + "px");
        $("#sOaApproval").css("height", (clientHeight - headerHeight * 2) + "px");
        $("#sOaOperation").css("height", (clientHeight - headerHeight * 2) + "px");
        $(".ui-tab-content").css("height", (clientHeight - headerHeight * 3) + "px");

        // 为页面绑定滚动条
        new IScroll("#sOaForm", { probeType: 3, mouseWheel: true }).refresh();
        new IScroll("#divOaContentDoc", { probeType: 3, mouseWheel: true }).refresh();
        //new IScroll("#sOaAttachment", { probeType: 3, mouseWheel: true }).refresh();

        /* 为主页面的，表单，正文，附件，批办意见，发送，5个tab绑定回调方法 */
        mtabs.callback[0] = function () { };
        mtabs.callback[1] = function () { };
        mtabs.callback[2] = function () { };
        mtabs.callback[3] = function () { };
        mtabs.callback[4] = function () { };

        /* 为发送页面的，发送，传阅，2个tab绑定方法 */
        $("#liOperationSendTab").click(function () { toggleTab(this, "#liOperationSend"); });
        $("#liOperationPassTab").click(function () { toggleTab(this, "#liOperationPass"); });

        /* 为label绑定选中效果 */
        $(".ui-label").click(function () {
            $(this).siblings().removeClass("current");
            $(this).addClass("current");
        });

        /* 为一键办公按钮绑定方法 */
        $("#btnOneKey").click(function () {
            $("#divOaTab").hide();
            $("#divFastTab").show();
        });

        /* 为一键办公绑定选中效果 */
        $("#sOaWorkList").find("table").forEach(function (tbl) {
            $(tbl).click(function () {
                $(this).siblings().removeClass("current");
                $(this).addClass("current");
            });
        });

        /* 为常用意见按钮绑定弹出方法 */
        $("#btnCommonOpinion").click(function () {
            $("#divOaCommonOpinion").show();
            $("#divOaCommonOpinion").css("width", (clientWidth - 10) + "px");
            $("#divOaCommonOpinion").css("height", (clientHeight - 10 - headerHeight * 3) + "px");
            $("#divOaCommonOpinion").css("margin-left", 5 + "px");
            $("#divOaCommonOpinion").css("top", (5 + headerHeight) + "px");
            $("#divOaCommonOpinionContent").css("height", (clientHeight - 15 - headerHeight * 4) + "px");
            $("#liApprovalYourOpinion").css("overflow", "hidden");
        });

        /* 点击常用意见填充到意见框中 */
        $("#divOaCommonOpinionContent").find("a").forEach(function (element) {
            $(element).parent().click(function () {
                $("#txtOpinion")[0].value += $(element).text();
                $("#divOaCommonOpinion").hide();
                $("#liApprovalYourOpinion").css("overflow", "auto");
            });
        });

        /* 为常用意见窗口的关闭图标绑定方法 */
        $("#iOaCommonOpinionClose").bind("click touchend", function () {
            $("#divOaCommonOpinion").hide();
            $("#liApprovalYourOpinion").css("overflow", "auto");
        });

    });

    window.PdfViewer = function (divPdfContentId, count, baseUrl, bigPage) {
        this.count = typeof count === "number" ? count : parseInt(count);
        this.baseUrl = baseUrl;
        this.bigPage = bigPage;
        this.current = 0;
        this.clientWidth = document.documentElement.clientWidth || document.body.clientWidth;
        this.clientHeight = document.documentElement.clientHeight || document.body.clientHeight;
        this.headerHeight = getHeaderHeight(this.clientWidth);
        this.maxScrollUp = 0;
        this.maxScrollDown = 0;
        this.myScroll = undefined;
        this.init(divPdfContentId);
    }

    window.PdfViewer.prototype = {
        init: function (divPdfContentId) {
            var that = this;
            var html = '<div class="pdf-image"><div><img></div></div>';
            html += '<div class="pull-down"><span class="sp-pull-down">上一页</span></div>';
            html += '<div class="pull-up"><span class="sp-pull-up">下一页</span></div>';
            html += '<div class="page-nav-footer">第 <select></select> / <span></span> 页（<a target="_blank">点击查看大图</a>）</div>';
            $("#" + divPdfContentId).html(html);
            $(".pdf-image img").attr("src", that.baseUrl + "0.jpg");
            $(".page-nav-footer > a").attr("href", that.baseUrl + "0.jpg");
            for (var i = 1; i < that.count + 1; i++) {
                $(".oa-content-pdf select").append($("<option>").val(i - 1).text(i));
            }
            $(".page-nav-footer > select").bind("change", function () {
                that.toPage($(this).val());
            });
            $(".page-nav-footer > span").text(that.count);
            $(".page-nav-footer").css("width", that.clientWidth + "px");
            $(".page-nav-footer").css("top", (that.clientHeight - that.headerHeight * 2 - 40) + "px");
            $(".pull-down").css("width", (that.clientWidth) + "px");
            $(".pull-up").css("width", (that.clientWidth) + "px");
            $(".pull-up").css("top", (that.clientHeight - that.headerHeight * 2 - 30) + "px");
            that.myScroll = new IScroll(".pdf-image", {
                probeType: 3, mouseWheel: true
            });
            that.myScroll.on("scrollStart", function () {
                $(".page-nav-footer").hide();
            });
            that.myScroll.on("scroll", function () {
                if (this.y > that.maxScrollDown) that.maxScrollDown = this.y;
                if (this.y < that.maxScrollUp) that.maxScrollUp = this.y;
            });
            that.myScroll.on("scrollEnd", function () {
                if ((this.maxScrollY - that.maxScrollUp) > that.headerHeight)
                    that.toNextPage(); //上一页
                if (that.maxScrollDown > that.headerHeight)
                    that.toPrePage(); //下一页
                that.maxScrollDown = 0;
                that.maxScrollUp = 0;
                $(".page-nav-footer").show();
            });
            setTimeout(function () {
                var imgHeight = $(".pdf-image img").attr("height");
                if (imgHeight <= (that.clientHeight - that.headerHeight * 2))
                    $(".pdf-image > div").css("height", "101%");
                that.myScroll.refresh();
            }, 1000);
        },
        toPrePage: function () {
            if (this.current <= 0) return;
            this.current--;
            $(".page-nav-footer > select").val(this.current);
            $(".pdf-image img").attr("src", this.baseUrl + (this.current) + ".jpg");
            $(".page-nav-footer > a").attr("href", this.baseUrl + (this.current) + ".jpg");
            this.myScroll.scrollTo(0, 0);
        },
        toNextPage: function () {
            if (this.current >= (this.count - 1)) return;
            this.current++;
            $(".page-nav-footer > select").val(this.current);
            $(".pdf-image img").attr("src", this.baseUrl + (this.current) + ".jpg");
            $(".page-nav-footer > a").attr("href", this.baseUrl + (this.current) + ".jpg");
            this.myScroll.scrollTo(0, 0);
        },
        toPage: function (index) {
            if (index < 0) return;
            if (index > (this.count - 1)) return;
            this.current = index;
            $(".page-nav-footer > select").val(this.current);
            $(".pdf-image img").attr("src", this.baseUrl + (this.current) + ".jpg");
            $(".page-nav-footer > a").attr("href", this.baseUrl + (this.current) + ".jpg");
            this.myScroll.scrollTo(0, 0);
        }
    }

})(window, document, $);
