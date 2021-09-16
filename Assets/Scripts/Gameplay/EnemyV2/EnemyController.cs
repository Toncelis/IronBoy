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
            spriteRenderer.flipX = directionIsRight;
            realRB.velocity = Vector2.right * (running ? model.RunSpeed : model.MoveSpeed) * (directionIsRight ? 1 : -1);
            if (Mathf.Abs(target.x - realRB.position.x) < (running ? model.RunSpeed : model.MoveSpeed) * Time.fixedDeltaTime * 2)
            {
                GoIdle();
            }
        }

        public void GoHome()
        {
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

        public void Chase (Transform target)
        {
            if (currentBehaviourCoroutine != null)
            {
                StopCoroutine(currentBehaviourCoroutine);
            }
            currentBehaviourCoroutine = StartCoroutine(ChasingRoutine(target));
        }

        IEnumerator ChasingRoutine(Transform chaseTarget)
        {
            while (Mathf.Abs(realRB.position.x - chaseTarget.position.x) > epsilon)
            {
                MoveTo(chaseTarget.position, true);
                yield return new WaitForFixedUpdate();
                if (!CheckMovementSuccess())
                {
                    GoHome();
                }
            }
        }
        #endregion

        #region Attacking

        #endregion

        private void SetHome()
        {
            model.SetHomePosition(realRB.position);
        }

        [SerializeField] Transform testTarget;
        private void Start()
        {
            SetHome();
            epsilon = model.MoveSpeed * Time.fixedDeltaTime * 0.1f;

            antiPlayerRB = Instantiate(antiPlayerBorder, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            Chase(testTarget);
        }
        private void LateUpdate()
        {
            antiPlayerRB.MovePosition(realRB.position);
        }
    }
}

