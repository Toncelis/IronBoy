using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.StateMachine
{
    public class IdleState : PlayerState
    {
        protected override IEnumerator StateRoutine()
        {
            Debug.Log("Enter IdleState");

            animator.SetTrigger("Idle");
            playerRigidbody.velocity = Vector2.zero;
            
            while (true)
            {
                yield return null;
                condition.RaiseTemperature(-model.IdleCooling*Time.deltaTime, true);

                if (Input.GetAxis("Vertical")<0 & Input.GetButtonDown("Jump"))
                {
                    if (stateController.actionList.Drop())
                    {
                        continue;
                    }
                }
                if (Input.GetButtonDown("Jump"))
                { stateController.actionList.Jump(); }

                if (Input.GetButtonDown("WeakAttack"))
                {
                    // Attack
                }
                else if (Input.GetButtonDown("StrongAttack"))
                {
                    stateController.ChangeState(PlayerStateController.StateNames.strongAttack);
                    break;
                }
                else if (Input.GetButtonDown("Blink"))
                {
                    if (stateController.actionList.Blink())
                    {
                        stateController.ChangeState(PlayerStateController.StateNames.idle);
                        break;
                    }
                }
                else if (Input.GetButtonDown("Dash"))
                {
                    // Dash
                }
                else if (playerRigidbody.velocity.y != 0)
                {
                    stateController.ChangeState(PlayerStateController.StateNames.midair);
                    break;
                }
                else if (Input.GetAxis("Horizontal") != 0)
                {
                    stateController.ChangeState(PlayerStateController.StateNames.walk);
                    break;
                }
                else if (Input.GetButtonDown("Ladder"))
                {
                    if (stateController.actionList.CheckLadder() != null)
                    {
                        stateController.ChangeState(PlayerStateController.StateNames.ladder);
                        break;
                    }
                }
            }
            yield return null;
        }
    }
}
