using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Cinemachine;

public class ParticleBehaviour : MonoBehaviour
{
    ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    public float _explosiveForce, _upwardsForce, _explosionRadius, _damage;

    // Start is called before the first frame update
    void Start()
    {
        part = this.GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb)
            {               
                if (other.CompareTag("Building"))
                {
                    rb.GetComponent<BuildingBehaviour>().TakeDamage(_damage);
                }
                else
                {
                    rb.GetComponent<DinoBehaviour>().TakeDamage(_damage);
                }

                Vector2 pos = collisionEvents[i].intersection;
                Vector2 force = (rb.position - pos * _explosiveForce) + Vector2.up * _upwardsForce;
                //Rigidbody2DExt.AddExplosionForce(rb, _explosiveForce, pos, _explosionRadius, _upwardsForce, ForceMode2D.Impulse);

                rb.AddForceAtPosition(force, pos, ForceMode2D.Impulse);
                this.GetComponent<CinemachineImpulseSource>().GenerateImpulseWithForce(.5f);
            }
            i++;
        }
    }
}
