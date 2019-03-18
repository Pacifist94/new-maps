https://www.youtube.com/watch?v=rJesac0_Ftw


----------------------------------------------------------------
var myArea = document.getElementById("myArea");

var btn = document.getElementById("btn");

btn.addEventListener("click", function(){


var ourRequest = new XMLHttpRequest();
ourRequest.open('GET', 'https://api.myjson.com/bins/n5yvy');

ourRequest.onload = function(){

var ourData = JSON.parse(ourRequest.responseText);

renderHTML(ourData);

};
ourRequest.send();


});

function renderHTML(data){

//data.tempo[0].bpm; 			  //BPM

//data.tracks[1].notes[i].name ;  //Notes
//data.tracks[1].notes[i].time ;  //Time

/*
Factor = (posOfsecondNoteInSquares - 1) / timeOfSecondNote
Factor = (9-1) / 1.6
Factor = 5

ExactSquarePos = (Factor * notes.time) +1  
*/
var posOfsecondNoteInSquares = 9;
var timeOfSecondNote = data.tracks[1].notes[1].time;
var	 Factor = (posOfsecondNoteInSquares-1) / timeOfSecondNote;

var myChordPos = []
var myOutput = "";

var found = false;
//myArea.value += data.tracks.notes[0];

for (i = 1; i < data.tracks[1].notes.length + 1 ; i++) {

// Calculate ExatRounded
var noteTime = data.tracks[1].notes[i-1].time;
var ExactSquarePos = (Factor * noteTime) +1;
var ExactRounded = (Math.round(ExactSquarePos * 100) / 100) 

//add it to the array
myChordPos.push(ExactRounded);

}


/*
Compare Time in both notes and if they are the same / Group them together
put them in the same item in the array




*/



for (i = 1; i < data.tracks[1].notes.length +1  ; i++) {



//Check if j index value exist in array

for (j = 0; j < myChordPos.length + 1 ; j++) {


if (i == myChordPos[j]) {

//myArea.value += "index: " + i + " exist in Array [" + j + "]\n";
myArea.value  += "<cord" + (i )+ "> " + /* Array of Chords +*/ " </cord"+ (i ) + "> \n";

found = true;
break;
}
else if(j == myChordPos.length && found == false ){
myArea.value  += "<cord" + (i )+ "> " + 0 + " </cord"+ (i ) + "> \n";
}


}
found = false;

//Show in textarea
//myArea.value += myChordPos[i-1] + "\n";

}

	



//myArea.value += data[i].name + "\n";





//myOutput += "<cord" + (i +1)+ "> " + (Math.round(ExactSquarePos * 100) / 100) + "</cord"+ (i +1) + "> \n";

//myOutput += "<cord" + (i +1)+ "> " + 0 + "</cord"+ (i +1) + "> \n";





//myArea.value += data.tracks[1].notes[i].name + "	" + (Math.round(ExactSquarePos * 100) / 100) + "\n";















//animalDiv.insertAdjacentHTML('beforeend',htmlString);
}
----------------------------------------------------------------
<!DOCTYPE HTML>
<html>
	<head>
		<title>JSON and AJAX</title>
	</head>

	<body>

	<button id="btn">Fetch Info</button>

	<textarea id="myArea"></textarea>

	<div id="animal-info"> </div>


	<script src="js/main.js"></script>
	</body>
</html>

