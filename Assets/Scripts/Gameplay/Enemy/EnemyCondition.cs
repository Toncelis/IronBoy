using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Errors;

namespace Game.Enemy
{
    public class EnemyCondition : MonoBehaviour
    {
        float hpMax;
        float hp;
        [SerializeField] EnemyModel myModel;

        [SerializeField] Animator myAnimator;

        [SerializeField]
        UnityEngine.UI.Image hpBar;

        [Range(0,1)]
        [SerializeField] float MOVE_TO_MODEL_lootChance_WIP;
        [SerializeField] List<GameObject> MOVE_TO_MODEL_LootVariants_WIP;
        [SerializeField] Transform lootSpawn;

        private void Start()
        {
            hpBar.fillAmount = 1;
            hpMax = myModel.Hp;

            if (hpMax <= 0)
            {
                ErrorManager.ThrowError(ErrorEnum.NONPOSITIVE_MAX_HP, $"{name} based on {myModel.name} model");
                hpMax = 1;
            }

            hp = hpMax;
            UpdateHpBar();
        }

        private void UpdateHpBar ()
        {
            hpBar.fillAmount = hp / hpMax;
        }

        public void GetHit (float damage)
        {
            if (damage > hp)
            {
                hp = 0;
                hpBar.fillAmount = 0;
                myAnimator.SetTrigger("Hit");
                UpdateHpBar();
                Die();
            }
            else
            {
                hp -= damage;
                hpBar.fillAmount = hp / hpMax;
                myAnimator.SetTrigger("Hit");
                UpdateHpBar();
            }
        }

        private void SpawnLoot ()
        {
            float chance = Random.Range(0, 1f);
            if (chance <= MOVE_TO_MODEL_lootChance_WIP)
            {
                Instantiate(MOVE_TO_MODEL_LootVariants_WIP[Random.Range(0, MOVE_TO_MODEL_LootVariants_WIP.Count)], lootSpawn.position, Quaternion.identity);
            }
        }

        private void Die()
        {
            myAnimator.SetTrigger("Death");
            foreach(Collider2D collider in GetComponents<Collider2D>())
            {
                collider.enabled = false;
            }
            
            /* WIP */ Invoke("SpawnLoot", 0.9f);
            Destroy(gameObject, 1f);
        }

        public bool Dead => (hp == 0);
    }
}
