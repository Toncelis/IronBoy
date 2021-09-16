using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.StateMachine
{
    public class StrongAttackState : PlayerState
    {
        private float damage => model.StrongAttackDamage;

        [SerializeField]
        private Transform hitPoint;
        [SerializeField]
        private float attackTriggerTime; 

        private void Attack()
        {
            Collider2D[] enemys = Physics2D.OverlapBoxAll(hitPoint.position, hitPoint.lossyScale * 0.5f, 0, LayerMask.GetMask("Enemy"));
            foreach (Collider2D enemy in enemys)
            {
                enemy.gameObject.GetComponent<Game.Enemy.EnemyCondition>()?.GetHit(damage);
            }
        }

        #region Attacking coroutine
        Coroutine _attackCoroutine;
        
        IEnumerator Attacking()
        {
            //Debug.Log("Start attacking");
            yield return new WaitForSeconds(attackTriggerTime);
            Attack();
            yield return null;
        }

        public void MakeAttack()
        {
            animator.SetTrigger("Attack");
            _attackCoroutine = StartCoroutine(Attacking());
        }

        public void StopAttack()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }
        }
        #endregion

        public override void StopState ()
        {
            StopCoroutine(_coroutine);
            StopCoroutine(_attackCoroutine);
        }

        protected override IEnumerator StateRoutine()
        {
            Debug.Log("Enter StrongAttackState");
            
            animator.SetTrigger("Attack");
            playerRigidbody.velocity = Vector2.zero;

            condition.RaiseTemperature(model.StrongAttackHeat, true);
            MakeAttack();

            float timer = model.StrongAttackLength;

            while (timer > 0)
            {
                yield return null;
            
                timer -= Time.deltaTime;
                Debug.Log(timer);
                if (Input.GetButtonDown("Blink"))
                {
                    if (stateController.actionList.Blink())
                    {
                        break;
                    }
                }
                else if (Input.GetButtonDown("Dash"))
                {
                    //Dash
                    //break;
                }
            }

            stateController.ChangeState(PlayerStateController.StateNames.idle);
            yield return null;
        }
    }
}
