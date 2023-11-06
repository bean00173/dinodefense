using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UILevelInteractable : MonoBehaviour
{
    public Level level;
    public UnityEvent loadLevel = new UnityEvent();
    [SerializeField] private LevelSelectEvent selectLevel;

    public Transform starContainer;
    public GameObject disabledImg;
    public GameObject button;
    
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Level {level.levelNum}";
        if(LevelSelectManager.instance.totalStars >= level.starsNeeded)
        {
            disabledImg.SetActive(false);
            button.SetActive(true);
        }
        foreach(Transform star in starContainer)
        {
            star.GetChild(0).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelSelected()
    {
        loadLevel?.Invoke();
        selectLevel?.Invoke(this);
        SceneManager.instance.LoadScene(level.sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void StoreScore(int score)
    {
        this.level.stars = score;

        int stars = 0;
        foreach (Transform star in starContainer)
        {
            if (stars < score)
            {
                star.GetChild(0).gameObject.SetActive(true);
                stars++;
            }
            else
            {
                star.GetChild(0).gameObject.SetActive(false);
            }
        }
        // change stars
    }
}
