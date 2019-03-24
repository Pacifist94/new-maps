https://www.youtube.com/watch?v=rJesac0_Ftw
https://jsonformatter.curiousconcept.com/

https://docs.zoho.com/sheet/open/chxildb32a2b3f5a94cb08b48a4ec9ab4afcd/sheets/Hoja1/ranges/L3:L992
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

//----------------------------------------------
<!DOCTYPE HTML>
<html>
	<head>
		<title>JSON and AJAX</title>
	</head>

	<body>

	<button id="btn">Fetch Info</button><br>


<textarea id="myArea3" style="margin: 0px; width: 200px; height: 400px;"></textarea>

<textarea id="myArea2" style="margin: 0px; width: 200px; height: 400px;"></textarea>

	<textarea id="myArea" style="margin: 0px; width: 200px; height: 400px;"></textarea>



	<div id="animal-info"> </div>


	<script src="js/main.js"></script>
	</body>
</html>
//--------------------------------
var myArea = document.getElementById("myArea");
var myArea2 = document.getElementById("myArea2");
var myArea3 = document.getElementById("myArea3");
var btn = document.getElementById("btn");

//--------------------------------------------
var Level = 3;
var TrackNumber = 2; 

var songArray = [
"https://api.myjson.com/bins/n5yvy",
"https://api.myjson.com/bins/rmdnm",
"https://api.myjson.com/bins/1hbd1e"
];

var posOfsecondNoteInSquaresArr = [
9, 
11,
33
];
//--------------------------------------------


