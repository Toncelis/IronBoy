using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Objects;

namespace Game.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] EnemyModel model;

        [SerializeField]
        EnemyVision vision;
        
        Transform player;
        
        [SerializeField]
        Rigidbody2D myRigidbody;

        float walkingSpeed => model.WalkSpeed;
        float runningSpeed => model.RunSpeed;
        [SerializeField]
        float frameRate;

        [SerializeField]
        Animator animator;

        [SerializeField]
        Transform attackHitbox;
        [SerializeField]
        float attackDistance;

        [SerializeField]
        float attackLength;
        [SerializeField]
        float attackTriggerTime;

        float damage => model.Damage;

        [SerializeField]
        string stateInfo;


        Coroutine idleCoroutine;
        Coroutine chaseCoroutine;

        [SerializeField]
        SpriteRenderer spriteRenderer;

        [SerializeField]
        GameObject hitboxHolder;
        Transform[] hitboxes;
        List<float> hitboxesStandartPosition;

        [SerializeField] float checkDistance;

        private void Start ()
        {
            hitboxes = hitboxHolder.GetComponentsInChildren<Transform>();
            hitboxesStandartPosition = new List<float>();
            foreach (Transform hitbox in hitboxes)
            {
                hitboxesStandartPosition.Add(hitbox.localPosition.x);
            }

            vision.playerSpotted += SpotPlayer;
            StartIdling();
        }

        private void SpotPlayer(Player.PlayerCondition playerCondition)
        {
            player = playerCondition.transform;
            Debug.Log("spotted " + (idleCoroutine != null));
            if (idleCoroutine != null)
            {
                StopIdling();
                StartChasing();
            }
        }

        private void Attack()
        {
            foreach (Collider2D collider in Physics2D.OverlapBoxAll(attackHitbox.position, attackHitbox.lossyScale * 0.5f,0, LayerMask.GetMask("Player")))
            {
                Player.PlayerCondition playerCondition = collider.gameObject.GetComponent<Player.PlayerCondition>();
                if (playerCondition!=null)
                {
                    playerCondition.GetHit(damage);
                    break;
                }
            }
        }

        #region Chase routine
        private void StartChasing ()
        {
            if (chaseCoroutine != null)
            {
                StopCoroutine(chaseCoroutine);
            }

            chaseCoroutine = StartCoroutine(ChasingPlayer ());
        }
        private void StopChasing ()
        {
            if (chaseCoroutine != null)
            {
                StopCoroutine(chaseCoroutine);
                chaseCoroutine = null;
            }
        }

        [SerializeField] private float blindChaseLength;

        IEnumerator ChasingPlayer ()
        {
            yield return null;
            animator.SetTrigger("Run");
            stateInfo = "chasing";

            float chaseTimer = 0f;

            while (Mathf.Abs(player.position.x - myRigidbody.position.x) > attackDistance)
            {
                movementTargetX = player.position.x;
                MakeStep(true);
                if(vision.CheckChasingVision())
                { chaseTimer = 0; }
                else
                { 
                    chaseTimer += 1 / frameRate; 
                    if (blindChaseLength < chaseTimer)
                    {
                        StopChasing();
                        StartIdling();
                    }
                }
                yield return new WaitForSeconds(1/frameRate);
            }
            
            StartCoroutine(AttackingPlayer());
            chaseCoroutine = null;
            yield return null;
        }
        #endregion 

        IEnumerator AttackingPlayer()
        {
            yield return null;
            stateInfo = "attacking";

            if (movementDirectionisRight)
            {
                if (player.position.x < transform.position.x)
                {
                    Flip();
                    movementDirectionisRight = !movementDirectionisRight;
                }
            }
            else
            {
                if (player.position.x > transform.position.x)
                {
                    Flip();
                    movementDirectionisRight = !movementDirectionisRight;
                }
            }

            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(attackTriggerTime);
            
            Attack();
            yield return new WaitForSeconds(attackLength - attackTriggerTime);
            
            if (vision.CheckChasingVision())
            {
                StartChasing();
            }
            else
            {
                StartIdling();
            }
            yield return null;
        }

        IEnumerator StepingBack (bool oldMovementDirection)
        {
            yield return null;
            stateInfo = "Cooling down";
            animator.SetTrigger("Idle");
            movementTargetX = myRigidbody.position.x + (oldMovementDirection ? -2 : 2);

            yield return new WaitForSeconds(1f);

            animator.SetTrigger("Run");
            while (!MakeStep(true))
            {
                yield return new WaitForSeconds(1 / frameRate);
            }

            if (vision.CheckForPlayer())
            {
                StartChasing();
            }
            else
            {
                StartIdling();
            }
            yield return null;
        }

        public void Stop()
        {
            Debug.Log("Stop command happened!");
            if (chaseCoroutine != null || idleCoroutine != null)
            {
                Debug.Log("stopingCoroutines");
                StopChasing();
                StopIdling();
                StartCoroutine(StepingBack(movementDirectionisRight));
            }
        }

        #region Idle routine
        private void StartIdling()
        {
            if (idleCoroutine != null)
            {
                StopCoroutine(idleCoroutine);
            }

            idleCoroutine = StartCoroutine(Idling());
        }
        private void StopIdling()
        {
            if (idleCoroutine != null)
            {
                StopCoroutine(idleCoroutine);
                idleCoroutine = null;
            }
        }
        IEnumerator Idling ()
        {
            yield return null;
            stateInfo = "Idle";

            while (true)
            {
                animator.SetTrigger("Idle");
                yield return new WaitForSeconds(1f);
            }
        }
        #endregion

        #region Movement
        private bool movementDirectionisRight;
        private float movementTargetX;

        /// <summary>
        /// Returns true if rigidbody reaches supposed destination
        /// </summary>
        /// <param name="running"></param>
        /// <returns></returns>
        private bool MakeStep (bool running)
        {
            Vector2 calculatedPosition;
            
            float speed = running ? runningSpeed : walkingSpeed;
            speed /= frameRate;

            float delta = movementTargetX - myRigidbody.position.x;
            if (delta < 0)
            {
                if (movementDirectionisRight)
                { Flip(); }
                movementDirectionisRight = false;
            }
            else
            {
                if (!movementDirectionisRight)
                { Flip(); }
                movementDirectionisRight = true;
            }

            if (speed > Mathf.Abs(delta))
            {
                myRigidbody.MovePosition(new Vector2(movementTargetX, myRigidbody.position.y));
                return true;
            }

            calculatedPosition = myRigidbody.position + 
                speed * (movementDirectionisRight?Vector2.right:Vector2.left);
            myRigidbody.MovePosition(calculatedPosition);
            return false;
        }

        private void Flip ()
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            for (int i = 0; i < hitboxes.Length; i++)
            {
                Rigidbody2D rb2d = hitboxes[i].GetComponent<Rigidbody2D>();
                if (rb2d != null)
                {
                    rb2d.MovePosition(rb2d.position - Vector2.right * 2 * (hitboxes[i].localPosition.x));
                }
                else
                {
                    hitboxes[i].localPosition = new Vector3(-hitboxes[i].localPosition.x, hitboxes[i].localPosition.y, hitboxes[i].localPosition.z);
                }
            }
        }
        #endregion
    }
}
