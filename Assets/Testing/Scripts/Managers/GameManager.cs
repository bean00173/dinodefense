using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum timeperiod
{
    prehistoric,
    medieval,
    darkage,
    modern,
    future
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool mouseValid;
    public bool intersecting;

    public static Level[] gameLevels { get; private set; } = new Level[15];
    public static Level currentLevel { get; private set; }

    public float leftBound { get; private set; } // LEVEL
    public float rightBound { get; private set; } // LEVEL

    public List<timeperiod> enabledTimePeriods { get ; private set; } = new List<timeperiod>();

    public GameObject currentObject; // LEVEL

    Rect screenRect;

    public UnityEvent exitApp = new UnityEvent();

    public Scene currentScene;

    [HideInInspector] public bool loadingMenu;

    public int totalStars { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        enabledTimePeriods.Add(timeperiod.prehistoric);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateCurrentScene("MainMenu");
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(totalStars);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void UpdateCurrentScene(string scene)
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(scene);
    }

    public bool RandomChance(int chance)
    {
        return UnityEngine.Random.value < chance / 100;
    }

    public void EnableNewTimePeriod(timeperiod period)
    {
        enabledTimePeriods.Add(period);
    }

    public void SetDinoBounds(float left, float right)
    {
        leftBound = left;
        rightBound = right;

        Debug.Log($"Max Left is {left}, Max Right is {right}");
    }

    public void StoreScore(int score)
    {
        totalStars += score;
        FindCurrentLevel(currentLevel).stars = score;

        if(totalStars >= 6 && totalStars < 15)
        {
            EnableNewTimePeriod(timeperiod.medieval);
        }
        else if(totalStars >= 15 && totalStars < 24)
        {
            EnableNewTimePeriod(timeperiod.darkage);
        }
        else if(totalStars >= 24 && totalStars < 33)
        {
            EnableNewTimePeriod(timeperiod.modern);
        }
        else if(totalStars >= 33)
        {
            EnableNewTimePeriod(timeperiod.future);
        }
    }

    public void AddLevel(Level level)
    {
        if (gameLevels[level.levelNum - 1] == null) gameLevels[level.levelNum - 1] = level;
        else Debug.Log("Level Already Stored!!!");
    }

    public void SetCurrentLevel(Level level)
    {
        currentLevel = level;
    }

    public Level FindCurrentLevel(Level level)
    {
        for(int i = 0; i < gameLevels.Length; i++)
        {
            if (gameLevels[i].levelNum == level.levelNum)
            {
                return gameLevels[i];
            }
        }

        return null;
    }

    public void UnlockAll()
    {
        totalStars = 500;

        if(!enabledTimePeriods.Contains(timeperiod.medieval))
        {
            EnableNewTimePeriod(timeperiod.medieval);
        }

        if (!enabledTimePeriods.Contains(timeperiod.darkage))
        {
            EnableNewTimePeriod(timeperiod.darkage);
        }

        if (!enabledTimePeriods.Contains(timeperiod.modern))
        {
            EnableNewTimePeriod(timeperiod.modern);
        }

        if (!enabledTimePeriods.Contains(timeperiod.future))
        {
            EnableNewTimePeriod(timeperiod.future);
        }
    }

}
