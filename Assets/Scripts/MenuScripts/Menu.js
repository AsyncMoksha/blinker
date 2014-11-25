var isQuitBtn = false;
var isTestBtn = false;
function OnMouseEnter () {
	renderer.material.color = Color.red;
}

function OnMouseExit () {
	renderer.material.color = Color.white;
}

function OnMouseUp () {
	if(isQuitBtn) {
		Application.Quit();
	} else if(isTestBtn){
		Application.LoadLevel(2);
	} else {
		Application.LoadLevel(1);
	}
}

//for test