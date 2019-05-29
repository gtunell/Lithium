using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HydrogenGameController : MonoBehaviour {

    HydrogenController hydrogenController;
    LithiumControllers0 lithiumControllers;
    FireballController fireballController;
    BoxController boxController;

    int highscore;
    int score;

    string[] winTexts = { "You succumb to the flame.", "That has got to hurt.", "Need some ointment for that burn?", "Burn, baby! Burn.", "*Ashton Kutchor voice* BURN!"};
    public Text winText;
    public Text scoreText;
    public Text highscoreText;
    public Text retryText;
    public bool gameover;

    GameObject fireball;

    bool canRestart;

    private void Awake()
    {
        highscore = 0;
    }

    // Use this for initialization
    void Start () {
        hydrogenController = GameObject.Find("Hydrogen").GetComponent<HydrogenController>();
        fireballController = GameObject.Find("FireballController").GetComponent<FireballController>();
        lithiumControllers = GameObject.Find("LithiumControllers0").GetComponent<LithiumControllers0>();
        boxController = GameObject.Find("BoxController").GetComponent<BoxController>();
        gameover = false;
        score = 0;
        winText.text = "";
        scoreText.text = "";
        highscoreText.text = "";
        retryText.text = "";
        canRestart = false;

	}
	
	// Update is called once per frame
	void Update () {

        // Quit anytime using ESC or "Q"
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        { 
            Time.timeScale = 1;
            Debug.Log("QUIT button hit");     // for debugging in editor
            // Application.Quit();
            SceneManager.LoadScene(0);
        }

        // Hit 'R' to restart level
        if (canRestart && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            // UnityEngine.SceneManagement.SceneManager.LoadScene("lvl1");
            score = 0;
            gameover = false;
            hydrogenController.Restart();
            fireballController.StartSpawn();
            lithiumControllers.StartSpawn();
            boxController.StartSpawn();

            winText.text = "";
            retryText.text = "";
            canRestart = false;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Time.timeScale = (Time.timeScale == 0.5f ? 1 : 0.5f);

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            fireballController.StopSpawn();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = (Time.timeScale > 0 ? 0 : 1);
        }

        if (!gameover && Input.GetKeyDown(KeyCode.Space))
        {
            hydrogenController.Jump();
        }

        if (!gameover && Input.GetKeyUp(KeyCode.Space))
        {
            hydrogenController.StopJump();
        }

        scoreText.text = "" + score;
        highscoreText.text = "Best: " + highscore; 

        

	}

    public void Score()
    {
        score++;
        if (score > highscore)
        {
            highscore = score;
        }
    }

    public void SignalFireball(bool tooSlow)
    {

        int rand = (int) (Random.value * 5);

        if (tooSlow)
            winText.text = "You snooze, you lose.";
        else
            winText.text = "That's gotta hurt.";
            
        //winText.text = winTexts[rand];
        

        canRestart = true;
        retryText.text = "Hit 'r' to retry.";
        gameover = true;
        hydrogenController.StopJump();
        hydrogenController.Burnt();
        fireballController.StopSpawn();
        lithiumControllers.StopSpawn();
        boxController.StopSpawn();
       
    }

    public void SignalFire() {

        winText.text = "You succumb to the flame.";

        canRestart = true;
        retryText.text = "Hit 'r' to retry.";
        gameover = true;
        hydrogenController.StopJump();
        hydrogenController.Burnt();
        fireballController.StopSpawn();
        lithiumControllers.StopSpawn();
        boxController.StopSpawn();
        
    }
}
