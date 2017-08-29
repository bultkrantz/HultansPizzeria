// Write your JavaScript code.
$(document).ready(function () {
    $('ul.tabs').tabs({
        swipeable: true
    });
    $('.tooltipped').tooltip({ delay: 50 });
    $('.collapsible').collapsible();
    $('.cart-sidenav').sideNav({
        menuWidth: 300, // Default is 300
        edge: 'right', // Choose the horizontal origin
        closeOnClick: false, // Closes side-nav on <a> clicks, useful for Angular/Meteor
        draggable: true, // Choose whether you can drag to open on touch screens,
    });
 
});

var addToCart = function (dishId) {
    var container = $('#cart-cortainer');
    $.get("/Cart/Add", { dishId: dishId }, function (data) { container.html(data); });
};


if ($('#success-message').length) {

    $('#success-message').animate({
        opacity: 1,
        left: '50px',
        top: '80px'
    });

    setTimeout(function () {
        $('#success-message').animate({
            opacity: 0,
            left: '-50px',
            top: '-50px'
        });
    }, 2000);

}