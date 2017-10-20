var mapProp = null;
var mapCenterX = 43.3194;
var mapCenterY = 21.8963;
var map = null;

var mapType = null;

var mapZoom = 14;

var ImageLocation = ServerAddres + "Images/";
var Markers = new Array();
var Info = new Array();
var Lines = new Array();
var Selected = null;
var SelectedElement = null;
var lineSymbol = {
    path: google.maps.SymbolPath.FORWARD_CLOSED_ARROW
};

class Point
{
    constructor(id1, id2)
    {
        this.id1 = id1;
        this.id2 = id2;
    }
}

//var Graph = new Array();

CreateMap = function () {
    mapType = google.maps.MapTypeId.HYBRID;
    var element = document.getElementById("mapPlace");
    mapProp = { center: new google.maps.LatLng(mapCenterX, mapCenterY), zoom: mapZoom, mapTypeId: mapType, overviewMapControl: true };
    map = new google.maps.Map(element, mapProp);
}

GetAllLandmarks = function () {
    $.ajax({
        url: ServerAddres + "RealTime/MakeAGraph",//add Controler Name,
        type: "POST",
        dataType: 'json',
        cache: false,
        success: function (result) {
            console.log(result);
            ReciveLandMarkData(result);
        },
        error: function (err) {
            alert("Error! Refresh");
        }
    });
}

function ReciveLandMarkData(data)
{

    for (var i = 0; i < data.length;i++)
    {
        Locations[data[i].Id] = data[i];
        console.log(data[i]);
        createMarker(Locations[data[i].Id]);
    }
    console.log(Locations);
   
}

function createMarker(element)
{
    var marker = Markers[element.Id];

    if (marker == null || marker === undefined) {
        console.log(element);
        var marker = new CustomMarker(new google.maps.LatLng(element.Lat, element.Lon), map/*slika*/);
        Markers[element.Id] = marker;
    }
    else {
        marker.setPosition(new google.maps.LatLng(element.Lat, element.Lon))
        marker.setMap(map);
        Markers[element.Id] = marker;
    }
    (function (marker,element) {
        google.maps.event.addListener(marker, "click", function (e) {
            if (Selected == null)
            {
                console.log(Selected);
                Selected = e;
                SelectedElement = element;
                e.classList.toggle("Selected");
            }
            else
            {
                if (Selected == e)
                {
                    Selected = null;
                    SelectedElement = null;
                    e.classList.toggle("Selected");
                }
                else
                {
                    var noviNode= new Point(SelectedElement.Id, element.Id);

                    Selected.classList.toggle("Selected");
                   // e.classList.toggle("Selected");
                    Graph.push(noviNode);

                    var flightPath = new google.maps.Polyline({
                        path: [new google.maps.LatLng(SelectedElement.Lat, SelectedElement.Lon), new google.maps.LatLng(element.Lat, element.Lon)],
                        strokeColor: "#FF0000",
                        strokeOpacity: 0.8,
                        strokeWeight: 2,
                        icons: [{
                            icon: lineSymbol,
                            offset: '100%'
                        }],
                    });
                    flightPath.setMap(map);
                    Lines[SelectedElement.Id + " " + element.Id] = flightPath;
                    Selected = null;
                    SelectedElement = null;
                    console.log(Graph);
                }
            }

            
            console.log(e);
        });
    })(marker, element);
    (function (marker, element) {
        google.maps.event.addListener(marker, "mouseover", function (e) {
            if (Info[element.Id] == null || Info[element.Id] === undefined)
            {
                var infowindow = new google.maps.InfoWindow({
                    content: element.Name,
                    pixelOffset: new google.maps.Size(9, 0)
                });
                console.log(infowindow);
                infowindow.open(map, marker);
                Info[element.Id] = infowindow;
            }
        });
    })(marker, element);
    (function (marker, element) {
        google.maps.event.addListener(marker, "mouseout", function (e) {
            if (Info[element.Id] == null || Info[element.Id] === undefined) {
                console.log(element);
            }
            else
            {
                Info[element.Id].close();
                Info[element.Id] = null;
            }
        });
    })(marker, element);
}



function CustomMarker(latlng, map3) {
   // console.log(imageSrc);
    this.latlng_ = latlng;
    //this.imageSrc = imageSrc;
    this.setMap(map3);
}

CustomMarker.prototype = new google.maps.OverlayView();

CustomMarker.prototype.draw = function () {
    var div = this.div_;
    if (!div) {
        div = this.div_ = document.createElement('div');

        div.className = "customMarker"


       // var img = document.createElement("img");
       // img.src = this.imageSrc;

       // div.appendChild(img);
        var x = this;
        google.maps.event.addDomListener(div, "click", function (event) {
            console.log(x);
            google.maps.event.trigger(x, "click",div);
        });
        google.maps.event.addDomListener(div, "mouseover", function (event) {
            console.log(x);
            google.maps.event.trigger(x, "mouseover", div);
        });
        google.maps.event.addDomListener(div, "mouseout", function (event) {
            console.log(x);
            google.maps.event.trigger(x, "mouseout", div);
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

CustomMarker.prototype.setPosition = function (latlng_) {
    this.latlng_ = latlng_;
}

BadResult = function (result)
{
    for (var i = 0; i < result.length;i++)
    {  
        console.log(Lines);
        var Line = Lines[result[i].ParentNode + " " + result[i].ChildNode];
        if (Line == null || Line === undefined)
        {
            var flightPath = new google.maps.Polyline({
                path: [new google.maps.LatLng(Locations[result[i].ParentNode].Lat, Locations[result[i].ParentNode].Lon), new google.maps.LatLng(Locations[result[i].ChildNode].Lat, Locations[result[i].ChildNode].Lon)],
                strokeColor: "#FF00FF",
                strokeOpacity: 0.8,
                strokeWeight: 2,
                icons: [{
                    icon: lineSymbol,
                    offset: '100%'
                }],
            });
            flightPath.setMap(map);
            Lines[result[i].ParentNode + " " + result[i].ChildNode] = flightPath;
        }
        else
        {
            Line.setOptions({ strokeColor: 'blue' });
        }
    }

    addRouts = function (result)
    {
        BadResult(result);
    }

}

GetAllLandmarks();
CreateMap();