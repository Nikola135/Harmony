using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour {

	//Is actualy the force, and speed is depending on mass/force relation.
	public int speed;

	public static Rigidbody rb;

	public static int s; //govori o stanju, stanje 0 je papir 100/0.25, stanje 1 je drvo 700/2, stanje 2 je kamen 2100/12

	// Use this for initialization
	void Start () {
		speed = 700;//1400 700 350 175/200 100   2100 1400 700  350  100
		rb = GetComponent<Rigidbody> ();
		rb.mass = (float)2;//8 2 1 0,5 0.25      12   8    2    1    0,25
		s = 1;
	}

	// Update is called once per frame
	void Update () {

		//If we can control the Ball
		if (!FollowBall.PauseTrue && !FollowBall.FinishTrue) {

			//Changing Ball properties by pressing X
			//TODO Ovo možda neće biti implementirano u finalnoj verziji.
			if (Input.GetKeyDown (KeyCode.X)) {
				s++;
				if (s == 3)
					s = 0;
			}
			//Changing Ball properties
			switch (s) {
			case 0:
				speed = 125;
				rb.mass = (float)0.25;
				break;
			case 1:
				speed = 700;
				rb.mass = (float)2;

				break;
			case 2:
				speed = 2700;
				rb.mass = (float)12;
				break;
			case 3:
			default:
				print ("This is unexpected XD");
				break;
			}

			if (Input.GetKey (FollowBall.ALeft) || Input.GetKey (FollowBall.Left))
				rb.AddForce (Vector3.left * speed * Time.deltaTime);

			if (Input.GetKey (FollowBall.ARight) || Input.GetKey (FollowBall.Right))
				rb.AddForce (-Vector3.left * speed * Time.deltaTime);

			if (Input.GetKey (FollowBall.AForward) || Input.GetKey (FollowBall.Forward))
				rb.AddForce (Vector3.forward * speed * Time.deltaTime);

			if (Input.GetKey (FollowBall.ABack) || Input.GetKey (FollowBall.Back))
				rb.AddForce (-Vector3.forward * speed * Time.deltaTime);

		} 

	}
}
/*Garbege:
//TODO ovo je kao implementacija nekakvog skoka, radi testiranja ventilatora, još nisam odlučio da li da implementiram skok
/*if (Input.GetKey (KeyCode.Space))
			rb.AddForce (Vector3.up * 200 * Time.deltaTime);

//transform.Rotate(Vector3.forward * speed * Time.deltaTime);
*/