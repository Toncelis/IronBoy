using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects.Loot
{
    public class LootStarter : MonoBehaviour
    {
        [SerializeField] float startSpeed;
        [SerializeField] float angleRange;
        [SerializeField] float prepTime;

        [Header("Зависимости")]
        [SerializeField] Rigidbody2D lootRB;
        [SerializeField] GameObject triggerHolder;

        private void Start()
        {
            float angle = Random.Range(-angleRange, angleRange);
            lootRB.velocity = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * startSpeed;

            Invoke("MakePickable", prepTime);
        }

        private void MakePickable()
        {
            triggerHolder.SetActive(true);
        }
    }
}

