
$(document).ready(function () {
    $('table.all-users tr').click(function () {
        window.location = $(this).attr('href');
        return false;
    });
});