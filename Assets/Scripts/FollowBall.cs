using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour {
	public GameObject ball;
	float ballx, bally, ballz;
	public static int c; //c ima vrednosti 0,1,2,3 gde je 0 podrazumevna opcija, 1 okreneš se 
	//public static int x;
	public static KeyCode Forward;
	public static KeyCode Back;
	public static KeyCode Left;
	public static KeyCode Right;

	Quaternion rotation;
	public float rotationSpeed;
	// Use this for initialization
	void Start () {
		rotationSpeed = 12.0F;
		c = 0;
		Forward = KeyCode.W;
		Back = KeyCode.S;
		Left = KeyCode.A;
		Right = KeyCode.D;
	}
	
	// Update is called once per frame
	void Update () {
		//c = x;
		ballx = ball.transform.position.x;
		bally = ball.transform.position.y + (float)3.6;
		ballz = ball.transform.position.z;
		if (Input.GetKeyDown (KeyCode.C)) {
			c++;
			if (c == 4)
				c = 0;
		}
		if (Input.GetKeyDown (KeyCode.V)) {
			c--;
			if (c == -1)
				c = 3;
		}
		switch (c) {
		case 0:
			ballz = ballz - (float)5.0;
			Forward = KeyCode.W;
			Back = KeyCode.S;
			Left = KeyCode.A;
			Right = KeyCode.D;
			//transform.position = new Vector3 (ballx, bally, ballz);
			rotation = Quaternion.Euler (40,0,0);
			transform.rotation =  Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
			break;
		case 1:
			ballx = ballx - (float)5.0;

			Forward = KeyCode.A;
			Back = KeyCode.D;
			Left = KeyCode.S;
			Right = KeyCode.W;
			//transform.position = new Vector3(ballx, bally, ballz);
			//transform.rotation = new Quaternion.Euler(40,90,0);

			rotation = Quaternion.Euler (40,90,0);
			transform.rotation =  Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
			break;
		case 2:
			ballz = ballz + (float)5.0;

			Forward = KeyCode.S;
			Back = KeyCode.W;
			Left = KeyCode.D;
			Right = KeyCode.A;
			//transform.position = new Vector3(ballx, bally, ballz);
			//transform.rotation = new Quaternion (40,180,0);

			rotation = Quaternion.Euler (40,180,0);
			transform.rotation =  Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
			break;
		case 3:
			ballx = ballx + (float)5.0;

			Forward = KeyCode.D;
			Back = KeyCode.A;
			Left = KeyCode.W;
			Right = KeyCode.S;
			//transform.position = new Vector3(ballx, bally, ballz);
			//transform.rotation = new Quaternion (40,270,0);

			rotation = Quaternion.Euler (40,270,0);
			transform.rotation =  Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
			break;

		default:
			ballx = ballx + (float)5.0;
			//print ("This is unexpected XD");
			rotation = Quaternion.Euler (40,270,0);
			transform.rotation =  Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
			break;
		
		}
		transform.position = Vector3.MoveTowards (transform.position, new Vector3 (ballx, bally, ballz), rotationSpeed/12);

		/*bally = ball.transform.position.y + (float)3.6;
		ballz = ball.transform.position.z - (float)5.0;
		transform.position = new Vector3(ball.transform.position.x, bally, ballz);*/
		//if()
		//transform.position.z = ballz - 5;
	}
}
