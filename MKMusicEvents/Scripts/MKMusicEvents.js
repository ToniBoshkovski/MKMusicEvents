﻿(function (m, $) {
    "use strict";

    $('[data-favorite-id="True"]').css('color', 'red');

    $('#ddlLoggedInUserButton').click(function () {
        document.getElementById("ddlLoggedUserContent").classList.toggle("show");
    });

    var onClickWindow = m.onclick;
    onClickWindow = function (event) {
        if (!event.target.matches('.dropdownLoggedUserButton')) {

            var dropdowns = document.getElementsByClassName("dropdownLoggedUser-content");
            var i;
            for (i = 0; i < dropdowns.length; i++) {
                var openDropdown = dropdowns[i];
                if (openDropdown.classList.contains('show')) {
                    openDropdown.classList.remove('show');
                }
            }
        }
    }

    m.InitIndexPage = function () {

        $('.btn-create').click(function (event) {
            event.preventDefault();
            $.get(this.href, function (response) {
                $('.divForAdd').html(response);
            });
            $('#AddModal').modal({
                backdrop: 'static',
            }, 'show');
        });

        $('.btn-edit').click(function (event) {
            event.preventDefault();
            $.get(this.href, function (response) {
                $('.divForUpdate').html(response);
            });
            $('#EditModal').modal({
                backdrop: 'static',
            }, 'show');
        });

        $('.btn-buy').click(function (event) {
            event.preventDefault();
            $.get(this.href, function (response) {
                $('.divForBuy').html(response);
            });
            $('#BuyModal').modal({
                backdrop: 'static',
            }, 'show');
        });

        $(".btn-favorite").click(function () {
            var eventId = parseInt($(this).data("id"));
            var isFavorite = $(this);
            MusicEvents.FavoriteButtonAjaxMethod(eventId, isFavorite);
        });

        $('.rating .fa').mouseover(function () {
            var onStar = parseInt($(this).data('id'), 10);

            $(this).parent().children('.fa').each(function (e) {
                if (m.IsAdmin != 2) {
                    if (e < onStar) {
                        $(this).removeClass('fa-star-o').addClass('fa-star');
                    }
                    else {
                        $(this).removeClass('fa-star').addClass('fa-star-o');
                    }
                }
            });
        }).mouseout(function () {
            MusicEvents.ResetStarRatingOnMouseOut($(this).parent());
        });

        $('.rating .fa').click(function () {
            var eventId = parseInt($(this).data("eventid"))
            var onStar = parseInt($(this).data('id'), 10);
            var stars = $(this).parent().children(".fa");
            var rating = $(this).data('id');

            if (m.IsAdmin != 2) {
                for (var i = 0; i < stars.length; i++) {
                    $(stars[i]).removeClass('fa-star').addClass('fa-star-o');
                }
                for (var i = 0; i < onStar; i++) {
                    $(stars[i]).removeClass('fa-star-o').addClass('fa-star');
                }
            }

            MusicEvents.RatingStarsAjaxMethod(eventId, rating);
        });

        m.FavoriteButtonAjaxMethod = function (eventId, isFavorite) {
            $.ajax({
                type: "POST",
                url: window.location.origin + "/Home/AddToFavorites",
                data: { "id": eventId },
                dataType: "json",
                success: function (response) {
                    if (response.ErrorCode == 0) {
                        isFavorite.css("color", "red");
                    }
                    else if (response.ErrorCode == 100) {
                        isFavorite.css("color", "");
                    }
                    else {
                        alert(response.Message);
                    }
                },
                failure: function (response) {
                    alert(response.Message);
                },
                error: function (response) {
                    alert(response.Message);
                }
            });
        };

        m.RatingStarsAjaxMethod = function (eventId, rating) {
            $.ajax({
                type: "POST",
                url: window.location.origin + "/Home/AddToRating",
                data: { "id": eventId, "rating": rating },
                dataType: "json",
                success: function (response) {
                    if (response.ResponseCode == 200 || response.ResponseCode == 100) {
                        $('.' + eventId).text("Rating: " + response.ResponseRatingGrade + "/5");
                        $(".rating").attr('data-ratingId', rating);
                        MusicEvents.ResetStarRatingOnMouseOut($("#rating-" + eventId));
                    }
                    else {
                        alert(response.ResponseMessage);
                    }
                },
                failure: function (response) {
                    alert(response.Message);
                },
                error: function (response) {
                    alert(response.Message);
                }
            });
        };

        m.ResetStarRatingOnMouseOut = function (parent) {
            var userEventRating = parent.attr('data-ratingId');
            parent.children('.fa').each(function (index, value) {
                if (m.IsAdmin != 2) {
                    if (index < userEventRating) {
                        $(value).removeClass('fa-star-o').addClass('fa-star');
                    }
                    else {
                        $(value).removeClass('fa-star').addClass('fa-star-o');
                    }
                }
            });
        };
    };

    m.IsAdminShowButtons = function () {

        if (m.IsAdmin == 1) {
            $('.btn-buy').addClass('btnBuyDisabled');
            $('.btn-create').show();
            $('.btn-edit').show();
            $('.btn-delete').show();
            $('.btn-favorite').hide();
            $('.ratingClass').remove();

        } else {
            if (m.IsAdmin == 2) {
                $('.btn-buy').addClass('btnBuyDisabled');
            }

            $('.btn-create').hide();
            $('.btn-edit').hide();
            $('.btn-delete').hide();
            $('.btn-favorite').show();
        }
    };

    m.IsAdminDropdownButtons = function () {
        if (m.IsAdmin == 1) {
            $('.DropdownFavoritesButton').remove();
            $('.DropdownMyTicketsButton').remove();
        }
    };

    m.FavoriteCheckboxEvent = function (listOfEvents) {

        $('.checkbox').click(function () {
            if ($(this).hasClass('favoriteEventCheckbox')) {
                $(this).removeClass('favoriteEventCheckbox');
                for(var i=0; i< listOfEvents.length; i++){
                    if (listOfEvents[i] == $(this).parent().parent().attr("data-event-id")) {
                        listOfEvents.splice(i, 1);
                        console.log(listOfEvents);
                    }
                }
            }
            else {
                $(this).addClass('favoriteEventCheckbox');
                listOfEvents.push($(this).parent().parent().attr("data-event-id"));
                console.log(listOfEvents);
            }
        });

        m.FavoritesButtons = function (listOfEvents) {
            $('.btn-favorite-delete').click(function () {
                if (listOfEvents != 0) {
                    MusicEvents.FavoritesDeleteButtonAjaxMethod(listOfEvents);
                }
            });

            $('btn-favorite-buy').click(function () {
                if (listOfEvents != 0) {
                    MusicEvents.FavoritesBuyButtonAjaxMethod(listOfEvents);
                }
            });
        };

        m.FavoritesDeleteButtonAjaxMethod = function (listOfEvents) {
            $.ajax({
                type: "POST",
                url: window.location.origin + "/Home/FavoritesDeleteEvent",
                data: { "id": listOfEvents },
                dataType: "json",
                success: function (response) {
                    for (var i = 0; i < listOfEvents.length; i++) {
                        document.querySelector('[data-event-id="' + listOfEvents[i] + '"]').remove();
                    }
                },
                failure: function (response) {
                    alert(response.Message);
                },
                error: function (response) {
                    alert(response.Message);
                }
            });

        };

        m.FavoritesBuyButtonAjaxMethod = function (listOfEvents) {
            
        };
    };

    m.DataTable = function () {
        $('#MyTicketsTable').DataTable();

        $('.dataTables_filter').hide();
        var table = $('#MyTicketsTable').DataTable();
        $('#ticketsSearch').keyup(function () {
            table.search($(this).val()).draw();
        })
    };

    m.FooterFixed = function () {
            function setBodyMinHeight() {
                $('.body-content').css('minHeight', window.innerHeight - $('.navbar').height() - $('#footer').height());
            }
            setBodyMinHeight();

            window.onresize = function () {
                setBodyMinHeight();
            }
    };

}(window.MusicEvents = window.MusicEvents || {}, jQuery));