
function myFunc(id) {
    $.get('Cart/Edit', { cartItemId: id }, function (data) {
        $('#cart-modal').html(data);
    });
}

function toastAdd(name) {
        Materialize.toast('1 st ' + name + ' lades till i varukorgen', 3000);
};

function toastRemove(name) {
    Materialize.toast(name + ' togs bort från varukorgen', 3000);
};
