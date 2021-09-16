using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;

namespace Game.Objects.Loot
{
    public class EnergyDrop : MonoBehaviour
    {
        [SerializeField] float energyAmount;

        [SerializeField] private GameObject parent;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerCondition playerCondition = collision.gameObject.GetComponent<PlayerCondition>();
                playerCondition.GetHit(-energyAmount);

                Destroy(parent);
            }
        }
    }
}
