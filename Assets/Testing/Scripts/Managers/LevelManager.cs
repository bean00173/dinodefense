using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;

public enum levelState
{
    prep,
    sim,
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public ParticleSystem meteor;
    public Transform dinoContainer, buildingContainer;
    [HideInInspector] public bool simulating;
    [HideInInspector] public levelState state;

    public int money;
    public int currentMoney { get; private set; }

    public ValidSpace validSpace;

    public float simulationTime;
    float time;
    public TextMeshProUGUI timer;

    public Results results;

    [Header("Simulation Events")]
    public UnityEvent simStart = new UnityEvent();
    public UnityEvent simFinished = new UnityEvent();

    float dinoAmount;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        time = simulationTime;
        StartCoroutine(SetupTimer());
        state = levelState.prep;

        currentMoney = money;

        dinoAmount = dinoContainer.childCount;
        Debug.Log(CalculateScore());
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != null)
        {
            timer.text = TimerString();
        }

        if (!meteor.isPlaying && state == levelState.sim)
        {
            SimFinished();
        }
    }

    public void StartGame()
    {
        StopAllCoroutines();
        timer.gameObject.SetActive(false);
        state = levelState.sim;
        simStart?.Invoke();
    }

    public void SimFinished()
    {
        //if(dinoContainer.childCount == 0)
        //{
        //    Debug.Log("You Lose... >:(");
        //}
        //else
        //{
        //    Debug.Log($"Victory !! Stars Earned.... {CalculateScore()}");
        //}
        GameManager.instance.StoreScore(CalculateScore());
        results.gameObject.SetActive(true);
        results.SendResults(CalculateScore());
    }

    private IEnumerator SetupTimer()
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        
    }

    private string TimerString()
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time - minutes * 60);

        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void Spend(int spendings)
    {
        currentMoney -= spendings;
    }

    public int CalculateScore()
    {
        float scoreFloat = dinoContainer.childCount / dinoAmount;

        if (scoreFloat == 1)
        {
            return 3;
        }
        else if(scoreFloat < 1 && scoreFloat >= .66f)
        {
            return 2;
        }
        else if(scoreFloat < .66f && scoreFloat >= .33)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
