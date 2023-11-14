using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ClickAndDrag : MonoBehaviour
{
    // this is the building counterpart to the dinosaur movement script
    // it controls all of the movement starting from dragging the building into the scene from the market and afterwards

    Vector2 startPos;
    bool holding;
    public GameObject xmark;

    public bool devBuilding;

    BuildingBehaviour bb;

    // Start is called before the first frame update
    void Start()
    {
        bb = this.GetComponent<BuildingBehaviour>();
       startPos = this.transform.position;
        if(!devBuilding)
        {
            holding = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // this script separates out all of the dragging into the update so that it doesnt rely on the OnMouseDrag 
        // this makes it so that you dont have to spawn an object in the market, and then drag it into the scene

        if (holding) // if you are holding (which is on by default so object follows mouse out of the market)
        {
            Debug.Log("Holding");

            xmark.SetActive(!GameManager.instance.mouseValid); // activate the delete icon based on whether the object is outside the valid space or not 

            GameManager.instance.currentObject = this.gameObject; // all this is the same functionality as the dinosaurs
            LevelManager.instance.validSpace.ShowSpace();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            this.transform.position = mousePosition;

            if (Input.GetKeyDown(KeyCode.R)) // if r is tapped when the object is held, rotate the object 90 degrees
            {
                Quaternion fromAngle = transform.rotation;
                Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 90f));

                this.transform.rotation = Quaternion.Lerp(fromAngle, toAngle, 1f);
            }

            if (Input.GetKeyDown(KeyCode.F)) // if f is tapped when the object is held flip the local Y scale
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x, -this.transform.localScale.y, this.transform.localScale.z);
            }

            if (Input.GetKeyUp(KeyCode.Mouse0)) // if the mouse button is released, drop the building piece
            {
                if (!GameManager.instance.mouseValid) // if not in a valid spot 
                {
                    this.GetComponent<Rigidbody2D>().gravityScale = 0; 
                    LevelManager.instance.Spend(-this.GetComponent<BuildingBehaviour>().building.cost); // refund cost
                    Destroy(this.gameObject); // destroy object
                }

                if (holding) // if released when holding, update the sorting layer
                {
                    if(this.GetComponent<SpriteRenderer>().sortingLayerName == "UI")
                    {
                        this.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Default");
                        bb.ObjectPlaced(); // invoke building place event
                    }
                    else
                    {
                        bb.onDrop?.Invoke(); // if building dropped after being placed invoke event
                    }
                    holding = false;
                }

                GameManager.instance.currentObject = null;
                LevelManager.instance.validSpace.HideSpace(); 
            }
        }
    }

    private void OnMouseDown()
    {
        bb.onPickUp?.Invoke(); // when building clicked invoke event
    }
    private void OnMouseDrag() // if being dragged 
    {
        holding = true; // set holding to true
    }

}
