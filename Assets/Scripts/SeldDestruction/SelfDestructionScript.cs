using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SelfDestruction
{
    public class SelfDestructionScript : MonoBehaviour
    {
        public float delay = 1f;

        void Start()
        {
            StartCoroutine(DestroyPaticle(delay));
        }

        private IEnumerator DestroyPaticle(float delay)
        {
            yield return new WaitForSeconds(delay);

            Destroy(gameObject);
        }
    }
}