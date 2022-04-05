using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private StarterAssetsInputs starterAssetsInputs;

    public GameObject enemyInstance;
    public GameObject spEnemyInstance;
    public Transform spawnPoint;

    public Transform playerRef;
    public Transform camRef;

    public TMP_Text timerDisplayRef;
    public TMP_Text killsDisplayRef;
    public Image deathPanel;
    public GameObject restartButton;

    public float timer = 0f;
    public float loops = 0f;

    public Stopwatch stopwatch;

    public int mobsCount = 0;
    public int kills = 0;

    public bool gameFinished;
    private void Awake()
    {
        Instance = this;
        starterAssetsInputs = playerRef.GetComponent<StarterAssetsInputs>();
    }
    private void Start()
    {
        stopwatch = new Stopwatch();

        stopwatch.Start();

    }
    private void Update()
    {
        if (gameFinished && starterAssetsInputs.jump)
        {
            RestartGame();
        }
        if (gameFinished) return;
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            loops += 1;
            timer = 0f;

            // spawn units
            spawnUnits();
        }

        SetTime();
        SetKills();

    } 

    private void spawnUnits()
    {
        if (mobsCount < 5)
        {
            GameObject enemy = Instantiate(enemyInstance, spawnPoint.position, Quaternion.identity); ;
            enemy.GetComponent<NavMesh>().moveTransform = playerRef;
            enemy.transform.Find("Canvas").GetComponent<BillBoard>().cam = camRef;
        } else
        {
            GameObject enemy_2 = Instantiate(spEnemyInstance, spawnPoint.position, Quaternion.identity); ;
            enemy_2.GetComponent<NavMeshSp>().moveTransform = playerRef;
            enemy_2.transform.Find("Canvas").GetComponent<BillBoard>().cam = camRef;
        }
        mobsCount += 1;

    }
    public void EndGame()
    {
        // death sound

        // show panel
        Color c = deathPanel.color;
        c.a = 255;
        deathPanel.color = c;

        // show button
        restartButton.SetActive(true);

        // free mouse
        starterAssetsInputs.cursorLocked = false;
        starterAssetsInputs.enabled = false;
        starterAssetsInputs.enabled = true;

        //stop timer
        stopwatch.Stop();

        gameFinished = true;
    }

    public void RestartGame()
    {
        //stopwatch.Stop();
        //TimeSpan ts = stopwatch.Elapsed;

        //// Format and display the TimeSpan value.
        //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        //    ts.Hours, ts.Minutes, ts.Seconds,
        //    ts.Milliseconds / 10);
        //UnityEngine.Debug.Log("RunTime " + elapsedTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void SetTime()
    {
        TimeSpan ts = stopwatch.Elapsed;

        // Format and display the TimeSpan value.
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        timerDisplayRef.text = elapsedTime;
    }

    private void SetKills()
    {
        killsDisplayRef.text = $"Kills: {kills}";
    }
}
