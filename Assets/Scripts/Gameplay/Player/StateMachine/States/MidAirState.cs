using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.StateMachine
{
    public class MidAirState : PlayerState
    {
        private Transform _tempTransform;
        bool jumping;
        float speed;
        float cooling;

        protected override IEnumerator StateRoutine()
        {
            Debug.Log("Enter MidAirState");

            animator.SetTrigger("Jump");

            if (playerRigidbody.velocity.y>0)
            {
                jumping = true;
            }

            speed = Input.GetButton("Run") ? model.RunSpeed : model.WalkSpeed;
            cooling = Input.GetButton("Run") ? model.RunCooling : model.WalkCooling;

            while (true)
            {
                yield return null;
                if (playerRigidbody.velocity.y <= 0 )
                {
                    jumping = false;
                }
                if (jumping & !Input.GetButton("Jump"))
                {
                    jumping = false;
                    playerRigidbody.velocity =
                        new Vector2(Input.GetAxis("Horizontal") * speed, 0);
                }
                else
                {
                    playerRigidbody.velocity =
                        new Vector2(Input.GetAxis("Horizontal") * speed, playerRigidbody.velocity.y);
                }
                condition.RaiseTemperature(-cooling * Time.deltaTime, true);

                if (Input.GetButtonDown("WeakAttack"))
                {
                    // Attack
                }
                else if (Input.GetButtonDown("StrongAttack") & (Input.GetAxis("Vertical")<0))
                {
                    // Bush
                }
                else if (Input.GetButtonDown("Blink"))
                {
                    yield return null;
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
                else if (playerRigidbody.velocity.y == 0)
                {
                    stateController.ChangeState(PlayerStateController.StateNames.idle);
                    break;
                }
                else if (Input.GetButtonDown("Ladder"))
                {
                    if (stateController.actionList.CheckLedge()!=null)
                    {
                        Debug.Log("Found Ledge");
                        stateController.ChangeState(PlayerStateController.StateNames.ledge);
                        break;
                    }
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
