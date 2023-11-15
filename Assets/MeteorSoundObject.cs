using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeteorSoundObject : MonoBehaviour
{
    public float lifetime {  get; private set; }    
    public UnityEvent doDestroy = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;

        if(this.GetComponent<AudioSource>().isPlaying == false || lifetime >= 5.0)
        {
            Destroy(this.gameObject);
        }
    }

    
}
