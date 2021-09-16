using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Objects;

namespace Game.Player
{
    public class GeneralActions : MonoBehaviour
    {
        [SerializeField] private Transform interactablesCollider;
        private void Update()
        {
            if (Input.GetButtonDown("Ladder"))
            {
                var interectables = Physics2D.OverlapBoxAll(interactablesCollider.position, interactablesCollider.lossyScale, 0, LayerMask.GetMask("BackgroundInteractables"));
                foreach (Collider2D collider in interectables)
                {
                    collider.GetComponent<IInterectableObject>()?.Interact(transform.position.x);
                }
            }
        }
    }
}
