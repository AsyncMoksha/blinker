var footStepSFXLeft:AudioClip;
var footStepSFXRight:AudioClip;
var isPlaying : boolean;
var lastTime : int;

/*
function Update () {
	WaitForSeconds(1);
	if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||Input.GetKey(KeyCode.D) ) {
		
		audio.PlayOneShot(footStepSFXLeft);
		WaitForSeconds(1);
		audio.PlayOneShot(footStepSFXRight);
		
		isPlaying = true;
		WaitForSeconds(1);
	} else {
		isPlaying = false;
	
	}
	
	if(isPlaying) {
		
		audio.PlayOneShot(footStepSFXLeft);
		isPlaying = false;
		WaitForSeconds(1);
	}
}
*/

function Update () {
	
	if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||Input.GetKey(KeyCode.D) ) {
		if(lastTime % 20 == 0) {
			audio.PlayOneShot(footStepSFXLeft);
			lastTime++;
		} else if(lastTime % 40 == 0){
			audio.PlayOneShot(footStepSFXRight);
			lastTime++;
		} else {
		
			lastTime ++;
		}
	}
}