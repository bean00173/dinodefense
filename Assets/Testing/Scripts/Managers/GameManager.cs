using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool mouseInvalid;
    public bool intersecting;

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

    }

    // Start is called before the first frame update
    void Start()
    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {

        if (currentObject != null && !intersecting)
        {
            mouseInvalid = !screenRect.Contains(Input.mousePosition) ? true : false;
        }

        Debug.Log(mouseInvalid);
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
}
