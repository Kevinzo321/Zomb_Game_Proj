using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public GameObject zombCont, hudCont, gameOverPanel;
    public Text zombCount, timeCount, countDownText;
    public bool gamePlaying { get; private set; } // safe
    public int countdownTime;


    private int numTotalZomb, numSlayedZomb;
    private float startTime, elapsedTime;
    TimeSpan timePlaying;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        numTotalZomb = zombCont.transform.childCount;
        numSlayedZomb = 0;
        zombCount.text = "Zombie : 0 / " + numTotalZomb;
        timeCount.text = "Time: 00:00.00";
        gamePlaying = false;

        StartCoroutine(CountdownToStart());
    }

    private void BeginGame()
    {
        gamePlaying = true;
        startTime = Time.time;
    }

    private void Update()
    {
        if(gamePlaying)
        {
            elapsedTime = Time.time - startTime; 
            timePlaying = TimeSpan.FromSeconds(elapsedTime);

            string timePlayerStr = "Time: " + timePlaying.ToString("mm' : 'ss'.'ff");
            timeCount.text = timePlayerStr;
        }
    }

    public void SlayZomb()
    {
        numSlayedZomb++;

        string zombCounterStr = "Zombie: " + numSlayedZomb + " / " + numTotalZomb;
        zombCount.text = zombCounterStr;

        if(numSlayedZomb >= numTotalZomb)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gamePlaying = false;
        Invoke("ShowGameOverScreen", 1.25f);
    }

    private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        hudCont.SetActive(false);
        string timePlayerStr = "Time: " + timePlaying.ToString("mm' : 'ss'.'ff");
        gameOverPanel.transform.Find("FinalTimeText").GetComponent<Text>().text = timePlayerStr;
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countDownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        BeginGame();
        countDownText.text = "KILL!";

        yield return new WaitForSeconds(1f);

        countDownText.gameObject.SetActive(false);
    }

}
