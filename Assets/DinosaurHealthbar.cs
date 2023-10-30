using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DinosaurHealthbar : MonoBehaviour
{
    DinoBehaviour db;

    // Start is called before the first frame update
    void Start()
    {
        db = this.GetComponentInParent<DinoBehaviour>();
        this.transform.SetParent(GameObject.Find("HealthCanvas").transform);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = db.transform.position;
        this.GetComponent<TextMeshProUGUI>().text = db.currentHealth.ToString();
    }
}
