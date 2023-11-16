using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ParticleBehaviour : MonoBehaviour
{
    // this script is attached to the meteor particle system and controls the collisions 

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
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents); // stores how many collision events have happened
        int i = 0;


        while (i < numCollisionEvents) 
        {
            //RaycastHit2D[] hit = Physics2D.CircleCastAll(other.transform.position, _radius, Vector2.up); // casts a circle from the collision point of the specified event
            //foreach (RaycastHit2D rayHit in hit)
            //{
            //    try
            //    {
            //        Rigidbody2D rb = rayHit.collider.GetComponent<Rigidbody2D>();
            //        if (rb)
            //        {
            //            Debug.Log(other.tag);

            //            if (other.CompareTag("Building")) // if the collision object has a rigidbody and is tagged building then the building takes damage
            //            {
            //                rb.GetComponent<BuildingBehaviour>().TakeDamage(_damage);
            //            }
            //            else // if it has a rigidbody but is not a building then it must be a dinosaur and therefore the dinosaur takes damage
            //            {
            //                rb.GetComponent<DinoBehaviour>().TakeDamage(_damage);
            //            }

            //            Vector2 pos = collisionEvents[i].intersection;
            //            Vector2 force = (rb.position - pos * _explosiveForce) + Vector2.up * _upwardsForce; // sends out a force defined by inspector values to the object in case it has not died, in which case it will be pushed
            //            rb.AddForceAtPosition(force, pos, ForceMode2D.Impulse);
            //        }
            //    }
            //    catch (System.Exception e)
            //    {
            //        Debug.Log("Rigidbody this one has not.");
            //    }

            //}

            if (other.CompareTag("Building")) // if the collision object has a rigidbody and is tagged building then the building takes damage
            {
                RaycastHit2D[] hit = Physics2D.CircleCastAll(other.transform.position, _radius, Vector2.up); // casts a circle from the collision point of the specified event
                foreach (RaycastHit2D rayHit in hit)
                {
                    try
                    {
                        Rigidbody2D rb = rayHit.collider.GetComponent<Rigidbody2D>();
                        if (rb)
                        {
                            Debug.Log(other.tag);

                            if (other.CompareTag("Building")) // if the collision object has a rigidbody and is tagged building then the building takes damage
                            {
                                rb.GetComponent<BuildingBehaviour>().TakeDamage(_damage);
                            }
                            Vector2 pos = collisionEvents[i].intersection;
                            Vector2 force = (rb.position - pos * _explosiveForce) + Vector2.up * _upwardsForce; // sends out a force defined by inspector values to the object in case it has not died, in which case it will be pushed
                            rb.AddForceAtPosition(force, pos, ForceMode2D.Impulse);
                        }
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log("Rigidbody this one has not.");
                    }

                }
            }
            else if (other.CompareTag("Dinosaur"))// if it has a rigidbody but is not a building then it must be a dinosaur and therefore the dinosaur takes damage
            {
                other.GetComponent<DinoBehaviour>().TakeDamage(_damage);
            }

            i++;
        }

        this.GetComponent<CinemachineImpulseSource>().GenerateImpulseWithForce(.5f); // regardless of anything, send out some camera shake to the camera when a collision happens
    }
}
