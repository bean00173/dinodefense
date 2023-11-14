using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathGhost : MonoBehaviour
{
    public float speed, lifetime;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DeleteThis), lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.up * Time.deltaTime * speed;
    }

    private void DeleteThis()
    {
        Destroy(this.gameObject);
    }
}
