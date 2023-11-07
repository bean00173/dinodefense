using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<ParticleSystem>().isPlaying) // checks if particle system is done then destroys it to save RAM
        {
            Destroy(this.gameObject);
        }
    }
}
