using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LevelSelectEvent : UnityEvent<UILevelInteractable>
{
    
}

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
        GameManager.instance.AddLevel(this.level);
        foreach(Transform star in starContainer)
        {
            star.GetChild(0).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.totalStars >= level.starsNeeded)
        {
            Debug.Log($"Activating Level {this.level.levelNum}! {GameManager.instance.totalStars}/ {this.level.starsNeeded} Required Stars");
            disabledImg.SetActive(false);
            button.SetActive(true);
        }

        if (GameManager.instance.FindCurrentLevel(this.level) != null)
        {
            Debug.Log($"Level {this.level.levelNum} has an achieved score of : {GameManager.instance.FindCurrentLevel(this.level).stars} stars !");
            this.level.stars = GameManager.instance.FindCurrentLevel(this.level).stars;
            SetStars();
        }
        else
        {
            Debug.Log($"{this.gameObject.name} has no sister in the Array");
        }
        

        //if(this.level.stars != GameManager.instance.FindCurrentLevel(this.level).stars)
        //{
        //    this.level.stars = GameManager.instance.FindCurrentLevel(this.level).stars;
        //    SetStars();
        //}
    }

    public void LevelSelected()
    {
        loadLevel?.Invoke();
        GameManager.instance.SetCurrentLevel(this.level);
        //selectLevel?.Invoke(this);
        SceneManager.instance.LoadScene(level.sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void SetStars()
    {
        int stars = 0;
        foreach(Transform star in starContainer)
        {
            if(stars < this.level.stars)
            {
                star.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                star.GetChild(0).gameObject.SetActive(false);
            }

            stars++;
        }
    }
}
