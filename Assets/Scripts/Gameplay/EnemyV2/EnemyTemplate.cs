using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.EnemyV2
{
    [CreateAssetMenu(menuName = "SO/EnemyTemplates/basic", fileName ="basicEnemyTemplate", order = 1)]
    public class EnemyTemplate : ScriptableObject
    {
        [Header("Durability")]
        [SerializeField] private float hp;
        public float Hp => hp;

        [Header("Movement")]
        [SerializeField] private float moveSpeed;
        public float MoveSpeed => moveSpeed;

        [SerializeField] private float runSpeed;
        public float RunSpeed => runSpeed;

        [Header("Attack")]
        [SerializeField] private float attackDamage;
        public float Damage => attackDamage;

        [SerializeField] private float attackSpeedModifier;
        public float AttackSpeedModifier => attackSpeedModifier;
        
        [SerializeField] private float attackRange;
        public float AttackRange => attackRange;

        [SerializeField] private float attackHeight;
        public float AttackHeight => attackHeight;

        [Header("Behaviour")]
        [SerializeField] private float idleDetectionRange;
        public float IdleDetectionRange => idleDetectionRange;
        [SerializeField] private float idleBackDetectionRange;
        public float IdleBackDetectionRange => idleBackDetectionRange;
        [SerializeField] private float idleDetectionHeight;
        public float IdleDetectionHeight => idleDetectionHeight;

        [SerializeField] private float chaseDetectionRange;
        public float ChaseDetectionRange => chaseDetectionRange;
        [SerializeField] private float chaseBackDetectionRange;
        public float ChaseBackDetectionRange => chaseBackDetectionRange;
        [SerializeField] private float chaseDetectionHeight;
        public float ChaseDetectionHeight => chaseDetectionHeight;
    }
}
