using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceableObject : MonoBehaviour
{
    Vector3 startPos;
    bool invalid;

    public LayerMask invalidLayers;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseDrag()
    {
        Debug.Log("Holding");

        GameManager.instance.currentObject = this.gameObject;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        this.transform.position = mousePosition;
    }

    private void OnMouseUp()
    {
        if (GameManager.instance.mouseInvalid)
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
            StartCoroutine(ReturnToInitialPos());
        }
        
        if(GameManager.instance.currentObject == this) GameManager.instance.currentObject = null;
    }

    private IEnumerator ReturnToInitialPos()
    {
        Vector3 initial = this.transform.position;
        float time = 0;
        float duration = .25f;

        while(time < duration)
        {
            this.transform.position = Vector3.Lerp(initial, startPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        this.transform.position = startPos;
        this.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
