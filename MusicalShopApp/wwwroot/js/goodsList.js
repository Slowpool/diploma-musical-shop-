$(document).ready(function () {
    $('form.mini-card-add-remove').on('submit', function () {
        var button = this.querySelector('.add-remove-cart-button');
        var resultsDiv = this.querySelector('.adding-removing-results');
        var isInCartInput = this.querySelector('[name="isInCart"]');
        $.ajax(
            {
                url: '/Goods/AddToOrRemoveFromCart',
                method: 'post',
                dataType: 'text',
                data: $(this).serialize(),
                success: function (result) {
                    markSwitched(button);
                    var newIsInCart = isInCartInput.value == 'true' ? false : true; // js workaround
                    isInCartInput.value = newIsInCart;
                    resultsDiv.innerHTML = newIsInCart ? 'Added to cart' : 'Removed from cart';
                },
                error: function (result) {
                    alert('fail: ' + result);
                }
            }
        )
        return false;
    });
});

function markSwitched(button) {
    button.innerHTML = button.innerHTML == '+' ? '-' : '+';
}