using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DinoCount : MonoBehaviour
{
    int maxCount;

    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();
        maxCount = LevelManager.instance.dinoContainer.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"{LevelManager.instance.dinoContainer.childCount}/{maxCount}";
    }
}
