var isMovingRight : boolean = true;
var speed : float = 1.0;
var timeCount : float = 0.0;

function Update () {

	timeCount += Time.deltaTime;
	Debug.Log(timeCount);
	if(timeCount <= 2.0f) {
		
		isMovingRight = true;
	} 
	
	if(timeCount > 2.0f && timeCount <= 4.0f) {
		
		isMovingRight = false;
	} else if(timeCount > 4.1f) {
		timeCount = 0.0f;
	}
	
	if(isMovingRight) {
		transform.Translate(Vector3(0.5,0,0) * Time.deltaTime * speed);
		//transform.Translate(transform.right * Time.deltaTime * speed);
	} else {
		transform.Translate(Vector3(0.5,0,0) * Time.deltaTime * speed * (-1));
		//transform.Translate(transform.left * Time.deltaTime * speed);
		
	}
	
	

	//transform.Translate(Vector3(0.5,0,0) * Time.deltaTime * speed);

}