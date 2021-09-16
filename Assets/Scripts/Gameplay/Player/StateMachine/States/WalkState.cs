using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.StateMachine
{
    public class WalkState : PlayerState
    {
        protected override IEnumerator StateRoutine()
        {
            Debug.Log("Enter WalkState");

            animator.SetTrigger("Run");

            while (true)
            {
                yield return null;

                condition.RaiseTemperature(-model.WalkCooling * Time.deltaTime, true);
                playerRigidbody.velocity =
                    new Vector2(Input.GetAxis("Horizontal") * model.WalkSpeed, playerRigidbody.velocity.y);

                if (Input.GetAxis("Vertical") < 0 & Input.GetButtonDown("Jump"))
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
                else if (Input.GetAxis("Horizontal") == 0)
                {
                    stateController.ChangeState(PlayerStateController.StateNames.idle);
                    break;
                }
                else if (Input.GetButton("Run"))
                {
                    stateController.ChangeState(PlayerStateController.StateNames.run);
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