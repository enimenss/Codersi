

var mapProp = null;
var mapCenterX = 43.3194;
var mapCenterY = 21.8963;
var map = null;

var mapType = null;

var mapZoom = 15;

var myPositionX = null;
var myPositionY = null;

var minSeparation = 5;
var MyIcon = "/Images/aca.jpg";
var TeamMateIcon = "/Images/TeamMatePosition.png";
var ImageLocation = ServerAddres+ "Images/";
var Markers = new Array();
var Info = new FormData();






CreateMap = function (position) {
    mapCenterX = position.coords.latitude;
    mapCenterY = position.coords.longitude;
    mapType = google.maps.MapTypeId.HYBRID;
    var element = document.getElementById("mapPlace");
    mapProp = { center: new google.maps.LatLng(mapCenterX, mapCenterY), zoom: mapZoom, mapTypeId: mapType, overviewMapControl: true };
    map = new google.maps.Map(element, mapProp);
}

UpdateMyPosition = function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(SetPosition);
    }
    else {
        alert("enable Location and refresh");
    }
}






PopulateTeamData = function () {
    $.ajax({
        url: ServerAddres + "Home/GetGroup",//add Controler Name,
        type: "POST",
        dataType: 'json',
        cache: false,
        success: function (result) {
            console.log(result);
            ReciveTeamData(result);
        },
        error: function (err) {
            alert("Error! Refresh");
        }
    });
}
function ReciveTeamData(array) {
    for (var i = 0; i < array.length; i++) {
        TeamData[array[i].Guidd]= array[i];
        console.log(TeamData[array[i].Guidd]);

    }
    UpadateMap();
}

UpadateMap = function () {
    for (var i in TeamData) {
        if (myIndetification == i.Guidd) continue;
        console.log(i.Guidd);
        if (i.PosX == null || i.PosY == null) continue;
        console.log(ImageLocation);
        setTeamMatePosition(i.Guidd, i.PosX, i.PosY, i.Picture);
    }
}
function SetPosition(position) {

    console.log(TeamData);
    if (myPositionX == null || myPositionX == undefined || myPositionY == null || myPositionY == undefined) {
        // console.log(position.coords.latitude);
        myPositionX = position.coords.latitude;
        myPositionY = position.coords.longitude;
        TeamData[myIndetification].PosX = position.coords.latitude;
        TeamData[myIndetification].PosY = position.coords.longitude;
        setMyMarker();

        return;
    }
    var x = getDistanceFromLatLonInKm(myPositionX, myPositionY, position.coords.latitude, position.coords.longitude) * 1000;
    console.log(x);
    if (x > minSeparation) {
        myPositionX = position.coords.latitude;
        myPositionY = position.coords.longitude;
        TeamData[myIndetification].PosX = position.coords.latitude;
        TeamData[myIndetification].PosY = position.coords.longitude;
        console.log(position.coords.latitude + "," + position.coords.longitude);
        setMyMarker();

    }

}
setTeamMatePosition = function (Guidd, x, y, Picture) {
    console.log(Picture);
    var marker = Markers[Guidd];
    if (marker == null) {
        var marker = new CustomMarker(new google.maps.LatLng(x, y), map, ImageLocation+ Picture);
        Markers[Guidd]= marker;

        (function (marker, element) {
            google.maps.event.addListener(marker, "click", function (e) {
                var infoWindow = Info[element.Guidd];
                if (infoWindow == null || infoWindow === undefined) {
                    var infoWindow = new google.maps.InfoWindow({
                        content: element.Username,
                        pixelOffset: new google.maps.Size(9, 0)
                    });
                    Info[element.Guidd] = infoWindow;
                    infoWindow.open(map, marker);
                }
                else {
                    infoWindow.close();
                    Info[element.Guidd] = null; 
                }
                
            });
        })(marker, TeamData[Guidd]);
    }
    else {
        console.log(marker);

        marker.setPosition(new google.maps.LatLng(x, y));
        marker.setMap(map);
        Markers[Guidd]= marker;
    }
}

function setMyMarker() {
    var marker = Markers[myIndetification];

    if (marker == null) {
        var marker = new CustomMarker(new google.maps.LatLng(myPositionX, myPositionY), map, ImageLocation + TeamData[myIndetification].Picture);
        Markers[myIndetification]=marker;
    }
    else {
        marker.setPosition(new google.maps.LatLng(myPositionX, myPositionY))
        marker.setMap(map);
        Markers[myIndetification]=marker;
    }
    (function (marker) {
        google.maps.event.addListener(marker, "click", function (e) {
            if (Info[myIndetification] == null || Info[myIndetification] === undefined)
            {
                var infowindow = new google.maps.InfoWindow({
                    content: TeamData[myIndetification].Username,
                    pixelOffset: new google.maps.Size(9, 0)
                });
                console.log(infowindow);
                infowindow.open(map, marker);
                Info[myIndetification] = infowindow;
            }
            else {
                Info[myIndetification].close();
                Info[myIndetification] = null;
            }
        });
    })(marker);
}

function getDistanceFromLatLonInKm(x1, y1, x2, y2) {
    var R = 6371;
    var dLat = deg2rad(x2 - x1);
    var dLon = deg2rad(y2 - y1);
    var a =
        Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.cos(deg2rad(x1)) * Math.cos(deg2rad(x2)) *
        Math.sin(dLon / 2) * Math.sin(dLon / 2)
        ;
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c;
    return d;
}

function deg2rad(deg) {
    return deg * (Math.PI / 180)
}

function CustomMarker(latlng, map3, imageSrc) {
    console.log(imageSrc);
    this.latlng_ = latlng;
    this.imageSrc = imageSrc;
    this.setMap(map3);
}

CustomMarker.prototype = new google.maps.OverlayView();

CustomMarker.prototype.draw = function () {
    var div = this.div_;
    if (!div) {
        div = this.div_ = document.createElement('div');

        div.className = "customMarker"


        var img = document.createElement("img");
        img.src = this.imageSrc;

        div.appendChild(img);
        var x = this;
        google.maps.event.addDomListener(div, "click", function (event) {
            console.log(x);
            google.maps.event.trigger(x, "click");
        });


        var panes = this.getPanes();
        panes.overlayImage.appendChild(div);
    }

    var point = this.getProjection().fromLatLngToDivPixel(this.latlng_);
    if (point) {
        div.style.left = point.x + 'px';
        div.style.top = point.y + 'px';
    }
};

CustomMarker.prototype.remove = function () {
    if (this.div_) {
        this.div_.parentNode.removeChild(this.div_);
        this.div_ = null;
    }
};

CustomMarker.prototype.getPosition = function () {
    return this.latlng_;
};

CustomMarker.prototype.setPosition = function (latlng_)
{
    this.latlng_ = latlng_;
}

PopulateTeamData();
if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(CreateMap);
}
else {
    alert("enable Location and refresh");
}
setInterval(UpdateMyPosition, 1000);
console.log(ServerAddres);
