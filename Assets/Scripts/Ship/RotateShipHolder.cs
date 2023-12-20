using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ship
{
    public class RotateShipHolder : MonoBehaviour
    {
        public float degreesPerSecond = 90f;

        void Update()
        {
            float stepY = degreesPerSecond * Time.deltaTime;
            transform.Rotate(0, stepY, 0);
        }
    }
}