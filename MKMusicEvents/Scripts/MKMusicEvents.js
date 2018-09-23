function ddlUser() {
    document.getElementById("myDropdown").classList.toggle("show");
}

window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {

        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}

//$(document).ready(function () {

//    //$('#userPopover').popover({
//    //    content: "<li><a href='/Manage/ChangePassword'>Change Password</a></li></br><li><a href='/Home/Favorites'>Favorites</a></li></br><li><a href='/Home/MyTickets'>My Tickets</a></li>"
//    //});
//});
