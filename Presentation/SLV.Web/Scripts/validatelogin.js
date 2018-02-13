var value = getParameterByName('value');

if (value == 'validate') {
    deactivateTabIfPasswordNotValidate();
}
function deactivateTabIfPasswordNotValidate() {
    $(".nav-tabs li:nth-child(1)").attr('aria-expanded', false);
    $(".nav-tabs li:nth-child(1)").removeClass('active');
    $(".nav-tabs li:nth-child(1)").css('pointer-events', 'none');
    $(".tab-content div:nth-child(1)").removeClass('active');


    $(".nav-tabs li:nth-child(2)").attr('aria-expanded', false);
    $(".nav-tabs li:nth-child(2)").removeClass('active');
    $(".nav-tabs li:nth-child(2)").css('pointer-events', 'none');
    $(".tab-content div:nth-child(2)").removeClass('active');

    $(".nav-tabs li:nth-child(4)").attr('aria-expanded', false);
    $(".nav-tabs li:nth-child(4)").removeClass('active');
    $(".nav-tabs li:nth-child(4)").css('pointer-events', 'none');
    $(".tab-content div:nth-child(4)").removeClass('active');

    $(".nav-tabs li:nth-child(3)").attr('aria-expanded', true);
    $(".nav-tabs li:nth-child(3)").addClass('active');
    $(".tab-content div:nth-child(3)").addClass('active');
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)", "i"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}