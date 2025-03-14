$(document).ready(function () {
    $('.add-remove-cart-button').on('click', function (e) {
        button = e.target;
        markSwitched(button);
        form = button.closest('form');
        input = form.querySelector('[name="isInCart"]');
        input.value = input.value == 'true' ? 'false' : 'true';
        // TODO handle form submit instead of on button click
    });
});

function markSwitched(button) {
    button.innerHTML = button.innerHTML == '+' ? '-' : '+';
}