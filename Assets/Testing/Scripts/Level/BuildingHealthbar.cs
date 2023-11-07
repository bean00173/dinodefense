using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingHealthbar : MonoBehaviour
{
    // essentially same as DinosaurHealthbar script but instead for buildings
    // necessary to have because the health comes from BuildingBehaviour instead of DinoBehaviour
    // this negates conflict of interests
    
    BuildingBehaviour tbb;

    // Start is called before the first frame update
    void Start()
    {
        tbb = this.GetComponentInParent<BuildingBehaviour>();
        this.transform.SetParent(GameObject.Find("HealthCanvas").transform);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = tbb.transform.position;
        this.GetComponent<TextMeshProUGUI>().text = tbb.currentHealth.ToString();
    }
}
