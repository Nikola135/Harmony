using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour {

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
		if (!FollowBall.PauseTrue) {
			//TODO Ovo možda neće biti implementirano u finalnoj verziji.
			if (Input.GetKeyDown (KeyCode.X)) {
				s++;
				if (s == 3)
					s = 0;
			}
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
			//TODO ovo je kao implementacija nekakvog skoka, radi testiranja ventilatora, još nisam odlučio da li da implementiram skok
			/*if (Input.GetKey (KeyCode.Space))
			rb.AddForce (Vector3.up * 200 * Time.deltaTime);*/

			if (Input.GetKey (KeyCode.LeftArrow) /*|| Input.GetKey (KeyCode.A)*/ || Input.GetKey (FollowBall.Left))
			//transform.Rotate(Vector3.forward * speed * Time.deltaTime);
				rb.AddForce (Vector3.left * speed * Time.deltaTime);
			if (Input.GetKey (KeyCode.RightArrow) /*|| Input.GetKey (KeyCode.D)*/ || Input.GetKey (FollowBall.Right))
			//transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
				rb.AddForce (-Vector3.left * speed * Time.deltaTime);
			if (Input.GetKey (KeyCode.UpArrow) || /*Input.GetKey (KeyCode.W) ||*/ Input.GetKey (FollowBall.Forward))
			//transform.Rotate(Vector3.forward * speed * Time.deltaTime);
				rb.AddForce (Vector3.forward * speed * Time.deltaTime);
			if (Input.GetKey (KeyCode.DownArrow) /*|| Input.GetKey (KeyCode.S)*/ || Input.GetKey (FollowBall.Back))
			//transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
				rb.AddForce (-Vector3.forward * speed * Time.deltaTime);

		} 

	}
}
