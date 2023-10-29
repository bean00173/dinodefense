using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UILevelInteractable : MonoBehaviour
{
    public Level level;
    public UnityEvent loadLevel = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        
    }

    private void OnMouseDown()
    {
        loadLevel?.Invoke();
        SceneManager.instance.LoadScene(level.sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
