using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public interface IInterectableObject
    {
        public void Interact(float interactorXPosition);
    }
}
