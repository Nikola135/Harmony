using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This Class controls Fan for lifting paper Ball.*/

public class FenWind : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	//Adds fors to the ball that enters Fan area of effect
	void OnTriggerEnter(Collider col)
	{

		print ("Sudar!!!");
		col.gameObject.GetComponent<Rigidbody>().AddForce (Vector3.up * 200 * Time.deltaTime);

	}
	void OnTriggerStay(Collider col)
	{

		print ("Sudar!!!");
		col.gameObject.GetComponent<Rigidbody>().AddForce (Vector3.up * 200 * Time.deltaTime);
	
	}
}
/*Garbege:
		/*if (col.gameObject.tag == "Sphere")
			Debug.Log ("Collision!");
		{
//}
		/*if (col.gameObject.tag == "Sphere")
			Debug.Log ("Collision!");
		{
			//}
 */