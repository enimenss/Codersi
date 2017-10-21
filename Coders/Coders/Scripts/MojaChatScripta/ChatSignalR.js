
    $(function () {

        //var friendObj = @Html.Raw(Json.Encode(friendObj));
        //var Me =@Html.Raw(Json.Encode(me) );



        var myHub = $.connection.CodersHub;
        myHub.client.Proba = function ()
          {
            $('#aa').append("<p>Radii</p>");
        }

        myHub.client.primiPoruku = function (message,sender,slika, datum,senderName) {

            if (sender === Identity) {
                mojaPoruka(message, sender, slika, datum);
                return;
            }

            var upisiPoruku = $("#group");

            var dete = upisiPoruku.children().last();
            var predhodnik = dete.prev();
            // var count = $(predhodnik[0]).children().length;
            console.log(sender);
            console.log(predhodnik[0]);
            var is = $(predhodnik[0]).hasClass(sender);
            var skloni = $(predhodnik[0]).children().first();
            if (!is) {
                upisiPoruku.append("<div class='"+sender+"' style='margin-left:5%;margin-top:0.5%;margin-bottom:0.5%;float:left;width:55%;text-align:left'>" +
                    "<span style='float:left;width:100%;word-wrap: break-word;color:gray;font-size:24px'><span style='float:left;height:1px;width:38px;margin-right:3%'></span><span>"+senderName+" </span></span>" +
                    " </div>");
            }
            if (is) {
                skloni.remove();
                predhodnik.prepend("<span style='float:left;margin-right:3%;width:38px;height:1px'></span>");
            }
          

            upisiPoruku.append("<div class='"+sender+"' style='margin-left:5%;margin-top:0.5%;margin-bottom:0.5%;float:left;width:55%'>" +
                "<img src='" + ServerAddress + "Images/" + slika + "' class='slicka' style='float:left;margin-right:3%' width='38' height='38' />" +
                "<span class='round-corner tooltipR' style='float:left;max-width:70%;word-wrap: break-word;background-color:dimgrey;color:white;font-size:20px'>" + htmlEncode(message) + " <span class='tooltiptextR'>" + datum + "</span></span></div><div style='width:100%;float:left'></div>");
            
            if (document.getElementsByClassName('chatProba').length !== 0) {
                $(".chatProba").animate({ scrollTop: document.getElementsByClassName('chatProba')[0].scrollHeight }, "fast");
            }
        }

           mojaPoruka = function (message,sender,slika,datum) {

            var upisiPoruku = $("#group");
          
            var type = $("#typping");
            var typeCount = type.length;
            if (typeCount > 0) {
                type.next().remove();
                StopTypingAnimation();
                type.remove();
            }

            var dete = upisiPoruku.children().last();
            var predhodnik = dete.prev();
   
            var is = $(predhodnik[0]).hasClass(sender);
            var skloni = $(predhodnik[0]).children().first();

            //var okvirZaPorukuiSeen = $(predhodnik[0]).children().last();
            //var malaSlicka = okvirZaPorukuiSeen.children().last();

            //var seen = malaSlicka.hasClass("myseen");

            if (is) {
                skloni.remove();
                predhodnik.prepend("<span style='float:right;margin-left:3%;width:38px;height:1px'></span>");
                
            }
           
            upisiPoruku.append("<div value='0' class='"+sender+"' style='margin-right:5%;margin-top:0.5%;margin-bottom:0.5%;float:right;width:55%'>" +
                "<img src='" + ServerAddress + "Images/" + slika + "' class='slicka' style='float:right;margin-left:3%' width='38' height='38' />" +
                "<div style='width:70%;float:right'>" +
                "<div class='round-corner send tooltipR' style='float:right;word-wrap:break-word;background-color:blue;color:white;max-width:100%;text-align:right;font-size:20px'>" + message + "<span class='tooltiptextR'>" + datum + "</span></div>" +
                "</div></div><div style='width:100%;float:left'></div>");

            if (typeCount > 0) {

                upisiPoruku.append("<div id='typping' class='recv' style='margin-left:5%;margin-top:0.5%;margin-bottom:0.5%;float:left;width:55%'>" +
                    "<img src='" + ServerAddress + "Images/" + slika + "' class='slicka' style='float:left;margin-right:3%' width='38' height='38' />" +
                    "<span id='type' class='round-corner tooltipR' style='float:left;max-width:70%;word-wrap: break-word;background-color:dimgrey;color:white;font-size:20px'></span></div><div style='width:100%;float:left'></div>");
                RunTypingAnimation();
                if (document.getElementsByClassName('chatProba').length !== 0) {
                    $(".chatProba").animate({ scrollTop: document.getElementsByClassName('chatProba')[0].scrollHeight }, "fast");
                }
            }

            if (document.getElementsByClassName('chatProba').length !== 0) {
                $(".chatProba").animate({ scrollTop: document.getElementsByClassName('chatProba')[0].scrollHeight }, "fast");
            }
        }





           myHub.client.typping = function (sender, slika) {
               if (sender === Identity) {
                   return;
               }
               typpingCount = typpingCount + 1;
               var type = $("#typping");
               var typeCount = type.length;
               if (typeCount > 0) {
                   return;
               }
           
            var upisiPoruku = $("#group");
            if (upisiPoruku.length <= 0) {
                console.log("ne postoji");
                return;
            }

            var dete = upisiPoruku.children().last();
            var predhodnik = dete.prev();
            //var is = $(predhodnik[0]).hasClass("recv");
            //var skloni = $(predhodnik[0]).children().first();
            //if (is) {
            //    skloni.remove();
            //    predhodnik.prepend("<span style='float:left;margin-right:3%;width:38px;height:1px'></span>");
            //}


            upisiPoruku.append("<div id='typping' class='"+sender+"' style='margin-left:5%;margin-top:0.5%;margin-bottom:0.5%;float:left;width:55%'>" +
                "<img src='" + ServerAddress + "Images/" + slika + "' class='slicka' style='float:left;margin-right:3%' width='38' height='38' />" +
                "<span id='type' class='round-corner tooltipR' style='float:left;max-width:70%;word-wrap: break-word;background-color:dimgrey;color:white;font-size:20px'></span></div>");
            RunTypingAnimation();
            if (document.getElementsByClassName('chatProba').length !== 0) {
                $(".chatProba").animate({ scrollTop: document.getElementsByClassName('chatProba')[0].scrollHeight }, "fast");
            }

        }

           myHub.client.untypping = function (sender) {
               if (sender === Identity) {
                   return;
               }

               typpingCount = typpingCount - 1;

         
            var upisiPoruku = $("#group");
            if (upisiPoruku.length <= 0) {
                console.log("ne postoji");
                return;
            }
            if (typpingCount <= 0) {
                StopTypingAnimation();
                $("#typping").remove();
                typpingCount =0;
            }

            if (document.getElementsByClassName('chatProba').length !== 0) {
                $(".chatProba").animate({ scrollTop: document.getElementsByClassName('chatProba')[0].scrollHeight }, "fast");
            }


        }


           myHub.client.onlineSam = function (name) {

            var update = $("#" + name);
            var dete = update.children().last();
            dete.remove();
            update.append("<td width='20px'><div style='float:left;margin-top:80%' class='circle'> </div></td>");
        }

           myHub.client.offSam = function (name) {

            var update = $("#" + name);
            var dete = update.children().last();
            dete.remove();
            update.append("<td width='20px'><div style='float:left;margin-top:80%' class='circleOff'> </div></td>");
        }

           myHub.client.malaSlicka = function (name) {

            var upisiSeen = $("#" + name + 'm');
            if (upisiSeen.length <= 0) {
                console.log("ne postoji");
                return;
            }
            var dete = upisiSeen.children().last();
            var predhodnik = dete.prev();  //treba se tu setuje i value na 1 ako zatreba nekad
            // var count = $(predhodnik[0]).children().length;
            var is = $(predhodnik[0]).hasClass("send");


            var okvirZaPorukuiSeen = $(predhodnik[0]).children().last();
            var malaSlicka = okvirZaPorukuiSeen.children().last();

            var seen = malaSlicka.hasClass("myseen");

            if (is && !seen) {
                var izbrisiSeen = $(".myseen");
                izbrisiSeen.remove();
                console.log("aaaaa");
                okvirZaPorukuiSeen.append("<p class='myseen' style='float:right;width:100%'><span class='tooltipS' style='float:right'><img src='" + ServerAddress + "Images/" + friendObj.Avatar + "' class='slicka' style='float:right' width='24' height='24' /><span class='tooltiptextS'>Seen !!!</span></span></p>");
                if (document.getElementsByClassName('chatProba').length !== 0) {
                    $(".chatProba").animate({ scrollTop: document.getElementsByClassName('chatProba')[0].scrollHeight }, "fast");
                }
            }
        }


        $.connection.hub.qs = { "GUID": Identity };
        $.connection.hub.start().done(function () {

            $('#posalji').keypress(function (e) {
                var key = e.which;
                if (key === 13) {
                    onBlur(Identity);
                    console.log("omg");
                    myHub.server.posaljiPoruku($('#posalji').val());
                    $('#posalji').val('');

                }
            });

            //$('#seen').click(function () {
            //    //var cale = $("#" + friendObj.Username + 'm');

            //    //var dete = cale.children().last();
            //    //var predhodnik = dete.prev();
            //    //var is = $(predhodnik[0]).hasClass("recv");
            //    //var skloni = $(predhodnik[0]).children().first();
            //    //if (is) {
            //    //    var val = (predhodnik[0]).getAttribute('value');
            //    //    if (val == 1) {
            //    //        return;
            //    //    }
            //    jcs.server.seen(friendObj.Username);
            //    //}

            //});

            onFocus = function () {
                console.log("focus");
                myHub.server.typping(Identity);
            }

            onBlur = function () {
                console.log("blur");
                myHub.server.untypping(Identity);
            }

            var seen = function () {
                console.log("uso");
            }

            var RefreshRate = 1000;//1 second

            
           
            updateMyPositionInGroup=function(position) {

                console.log(position);
                myHub.server.changeLocation(position.coords.latitude, position.coords.longitude);
            }


        });

    });

    $(".chatProba").animate({ scrollTop: document.getElementsByClassName('chatProba')[0].scrollHeight }, "fast");

    function htmlEncode(value) {
        var encodedValue = $('<div />').text(value).html();
        return encodedValue;
    }

refreshPosition=function() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(updateMyPositionInGroup);
    }
    else {
        alert("enable Location and refresh");
    }
}
    setInterval(refreshPosition, 1000);