using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeliumGameController : MonoBehaviour {

    HeliumController heliumController;
    LithiumControllers1 lithiumControllers;
    TileController tileController;
    QuadControllers quadControllers;
    HellicopterControllers hellicopterControllers;

    int highscore;
    int score;

    // string[] winTexts = { "You succumb to the flame.", "That has got to hurt.", "Need some ointment for that burn?", "Burn, baby! Burn.", "*Ashton Kutchor voice* BURN!" };
    public Text winText;
    public Text scoreText;
    public Text highscoreText;
    public Text retryText;
    public bool gameover;

    GameObject fireball;
    

    private void Awake()
    {
        highscore = 0;
    }

    // Use this for initialization
    void Start()
    {
        heliumController = GameObject.Find("Helium").GetComponent<HeliumController>();
        lithiumControllers = GameObject.Find("LithiumControllers1").GetComponent<LithiumControllers1>();
        // tileController = GameObject.Find("TileController").GetComponent<TileController>();
        // quadControllers = GameObject.Find("QuadControllers").GetComponent<QuadControllers>();
        hellicopterControllers = GameObject.Find("HellicopterControllers").GetComponent<HellicopterControllers>();
        gameover = false;
        score = 0;
        winText.text = "";
        scoreText.text = "";
        highscoreText.text = "";
        retryText.text = "";

    }

    // Update is called once per frame
    void Update()
    {

        // Quit anytime using ESC or "Q"
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale = 1;
            Debug.Log("QUIT button hit");     // for debugging in editor
            // Application.Quit();
            SceneManager.LoadScene(0);
        }

        // Hit 'R' to restart level
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            // UnityEngine.SceneManagement.SceneManager.LoadScene("lvl1");
            score = 0;
            gameover = false;
            heliumController.Restart();
            lithiumControllers.StartSpawn();

            winText.text = "";
            retryText.text = "";
            hellicopterControllers.StartSpawn();

            Time.timeScale = 1.0f;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Time.timeScale = (Time.timeScale == 0.5f ? 1 : 0.5f);

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // fireballController.StopSpawn();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = (Time.timeScale > 0 ? 0 : 1);
        }

        if (!gameover && Input.GetKeyDown(KeyCode.Space))
        {
            heliumController.Jump();
        }

        if (!gameover && Input.GetKeyUp(KeyCode.Space))
        {
            heliumController.StopJump();
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
    
    public void SignalLoss()
    {
        if (!gameover)
        {
            winText.text = "You are going the wrong way.";
            retryText.text = "Hit 'r' to retry.";
            gameover = true;
            heliumController.StopJump();
            lithiumControllers.StopSpawn();
            hellicopterControllers.StopSpawn();

            Time.timeScale = 0.3f;
            heliumController.Jump();
        }
    }
    
    public void SignalHellicopter()
    {
        winText.text = "Hmm. No it's not that.";
        retryText.text = "Hit 'r' to retry.";
        gameover = true;
        heliumController.StopJump();
        lithiumControllers.StopSpawn();
        hellicopterControllers.StopSpawn();

        Time.timeScale = 0.3f;
        heliumController.Jump();
    }
    
}
