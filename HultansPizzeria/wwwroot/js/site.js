// Write your JavaScript code.
$(document).ready(function () {
    $('ul.tabs').tabs({
        swipeable: true
    });
    $('.modal').modal();
    $('.collapsible').collapsible();
    $('.cart-sidenav').sideNav({
        menuWidth: 300, // Default is 300
        edge: 'right', // Choose the horizontal origin
        closeOnClick: false, // Closes side-nav on <a> clicks, useful for Angular/Meteor
        draggable: true, // Choose whether you can drag to open on touch screens,
    });
    $('.tooltipped').tooltip({ delay: 50 });

});

//Sticky navigation 
var mn = $(".main-nav");
var m = $(".page-content");
mns = "main-nav-scrolled";
mmt = "main-margin-top";
hdr = $('.header').height();


$(window).scroll(function () {
    if ($(this).scrollTop() > hdr) {
        mn.addClass(mns);
        m.addClass(mmt);
    } else {
        mn.removeClass(mns);
        m.removeClass(mmt);
    }
});
