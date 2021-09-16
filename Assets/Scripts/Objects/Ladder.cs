using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField]
        private Transform ladderHitbox;

        public Vector2 TopRightBound
        {
            get
            {
                return ladderHitbox.position + ladderHitbox.lossyScale * 0.5f;
            }
        }

        public Vector2 BotLeftBound
        {
            get
            {
                return ladderHitbox.position - ladderHitbox.lossyScale * 0.5f;
            }
        }
    }
}
