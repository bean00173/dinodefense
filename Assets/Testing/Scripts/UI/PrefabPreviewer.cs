using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class PrefabPreviewer : MonoBehaviour
{
    public static PrefabPreviewer Instance;

    public TextMeshProUGUI _buildingName, _buildingMaterial, _buildingCost, _buildingEndurance;
    public Image _buildingIcon;

    bool active;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.instance.state == levelState.sim) active = false;

        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(active);
        }
    }
    
    public void ActivateWithDetails(GameObject go)
    {
        Building building = go.GetComponent<BuildingBehaviour>().building;
        active = true;
        _buildingName.text = building.name;
        _buildingMaterial.text = $"Material : {building.material}";
        _buildingCost.text = building.cost.ToString();
        _buildingEndurance.text = $"Endurance : {building.endurance}";
        _buildingIcon.sprite = go.GetComponent<SpriteRenderer>().sprite;
    }

    public void Deactivate()
    {
        active = false;
    }
}
