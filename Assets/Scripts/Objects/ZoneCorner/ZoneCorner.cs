using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public class ZoneCorner : MonoBehaviour
    {
        [SerializeField] float checkDistance;

        [SerializeField]
        new Collider2D collider;

        /*
        private void Start()
        {
            RaycastHit2D hit;

            hit = Physics2D.Raycast(transform.position, Vector2.left, checkDistance, LayerMask.GetMask("ZoneCorner"));
            if (hit)
            {
                leftCorner = hit.transform.gameObject.GetComponent<ZoneCorner>();
            }
            else
            {
                leftCorner = null;
            }

            hit = Physics2D.Raycast(transform.position, Vector2.right, checkDistance, LayerMask.GetMask("ZoneCorner"));
            if (hit)
            {
                rightCorner = hit.transform.gameObject.GetComponent<ZoneCorner>();
            }
            else
            {
                rightCorner = null;
            }
        }
        */

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy.EnemyController>().Stop();
            }
        }

        public bool Occupied ()
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.NoFilter();
            List<Collider2D> collidersList = new List<Collider2D>();
            if (collider.OverlapCollider(filter, collidersList) != 0)
            {
                return true;
            }
            return false;
        }
    }
}
