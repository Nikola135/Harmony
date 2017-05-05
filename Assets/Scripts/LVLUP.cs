using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVLUP : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	void OnTriggerEnter(Collider col)
	{
		/*if (col.gameObject.tag == "Sphere")
			Debug.Log ("Collision!");
		{*/
		print ("LVLUP!!!");
		FollowBall.c = 1000;
		FollowBall.Forward = KeyCode.Y;
		FollowBall.Back = KeyCode.Y;
		FollowBall.Left = KeyCode.Y;
		FollowBall.Right = KeyCode.Y;
		//col.gameObject.GetComponent<Rigidbody>().AddForce (Vector3.up * 200 * Time.deltaTime);
		//}
	}
}
