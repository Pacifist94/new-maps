
//-------------------------------- 
var myArea = document.getElementById("myArea"); 
var myArea2 = document.getElementById("myArea2"); 
var myArea3 = document.getElementById("myArea3"); 
var btn = document.getElementById("btn");

var myAreaB = document.getElementById("myAreaB"); 
var myArea2B = document.getElementById("myArea2B"); 
var myArea3B = document.getElementById("myArea3B"); 
var btnB = document.getElementById("btnB");
//-------------------------------------------- 
var Level = 3; 
var TrackNumber = 1;
var TrackNumberB = 2;

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



btnB.addEventListener("click", function(){

	var ourRequest = new XMLHttpRequest();
	//https://api.myjson.com/bins/n5yvy | posOfsecondNoteInSquares = 9;
	//https://api.myjson.com/bins/rmdnm | posOfsecondNoteInSquares = 11;
		ourRequest.open('GET', songArray[Level -1]);
		ourRequest.onload = function(){
			var ourData = JSON.parse(ourRequest.responseText);
			renderHTML2(ourData);
		};
	ourRequest.send();
});


function renderHTML(data)
{

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
//For Track1 // -2 = 1000000 // -1 = 1000001 // 0 = 1000002 // 1 = 1000003 // 2 = 1000004

// For Track2 //-4 = 1000006 //-5 = 1000007 //-6 = 1000008 //-7 = 1000009 //-8 = 1000010 //-9 = 1000011 //-10 = 1000012 //-11 = 1000013 //-12 = 1000014 //-13 = 1000015

var	m_ChordPosition = [];

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

var	m_ChordPrefab = []; 
var PresetPrefab = ["1", "1"];

var m_ChordJoined = new Array();
var m_PositionJoined = new	Array(); 
var m_PrefabJoined = new Array();

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
var mainStringPosition = "" ; var mainStringPrefab = "" ;

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
m_ChordPrefab.push(PresetPrefab[(i % 2)]) ; }

for (i = 0; i < data.tracks[TrackNumber].notes.length  ; i++) {

	//If is not the first compare with the previous
	if (i > 0) {

		// is the same?
		if (m_ChordTime[i] == m_ChordTime[i-1]) {


		mainString += "," + m_ChordName[i];
		mainStringTime += "," + m_ChordTime[i];
mainStringPosition += "," + m_ChordPosition[i]; mainStringPrefab += "," + m_ChordPrefab[i];

		}
		// is different
		else{
		 mainString += "|" + m_ChordName[i]	;
		 mainStringTime += "|" + m_ChordTime[i];
mainStringPosition += "|" + m_ChordPosition[i]; mainStringPrefab += "|" + m_ChordPrefab[i];

		 //NO REPEAT TIME
		 mainStringTimeNoREPEAT += "|" + m_ChordTime[i];
		}
	}
	// Is 0
	else if (i == 0){
	mainString = m_ChordName[0] ;
mainStringPosition = m_ChordPosition[0]; mainStringPrefab = m_ChordPrefab[0]; mainStringTime = m_ChordTime[0] ;

	//NO REPEAT TIME
	mainStringTimeNoREPEAT = m_ChordTime[0] ;
	}

}




//Fill new Arrays 
m_ChordJoined = mainString.split("|");
m_PositionJoined = mainStringPosition.split("|"); m_PrefabJoined = mainStringPrefab.split("|"); m_TimeJoined = mainStringTime.split("|"); m_TimeJoinedNoRepeat = mainStringTimeNoREPEAT.split("|");

////LIMIT For Loop
var lastValueOfTime = m_TimeJoinedNoRepeat[m_TimeJoinedNoRepeat.length-1]; // 321


//index to display proper contect
var mycounter = 0;

