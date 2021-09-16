using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public class Ledge : MonoBehaviour
    {
        [SerializeField]
        Transform playerPositionOnLedge;

        public Vector2 positionOnLedge => playerPositionOnLedge.position + playerPositionOnLedge.lossyScale.y *0.5f * Vector3.down; 
    }
}

