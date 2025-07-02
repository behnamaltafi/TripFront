let charts = {};

export function createBarChart(canvasId, data, options) {
    // Destroy existing chart if exists
    if (charts[canvasId]) {
        charts[canvasId].destroy();
    }

    const ctx = document.getElementById(canvasId);
    if (!ctx) {
        console.error(`Canvas element with id '${canvasId}' not found`);
        return;
    }

    // Process callback functions
    if (options.scales?.y?.ticks?.callback) {
        const callbackStr = options.scales.y.ticks.callback.replace('@@', '');
        options.scales.y.ticks.callback = eval('(' + callbackStr + ')');
    }

    try {
        charts[canvasId] = new Chart(ctx, {
            type: 'bar',
            data: data,
            options: options
        });
    } catch (error) {
        console.error('Error creating chart:', error);
    }
}

export function destroyChart(canvasId) {
    if (charts[canvasId]) {
        charts[canvasId].destroy();
        delete charts[canvasId];
    }
}

// Also expose as global for fallback
window.chartHelper = {
    createBarChart,
    destroyChart
};