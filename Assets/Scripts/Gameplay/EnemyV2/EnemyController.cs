using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.EnemyV2
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] EnemyModel model;
        float epsilon;


        #region Movement
        [SerializeField] Rigidbody2D realRB;

        [SerializeField] GameObject antiPlayerBorder;
        Rigidbody2D antiPlayerRB;

        [SerializeField] bool directionIsRight;
        [SerializeField] SpriteRenderer spriteRenderer;

        private Vector2 oldPosition;
        private bool CheckMovementSuccess()
        {
            return ((realRB.position - oldPosition).magnitude > epsilon);
        }
        private void FixedUpdate()
        {
            oldPosition = realRB.position;
        }

        private void MoveTo(Vector2 target, bool running)
        {
            Debug.Log("Moving");
            directionIsRight = (target.x - realRB.position.x > 0);
            
            if (spriteRenderer.flipX != directionIsRight)
            {
                Flip();
            }


            realRB.velocity = Vector2.right * (running ? model.RunSpeed : model.MoveSpeed) * (directionIsRight ? 1 : -1);
            if (Mathf.Abs(target.x - realRB.position.x) < (running ? model.RunSpeed : model.MoveSpeed) * Time.fixedDeltaTime * 2)
            {
                GoIdle();
            }
        }

        public void GoHome()
        {
            animator.SetTrigger("Run");
            busy = false;
            if (currentBehaviourCoroutine != null)
            {
                StopCoroutine(currentBehaviourCoroutine);
            }
            currentBehaviourCoroutine = StartCoroutine(MovingHomeRoutine());
        }

        IEnumerator MovingHomeRoutine()
        {
            while (Mathf.Abs(realRB.position.x - model.HomePosition.x) > epsilon)
            {
                MoveTo(model.HomePosition, false);
                yield return new WaitForFixedUpdate();
                if (!CheckMovementSuccess())
                {
                    SetHome();
                    GoIdle();
                }
            }
        }

        public void GoIdle()
        {
            animator.SetTrigger("Idle");
            if (currentBehaviourCoroutine != null)
            {
                StopCoroutine(currentBehaviourCoroutine);
            }
            realRB.velocity = Vector2.zero;
        }
        #endregion

        #region Behaviour
        Coroutine currentBehaviourCoroutine;
        [SerializeField] Animator animator;

        public void ChaseTarget (Transform target)
        {
            if (!busy)
            {
                Chase(target);
            }
        }

        private void Chase (Transform target)
        {
            if (currentBehaviourCoroutine != null)
            {
                StopCoroutine(currentBehaviourCoroutine);
            }
            currentBehaviourCoroutine = StartCoroutine(ChasingRoutine(target));
        }

        [SerializeField] Game.Enemy.EnemyVision vision;

        IEnumerator ChasingRoutine(Transform chaseTarget)
        {
            animator.SetTrigger("Run");
            busy = false;
            while (Mathf.Abs(realRB.position.x - chaseTarget.position.x) > model.AttackRange)
            {
                MoveTo(chaseTarget.position, true);
                yield return new WaitForFixedUpdate();
                
                if (!CheckMovementSuccess() )
                {
                    GoHome();
                }

                if (!vision.CheckChasingVision())
                {
                    GoHome();
                }
            }
            MakeAttack();
            currentBehaviourCoroutine = null;
            yield return null;
        }
        #endregion

        #region Attacking
        [SerializeField]
        Transform attackHitbox;

        [Header("TEMP")]
        [SerializeField] float attackTriggerLength;
        [SerializeField] float attackFullLength;

        private void Attack()
        {
            foreach (Collider2D collider in Physics2D.OverlapBoxAll(attackHitbox.position, attackHitbox.lossyScale, 0, LayerMask.GetMask("Player")))
            {
                Player.PlayerCondition playerCondition = collider.gameObject.GetComponent<Player.PlayerCondition>();
                if (playerCondition != null)
                {
                    playerCondition.GetHit(model.Damage);
                    break;
                }
            }
        }

        bool busy = false;
        IEnumerator MakingAttack ()
        {
            animator.SetTrigger("Attack");
            busy = true;
            yield return new WaitForSeconds(attackTriggerLength);
            Attack();
            yield return new WaitForSeconds(attackFullLength);
            if (vision.CheckChasingVision())
            {
                Chase(vision.GetPlayer());
            }
            else
            {
                GoHome();
            }
        }

        private void MakeAttack()
        {
            StartCoroutine(MakingAttack());
        }


        #endregion

        [SerializeField] GameObject hitboxHolder;
        Transform[] hitboxes;
        //List<float> hitboxesStandartPosition;

        private void SetHome()
        {
            model.SetHomePosition(realRB.position);
        }

        private void Start()
        {
            SetHome();
            epsilon = model.MoveSpeed * Time.fixedDeltaTime * 0.1f;

            antiPlayerRB = Instantiate(antiPlayerBorder, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

            hitboxes = hitboxHolder.GetComponentsInChildren<Transform>();

            vision.playerSpotted += ChaseTarget;
            /*
            hitboxesStandartPosition = new List<float>();
            foreach (Transform hitbox in hitboxes)
            {
                hitboxesStandartPosition.Add(hitbox.localPosition.x);
            }
            */
        }

        private void Flip()
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

        private void LateUpdate()
        {
            antiPlayerRB.MovePosition(realRB.position);
        }
    }
}

