function postToUnity(pathParams){
	let url = "http://localhost:20000";
	let first = true;
	for (var key in pathParams){
		if (first){
			url = url + "?" + key + "=" + pathParams[key];
			first = false;
		}else{
			url = url + "&" + key + "=" + pathParams[key];
		}
	}
	$.ajax({
		url: url,
		dataType: 'jsonp',
		type: 'POST',
		success: function(response){
			//worked
		},
		error: function(a){
			//error
		}
	});
}

//todo: mapster is a string. treat it as suchs
function mapsterToStudent(mapster){
	let student = "";
	let studentArray = mapster.split(',');
	for (let i=0; i<studentArray.length; i++){
		if (studentArray[i]<18){
			student += "0" + Math.floor(studentArray[i] / 2 + 1);
		}else{
			student += "" + Math.floor(studentArray[i] / 2 + 1);
		}
		if (studentArray[i] % 2 == 1){
			student+="R"
		}else{
			student+="L"
		}
	}
	return student;
}