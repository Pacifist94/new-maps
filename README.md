https://www.youtube.com/watch?v=rJesac0_Ftw
https://jsonformatter.curiousconcept.com/

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
---------------------------------------------------
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

var myChordPos = [];
var myOutput = "";

var m_ChordTime = [];
var m_ChordName = [];


var m_ChordJoined = new Array();
var m_TimeJoined = new Array();
var m_TimeJoinedNoRepeat = new	Array();


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

//Declares String for each type pf data
var mainString = "";
var mainStringTime = "";
var mainStringTimeNoREPEAT = "";

//Compare time with the previous to see if there are the same, then Concatenate
for (i = 0; i < data.tracks[1].notes.length  ; i++) {

//console.log("data.tracks[1].notes.length" + data.tracks[1].notes.length);
	// Calculate ExatRounded
	var noteTime = data.tracks[1].notes[i].time;
	var ExactSquarePos = (Factor * noteTime) +1;
	var ExactRounded = (Math.round(ExactSquarePos * 100) / 100)

	m_ChordTime.push(ExactRounded);


	var noteName = data.tracks[1].notes[i].name;

	m_ChordName.push(noteName); 

	//If is not the first compare with the previous
	if (i > 0) {

		// is the same?
		if (m_ChordTime[i] == m_ChordTime[i-1]) {


		mainString += "," + m_ChordName[i];
		mainStringTime += "," + m_ChordTime[i];

				

		}
		// is different
		else{
		 mainString += "|" + m_ChordName[i]	;
		 mainStringTime += "|" + m_ChordTime[i];

		 //NO REPEAT TIME
		 mainStringTimeNoREPEAT += "|" + m_ChordTime[i];
		}
	}
	// Is 0
	else if (i == 0){
	mainString = m_ChordName[0] ;
	mainStringTime = m_ChordTime[0] ;

	//NO REPEAT TIME
	mainStringTimeNoREPEAT = m_ChordTime[0] ;
	}


}

//Fill new Arrays 
m_ChordJoined = mainString.split("|");
m_TimeJoined = mainStringTime.split("|");
m_TimeJoinedNoRepeat = mainStringTimeNoREPEAT.split("|");



////REMOVE

for (i = 1; i < 322; i++) {

console.log(m_TimeJoinedNoRepeat[i-1]);
//myArea.value += "" +   m_TimeJoinedNoRepeat[i] + "\n";

if (m_TimeJoinedNoRepeat.includes("" + i)) {

myArea.value  += "<cord" + (i )+ "> " + m_ChordJoined[i] + " </cord"+ (i ) + "> \n";
		
}
else {
myArea.value  += "<cord" + (i)+ "> " + 0 + " </cord"+ (i) + "> \n";
}

}



//console.log(m_TimeJoinedNoRepeat[m_TimeJoinedNoRepeat.length]);

//for (i = 1; i < m_TimeJoinedNoRepeat[m_TimeJoinedNoRepeat.length] ; i++) {




/*
if (m_TimeJoinedNoRepeat.includes("" + i)) {

myArea.value  += "<cord" + (i )+ "> " + m_ChordJoined[i] + " </cord"+ (i ) + "> \n";
		
}
else {
myArea.value  += "<cord" + (i)+ "> " + 0 + " </cord"+ (i) + "> \n";
}*/

//}
/*


//Loops through 1 to < 83 ( total 82)
for (i = 1; i < m_TimeJoinedNoRepeat.length +1  ; i++) {


  for (j = 0; j < m_TimeJoinedNoRepeat.length ; j++) {


		if (i == m_TimeJoinedNoRepeat[j]) {

			//myArea.value += "index: " + i + " exist in Array [" + j + "]\n";
			myArea.value  += "<cord" + (i )+ "> " + m_ChordJoined[i-1] + " </cord"+ (i ) + "> \n";
		
			found = true;
			//break;
		}
		else if(i == m_TimeJoinedNoRepeat.length && found == false ){
			myArea.value  += "<cord" + (i )+ "> " + 0 + " </cord"+ (i ) + "> \n";
		}
		console.log("i" + i + " length " + m_TimeJoinedNoRepeat.length + " " + found);

	}
	found = false;
  }
}
*/


//myArea.value += m_TimeJoinedNoRepeat[i-1] + "\n";








/*


for (i = 1; i < m_ChordJoined.length +1  ; i++) {



	//Check if j index value exist in array

	for (j = 0; j < myChordPos.length + 1 ; j++) {


	if (i == myChordPos[j]) {

	//myArea.value += "index: " + i + " exist in Array [" + j + "]\n";
	myArea.value  += "<cord" + (i )+ "> " + " </cord"+ (i ) + "> \n";
										// Array of Chord
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

*/	



//myArea.value += data[i].name + "\n";





//myOutput += "<cord" + (i +1)+ "> " + (Math.round(ExactSquarePos * 100) / 100) + "</cord"+ (i +1) + "> \n";

//myOutput += "<cord" + (i +1)+ "> " + 0 + "</cord"+ (i +1) + "> \n";





//myArea.value += data.tracks[1].notes[i].name + "	" + (Math.round(ExactSquarePos * 100) / 100) + "\n";















//animalDiv.insertAdjacentHTML('beforeend',htmlString);
}
------------------------------------------------
<!DOCTYPE HTML>
<html>
	<head>
		<title>JSON and AJAX</title>
	</head>

	<body>

	<button id="btn">Fetch Info</button>

	<textarea id="myArea" style="margin: 0px; width: 475px; height: 473px;"></textarea>

	<div id="animal-info"> </div>


	<script src="js/main.js"></script>
	</body>
</html>



