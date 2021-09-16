using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.StateMachine
{
    public class LedgeState : PlayerState
    {
        public override void StopState ()
        {
            playerRigidbody.gravityScale = 1;
            base.StopState();
        }

        protected override IEnumerator StateRoutine()
        {
            Debug.Log("Ledge State");

            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.transform.position = stateController.actionList.CheckLedge().positionOnLedge;
            playerRigidbody.gravityScale = 0;

            animator.SetTrigger("Ledge");

            yield return null;

            while (true)
            {
                yield return null;

                if (Input.GetButtonDown("Jump") || Input.GetAxis("Vertical") > 0)
                {
                    stateController.actionList.UnsafeJump();
                    stateController.ChangeState(PlayerStateController.StateNames.midair);
                    break;
                }
                else if (Input.GetAxis("Vertical")<0)
                {
                    stateController.ChangeState(PlayerStateController.StateNames.idle);
                }
                else if (Input.GetButtonDown("Blink"))
                {
                    if (stateController.actionList.Blink())
                    {
                        stateController.ChangeState(PlayerStateController.StateNames.idle);
                        break;
                    }
                }
            }

            yield return null;
        }
    }
}
