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
	{"goodBehaviour": "idle"}, 
	{"goodBehaviour": "raiseArm"}, 
	{"goodBehaviour": "askQuestion"},
	{"goodBehaviour": "workingPoster"}];
	let noiseDirections = [{"noiseDirection": "up"},{"noiseDirection": "down"}];
	let impulseTowards = [{"impulse": "positive"}, 
	{"impulse": "neutral"}, 
	{"impulse": "negative"}];
	let startDiscussionAbout = [{"theme": "Bismarck"}, 
	{"theme": "Israel"}];
	
	
	$("#studentTemplate").tmpl(students).appendTo("#Mapi");
	$("#singleMisbehaviourTemplate").tmpl(misbehaviours).appendTo("#singleMisbehaviourCollection");
	//$("#allMisbehaviourTemplate").tmpl(misbehaviours).appendTo("#allMisbehaviourCollection");
	$("#noiseTemplate").tmpl(noiseDirections).appendTo("#noiseCollection");
	$("#mitarbeitTemplate").tmpl(goodBehaviours).appendTo("#mitarbeitCollection");
	$("#allMitarbeitTemplate").tmpl(goodBehaviours).appendTo("#mitarbeitCollectionAll");
	
	$("#impulseTowardsTemplate").tmpl(impulseTowards).appendTo("#impulseTowardsCollection");
	$("#startDiscussionAboutTemplate").tmpl(startDiscussionAbout).appendTo("#startDiscussionAboutCollection");

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
	var pathParams = {
		student: mapsterToStudent(img.mapster('get')),
		stoerung: misbehaviour
	};
	postToUnity(pathParams);
	resetMapster();
}

function triggerDistortionForAll(misbehaviour){
	var timeLine = $('#timeLine').html();
	$('#timeLine').html(timeLine + "all " + misbehaviour + "<br>");
	var pathParams = {
		student: "all",
		stoerung: misbehaviour
	};
	postToUnity(pathParams);
	resetMapster();
}

function changeNoiseLevel(direction){
	var pathParams = {
		noise: direction
	};
	postToUnity(pathParams);
}

function setTheme(theme){
	var pathParams = {
		theme: theme
	};
	postToUnity(pathParams);
}

function setImpulse(impulse){
	var pathParams = {
		impulse: impulse
	};
	postToUnity(pathParams);
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
function impulseFct() {
    document.getElementById("impulseTowardsCollection").classList.toggle("show");
}
function discussionFct() {
    document.getElementById("startDiscussionAboutCollection").classList.toggle("show");
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