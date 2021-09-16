using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyVision : MonoBehaviour
    {
        [SerializeField]
        Transform visionHitbox;

        [SerializeField]
        private Transform chasingVisionHitbox;

        public delegate void SimpleEventHandler(Player.PlayerCondition playerCondition);

        public event SimpleEventHandler playerSpotted;

        public bool CheckForPlayer ()
        {
            if (Physics2D.OverlapBox(visionHitbox.position, visionHitbox.lossyScale, 0, LayerMask.GetMask("Player"))!=null)
            {
                return true;
            }
            return false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            { 
                playerSpotted?.Invoke(collision.gameObject.GetComponent<Player.PlayerCondition>());
            } 
        }

        public bool CheckChasingVision()
        {
            if (Physics2D.OverlapBox(chasingVisionHitbox.position, chasingVisionHitbox.lossyScale, 0, LayerMask.GetMask("Player")) != null)
            {
                return true;
            }
            return false;
        }
    }
}
