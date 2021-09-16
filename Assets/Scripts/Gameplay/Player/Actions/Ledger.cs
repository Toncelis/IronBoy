using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class Ledger : MonoBehaviour
    {
        [SerializeField]
        Transform playerTransform;

        public Objects.Ledge CheckForLedge()
        {
            Objects.Ledge tempLedge = null;

            foreach (Collider2D collider in Physics2D.OverlapBoxAll(
                playerTransform.position, playerTransform.lossyScale, 0, LayerMask.GetMask("BackgroundInteractables"))
                )
            {
                if ((tempLedge = collider.GetComponent<Objects.Ledge>()) != null) break;
            }
            return tempLedge;
        }
    }
}
