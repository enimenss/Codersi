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
var paths = new Array();
var newPaths = new Array();
var InfoWindowForPaths = new Array();
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

changeCity = function (newCity)
{
    $.ajax({
        url: ServerAddres + "RealTime/getNewCityPosition",//add Controler Name,
        type: "POST",
        dataType: 'json',
        cache: false,
        data: { "newCity": newCity },
        success: function (result) {
            console.log(result);
            changeToNewLocation(result);
        },
        error: function (err) {
            alert("Error! Refresh");
        }
    });
}

function changeToNewLocation(result)
{
    clearLines();
    clearAllMarks();
    clearAllInfoWindow();
    clearNewPaths();
    clearInfoWindowForPaths();
    clearPath();
    GetAllLandmarks(result);
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': result }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            mapCenterX = results[0].geometry.location.lat();
            mapCenterY = results[0].geometry.location.lng();
        } else {
            alert("Something got wrong " + status);
        }
    })
    map.setCenter(new google.maps.LatLng(mapCenterX, mapCenterY));
    map.setZoom(mapZoom);

}
CreateMap = function () {
    mapType = google.maps.MapTypeId.HYBRID;
    var element = document.getElementById("mapPlace");
    mapProp = { center: new google.maps.LatLng(mapCenterX, mapCenterY), zoom: mapZoom, mapTypeId: mapType, overviewMapControl: true };
    map = new google.maps.Map(element, mapProp);
}