btn.addEventListener("click", function(){


	var ourRequest = new XMLHttpRequest();
	//https://api.myjson.com/bins/n5yvy | posOfsecondNoteInSquares = 9;
	//https://api.myjson.com/bins/rmdnm | posOfsecondNoteInSquares = 11;
	ourRequest.open('GET', songArray[Level -1]);
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
	var posOfsecondNoteInSquares = posOfsecondNoteInSquaresArr[ Level -1];
	var timeOfSecondNote = data.tracks[1].notes[1].time;// allways use the main value
	var	 Factor = (posOfsecondNoteInSquares-1) / timeOfSecondNote;

	var myChordPos = [];
	var myOutput = "";

	var m_ChordTime = [];
	var m_ChordName = [];

//For Track1
// -2 = 1000000
// -1 = 1000001
//  0 = 1000002
//  1 = 1000003
//  2 = 1000004

// For Track2
//-4 = 1000006
//-5 = 1000007
//-6 = 1000008
//-7 = 1000009
//-8 = 1000010
//-9 = 1000011
//-10 = 1000012
//-11 = 1000013
//-12 = 1000014
//-13 = 1000015

var	 m_ChordPosition = [];


//----------------------
var PresetPositions = "";

//---------------------
if (TrackNumber == 1) {

//For Track1
//PresetPositions = ["-1", "-2", "2", "0", "1", "-2", "2", "0", "1", "-1", "-2", "1", "2", "-1", "0"];
 PresetPositions = ["1000001", "1000000", "1000004", "1000002", "1000003", "1000000", "1000004", "1000002", "1000003", "1000001", "1000000", "1000003", "1000004", "1000001", "1000002"];
}

//--------------------
if (TrackNumber == 2) {

//For Track2
//var PresetPositions = ["-4", "-5", "-6", "-7", "-8", "-9", "-10", "-11", "-12", "-13", "-4", "-5", "-6", "-7", "-8"];
 PresetPositions = ["1000006", "1000007", "1000008", "1000009", "1000010", "1000011", "1000012", "1000013", "1000014", "1000015", "1000006", "1000007", "1000008", "1000009", "1000010"];
}
//---------------------------


var	 m_ChordPrefab = [];
var PresetPrefab = ["1", "1"];

	var m_ChordJoined = new Array();
var m_PositionJoined = new	Array();
var m_PrefabJoined = new	Array();

	var m_TimeJoined = new Array();
	var m_TimeJoinedNoRepeat = new	Array();


	var found = false;
	//myArea.value += data.tracks.notes[0];

	for (i = 1; i < data.tracks[TrackNumber].notes.length + 1 ; i++) {

	// Calculate ExatRounded
	var noteTime = data.tracks[TrackNumber].notes[i-1].time;
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

var mainStringPosition = "" ;
var mainStringPrefab = "" ;

	//Compare time with the previous to see if there are the same, then Concatenate
	for (i = 0; i < data.tracks[TrackNumber].notes.length  ; i++) {

	//console.log("data.tracks[1].notes.length" + data.tracks[1].notes.length);
		// Calculate ExatRounded
		var noteTime = data.tracks[TrackNumber].notes[i].time;
		var ExactSquarePos = (Factor * noteTime) +1;
		var ExactRounded = (Math.round(ExactSquarePos * 100) / 100)

		m_ChordTime.push(ExactRounded);


		var noteName = data.tracks[TrackNumber].notes[i].name;

		m_ChordName.push(noteName); 

	m_ChordPosition.push(PresetPositions[(i % 15)]) ;	

m_ChordPrefab.push(PresetPrefab[(i % 2)]) ;
	}






	for (i = 0; i < data.tracks[TrackNumber].notes.length  ; i++) {

		//If is not the first compare with the previous
		if (i > 0) {

			// is the same?
			if (m_ChordTime[i] == m_ChordTime[i-1]) {


			mainString += "," + m_ChordName[i];
			mainStringTime += "," + m_ChordTime[i];

mainStringPosition += "," + m_ChordPosition[i];
mainStringPrefab  += "," + m_ChordPrefab[i];	

			}
			// is different
			else{
			 mainString += "|" + m_ChordName[i]	;
			 mainStringTime += "|" + m_ChordTime[i];

mainStringPosition += "|" + m_ChordPosition[i];
mainStringPrefab += "|" + m_ChordPrefab[i];

			 //NO REPEAT TIME
			 mainStringTimeNoREPEAT += "|" + m_ChordTime[i];
			}
		}
		// Is 0
		else if (i == 0){
		mainString = m_ChordName[0] ;
mainStringPosition = m_ChordPosition[0];
mainStringPrefab = m_ChordPrefab[0];
		mainStringTime = m_ChordTime[0] ;

		//NO REPEAT TIME
		mainStringTimeNoREPEAT = m_ChordTime[0] ;
		}

	}




	//Fill new Arrays 
	m_ChordJoined = mainString.split("|");
m_PositionJoined = mainStringPosition.split("|");
m_PrefabJoined = mainStringPrefab.split("|");
	m_TimeJoined = mainStringTime.split("|");
	m_TimeJoinedNoRepeat = mainStringTimeNoREPEAT.split("|");



	////LIMIT For Loop
	var lastValueOfTime = m_TimeJoinedNoRepeat[m_TimeJoinedNoRepeat.length-1]; // 321


	//index to display proper contect
	var mycounter = 0;

	for (i = 1; i <= lastValueOfTime; i++) {


		if (m_TimeJoinedNoRepeat.includes("" + i)) {

			myArea.value  += "<zzz" + (i )+ "> " + m_ChordJoined[mycounter] + " </zzz"+ (i ) + "> \n";
			myArea2.value  += "<zzz" + (i )+ "> " + m_PositionJoined[mycounter] + " </zzz"+ (i ) + "> \n";
			myArea3.value  += "<zzz" + (i )+ "> " + m_PrefabJoined[mycounter] + " </zzz"+ (i ) + "> \n";
				
			mycounter++;		
		}
		else {
			myArea.value  += "<zzz" + (i)+ "> " + 0 + " </zzz"+ (i) + "> \n";
			myArea2.value  += "<zzz" + (i )+ "> " + 0 + " </zzz"+ (i ) + "> \n";
			myArea3.value  += "<zzz" + (i )+ "> " + 0 + " </zzz"+ (i ) + "> \n";
				
		}

	}

}
//------------------------------


<html><head>
	<title>NR</title>
</head>
<body> 



<textarea id="areaOrigen" name="text"  rows="20" cols="30">

</textarea> <br><br>


<button onclick="fncreplacing()" >SWAP</button><br> 

<!--<button onclick="fncreplacing2()" >SWAP Back</button><br> --> 


</form>
<script> 
var mainText; 
var varFrom; 
var varTo; 
var inputs;
//----------------

// My Default values
var myArray = ["A#2", "A#3", "A#4", "A#5", "A#6", "A#7", "A#8", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "C#2", "C#3", "C#4", "C#5", "C#6", "C#7", "C#8", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "D#2", "D#3", "D#4", "D#5", "D#6", "D#7", "D#8", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "F#2", "F#3", "F#4", "F#5", "F#6", "F#7", "F#8", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "G#2", "G#3", "G#4", "G#5", "G#6", "G#7", "G#8", "G2", "G3", "G4", "G5", "G6", "G7", "G8"]; 


var myArray2 = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83"]; 


//------------------------------- Values when Position are decoded
/*

//For Track1
// -2 = 1000000
// -1 = 1000001
//  0 = 1000002
//  1 = 1000003
//  2 = 1000004

// For Track2
//-4 = 1000006
//-5 = 1000007
//-6 = 1000008
//-7 = 1000009
//-8 = 1000010
//-9 = 1000011
//-10 = 1000012
//-11 = 1000013
//-12 = 1000014
//-13 = 1000015

var myArray = ["1000000", "1000001", "1000002", "1000003", "1000004", "1000006", "1000007", "1000008", "1000009", "1000010", "1000011", "1000012", "1000013", "1000014", "1000015"]; 


var myArray2 = [ "-2", "-1", "0", "1", "2", "-4", "-5", "-6", "-7", "-8", "-9", "-10", "-11", "-12", "-13"]; 

*/
//------------------------------ Values when Offset is needed
/*

var myArray = ["A#1","A#2", "A#3", "A#4", "A#5", "A#6", "A#7", "A#8", "A1","A2", "A3", "A4", "A5", "A6", "A7", "A8", "B1","B2", "B3", "B4", "B5", "B6", "B7", "B8", "C#1","C#2", "C#3", "C#4", "C#5", "C#6", "C#7", "C#8", "C1","C2", "C3", "C4", "C5", "C6", "C7", "C8", "D#1","D#2", "D#3", "D#4", "D#5", "D#6", "D#7", "D#8", "D1","D2", "D3", "D4", "D5", "D6", "D7", "D8", "E1","E2", "E3", "E4", "E5", "E6", "E7", "E8", "F#1","F#2", "F#3", "F#4", "F#5", "F#6", "F#7", "F#8", "F1","F2", "F3", "F4", "F5", "F6", "F7", "F8", "G#1","G#2", "G#3", "G#4", "G#5", "G#6", "G#7", "G#8", "G1","G2", "G3", "G4", "G5", "G6", "G7", "G8"]; 


var myArray2 = [ "A#2","A#3", "A#4", "A#5", "A#6", "A#7", "A#8","A#9",  "A2","A3", "A4", "A5", "A6", "A7", "A8","A9", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "C#2", "C#3", "C#4", "C#5", "C#6", "C#7", "C#8", "C#9", "C2","C3", "C4", "C5", "C6", "C7", "C8", "C9", "D#2","D#3", "D#4", "D#5", "D#6", "D#7", "D#8", "D#9", "D2","D3", "D4", "D5", "D6", "D7", "D8", "D9", "E2","E3", "E4", "E5", "E6", "E7", "E8", "E9", "F#2","F#3", "F#4", "F#5", "F#6", "F#7", "F#8", "F#9", "F2","F3", "F4", "F5", "F6", "F7", "F8", "F9", "G#2","G#3", "G#4", "G#5", "G#6", "G#7", "G#8", "G#9", "G2","G3", "G4", "G5", "G6", "G7", "G8", "G9"]; 

myArray.reverse();
myArray2.reverse();
*/
//-------------
function initializer(){ 

	mainText = document.getElementById("areaOrigen").value; 

} 

function fncreplacing(){

	initializer() 

	//----------------Converts the numbers to Symbols---------------------
		for (var j=0;j<=myArray2.length;j++) {
	     varFrom = myArray[j];
	     varTo = myArray2[j];

			for (var i=1;i<=mainText.length;i++) { 
				var replacedText=mainText.replace(varFrom,varTo); 
				mainText=replacedText; 
			} 
		}	
	 //   document.getElementById("areaOrigen").value= mainText; 

	//----------------Converts the Symbols to Code ---------------------
		
//---------------------------


		    document.getElementById("areaOrigen").value= mainText ;

} 



function fncreplacing2(){

	initializer() 

	//----------------Converts the numbers to Symbols---------------------
		for (var j=0;j<=myArray2.length;j++) {
	     varFrom = myArray2[j];
	     varTo = myArray[j];

			for (var i=1;i<=mainText.length;i++) { 
				var replacedText=mainText.replace(varFrom,varTo); 
				mainText=replacedText; 
			} 
		}	
	 //   document.getElementById("areaOrigen").value= mainText; 

	//----------------Converts the Symbols to Code ---------------------
		
//---------------------------


		    document.getElementById("areaOrigen").value= mainText ;

} 


</script> 

</body></html>


// replace space with tabs




//-----------------------------------

//Make a Collider reference array when spawning the prefab
Collider2D colsSpawned[];


// save Collider to array if position is (-2,-1,0,1,2)
if(position == -2 ||position == -1 ||position == 0 ||position == 1 ||position == 2 )
{
colsSpawned.push();	
}



//Have a MoveDown Script for each TouchID / Or just one who checkes all collider 

//Dedicated fingerId 1
//Try Pool with Dictionary
void Update(){

if(Input.){

}


}




// Once we touch a prefab remove it from the array


Lists








