using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Cinemachine;
using static UnityEditor.PlayerSettings;

public class ParticleBehaviour : MonoBehaviour
{
    ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    public float _explosiveForce, _upwardsForce, _radius, _damage;

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
        int i = 0;

        while (i < numCollisionEvents)
        {
            RaycastHit2D[] hit = Physics2D.CircleCastAll(collisionEvents[i].intersection, _radius, Vector2.up);
            foreach (RaycastHit2D rayHit in hit)
            {
                try
                {
                    Rigidbody2D rb = rayHit.collider.GetComponent<Rigidbody2D>();
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
                        rb.AddForceAtPosition(force, pos, ForceMode2D.Impulse);
                    }                    
                }
                catch (System.Exception e)
                {
                    Debug.Log("Rigidbody this one has not.");
                }

            }

            i++;
        }

        this.GetComponent<CinemachineImpulseSource>().GenerateImpulseWithForce(.5f);
    }
}
