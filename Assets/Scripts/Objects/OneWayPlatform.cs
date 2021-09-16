using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public class OneWayPlatform : MonoBehaviour
    {
        [SerializeField]
        Collider2D myCollider;
        [SerializeField]
        float timer;


        private void Start()
        {
            gameObject.tag = "Platform";
        }

        public void SetCollisionOff ()
        {
            myCollider.enabled = false;
            Invoke("SetCollisionOn", timer);
        }

        private void SetCollisionOn ()
        {
            myCollider.enabled = true;
        }
    }
}
