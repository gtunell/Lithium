using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerNeon : MonoBehaviour
{
    public GameObject player;
    NeonController neonController;
    // LithiumControllers2 lithiumControllers;

    int highscore;
    int score;

    // string[] winTexts = { "You succumb to the flame.", "That has got to hurt.", "Need some ointment for that burn?", "Burn, baby! Burn.", "*Ashton Kutchor voice* BURN!" };
    public Text winText;
    public Text scoreText;
    public Text highscoreText;
    public Text retryText;
    public bool gameover;

    GameObject fireball;
    GameObject robot;
    GameObject hellicopter;
    GameObject lithium;

    public float speed;

    private Rigidbody rb;

    private void Awake()
    {
        highscore = 0;
    }

    // Use this for initialization
    void Start()
    {
        neonController = GameObject.Find("Neon").GetComponent<NeonController>();
       // lithiumControllers = GameObject.Find("LithiumControllers2").GetComponent<LithiumControllers2>();
        // tileController = GameObject.Find("TileController").GetComponent<TileController>();
        // quadControllers = GameObject.Find("QuadControllers").GetComponent<QuadControllers>();
        // hellicopterControllers = GameObject.Find("HellicopterControllers").GetComponent<HellicopterControllers>();
        gameover = false;
        score = 0;
        winText.text = "";
        scoreText.text = "";
        highscoreText.text = "";
        retryText.text = "";


        robot = Resources.Load("Prefabs/Robot") as GameObject;
        fireball = Resources.Load("Prefabs/NeonFireball") as GameObject;
        hellicopter = Resources.Load("Prefabs/NeonHellicopter") as GameObject;
        lithium = Resources.Load("Prefabs/Lithium0") as GameObject;

        InvokeRepeating("SpawnRobots", 0.0f, 1.0f);
        InvokeRepeating("SpawnFireball", 0.0f, 2.0f);
        InvokeRepeating("SpawnHellicopter", 1.0f, 2.0f);
        InvokeRepeating("SpawnLithium", 0.0f, 3.0f);
    }

    void SpawnRobots()
    {
        float randx = Random.value * 16 - 8;
        float randz = Random.value * 200;
        Vector3 spawn = new Vector3(randx, 0.5f, randz);
        GameObject obj = Instantiate(robot, spawn, Quaternion.Euler(0, 180, 0));
        obj.transform.parent = this.transform;
    }

    void SpawnFireball()
    {
        float randX = Random.value * 16 - 8;
        float randY = Random.value * 9 + 3;

        GameObject obj = Instantiate(fireball, new Vector3(randX, randY, -50), Quaternion.Euler(0, 180, 0));
        obj.transform.parent = this.transform;
    }

    void SpawnHellicopter()
    {
        float randX = Random.value * 16 - 8;
        float randY = Random.value * 11 + 1;

        GameObject obj = Instantiate(hellicopter, new Vector3(randX, randY, 0), Quaternion.Euler(0, 270, 0));
        obj.transform.parent = this.transform;
    }

    void SpawnLithium()
    {


    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * 0.2f;
        float moveVertical = Input.GetAxis("Vertical") * 0.2f;
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        if (neonController.InBounds(player.transform.position + movement))
        {
            player.transform.position = player.transform.position + movement;
        }

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
            neonController.Restart();
            // lithiumControllers.StartSpawn();

            winText.text = "";
            retryText.text = "";
            //hellicopterControllers.StartSpawn();

            Time.timeScale = 1.0f;

            foreach (Transform t in this.transform)
            {
                Destroy(t.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            //transform.Translate(x, 0, 0);
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
            neonController.Jump();
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

    public void SignalGameOver()
    {
        if (!gameover)
        {
            winText.text = "Game Over.";
            retryText.text = "Hit 'r' to retry.";
            gameover = true;
            neonController.StopJump();

            Time.timeScale = 0.3f;
            neonController.Jump();
        }
    }

    public void SignalLoss()
    {
        if (!gameover)
        {
            winText.text = "You are going the wrong way.";
            retryText.text = "Hit 'r' to retry.";
            gameover = true;
            neonController.StopJump();
            // lithiumControllers.StopSpawn();
            // hellicopterControllers.StopSpawn();

            Time.timeScale = 0.02f;
            neonController.Jump();
        }
    }

    public void SignalHellicopter()
    {
        winText.text = "Hmm. No it's not that.";
        retryText.text = "Hit 'r' to retry.";
        gameover = true;
        neonController.StopJump();
        // lithiumControllers.StopSpawn();
        // hellicopterControllers.StopSpawn();

        Time.timeScale = 0.3f;
        neonController.Jump();
    }

}