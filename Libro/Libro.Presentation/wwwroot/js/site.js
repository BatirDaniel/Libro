//function for displaying all toasts
const showToast = (svg, message) => {
    var toastContainer = document.getElementById("toast-container");

    if (svg && message) {
        var toastContainer = document.getElementById("toast-container");

        var toastHtml = `
            <div id="toast-default" class="flex items-center w-full max-w-sm p-4 text-gray-500 bg-white rounded-lg shadow dark:text-gray-400 dark:bg-gray-800" role="alert">
                <div class="flex items-center justify-center space-x-4">
                    <div class="inline-flex items-center justify-center flex-shrink-0 w-8 h-8 text-blue-500 bg-blue-100 rounded-lg dark:bg-blue-800 dark:text-blue-200">
                        ${svg}
                    </div>
                    <div class="text-sm font-normal">${message}</div>
                </div>
                <button id='closeToast' type="button" class="ml-auto -mx-1.5 -my-1.5 bg-white text-gray-400 hover:text-gray-900 rounded-lg focus:ring-2 focus:ring-gray-300 p-1.5 hover:bg-gray-100 inline-flex items-center justify-center h-8 w-8 dark:text-gray-500 dark:hover:text-white dark:bg-gray-800 dark:hover:bg-gray-700" data-dismiss-target="#toast-default" aria-label="Close">
                    <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
                        <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6" />
                    </svg>
                </button>
            </div>
        `;

        toastContainer.innerHTML = toastHtml;

        setTimeout(() => {
            toastContainer.innerHTML = "";
        }, 4000);

        $('#closeToast').on('click', function () {
            toastContainer.innerHTML = ""
        })
    }
}

function spaLoad(action, type, data, pushHistory) {
    abortAjax();
    $.ajax({
        url: action,
        type: type,
        dataType: 'html',
    }).done(function (response, status, xhr) {
        renderPage(xhr, pushHistory);
    }).fail(function (xhr) {
        // unauthorized or access denied
        if (xhr.status === 401 || xhr.status === 403) {
            return;
        }

        // ignore aborted requests
        if (xhr.status === 0 || xhr.readyState === 0) {
            return;
        }
        // mark status as handled/aborted
        xhr.status = 0;
        renderPage(xhr, pushHistory);
    });
}

function renderPage(xhr, pushHistory) {
    // clear/reset js state
    clearIntervals();
    resetGlobalVariables();

    if (pushHistory == false || pushHistory == true) {
        // location will be null by custom asp.net error page
        history.pushState(pushHistory);
    }

    // load document
    if (xhr.responseText.startsWith('<!DOCTYPE html>')) {
        // whole document is refreshed
        var newDocument = document.open('text/html');
        newDocument.write(xhr.responseText);
        newDocument.close();
    } else {
        $('#spa-content').html(xhr.responseText);
        // init new page
        initControls($('#spa-content'));
    }
}


// history management
$(function () {
    window.onpopstate = function () {
        var action = window.location.pathname + window.location.search;
        spaLoad(action, "GET", null, "/errors/404");
    };

    $(document).ajaxSend(function (e, xhr) {
        xhrPool.push(xhr);
    });
    $(document).ajaxComplete(function (e, xhr) {
        xhrPool = $.grep(xhrPool, function (x) { return x != xhr; });
    });
});

var xhrPool = [];
var abortAjax = function () {
    $.each(xhrPool, function (idx, xhr) {
        xhr.abort();
    });
};

function resetGlobalVariables() {
    window.clickCount = 0;
    window.times = [];
}

function clearIntervals() {
    for (var i = 0; i < window.intervals.length; i++) {
        window.clearInterval(window.intervals[i]);
    }
    window.intervalsintervals = [];
}

function setHistory(term) {
    switch (term) {
        case "/dashboard":
            history.pushState(null, null, term);
            break;
        case "/administration/users":
            history.pushState(null, null, term);
            break;
        case "/pos":
            history.pushState(null, null, term);
            break;
        case "/issues/add":
            history.pushState(null, null, term);
            break;
        case "/issues/view":
            history.pushState(null, null, term);
            break;
        case "":
            history.pushState(null, null, term);
            break;
    }
}