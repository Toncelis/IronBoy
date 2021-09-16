using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField]
        Animator animator;

        [SerializeField]
        float damage;

        [SerializeField]
        Transform hitPoint;

        private Coroutine attackCoroutine;

        private void Attack()
        {
            Collider2D[] enemys = Physics2D.OverlapBoxAll(hitPoint.position, hitPoint.lossyScale, 0);
            foreach (Collider2D enemy in enemys)
            {
                enemy.gameObject.GetComponent<Game.Enemy.EnemyCondition>()?.GetHit(damage);
            }
        }

        IEnumerator Attacking()
        {
            yield return new WaitForSeconds(0.2f);
            Attack();
            yield return null;
        }

        public void MakeAttack()
        {
            animator.SetTrigger("Attack");
            attackCoroutine = StartCoroutine(Attacking());
        }

        public void StopAttack ()
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
            }
            animator.SetTrigger("Idle");
        }
    }
}
