using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Results : MonoBehaviour
{
    public Transform starContainer;
    public TextMeshProUGUI titleText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendResults(int score)
    {
        int stars = 0;
        foreach(Transform star in starContainer)
        {
            if(stars < score)
            {
                star.GetChild(0).gameObject.SetActive(true);
                stars++;
            }
            else
            {
                star.GetChild(0).gameObject.SetActive(false);
            }
        }
        titleText.text = score > 0 ? "You Win!" : "Game Over";
    }
}