GetAllLandmarks = function (city) {
    $.ajax({
        url: ServerAddres + "RealTime/MakeAGraph",//add Controler Name,
        type: "POST",
        dataType: 'json',
        cache: false,
        data:{"city":city},
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

    if (marker === null || marker === undefined) {
        console.log(element);
          marker = new CustomMarker(new google.maps.LatLng(element.Lat, element.Lon), map/*slika*/);
        Markers[element.Id] = marker;
    }
    else {
        marker.setPosition(new google.maps.LatLng(element.Lat, element.Lon))
        marker.setMap(map);
        Markers[element.Id] = marker;
    }
    (function (marker,element) {
        google.maps.event.addListener(marker, "click", function (e) {
            if (Selected === null)
            {
                console.log(Selected);
                Selected = e;
                SelectedElement = element;
                e.classList.toggle("Selected");
            }
            else
            {
                if (Selected === e)
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
                        strokeWeight: 4,
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
            if (Info[element.Id] === null || Info[element.Id] === undefined)
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
            if (Info[element.Id] === null || Info[element.Id] === undefined) {
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

BadResult = function (result) {
    for (var el in Lines)
    {
        Lines[el].setOptions({ strokeColor: '#ff0000' });
    }
    for (var i = 0; i < result.length; i++) {
        console.log(Lines);
        var Line = Lines[result[i].ParentNode + " " + result[i].ChildNode];
        if (Line === null || Line === undefined) {
            var flightPath = new google.maps.Polyline({
                path: [new google.maps.LatLng(Locations[result[i].ParentNode].Lat, Locations[result[i].ParentNode].Lon), new google.maps.LatLng(Locations[result[i].ChildNode].Lat, Locations[result[i].ChildNode].Lon)],
                strokeColor: "#FF00FF",
                strokeOpacity: 0.8,
                strokeWeight: 4,
                icons: [{
                    icon: lineSymbol,
                    offset: '100%'
                }],
            });
            flightPath.setMap(map);
            newPaths[result[i].ParentNode + " " + result[i].ChildNode] = flightPath;
        }
        else {
            Line.setOptions({ strokeColor: 'blue' });
            (function (line,element) {
                google.maps.event.addListener(line, "click", function (e) {
                    line.setMap(null);
                    var ln = new Array();
                    var indxT = element.ParentNode + " " + element.ChildNode;
                    for (var elm in Lines)
                    {
                        if (!(elm === indxT))
                            ln[elm] = Lines[elm];
                    }
                    Lines = ln;
                    var pt = new Point(element.ParentNode, element.ChildNode);
                    var gf = new Array();
                    for (var el in Graph)
                    {
                        if (Graph[el].id1 !== pt.id1 || Graph[el].id2 !== pt.id2)
                            gf.push(Graph[el]);
                    }
                    Graph = gf;
                    console.log(Graph);
                });
            })(Line, result[i]);
        }
    }
}


function clearLines()
{
    for (var el in Lines)
    {
        Lines[el].setMap(null);
    }

    Lines = new Array();
}
function clearAllMarks()
{
    for (var el in Markers)
    {
        Markers[el].setMap(null);
    }
    Markers = new Array();
}
function clearAllInfoWindow()
{
    for (var el in Info)
    {
        Info[el].setMap(null);
    }
    Info = new Array();
}
function clearNewPaths()
{
    for (var el in newPaths)
    {
        newPaths[el].setMap(null);
    }
    newPaths = new Array();
}
function clearInfoWindowForPaths()
{
    for (var el in InfoWindowForPaths)
    {
        InfoWindowForPaths[el].setMap(null);
    }
    InfoWindowForPaths = new Array();
}
function getColor(element)
{
    var color = "#";
    if (element.path.includes(1))
    {
        color += "ff";
    }
    else
    {
        color += "00";
    }
    if (element.path.includes(2)) {
        color += "ff";
    }
    else {
        color += "00";
    }
    if (element.path.includes(3)) {
        color += "ff";
    }
    else {
        color += "00";
    }
    return color;
}

function getTeamMates(element) {
    var color = "";
    for (var i = 0; i < element.path.length; i++)
        color += " , " + element.path[i];
    return color;
}

addRouts = function (result)
{
    ressetLines();
    clearPath();
    console.log(result);
    for (var i = 0; i < result.length; i++) {
        console.log(Lines);
        if (result[i].path.length > 0)
        {
            var flightPath = new google.maps.Polyline({
                path: [new google.maps.LatLng(Locations[result[i].ParentNode].Lat, Locations[result[i].ParentNode].Lon), new google.maps.LatLng(Locations[result[i].ChildNode].Lat, Locations[result[i].ChildNode].Lon)],
                strokeColor: getColor(result[i]),
                strokeOpacity: 0.8,
                strokeWeight: 5,
                icons: [{
                    icon: lineSymbol,
                    offset: '100%'
                }],
            });
            flightPath.setMap(map);
            paths[result[i].ParentNode + " " + result[i].ChildNode] = flightPath;
            (function (line, el) {
                google.maps.event.addListener(line, "click", function (e) {
                    var inf = InfoWindowForPaths[el.ParentNode.Id + " "+el.ChildNode.Id];
                    if (inf === null || inf === undefined) {
                        console.log("Igrac: " + getTeamMates(el));
                        var infowindow = new google.maps.InfoWindow({
                            content: "Igrac: "+getTeamMates(el)
                        });
                        console.log(el.ParentNode);
                        console.log(el.ChildNode);
                        var inBetween = google.maps.geometry.spherical.interpolate(new google.maps.LatLng(Locations[el.ParentNode].Lat, Locations[el.ParentNode].Lon), new google.maps.LatLng(Locations[el.ChildNode].Lat, Locations[el.ChildNode].Lon), 0.5);
                        console.log(inBetween);
                        infowindow.setPosition(inBetween);
                        infowindow.open(map);
                        InfoWindowForPaths[el.ParentNode.Id + " " + el.ChildNode.Id] = infowindow;
                    }
                    else
                    {
                        InfoWindowForPaths[el.ParentNode.Id + " " + el.ChildNode.Id].close();
                        InfoWindowForPaths[el.ParentNode.Id + " " + el.ChildNode.Id] = null;
                    }
                });
            })(flightPath, result[i]);
        }
    }
}

confirmChangesAsYes = function ()
{
    for (var index in newPaths)
    {
        newPaths[index].setOptions({ strokeColor: '#ff0000' });
        var indxGf = index.split(" ");
        var pt = new Point(Number(indxGf[0]), Number(indxGf[1]));
        console.log(pt);
        Graph.push(pt);
    }
    console.log(Graph);
}

confirmChangesAsNo = function ()
{
    for (var index in newPaths)
    {
        newPaths[index].setMap(null);
    }
    newPaths = new Array();
}

ressetLines=function()
{
    for (var el in Lines)
    {
        Lines[el].setMap(null);
    }
}

function clearPath()
{
    for (var el in paths)
    {
        paths[el].setMap(null);
    }
    paths = new Array();
}

UndoGraph = function ()
{
    clearPath();
    for (var el in Lines) {
        Lines[el].setMap(map);
    }
}

sendGraphToServerForValidation = function (SaveToDatabase, City, CompatitionId)
{
    $.ajax({
        url: ServerAddres + "RealTime/GetGraph",//add Controler Name,
        type: "POST",
        dataType: 'json',
        data: {
            "graph": Graph, "saveToDatabase": SaveToDatabase, "City": City, "CompatitionId": CompatitionId},
        cache: false,
        success: function (result) {
            console.log(result);
            BadResult(result);
        },
        error: function (err) {
           // alert("Error! Refresh");//grafnijedobar
        }
    });
}

getpathsForGraph = function (numBersOfTeamMates, fromDatabase, City, CompatitionId)
{
    $.ajax({
        url: ServerAddres + "RealTime/GetRouts",//add Controler Name,
        type: "POST",
        dataType: 'json',
        data: {
            "graph": Graph, "numBersOfTeamMates": numBersOfTeamMates, "fromDatabase": fromDatabase, "City": City, "CompatitionId": CompatitionId},
        cache: false,
        success: function (result) {
            console.log(result);
            addRouts(result);
        },
        error: function (err) {
            // alert("Error! Refresh");//grafnijedobar
        }
    });
}


GetAllLandmarks(null);
CreateMap();