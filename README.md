https://www.youtube.com/watch?v=rJesac0_Ftw


----------------------------------------------------------------
var myArea = document.getElementById("myArea");

var btn = document.getElementById("btn");

btn.addEventListener("click", function(){


var ourRequest = new XMLHttpRequest();
ourRequest.open('GET', 'https://learnwebcode.github.io/json-example/animals-1.json');

ourRequest.onload = function(){

var ourData = JSON.parse(ourRequest.responseText);

renderHTML(ourData);

};
ourRequest.send();


});

function renderHTML(data){



for (i = 0; i < data.length; i++) {

myArea.value += data[i].name + "\n";


};



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
