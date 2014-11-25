animation.wrapMode = WrapMode.Loop;

animation["jump"].wrapMode = WrapMode.Clamp;
animation["shoot"].wrapMode = WrapMode.Clamp;

animation["idle"].layer = -1;
animation["run"].layer = -1;

animation.Stop();

function Update () {
	if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1)
		animation.CrossFade("run");
	else
		animation.CrossFade("idle");


	if (Input.GetButtonDown ("Jump"))
		animation.CrossFade("jump");
	
	if (Input.GetButtonDown ("Fire1"))
		animation.CrossFadeQueued("shoot", 0.3, QueueMode.PlayNow);
}