using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UILevelInteractable : MonoBehaviour
{
    public Level level;
    public UnityEvent loadLevel = new UnityEvent();
    
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Level {level.levelNum}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelSelected()
    {
        loadLevel?.Invoke();
        SceneManager.instance.LoadScene(level.sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
