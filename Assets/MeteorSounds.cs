using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSounds : MonoBehaviour
{
    public GameObject meteorSoundObject;
    public Transform meteorSoundContainer;

    float lifetime;
    float oldest;

    GameObject chosen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDeathSound()
    {
        if(meteorSoundContainer.childCount > 0)
        {
            foreach (Transform child in meteorSoundContainer)
            {
                if (child.GetComponent<MeteorSoundObject>().lifetime > oldest)
                {
                    oldest = lifetime;
                    chosen = child.gameObject;
                }
            }

            chosen.GetComponent<MeteorSoundObject>().doDestroy?.Invoke();
        }
    }

    public void Spawn()
    {
        Instantiate(meteorSoundObject, meteorSoundContainer);
    }
}
