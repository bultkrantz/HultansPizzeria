
function myFunc(id) {
    $.get('Cart/Edit', { cartItemId: id }, function (data) {
        $('#cart-modal').html(data);
    });
}
