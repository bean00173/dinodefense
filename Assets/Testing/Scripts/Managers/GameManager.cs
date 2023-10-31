using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public List<timeperiod> enabledTimePeriods { get ; private set; } = new List<timeperiod>();

    public GameObject currentObject;

    Rect screenRect;

    public UnityEvent exitApp = new UnityEvent();

    public Scene currentScene;

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
        Debug.Log($"{currentObject} : {mouseValid}");
    }

    public void ExitApplication()
    {
        exitApp?.Invoke();

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
}
