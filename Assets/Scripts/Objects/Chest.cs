using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public class Chest : MonoBehaviour, IInterectableObject
    {
        [SerializeField] List<GameObject> Loot;
        [SerializeField] Transform spawnPosition;
        [SerializeField] Transform chestCap;
        [SerializeField] float openingTime;

        [SerializeField] Collider2D interactionCollider;

        private float openedAngle = 30;

        private void SpawnLoot()
        {
            Instantiate(Loot[Random.Range(0, Loot.Count)], spawnPosition.position, Quaternion.identity);
        }

        private IEnumerator OpeningChestCoroutine ()
        {
            float rotationSpeed = openedAngle / openingTime * Time.fixedDeltaTime;
            float currentAngle = 0;

            while (currentAngle < openedAngle)
            {
                currentAngle += rotationSpeed;
                chestCap.rotation = Quaternion.Euler(currentAngle, 0, 0);
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            SpawnLoot();
            yield return null;
        }

        public void Interact (float interactorXPosition)
        {
            interactionCollider.enabled = false;

            StartCoroutine(OpeningChestCoroutine());
        }
    }
}
