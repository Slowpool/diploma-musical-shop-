//import "jquery.maskedinput.min.js"
//import "jquery.min.js"
//$('.mask-phone').mask('+7 (999) 999-99-99');

//$(document).ready(function () {
//    $('table.all-users tr').click(function () {
//        window.location = $(this).attr('href');
//        return false;
//    });
//});

$(document).ready(function () {
    $('form').attr('autocomplete', 'off');
});
// okey, i did my best. this stuff doesn't work and i don't know why. i should improve my js skills to understand this thing, but i don't want to teach js. what should i do, hmm. 7 seconds after writing down these comments: GOT IT. it works. inappropriate way. now i realize why ids are mandatory here. hehe. fun fact: it'll be in my diploma?? i don't want to rub off all my comments with thoughts, so looks like it'll.
var checkLists = document.getElementsByClassName('dropdown-check-list');
for (checkList of checkLists) {
    checkList.getElementsByClassName('anchor')[0].onclick = function (evt) {
        //alert('works');
        if (checkList.classList.contains('visible')) {
            checkList.classList.remove('visible');
        }
        else {
            checkList.classList.add('visible');
        }
    }
}