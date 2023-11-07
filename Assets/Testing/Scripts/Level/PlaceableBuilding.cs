using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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

    public void SpawnNewObject()
    {
        //Vector3 spawnPos = Camera.main.transform.position + Vector3.up * 5;
        //spawnPos.z = 0;
        if(LevelManager.instance.currentMoney >= buildingPrefab.GetComponent<BuildingBehaviour>().building.cost)
        {
            Vector3 spawnPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            GameObject bb = Instantiate(buildingPrefab, spawnPos, Quaternion.identity, LevelManager.instance.buildingContainer);
            LevelManager.instance.Spend(bb.GetComponent<BuildingBehaviour>().building.cost);
        }
        else
        {
            Debug.Log("Bankruptcy Alert !!");
        }
    }

    private void OnMouseOver()
    {
        PrefabPreviewer.Instance.ActivateWithDetails(buildingPrefab);
    }

    private void OnMouseExit()
    {
        PrefabPreviewer.Instance.Deactivate();
    }
}