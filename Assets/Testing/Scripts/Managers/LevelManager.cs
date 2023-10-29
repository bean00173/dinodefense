using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public enum levelState
{
    prep,
    sim,
}
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public ParticleSystem meteor;
    public Transform dinoContainer;
    public bool simulating;

    public levelState state;

    public float simulationTime;
    float time;
    public TextMeshProUGUI timer;

    public UnityEvent simStart = new UnityEvent();
    public UnityEvent simFinished = new UnityEvent();

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
        meteor.gameObject.SetActive(true);
        simStart?.Invoke();
    }

    public void SimFinished()
    {
        if(dinoContainer.childCount == 0)
        {
            Debug.Log("Fail");
        }
        else
        {
            Debug.Log("Win");
        }
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
}
