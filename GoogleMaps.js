

var mapProp = null;
var mapCenterX = 43.3194;
var mapCenterY = 21.8963;
var map = null;

var mapType = null;

var mapZoom = 15;

var myPositionX = null;
var myPositionY = null;

var minSeparation = 20;
var MyIcon = "/Images/aca.jpg";
var TeamMateIcon = "/Images/TeamMatePosition.png";

var Markers =new FormData();
var Info =new FormData();

CreateMap = function ()
{
    mapType = google.maps.MapTypeId.HYBRID;
    var element = document.getElementById("mapPlace");
    mapProp = { center: new google.maps.LatLng(mapCenterX, mapCenterY), zoom: mapZoom, mapTypeId: mapType };
    map = new google.maps.Map(element, mapProp);
}

UpdateMyPosition = function ()
{
    if (navigator.geolocation)
    {
        navigator.geolocation.getCurrentPosition(SetPosition);
    }
    else
    {
        alert("enable Location and refresh");
    }
}


function ReciveTeamData(array)
{
    for (var i =0;i < array.lenght; i++)
    {
        TeamData.Set(array[i].Guid, array[i]);
        console.log(array[i]);
        UpadateMap();
    }
}



PopulateTeamData = function()
{
    $.ajax({
        url: ServerAddres+"",//add Controler Name,
        type: "POST",
        dataType: 'json',
        data: { "id": myIndetification },
        cache: false,
        success: function (result) {
            ReciveTeamData(result.data);
        },
        error: function (err) {
            alert("Error! Refresh");
        }
    });
}


UpadateMap = function ()
{
    for (var element of fileData.entries())
    {
        if (myIndetification == element[0]) continue;
        console.log(element[0]);
        var marker = Markers.get(element[0]);
        if (marker == null)
        {
            var marker = new google.maps.Marker({
                position: new google.maps.LatLng(element[1].PosX, element[1].PosY)
            });

            marker.setIcon({
                url: MyIcon,
                //the size of the image is 32x32, 
                //when you want to add a border you must add 2*borderWidth to the size
                size: new google.maps.Size(34, 34),
                //define the shape
                shape:{ coords: [17, 17, 18], type: 'circle' },
                //set optimized to false otherwise the marker  will be rendered via canvas 
                //and is not accessible via CSS
                optimized:false
             });
            marker.setMap(map);
            Markers.set(element[0], marker);

            (function (marker, element)
            {
                google.maps.event.addListener(marker, "click", function (e)
                {
                    var infoWindow = Info.get(element.Guid);
                    if (infoWindow == null)
                    {
                        var infowindow = new google.maps.InfoWindow({
                            content: element.Username ///PutStyle
                        });

                    }
                    else
                        infoWindow.setContent(element.Username);//PutStyle
                    infoWindow.open(map, marker);
                });
            })(marker, element[1]);
        }
        else
        {
            marker.setPosition(new google.maps.LatLng(element[1].PosX, element[1].PosY))
            marker.setMap(map);
            Markers.set(element[0], marker);
        }
    }
}
function SetPosition(position)
{
    
    if (myPositionX == null || myPositionX == undefined || myPositionY == null || myPositionY == undefined)
    {
       // console.log(position.coords.latitude);
        myPositionX = position.coords.latitude;
        myPositionY = position.coords.longitude;
        setMyMarker();

        return;
    }
    var x = getDistanceFromLatLonInKm(myPositionX, myPositionY, position.coords.latitude, position.coords.longitude) * 1000;
    console.log(x);
    if (x > minSeparation)
    {
        myPositionX = position.coords.latitude;
        myPositionY = position.coords.longitude;

        console.log(position.coords.latitude + "," + position.coords.longitude);
        setMyMarker();
        
    }

}

function setMyMarker()
{
    var marker = Markers.get(myIndetification);

    if (marker == null) {
        var marker = new CustomMarker(new google.maps.LatLng(myPositionX, myPositionY), map, MyIcon)
        Markers.set(myIndetification, marker);
    }
    else {
        marker.setPosition(new google.maps.LatLng(myPositionX, myPositionY))
        marker.setMap(map);
        Markers.set(myIndetification, marker);
    }
}

function getDistanceFromLatLonInKm(x1, y1, x2, y2)
{
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

function deg2rad(deg)
{
    return deg * (Math.PI / 180)
}

function CustomMarker(latlng, map, imageSrc) {
    this.latlng_ = latlng;
    this.imageSrc = imageSrc;
    // Once the LatLng and text are set, add the overlay to the map.  This will
    // trigger a call to panes_changed which should in turn call draw.
    this.setMap(map);
}

CustomMarker.prototype = new google.maps.OverlayView();

CustomMarker.prototype.draw = function () {
    // Check if the div has been created.
    var div = this.div_;
    if (!div) {
        // Create a overlay text DIV
        div = this.div_ = document.createElement('div');
        // Create the DIV representing our CustomMarker
        div.className = "customMarker"


        var img = document.createElement("img");
        img.src = this.imageSrc;
        div.appendChild(img);
        google.maps.event.addDomListener(div, "click", function (event) {
            google.maps.event.trigger(me, "click");
        });

        // Then add the overlay to the DOM
        var panes = this.getPanes();
        panes.overlayImage.appendChild(div);
    }

    // Position the overlay 
    var point = this.getProjection().fromLatLngToDivPixel(this.latlng_);
    if (point) {
        div.style.left = point.x + 'px';
        div.style.top = point.y + 'px';
    }
};

CustomMarker.prototype.remove = function () {
    // Check if the overlay was on the map and needs to be removed.
    if (this.div_) {
        this.div_.parentNode.removeChild(this.div_);
        this.div_ = null;
    }
};

CustomMarker.prototype.getPosition = function () {
    return this.latlng_;
};


