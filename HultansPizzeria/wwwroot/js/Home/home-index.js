﻿
function getModifyModal(id) {
    $.get('Cart/Edit', { cartItemId: id }, function (data) {
        $('#cart-modal').html(data);
    });
}

function toastAdd(name) {
        Materialize.toast('1 st ' + name + ' lades till i varukorgen', 3000);
};

function toastRemove(name) {
    $('.tooltipped').tooltip("close");
    Materialize.toast(name + ' togs bort från varukorgen', 3000);
};

function toastEdit() {
    Materialize.toast('Ändringar sparade', 3000);
};
