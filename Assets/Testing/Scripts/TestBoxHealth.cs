using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestBoxHealth : MonoBehaviour
{
    TempBoxBehaviour tbb;

    // Start is called before the first frame update
    void Start()
    {
        tbb = this.GetComponentInParent<TempBoxBehaviour>();
        this.transform.SetParent(GameObject.Find("HealthCanvas").transform);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = tbb.transform.position;
        this.GetComponent<TextMeshProUGUI>().text = tbb.currentHealth.ToString();
    }
}
