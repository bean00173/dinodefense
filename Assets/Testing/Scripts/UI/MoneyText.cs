using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour
{
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();
        text.text = $"{LevelManager.instance.money}";
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"{LevelManager.instance.currentMoney}";
    }
}
