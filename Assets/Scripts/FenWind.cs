using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenWind : MonoBehaviour {

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
		print ("Sudar!!!");
		col.gameObject.GetComponent<Rigidbody>().AddForce (Vector3.up * 200 * Time.deltaTime);
		//}
	}
	void OnTriggerStay(Collider col)
	{
		/*if (col.gameObject.tag == "Sphere")
			Debug.Log ("Collision!");
		{*/
		print ("Sudar!!!");
		col.gameObject.GetComponent<Rigidbody>().AddForce (Vector3.up * 200 * Time.deltaTime);
		//}
	}
}
