$(document).ready(function () {
    $('.add-remove-cart-button').on('click', function (e) {
        button = e.target;
        markSwitched(button);
        form = button.closest('form');
        input = form.children[2];
        input.dataset.value = input.dataset.value == 'true' ? 'false' : 'true';
        //form.submit();
    });
});

function markSwitched(button) {
    button.innerHTML = button.innerHTML == '+' ? '-' : '+';
}