using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.EnemyV2
{
    public class EnemyModel : MonoBehaviour
    {
        [SerializeField] EnemyTemplate template;
        
        private Vector2 homePosition;
        public void SetHomePosition(Vector2 newHomePosition)
        {
            homePosition = newHomePosition;
        }

        private float damageMultiplier = 1;
        private float damageAdder = 0;

        private float movementMultiplier = 1;
        private float attackSpeedMultiplier = 1;

        public float Damage => Mathf.Max(0, template.Damage + damageAdder) * damageMultiplier;
        public Vector2 HomePosition => homePosition;

        public float MoveSpeed => template.MoveSpeed * movementMultiplier;
        public float RunSpeed => template.RunSpeed * movementMultiplier;
        public float AttackSpeed => template.AttackSpeedModifier * attackSpeedMultiplier;

    }
}
