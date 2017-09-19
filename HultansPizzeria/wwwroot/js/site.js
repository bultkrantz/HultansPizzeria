// Write your JavaScript code.
$(document).ready(function () {
    $('ul.tabs').tabs({
        swipeable: true
    });
    $('.modal').modal();
    $('.collapsible').collapsible();
    $(".button-collapse").sideNav();
    $('.tooltipped').tooltip({ delay: 50 });
    $('.datepicker').pickadate({
        selectMonths: true, // Creates a dropdown to control month
        selectYears: 15, // Creates a dropdown of 15 years to control year,
        today: 'Today',
        clear: 'Clear',
        close: 'Ok',
        closeOnSelect: false // Close upon selecting a date,
    });
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
