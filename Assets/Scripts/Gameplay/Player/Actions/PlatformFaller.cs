using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlatformFaller : MonoBehaviour
    {
        [SerializeField]
        private Transform groundHitBox;
        [SerializeField]
        private PlayerV2.PlayerModel model;

        private bool CheckPlatform()
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(groundHitBox.position, groundHitBox.lossyScale, 0, LayerMask.GetMask("Default", "Enemy"));
            if (colliders == null)
            {
                return false;
            }

            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.name);
                if (!collider.CompareTag("Platform"))
                {
                    return false;
                }
            }
            return true;
        }

        private void Drop()
        {
            Debug.Log("dropped");
            Collider2D[] colliders = Physics2D.OverlapBoxAll(groundHitBox.position, groundHitBox.lossyScale * 0.5f, 0, LayerMask.GetMask("Default"));
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.name);
                if (collider.CompareTag("Platform"))
                {
                    collider.gameObject.GetComponent<Objects.OneWayPlatform>().SetCollisionOff();
                }
            }
        }

        public bool TryDrop()
        {
            if (CheckPlatform())
            {
                Drop();
                return true;
            }
            return false;
        }
    }
}
