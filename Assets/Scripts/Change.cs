using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		BallMove.s = 0;

		//print ("LVLUP!!!");
		//FollowBall.FinishTrue = true;
		//FollowBall.c = 1000;
	}

}