for (i = 1; i <= lastValueOfTime; i++) {


	if (m_TimeJoinedNoRepeat.includes("" + i)) {

		myArea.value  += "<zzz" + (i )+	"> " + m_ChordJoined[mycounter] + " </zzz"+ (i ) + "> \n";
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



function renderHTML2(data)
{

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
//For Track1 // -2 = 1000000 // -1 = 1000001 // 0 = 1000002 // 1 = 1000003 // 2 = 1000004

// For Track2 //-4 = 1000006 //-5 = 1000007 //-6 = 1000008 //-7 = 1000009 //-8 = 1000010 //-9 = 1000011 //-10 = 1000012 //-11 = 1000013 //-12 = 1000014 //-13 = 1000015

var	m_ChordPosition = [];

//---------------------- 
var PresetPositions = "";

//--------------------- 
if (TrackNumberB == 1) {

//For Track1 
//PresetPositions = ["-1", "-2", "2", "0", "1", "-2", "2", "0", "1", "-1", "-2", "1", "2", "-1", "0"]; 
PresetPositions = ["1000001", "1000000", "1000004", "1000002", "1000003", "1000000", "1000004", "1000002", "1000003", "1000001", "1000000", "1000003", "1000004", "1000001", "1000002"]; 
}

//-------------------- 
if (TrackNumberB == 2) {

//For Track2 
//var PresetPositions = ["-4", "-5", "-6", "-7", "-8", "-9", "-10", "-11", "-12", "-13", "-4", "-5", "-6", "-7", "-8"]; 
PresetPositions = ["1000006", "1000007", "1000008", "1000009", "1000010", "1000011", "1000012", "1000013", "1000014", "1000015", "1000006", "1000007", "1000008", "1000009", "1000010"]; 
} 
//---------------------------

var	m_ChordPrefab = []; 
var PresetPrefab = ["1", "1"];

var m_ChordJoined = new Array();
var m_PositionJoined = new	Array(); 
var m_PrefabJoined = new Array();

var m_TimeJoined = new Array();
var m_TimeJoinedNoRepeat = new	Array();


var found = false;
//myArea.value += data.tracks.notes[0];

for (i = 1; i < data.tracks[TrackNumberB].notes.length + 1 ; i++) {

// Calculate ExatRounded
var noteTime = data.tracks[TrackNumberB].notes[i-1].time;
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
var mainStringPosition = "" ; var mainStringPrefab = "" ;

//Compare time with the previous to see if there are the same, then Concatenate
for (i = 0; i < data.tracks[TrackNumberB].notes.length  ; i++) {

//console.log("data.tracks[1].notes.length" + data.tracks[1].notes.length);
	// Calculate ExatRounded
	var noteTime = data.tracks[TrackNumberB].notes[i].time;
	var ExactSquarePos = (Factor * noteTime) +1;
	var ExactRounded = (Math.round(ExactSquarePos * 100) / 100)

	m_ChordTime.push(ExactRounded);


	var noteName = data.tracks[TrackNumberB].notes[i].name;

	m_ChordName.push(noteName); 

m_ChordPosition.push(PresetPositions[(i % 15)]) ;	
m_ChordPrefab.push(PresetPrefab[(i % 2)]) ; }

for (i = 0; i < data.tracks[TrackNumberB].notes.length  ; i++) {

	//If is not the first compare with the previous
	if (i > 0) {

		// is the same?
		if (m_ChordTime[i] == m_ChordTime[i-1]) {


		mainString += "," + m_ChordName[i];
		mainStringTime += "," + m_ChordTime[i];
mainStringPosition += "," + m_ChordPosition[i]; mainStringPrefab += "," + m_ChordPrefab[i];

		}
		// is different
		else{
		 mainString += "|" + m_ChordName[i]	;
		 mainStringTime += "|" + m_ChordTime[i];
mainStringPosition += "|" + m_ChordPosition[i]; mainStringPrefab += "|" + m_ChordPrefab[i];

		 //NO REPEAT TIME
		 mainStringTimeNoREPEAT += "|" + m_ChordTime[i];
		}
	}
	// Is 0
	else if (i == 0){
	mainString = m_ChordName[0] ;
mainStringPosition = m_ChordPosition[0]; mainStringPrefab = m_ChordPrefab[0]; mainStringTime = m_ChordTime[0] ;

	//NO REPEAT TIME
	mainStringTimeNoREPEAT = m_ChordTime[0] ;
	}

}




//Fill new Arrays 
m_ChordJoined = mainString.split("|");
m_PositionJoined = mainStringPosition.split("|"); m_PrefabJoined = mainStringPrefab.split("|"); m_TimeJoined = mainStringTime.split("|"); m_TimeJoinedNoRepeat = mainStringTimeNoREPEAT.split("|");

////LIMIT For Loop
var lastValueOfTime = m_TimeJoinedNoRepeat[m_TimeJoinedNoRepeat.length-1]; // 321


//index to display proper contect
var mycounter = 0;

for (i = 1; i <= lastValueOfTime; i++) {


	if (m_TimeJoinedNoRepeat.includes("" + i)) {

		myAreaB.value  += "<zzz" + (i )+	"> " + m_ChordJoined[mycounter] + " </zzz"+ (i ) + "> \n";
		myArea2B.value  += "<zzz" + (i )+ "> " + m_PositionJoined[mycounter] + " </zzz"+ (i ) + "> \n";
		myArea3B.value  += "<zzz" + (i )+ "> " + m_PrefabJoined[mycounter] + " </zzz"+ (i ) + "> \n";
			
		mycounter++;		
	}
	else {
		myAreaB.value  += "<zzz" + (i)+ "> " + 0 + " </zzz"+ (i) + "> \n";
		myArea2B.value  += "<zzz" + (i )+ "> " + 0 + " </zzz"+ (i ) + "> \n";
		myArea3B.value  += "<zzz" + (i )+ "> " + 0 + " </zzz"+ (i ) + "> \n";
			
	}

}
} 
