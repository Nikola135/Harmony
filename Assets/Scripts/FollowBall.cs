using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FollowBall : MonoBehaviour {
	public GameObject ball;
	float ballx, bally, ballz;
	public static int c; //c ima vrednosti 0,1,2,3 gde je 0 podrazumevna opcija, 1 okreneš se 
	//public static int x;
	public static KeyCode Forward;
	public static KeyCode Back;
	public static KeyCode Left;
	public static KeyCode Right;

	public Canvas Pause;

	public static bool PauseTrue;

	public Button NextLVL;
	public Button Resume;
	public Button BackToManu;
	public Button Options;
	public Button Quit;
	//TODO?public Button Save;

	public Text timerText;
	private int milisecCount;
	private int centisecCount;
	private int decisecCount;
	private float secondsCount;
	private int minuteCount;
	private int hourCount;



	Vector3 savedVelocity;
	Vector3 savedAngularVelocity;


	Quaternion rotation;
	public float rotationSpeed;
	// Use this for initialization
	void Start () {
		milisecCount = 0;
		centisecCount = 0;
		decisecCount = 0;
		secondsCount = 0f;
		minuteCount = 0;
		hourCount = 0;


		PauseTrue = false;
		//NextLVL = yourButton.GetComponent<Button>();
		NextLVL.onClick.AddListener(NextLVLTaskOnClick);
		Resume.onClick.AddListener(ResumeTaskOnClick);
		BackToManu.onClick.AddListener(BackToManuTaskOnClick);
		Options.onClick.AddListener(OptionsTaskOnClick);
		Quit.onClick.AddListener(QuitTaskOnClick);

		rotationSpeed = 12.0F;
		c = 0;
		Forward = KeyCode.W;
		Back = KeyCode.S;
		Left = KeyCode.A;
		Right = KeyCode.D;
	}

	void NextLVLTaskOnClick(){
		print ("NextLVL...");
	
	}
	void ResumeTaskOnClick(){
		print ("Resume");
		Pause.enabled = false;
		PauseTrue = false;
		BallMove.rb.isKinematic = false;
		BallMove.rb.AddForce( savedVelocity, ForceMode.VelocityChange );
		BallMove.rb.AddTorque( savedAngularVelocity, ForceMode.VelocityChange );
	}

	void BackToManuTaskOnClick(){


	}

	void OptionsTaskOnClick(){


	}

	void QuitTaskOnClick(){
		print ("Quit...");
		Application.Quit ();
	}
	







	// Update is called once per frame
	void Update () {
		//c = x;
		UpdateTimerUI();
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Pause.enabled == true) {//Ako je već pauzirano, sada kažemo da se nastavlja
				Pause.enabled = false;
				PauseTrue = false;
				BallMove.rb.isKinematic = false;

				BallMove.rb.AddForce( savedVelocity, ForceMode.VelocityChange );
				BallMove.rb.AddTorque( savedAngularVelocity, ForceMode.VelocityChange );
			}
			else {//Ako nije pauzirano, sada pauziramo
				Pause.enabled = true;
				PauseTrue = true;
				//TODO Bad implementation of game pause > WORKING NOW

				savedVelocity = BallMove.rb.velocity;
				savedAngularVelocity = BallMove.rb.angularVelocity;

				BallMove.rb.isKinematic = true;

			}
		}

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

	//call this on update
	public void UpdateTimerUI(){
		//set timer UI
		if (PauseTrue == false) {
			secondsCount += Time.deltaTime;
			decisecCount = (int)(secondsCount * 10) - (int)secondsCount * 10;
			centisecCount = (int)(secondsCount * 100) - (int)secondsCount * 100 - decisecCount * 10;
			milisecCount = (int)(secondsCount * 1000) - (int)(secondsCount * 100) * 10;
			
			//print ("radi");
			if (secondsCount >= 60) {
				minuteCount++;
				secondsCount = 0;
			}
			if (minuteCount >= 60) {
				hourCount++;
				minuteCount = 0;
			}    
		}
		//print ("radi2");
		timerText.text = "Time: " + hourCount +":"+ minuteCount +":"+(int)secondsCount + "." + decisecCount + centisecCount + milisecCount;
	}



}
