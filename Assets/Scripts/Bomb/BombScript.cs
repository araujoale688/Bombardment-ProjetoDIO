using Life;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bomb
{
    public class BombScript : MonoBehaviour
    {
        public GameObject explosionParticlePrefab;
        public GameObject woodBreakingPrefab;

        public float explosionDelay = 5f;
        public float blastRadius = 5f;

        public int blastDamage = 10;

        void Start()
        {
            StartCoroutine(ExplosionCoroutine());
        }

        private IEnumerator ExplosionCoroutine()
        {
            yield return new WaitForSeconds(explosionDelay);

            Explode();
        }

        private void Explode()
        {
            if(explosionParticlePrefab != null)
            {
                Instantiate(explosionParticlePrefab, transform.position, explosionParticlePrefab.transform.rotation);
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

            foreach (Collider collider in colliders)
            {
                GameObject hitObject = collider.gameObject;

                if(hitObject.CompareTag("Platform"))
                {
                    LifeScript lifeScript = hitObject.GetComponent<LifeScript>();

                    if(lifeScript != null)
                    {
                        //Calcular Distancia.
                        float distance = (hitObject.transform.position - transform.position).magnitude;
                        float distanceRate = Mathf.Clamp(distance / blastRadius, 0, 1);

                        //Calcular Dano com Base na Distancia.
                        float damageRate = 1f - Mathf.Pow(distanceRate, 4);
                        int damage = (int) Mathf.Ceil(damageRate * blastDamage);

                        lifeScript.health -= damage;

                        if(lifeScript.health <= 0)
                        {
                            Instantiate(woodBreakingPrefab, hitObject.transform.position, woodBreakingPrefab.transform.rotation);

                            Destroy(collider.gameObject);
                        }
                    }              
                }          
            }

            Destroy(gameObject);
        }
    }
}