using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Results : MonoBehaviour
{
    // this script controls the results panel at the end of the simulation phase in each level

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

    public void SendResults(int score) // this method is called by LevelManager-->SimulationFinished(); in order to store the relevant score for the level
    {
        int stars = 0;
        foreach(Transform star in starContainer) // going through all the stars in the container
        {
            if(stars < score) // if the index 'stars' is less than the achieved score activate one star
            {
                star.GetChild(0).gameObject.SetActive(true);
                stars++; // increment the index
            }
            else // if the index is no longer less than the stars achieved
            {
                star.GetChild(0).gameObject.SetActive(false); // dont activate the star
            }
        }
        titleText.text = score > 0 ? "You Win!" : "Game Over"; // update the text in the panel based on the score
    }
}
