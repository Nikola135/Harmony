using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FollowBall : MonoBehaviour {

	//Ball that camera follows
	public GameObject ball;

	//Camera Position to be calculated
	float ballx, bally, ballz;

	//Used for changing camera view and controls change based on camera view.
	public static int c; //c ima vrednosti 0,1,2,3 gde je 0 podrazumevna opcija, 1 okreneš se 

	//Podešavanje kontorla: Forward... će biti na WASD, a AForward na Arrows.
	public static KeyCode Forward;
	public static KeyCode Back;
	public static KeyCode Left;
	public static KeyCode Right;
	public static KeyCode AForward;
	public static KeyCode ABack;
	public static KeyCode ALeft;
	public static KeyCode ARight;

	//TODO Razmišljam kako će izgledati main manu, ideja je da se učita poslednji pređeni nivo i da kamera leti random preko njega
	/*Takođe da upravo kamera bude objekat koji se ne uništava učitavanjem nove scene (takođe da se ne uništavaju ni kanvasi.)
	 Sled izvršavanja:
	 Učita se Main Manu scena, pregleda se u odgovarajućem file-u koji je poslednji pređeni nivo, učita se ta scena.
	 */
	/*Skraćenice: MM=Main Manu, LL=Last Level, S=Scene, C=Canvas, NL=Next Level, PRC=NEMAM POJMA, N=Nothing, */
	/*Redosled izvršavanja:
	 S:MM,C:MM>S:LL,C:MM>*/
	/*Main manu opcije:
	 Next Level>S:NL,C:N
	 Select Level>S:LL,C:SL
	 Load Level from File>PRC
	 Make Custom Level>PRC
	 Options>S:LL,C:OP
	 Quit*/

	//All Canvases that are going to exist.
	public Canvas CPause;
	public Canvas CFinish;
	public Canvas CMainManu;
	public Canvas COptions;
	public Canvas CSelectLevel;
	public Canvas CLoadLevel;

	//Bools that say what are we doing now.
	public static bool PauseTrue;
	public static bool FinishTrue;

	//All Buttos that we have
	public Button NextLVL;
	public Button Resume;
	public Button BackToManu;
	public Button Options;
	public Button Quit;
	//TODO?public Button Save;

	//Text that writes your time
	public Text timerText;

	//Exact time
	private int milisecCount;
	private int centisecCount;
	private int decisecCount;
	private float secondsCount;
	private int minuteCount;
	private int hourCount;

	//Saved state of the ball when we pause
	Vector3 savedVelocity;
	Vector3 savedAngularVelocity;

	//Used for calculating new rotation of the camera
	Quaternion rotation;
	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		//Initializing timers
		milisecCount = 0;
		centisecCount = 0;
		decisecCount = 0;
		secondsCount = 0f;
		minuteCount = 0;
		hourCount = 0;

		//Initializing Bools
		PauseTrue = false;
		FinishTrue = false;

		//Adding Listeners for Buttons
		NextLVL.onClick.AddListener(NextLVLTaskOnClick);
		Resume.onClick.AddListener(ResumeTaskOnClick);
		BackToManu.onClick.AddListener(BackToManuTaskOnClick);
		Options.onClick.AddListener(OptionsTaskOnClick);
		Quit.onClick.AddListener(QuitTaskOnClick);

		//Initializing Speed and Rotation Speed and camera view
		rotationSpeed = 12.0F;
		c = 0;

		//Initializing controls
		Forward = KeyCode.W;
		Back = KeyCode.S;
		Left = KeyCode.A;
		Right = KeyCode.D;
		AForward = KeyCode.UpArrow;
		ABack = KeyCode.DownArrow;
		ALeft = KeyCode.LeftArrow;
		ARight = KeyCode.RightArrow;
	}

	//TODO Will be shown in Main Manu and on Level finish, Will Load Next Level
	void NextLVLTaskOnClick(){
		print ("NextLVL...");
	}

	//Will be shown in Pause Manu, and Resumes game.
	void ResumeTaskOnClick(){
		print ("Resume");
		CPause.enabled = false;
		PauseTrue = false;
		BallMove.rb.isKinematic = false;
		BallMove.rb.AddForce( savedVelocity, ForceMode.VelocityChange );
		BallMove.rb.AddTorque( savedAngularVelocity, ForceMode.VelocityChange );
	}

	//TODO Will be shown in Pause Menu, Finish Manu, Options Manu (That we enterd from Main Manu) and Level Select Manu
	//TODO Will take us to Main Manu
	void BackToManuTaskOnClick(){
	}

	//TODO Will be shown in Main Manu and in Pause Manu (Probably won't be as powerfull as that one in Main Manu)
	//TODO Will offer Sound, Graphics and Control Settings
	void OptionsTaskOnClick(){
	}

	//TODO Will be shown in Main Manu, Finish Manu and Pause Manu
	//Quits Application
	void QuitTaskOnClick(){
		print ("Quit...");
		Application.Quit ();
	}

	// Update is called once per frame
	void Update () {

		UpdateTimerUI();

		//Pauses and unpauses game by pressing ESC
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (CPause.enabled == true) {//Ako je već pauzirano, sada kažemo da se nastavlja
				CPause.enabled = false;
				PauseTrue = false;
				//Allows Ball movment 
				BallMove.rb.isKinematic = false;
				//Loads Previous Ball movment stats so that Ball continues
				BallMove.rb.AddForce( savedVelocity, ForceMode.VelocityChange );
				BallMove.rb.AddTorque( savedAngularVelocity, ForceMode.VelocityChange );
			}
			else {//Ako nije pauzirano, sada pauziramo
				CPause.enabled = true;
				PauseTrue = true;
				//Saves Current Ball movment stats for later use
				savedVelocity = BallMove.rb.velocity;
				savedAngularVelocity = BallMove.rb.angularVelocity;
				//Makes Ball static
				BallMove.rb.isKinematic = true;
			}
		}


		//Updates Camera position Above the Ball
		ballx = ball.transform.position.x;
		bally = ball.transform.position.y + (float)3.6;
		ballz = ball.transform.position.z;

		//Changes Camera view if C or V pressed
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

		//Updates Camera position based on chosen view
		switch (c) {
		case 0:
			ballz = ballz - (float)5.0;

			//Updates Controls acordingly
			Forward = KeyCode.W;
			Back = KeyCode.S;
			Left = KeyCode.A;
			Right = KeyCode.D;
			AForward = KeyCode.UpArrow;
			ABack = KeyCode.DownArrow;
			ALeft = KeyCode.LeftArrow;
			ARight = KeyCode.RightArrow;

			//Changes Camera Rotation based on camera view
			rotation = Quaternion.Euler (40,0,0);

			//Moves Camera acordingly
			transform.rotation =  Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
			break;
		case 1:
			//Same as previous
			ballx = ballx - (float)5.0;

			Forward = KeyCode.A;
			Back = KeyCode.D;
			Left = KeyCode.S;
			Right = KeyCode.W;
			AForward = KeyCode.LeftArrow;
			ABack = KeyCode.RightArrow;
			ALeft = KeyCode.DownArrow;
			ARight = KeyCode.UpArrow;

			rotation = Quaternion.Euler (40,90,0);

			transform.rotation =  Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
			break;
		case 2:
			ballz = ballz + (float)5.0;

			Forward = KeyCode.S;
			Back = KeyCode.W;
			Left = KeyCode.D;
			Right = KeyCode.A;
			AForward = KeyCode.DownArrow;
			ABack = KeyCode.UpArrow;
			ALeft = KeyCode.RightArrow;
			ARight = KeyCode.LeftArrow;

			rotation = Quaternion.Euler (40,180,0);

			transform.rotation =  Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
			break;
		case 3:
			ballx = ballx + (float)5.0;

			Forward = KeyCode.D;
			Back = KeyCode.A;
			Left = KeyCode.W;
			Right = KeyCode.S;
			AForward = KeyCode.RightArrow;
			ABack = KeyCode.LeftArrow;
			ALeft = KeyCode.UpArrow;
			ARight = KeyCode.DownArrow;

			rotation = Quaternion.Euler (40,270,0);

			transform.rotation =  Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
			break;
		case 4:
			//TODO GAME LOST
			break;
		case 5:
			//TODO GAME FINISH
			break;
		case -1:
			//
		default:
			//What to do if no view is selected???
			//TODO Add loding game view
			//TODO Add Finish game view
			//TODO Add Lose game view
			ballx = ballx + (float)5.0;
			//print ("This is unexpected XD");
			rotation = Quaternion.Euler (40,270,0);
			transform.rotation =  Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
			break;
		
		}
		//Updates Camera View
		transform.position = Vector3.MoveTowards (transform.position, new Vector3 (ballx, bally, ballz), rotationSpeed/12);
	}

	//Call this on update
	public void UpdateTimerUI(){

		//Set timer UI
		if (PauseTrue == false) {
			secondsCount += Time.deltaTime;
			decisecCount = (int)(secondsCount * 10) - (int)secondsCount * 10;
			centisecCount = (int)(secondsCount * 100) - (int)secondsCount * 100 - decisecCount * 10;
			milisecCount = (int)(secondsCount * 1000) - (int)(secondsCount * 100) * 10;

			if (secondsCount >= 60) {
				minuteCount++;
				secondsCount = 0;
			}
			if (minuteCount >= 60) {
				hourCount++;
				minuteCount = 0;
			}    
		}
		//Update timer
		timerText.text = "Time: " + hourCount +":"+ minuteCount +":"+(int)secondsCount + "." + decisecCount + centisecCount + milisecCount;
	}



}
/*Garbege:
 * 		//NextLVL = yourButton.GetComponent<Button>();
		/*bally = ball.transform.position.y + (float)3.6;
		ballz = ball.transform.position.z - (float)5.0;
		transform.position = new Vector3(ball.transform.position.x, bally, ballz);
//if()
//transform.position.z = ballz - 5;
//transform.position = new Vector3(ballx, bally, ballz);
//transform.rotation = new Quaternion (40,270,0);
			//transform.position = new Vector3(ballx, bally, ballz);
			//transform.rotation = new Quaternion (40,180,0);
						//transform.position = new Vector3(ballx, bally, ballz);
			//transform.rotation = new Quaternion.Euler(40,90,0);
						//transform.position = new Vector3 (ballx, bally, ballz);
 */