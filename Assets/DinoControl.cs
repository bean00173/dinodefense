using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DinoControl : MonoBehaviour
{
    Vector2 startPos;
    bool holding;
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        holding = true;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnMouseDrag()
    {
        Debug.Log("Holding");

        GameManager.instance.currentObject = this.gameObject;
        LevelManager.instance.validSpace.ShowSpace();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        mousePosition.z = 0;
        this.transform.position = mousePosition;
    }

    private void OnMouseUp()
    {
        if (!GameManager.instance.mouseValid)
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
            StartCoroutine(ReturnToInitialPos());
        }

        GameManager.instance.currentObject = null;
        LevelManager.instance.validSpace.HideSpace();
    }

    private IEnumerator ReturnToInitialPos()
    {
        Vector3 initial = this.transform.position;
        float time = 0;
        float duration = .25f;

        while (time < duration)
        {
            this.transform.position = Vector3.Lerp(initial, startPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        this.transform.position = startPos;
        this.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
