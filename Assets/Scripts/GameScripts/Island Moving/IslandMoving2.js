var isMovingRight : boolean = true;
var speed : float = 13.0;
var timeCount : float = 0.0;

function Update () {

	timeCount += Time.deltaTime;
	Debug.Log(timeCount);
	if(timeCount <= 2.5f) {
		
		isMovingRight = true;
	} 
	
	if(timeCount > 2.5f && timeCount <= 5.0f) {
		
		isMovingRight = false;
	} else if(timeCount > 5.1f) {
		timeCount = 0.0f;
	}
	
	if(isMovingRight) {
		transform.Translate(Vector3(0,0,1) * Time.deltaTime * speed * (-1));
		//transform.Translate(transform.right * Time.deltaTime * speed);
	} else {
		transform.Translate(Vector3(0,0,1) * Time.deltaTime * speed  );
		//transform.Translate(transform.left * Time.deltaTime * speed);
		
	}
	
	

	//transform.Translate(Vector3(0.5,0,0) * Time.deltaTime * speed);

}