window.isMobile = () => window.innerWidth < 768;
window.attachScrollEvent = function (elementRef, dotNetHelper) {
    if (!elementRef) return;
    elementRef.onscroll = function () {
        if (window.isMobile()) {
            var scrollTop = elementRef.scrollTop;
            var scrollHeight = elementRef.scrollHeight;
            var clientHeight = elementRef.clientHeight;
            if ((scrollTop + clientHeight) >= (scrollHeight - 100)) {
                dotNetHelper.invokeMethodAsync('OnScrolledToEnd');
            }
        }
    };
}
window.detachScrollEvent = function (elementRef) {
    if (elementRef) elementRef.onscroll = null;
}