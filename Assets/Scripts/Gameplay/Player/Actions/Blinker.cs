using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class Blinker : MonoBehaviour
    {
        [Header("Parameteres")]
        /// <summary>
        /// Точность определения свободного пространства под скачок
        /// </summary>
        [SerializeField] private float checkStep;
        private float blinkCooldown => model.BlinkCooldown;
        [Header("Dependencies")]
        [SerializeField] private Transform playerHitbox;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private PlayerCondition condition;
        [SerializeField] private PlayerV2.PlayerModel model;

        [SerializeField] private UnityEngine.UI.Image blinkIcon; 
        
        /// <summary>
        /// Дальность скачка
        /// </summary>
        private float blinkRange => model.BlinkDistance;
        /// <summary>
        /// Температурная стоимость скачка
        /// </summary>
        private float blinkHeat => model.BlinkHeat;

        /// <param name="distance"></param>
        /// <returns></returns>
        private bool CheckLandingPoint(float distance)
        {
            var colliders = Physics2D.OverlapBoxAll(
                playerHitbox.position + distance * Vector3.right, playerHitbox.lossyScale, 0,
                LayerMask.GetMask("Default", "Wall", "Enemy"));

            foreach (var collider in colliders)
            {
                Debug.Log(collider.name);
            }

            if (colliders.Length == 0)
            {
                return false;
            }
            return true;
        }

        private void SearchForLandingPoint(float distance)
        {
            RaycastHit2D hit = 
                Physics2D.Raycast(playerHitbox.position, Vector2.right * Mathf.Sign(distance), Mathf.Abs(distance), LayerMask.GetMask("Wall"));

            if (hit)
            {
                distance = hit.point.x - playerHitbox.position.x;
            }

            while (CheckLandingPoint(distance) & Mathf.Abs(distance) > checkStep)
            {
                distance -= checkStep * Mathf.Sign(distance);
            }

            /*
            Debug.DrawLine(
                transform.position + new Vector3(distance + width * 0.5f, -Height * 0.5f, 0),
                transform.position + new Vector3(distance - width * 0.5f, Height * 0.5f, 0),
                Color.red,
                2f
                );


            Debug.DrawLine(
                transform.position + new Vector3(distance - width * 0.5f, -Height * 0.5f, 0),
                transform.position + new Vector3(distance + width * 0.5f, Height * 0.5f, 0),
                Color.red,
                2f
                );
            */

            Move(distance);
        }

        private void Move(float distance)
        {
            transform.position += Vector3.right * distance;
        }

        public bool Blink()
        {
            if (blinkTimer == 0)
            {
                blinkTimer = blinkCooldown;
                UpdateBlinkIcon();

                condition.RaiseTemperature(blinkHeat, true);

                if (spriteRenderer.flipX)
                {
                    SearchForLandingPoint(-blinkRange);
                }
                else
                {
                    SearchForLandingPoint(blinkRange);
                }
                return true;
            }
            return false;
        }

        private float blinkTimer;
        private void Start()
        {
            blinkTimer = 0f;
            UpdateBlinkIcon();
        }
        private void FixedUpdate()
        {
            if (blinkTimer != 0)
            {
                blinkTimer = Mathf.Max(0f, blinkTimer - Time.fixedDeltaTime);
                UpdateBlinkIcon();
            }
        }
        private void UpdateBlinkIcon ()
        {
            blinkIcon.fillAmount = blinkTimer / blinkCooldown;
        }
        /*
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(
                transform.position + new Vector3(blinkRange, 0, 0),
                new Vector3(width, Height, 1)
                );
        }
        */

        /*
        private void Update()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Blink();
            }
        }
        */
    }
}