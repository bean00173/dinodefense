using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaceableBuilding : MonoBehaviour
{
    public GameObject buildingPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(0).GetComponent<Image>().sprite = buildingPrefab.GetComponent<SpriteRenderer>().sprite;
        this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"${buildingPrefab.GetComponent<BuildingBehaviour>().building.cost}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
