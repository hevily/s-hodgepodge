function handleTouchEvent(event) {
    if (event.touches.length === 1) {
        var output = document.getElementById("output");
        switch (event.type) {
            case "touchstart":
                startTouchX = event.changedTouches[0].clientX;
                startTouchY = event.changedTouches[0].clientY;
                break;
            case "touchend":
                startTouchX = -1;
                startTouchY = -1;
                break;
            case "touchmove":
                var nowX = event.changedTouches[0].clientX;
                var nowY = event.changedTouches[0].clientY;
                var screenX = window.screen.availWidth;
                var screenY = window.screen.availHeight;
                var pageEventValueX = screenX * 0.3;
                var pageEventValueY = screenX * 0.4;
                var moveFarX = nowX - startTouchX;
                var moveFarY = nowY - startTouchY;
                if (moveFarX < -pageEventValueX) { gotoOtherPage("lnkNext"); }
                else if (moveFarX > pageEventValueX) { gotoOtherPage("lnkPre"); }
                if (moveFarY > pageEventValueY) window.document.getElementById("btnFlesh").click();
                //window.document.getElementById("btnFlesh").click();
                break;
        }
    }
}
function gotoOtherPage(tag) {
    var linkButtons = window.document.getElementById("gridMyJob").getElementsByTagName("A");
    var destItem = null;
    for (var i = 0; i < linkButtons.length; i++) {
        if (linkButtons[i].id.indexOf(tag) > 0) {
            destItem = linkButtons[i];
            break;
        }
    }
    if (destItem == null) {
        if (tag === "lnkPre")
            window.location = "../mainpage.aspx";
        return;
    }
    var href = destItem.href;
    if (href == null || href === undefined || href === "") {
        if (tag === "lnkPre")
            window.location = "../mainpage.aspx";
        return;
    }
    href = href.replace("javascript:", "");
    eval(href);
}
var startTouchX = -1;
var startTouchY = -1;
document.addEventListener("touchstart", handleTouchEvent, false);
document.addEventListener("touchend", handleTouchEvent, false);
document.addEventListener("touchmove", handleTouchEvent, false);