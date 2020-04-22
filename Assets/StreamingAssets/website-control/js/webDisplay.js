let img;
$(document).ready(function(){
	img=$('img');
	
	let misbehaviours = [{"misbehaviour":"drinking"}, 
	{"misbehaviour":"chatting"},
	{"misbehaviour":"letargic"}, 
	{"misbehaviour":"playing"}, 
	{"misbehaviour":"starring"}, 
	{"misbehaviour":"eat"}, 
	{"misbehaviour":"hit"}, 
	{"misbehaviour":"throw"}];
	let goodBehaviours = [{"goodBehaviour": "writing"}, 
	{"goodBehaviour": "breathing"}, 
	{"goodBehaviour": "raiseArm"}, 
	{"goodBehaviour": "askQuestion"},
	{"goodBehaviour": "posterSow1"}];
	let noiseDirections = [{"noiseDirection": "up"},{"noiseDirection": "down"}];
	
	$("#studentTemplate").tmpl(students).appendTo("#Mapi");
	$("#singleMisbehaviourTemplate").tmpl(misbehaviours).appendTo("#singleMisbehaviourCollection");
	//$("#allMisbehaviourTemplate").tmpl(misbehaviours).appendTo("#allMisbehaviourCollection");
	$("#noiseTemplate").tmpl(noiseDirections).appendTo("#noiseCollection");
	$("#mitarbeitTemplate").tmpl(goodBehaviours).appendTo("#mitarbeitCollection");
	$("#allMitarbeitTemplate").tmpl(goodBehaviours).appendTo("#mitarbeitCollectionAll");

	img.mapster({
		render_select: {
			fillColor: 'cdcdcd',
			fillOpacity: 0.5
		},
	});	
});

$(document).keypress(function(event) {
	event.preventDefault();
	switch (event.which){
		case 49:	//1
		    triggerDistortion("drinking");
			break;
		case 50:	//2
			triggerDistortion("starring");
			break;
		case 51:	//3
			triggerDistortion("letargic");
			break;
		case 52:	//4
			triggerDistortion("eat");
			break;
		case 53:	//5
			triggerDistortion("playing");
			break;
		case 54:	//6
			triggerDistortion("chatting");
			break;
		case 55:	//7
			triggerDistortion("throw");
			break;
		case 56:	//8
			triggerDistortion("hit");
			break;
		case 113:	//q
			triggerDistortion("breathing");
			break;
		case 119:	//w
			triggerDistortion("writing");
			break;
	}
});

function triggerDistortion(misbehaviour){
	var timeLine = $('#timeLine').html();
	$('#timeLine').html(timeLine + mapsterToStudent(img.mapster('get')) + " " + misbehaviour + "<br>");
	postDistortion(mapsterToStudent(img.mapster('get')), misbehaviour);
	resetMapster();
}

function triggerDistortionForAll(misbehaviour){
	var timeLine = $('#timeLine').html();
	$('#timeLine').html(timeLine + "all " + misbehaviour + "<br>");
	postDistortion("all", misbehaviour);
	resetMapster();
}

function resetMapster(){
	/*img.mapster({
		render_select: {
			fillColor: 'cdcdcd',
			fillOpacity: 0.5
		},
	});*/
}

/* When the user clicks on the button, 
toggle between hiding and showing the dropdown content */
function stoerfct() {
    document.getElementById("singleMisbehaviourCollection").classList.toggle("show");
}
function stoerfctall() {
    document.getElementById("allMisbehaviourCollection").classList.toggle("show");
}
function mitarbeitfct() {
    document.getElementById("mitarbeitDropDown").classList.toggle("show");
}
function noisefct() {
    document.getElementById("noiseCollection").classList.toggle("show");
}

// Close the dropdown if the user clicks outside of it
window.onclick = function(event) {
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