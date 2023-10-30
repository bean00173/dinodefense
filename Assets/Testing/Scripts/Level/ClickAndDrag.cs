using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickAndDrag : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
       
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
        //mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        mousePosition.z = 0;
        this.transform.position = mousePosition;
    }


    private void OnMouseUp()
    {
        if (GameManager.instance.mouseInvalid)
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
            StartCoroutine(ReturnToSafeVerticalPos());
        }

        if (GameManager.instance.currentObject == this) GameManager.instance.currentObject = null;
    }

    private IEnumerator ReturnToSafeVerticalPos()
    {
        Vector3 initial = this.transform.position;
        float time = 0;
        float duration = .25f;

        Vector3 returnPos = new Vector3(this.transform.position.x, 0, 0);

        while (time < duration)
        {
            this.transform.position = Vector3.Lerp(initial, returnPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        this.transform.position = returnPos;
        this.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
