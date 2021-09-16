using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class ActionList : MonoBehaviour
    {
        enum ActionsEnum
        {
            walk,
            run,

            dash,
            blink,

            //crouch,
            jump,

            weakAttack,
            strongAttack,
            bushAttack,

            die
        }

        [SerializeField]
        Blinker blinker;
        public bool Blink()
        {
            return blinker.Blink();
        }

        [SerializeField]
        Jumper jumper;
        public bool Jump ()
        {
            return jumper.TryJump();
        }
        public void UnsafeJump ()
        {
            jumper.Jump();
        }

        [SerializeField]
        PlatformFaller platformFaller;
        public bool Drop ()
        {
            return platformFaller.TryDrop();
        }

        [SerializeField]
        Ledger ledger;
        public Objects.Ledge CheckLedge ()
        {
            return ledger.CheckForLedge();
        }

        [SerializeField]
        Transform playerHitbox;
        public Objects.Ladder CheckLadder ()
        {
            return StateMachine.LadderState.CheckForLadder(playerHitbox);
        }

    }
}
