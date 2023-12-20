using UnityEngine;

namespace Life
{
    public class LifeScript : MonoBehaviour
    {
        public int maxHealth;

        [HideInInspector]
        public int health;

        private void Start()
        {
            health = maxHealth;
        }
    }
}