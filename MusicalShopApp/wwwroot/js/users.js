
$(document).ready(function () {
    //var forms = $('form.role-update-form');
    //forms.on('submit', function (e) {
    //    return false;
    //});
    var checkboxes = $('input.role-update-checkbox');
    checkboxes.on('change', function () {
        var form = this.parentNode;
        var serializedData = $(form).serialize();
        $.ajax({
            url: form.action,
            method: form.method,
            dataType: 'json',
            data: serializedData,
            success: function () {
                alert('success');
            },
            error: function (jqXHR) {
                alert(jqXHR.responseJSON.join('<br>'));
            }
        });
    });
});

