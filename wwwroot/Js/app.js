window.isMobile = () => window.innerWidth < 768;
window.detachScrollEvent = function (elementRef) {
    if (elementRef) elementRef.onscroll = null;
}
window.expenseScrollHandler = {
    attach: function (element, dotNetRef) {
        if (!element) return;

        const scrollHandler = function () {
            const scrollTop = element.scrollTop;
            const scrollHeight = element.scrollHeight;
            const clientHeight = element.clientHeight;

            // اگر 100px به انتها رسید
            if (scrollTop + clientHeight >= scrollHeight - 100) {
                dotNetRef.invokeMethodAsync('OnScrolledToEnd');
            }
        };

        element.addEventListener('scroll', scrollHandler);
        element._scrollHandler = scrollHandler;
        element._dotNetRef = dotNetRef;
    },

    detach: function (element) {
        if (!element || !element._scrollHandler) return;

        element.removeEventListener('scroll', element._scrollHandler);
        delete element._scrollHandler;
        delete element._dotNetRef;
    }
};