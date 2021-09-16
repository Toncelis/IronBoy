using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.StateMachine
{
    public class LadderState : PlayerState
    {
        private Vector2 toprightBounds => ladder.TopRightBound;
        private Vector2 botleftBounds => ladder.BotLeftBound;

        [SerializeField]
        private float climbingSpeed;
        [SerializeField]
        private Transform playerHitbox;

        private Objects.Ladder ladder;

        public static Objects.Ladder CheckForLadder (Transform checkHitbox)
        {
            Objects.Ladder tempLadder = null;

            foreach (Collider2D collider in Physics2D.OverlapBoxAll(
                checkHitbox.position, checkHitbox.lossyScale * 0.5f, 0, LayerMask.GetMask("BackgroundInteractables"))
                )
            {
                if ((tempLadder = collider.GetComponent<Objects.Ladder>()) != null) break;
            }
            return tempLadder;
        }


        public override void StopState()
        {
            playerRigidbody.gravityScale = 1;
            base.StopState();
        }

        protected override IEnumerator StateRoutine ()
        {
            Debug.Log("Ladder State");

            ladder = stateController.actionList.CheckLadder();
            playerRigidbody.gravityScale = 0;

            animator.SetTrigger("Ladder");

            yield return null;

            while (true)
            {
                yield return null;

                playerRigidbody.velocity = climbingSpeed * new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

                if (playerRigidbody.transform.position.x >= toprightBounds.x & playerRigidbody.velocity.x > 0)
                {
                    playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
                }
                else if (playerRigidbody.transform.position.x <= botleftBounds.x & playerRigidbody.velocity.x < 0)
                {
                    playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
                }

                if (playerRigidbody.transform.position.y >= toprightBounds.y & playerRigidbody.velocity.y > 0)
                {
                    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
                    stateController.actionList.UnsafeJump();
                    stateController.ChangeState(PlayerStateController.StateNames.midair);
                }
                else if (playerRigidbody.transform.position.y <= botleftBounds.y & playerRigidbody.velocity.y < 0)
                {
                    stateController.ChangeState(PlayerStateController.StateNames.idle);
                    break;
                }

                if (Input.GetButtonDown("Jump"))
                {
                    stateController.actionList.UnsafeJump();
                    stateController.ChangeState(PlayerStateController.StateNames.midair);
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
            }

            yield return null;
        }
    }
}
