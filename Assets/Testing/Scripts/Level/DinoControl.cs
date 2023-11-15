using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DinoControl : MonoBehaviour
{
    //this is a specialized script so that you can move around the dinosaurs

    Vector2 startPos;
    public bool holding { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag() // when the mouse is dragging the dinosaur
    {
        holding = true;
        Debug.Log("Holding");

        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        GameManager.instance.currentObject = this.gameObject;
        LevelManager.instance.validSpace.ShowSpace(); // tell the playable area to highlight itself so that the player can see where is valid
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // create a new vector based on the mouse position
        mousePosition.z = 0; // keep that position at 0 on the z axis
        this.transform.position = mousePosition; // update dinosaur position to the mouse position
    }

    private void OnMouseUp() // when the left mouse button is lifted 
    {
        if (!GameManager.instance.mouseValid) // if the mouse is not valid
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 0; // disable gravity 
            StartCoroutine(ReturnToInitialPos()); // run coroutine to return the dinosaur to its initial safe spot
        }

        holding = false;
        GameManager.instance.currentObject = null; 
        LevelManager.instance.validSpace.HideSpace(); // re-hide the valid space box
    }

    private IEnumerator ReturnToInitialPos() // coroutine to return dinosaurs to their original safe space
    {
        Vector3 initial = this.transform.position;
        float time = 0;
        float duration = .25f;

        while (time < duration) 
        {
            this.transform.position = Vector3.Lerp(initial, startPos, time / duration); // lerp position between initial and startPos based on hard coded duration (.25f)
            time += Time.deltaTime;
            yield return null;
        }

        this.transform.position = startPos; // once while loop is executed snap dinosaur to spot in case errors occured during Lerping
        this.GetComponent<Rigidbody2D>().gravityScale = 1; // reactivate gravity
    }
}
