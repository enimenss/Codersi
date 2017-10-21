



var c ;
var ctx ;


var DefaultSpeed=1;

var Red=255;
var Green=255;
var Blue=255;


var radios = 4;

var speed1 =-1*DefaultSpeed;
var speed2 =0;
var speed3 =0;

var xPos1=25;
var xPos2=25;
var xPos3=25;
var yPos1=4;
var yPos2=14;
var yPos3=24;
var Difrents =7;
var FramePerSecond=25
var UpdateFunc;
function init()
{
	ctx = c.getContext("2d");
	ctx.beginPath();
	ctx.arc(yPos1,xPos1,radios,0,2*Math.PI,false);
	ctx.stroke();
	ctx.beginPath();
	ctx.arc(yPos2,xPos2,radios,0,2*Math.PI,false);
	ctx.stroke();
	ctx.beginPath();
	ctx.arc(yPos3,xPos3,radios,0,2*Math.PI,false);
	ctx.stroke();
	UpdateFunc = setInterval(Update, FramePerSecond);
}


function Update()
{
	ctx.clearRect(0, 0, c.width, c.height);
	xPos1+=speed1;
	move(xPos1,yPos1);
	if(xPos1<radios || xPos1>c.height-radios)
		speed1*=-1;
	
	//console.log(xPos1 +" "+xPos2);
	if(xPos1<xPos2-Difrents && speed2===0)
		speed2=speed1;
	xPos2+=speed2;
	move(xPos2,yPos2);
	
	if(xPos2<radios || xPos2>c.height-radios)
		speed2*=-1;

	if(xPos2<xPos3-Difrents && speed3===0)
		speed3=speed2;
	xPos3+=speed3;
	move(xPos3,yPos3);
	if(xPos3<radios || xPos3>c.height-radios)
		speed3*=-1;
}	

function move(xPos,yPos)
{
	
	ctx.beginPath();
	ctx.arc(yPos,xPos,radios,0,2*Math.PI,false);
	ctx.fillStyle = 'rgb('+Red+','+Green+','+Blue+')';
    ctx.fill();
	ctx.stroke();
}

RunTypingAnimation =function()
{
	var x=document.getElementById("type");
	c=document.createElement("canvas");
    c.id = "myCanvas";
    //console.log(x.offsetHeight + " " + x.offsetWidth);
    c.width = 40;
	c.height=30;
    c.style ="background: rgba(0, 0, 0, 0.0)"
	x.appendChild(c);
	init();
}

StopTypingAnimation=function()
{
    clearInterval(UpdateFunc);
 //   var c = document.getElementById("myCanvas");
	//c.parrentNode.removeChild(c);
}
	




