using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class Jumper : MonoBehaviour
    {
        [SerializeField]
        private Transform groundHitBox;
        [SerializeField]
        private PlayerV2.PlayerModel model;
        [SerializeField]
        Rigidbody2D myRigidbody;

        private bool CheckGround ()
        {
            return (Physics2D.OverlapBoxAll(
                groundHitBox.position, groundHitBox.lossyScale, 0,
                LayerMask.GetMask("Default","Enemy")).Length != 0);                
        }

        public void Jump ()
        {
            myRigidbody.AddForce(model.JumpStrength * Vector2.up, ForceMode2D.Impulse);
        }

        public bool TryJump ()
        {
            if (CheckGround())
            {
                Jump();
                return true;
            }
            return false;
        }
    }
}
