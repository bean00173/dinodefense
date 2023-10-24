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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        this.transform.position = mousePosition;
    }

    private void OnMouseUp()
    {
        this.GetComponent<BoxCollider>().isTrigger = true;

        if (GameManager.instance.mouseInvalid)
        {
            StartCoroutine(ReturnToInitialPos());
        }
    }

    private IEnumerator ReturnToInitialPos()
    {
        float time = 0;
        float duration = .25f;

        while(time < duration)
        {
            Vector3.Lerp(this.transform.position, startPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        this.transform.position = startPos;
        this.GetComponent<BoxCollider>().isTrigger = false;
        Destroy(this.gameObject);
    }
}
