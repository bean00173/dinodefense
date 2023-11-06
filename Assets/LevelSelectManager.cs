using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LevelSelectEvent : UnityEvent<UILevelInteractable>
{

}
public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager instance;
    public static UILevelInteractable currentLevel;

    public int totalStars;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelSelected(UILevelInteractable level)
    {
        currentLevel = level;
    }

    public void ReturnedToMenu()
    {
        currentLevel.StoreScore(GameManager.instance.score);
        totalStars += GameManager.instance.score;
        GameManager.instance.StoreScore(0);
        currentLevel = null;
    }
}
