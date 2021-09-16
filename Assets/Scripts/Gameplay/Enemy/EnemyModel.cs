using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Model", menuName = "SO/Enemy/Model", order = 1)]
    public class EnemyModel : ScriptableObject
    {
        [Header("Condition")]
        [SerializeField]
        float hp;
        public float Hp => hp;

        /*
        [Header("Comportament")]
        [SerializeField]
        float visionDistance;
        public float VisionDistance => visionDistance;

        [SerializeField]
        float attackDistance;
        public float AttackDistance => attackDistance;

        [Header("Battle stats")]
        [SerializeField]
        float attackRange;
        [SerializeField]
        float attackHeight;
        public float AttackRange => attackRange;
        public float AttackHeight => attackHeight;
        */

        [SerializeField] float walkSpeed;
        public float WalkSpeed => walkSpeed;

        [SerializeField] float runSpeed;
        public float RunSpeed => runSpeed;

        [SerializeField]
        float damage;
        public float Damage => damage;

        [Serializable]
        public struct SaveableEnemyData
        {
            public float hp;
            public float walkS;
            public float runS;
            public float damage;

            public SaveableEnemyData(float Hp, float WalkS, float RunS, float Damage)
            {
                hp = Hp;
                walkS = WalkS;
                runS = RunS;
                damage = Damage;
            }
        }

        public void SaveDataToFile()
        {
            SaveableEnemyData save = new SaveableEnemyData
                (
                    Hp, WalkSpeed, RunSpeed, Damage
                );


            string destination = Application.persistentDataPath + "/EnemyGeneralInfo.json";
            File.WriteAllText(destination, JsonUtility.ToJson(save));
        }

        public void LoadDataFromFile()
        {
            string destination = Application.persistentDataPath + "/EnemyGeneralInfo.json";
            Debug.Log(destination);
            if (File.Exists(destination))
            {
                SaveableEnemyData save = JsonUtility.FromJson<SaveableEnemyData>(File.ReadAllText(destination));
                hp = save.hp;
                walkSpeed = save.walkS;
                runSpeed = save.runS;
                damage = save.damage;
            }
        }

        public void UpdateModel (float _hp, float _dmg, float _ws, float _rs)
        {
            hp = _hp;
            damage = _dmg;
            walkSpeed = _ws;
            runSpeed = _rs;
        }
    }
}
