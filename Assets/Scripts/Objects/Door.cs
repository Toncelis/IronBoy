using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public class Door : MonoBehaviour, IInterectableObject
    {
        [Tooltip("Длительность открытия/закрытия двери (секунды)")]
        [SerializeField] float closingAnimationLength;
        float rotationSpeed => 90 / closingAnimationLength;

        [Space(4)]
        [Header("Зависимости")]

        [SerializeField] Transform doorVisualsHolder;
        [SerializeField] GameObject doorCollider;

        [SerializeField] Collider2D middleTriggerZone;
        [SerializeField] Collider2D leftTriggerZone;
        [SerializeField] Collider2D rightTriggerZone;

        private bool isOpened = false;

        #region Visualising open/close
        enum rotationPurpose
        {
            OpenLeft = 90,
            OpenRight = -90,
            Close = 0
        }


        /// <summary>
        /// -90 - open right, 90 - open left, 0 - close
        /// </summary>
        /// <param name="targetRotation"></param>
        /// <returns></returns>
        IEnumerator RotatingDoor(float targetRotation)
        {
            float realAngle = doorVisualsHolder.rotation.eulerAngles.y;
            while (true)
            {
                if (realAngle > 180)
                {
                    realAngle -= 360;
                }
                else if (realAngle <= -180)
                {
                    realAngle += 360;
                }
                else break;
            }

            float angleToRotate = targetRotation - realAngle;
            float speed = rotationSpeed * Mathf.Sign(angleToRotate) * Time.fixedDeltaTime;
            float rotationDone;

            rotationDone = 0;
            doorCollider.SetActive(true);
            if (angleToRotate > 0)
            {
                while (rotationDone < angleToRotate)
                {
                    rotationDone += speed;
                    doorVisualsHolder.rotation = Quaternion.Euler(0, doorVisualsHolder.rotation.eulerAngles.y + speed, 0);
                    yield return new WaitForSeconds(Time.fixedDeltaTime);
                }
            }
            else if (angleToRotate < 0)
            {
                while (rotationDone > angleToRotate)
                {
                    rotationDone += speed;
                    doorVisualsHolder.rotation = Quaternion.Euler(0, doorVisualsHolder.rotation.eulerAngles.y + speed, 0);
                    yield return new WaitForSeconds(Time.fixedDeltaTime);
                }
            }

            doorVisualsHolder.rotation = Quaternion.Euler(0, targetRotation, 0);
            if (targetRotation != 0)
            {
                doorCollider.SetActive(false);
            }
            yield return null;
        }

        Coroutine doorRotatingCoroutine = null;

        private void RotateDoor(rotationPurpose target)
        {
            if (doorRotatingCoroutine != null)
                StopCoroutine(doorRotatingCoroutine);

            switch (target)
            {
                case rotationPurpose.Close:
                    doorRotatingCoroutine = StartCoroutine(RotatingDoor(0));
                    middleTriggerZone.enabled = true;
                    leftTriggerZone.enabled = false;
                    rightTriggerZone.enabled = false;

                    return;
                case rotationPurpose.OpenLeft:
                    doorRotatingCoroutine = StartCoroutine(RotatingDoor(90));
                    middleTriggerZone.enabled = false;
                    leftTriggerZone.enabled = true;
                    rightTriggerZone.enabled = false;

                    return;
                case rotationPurpose.OpenRight:
                    doorRotatingCoroutine = StartCoroutine(RotatingDoor(-90));
                    middleTriggerZone.enabled = false;
                    leftTriggerZone.enabled = false;
                    rightTriggerZone.enabled = true;

                    return;
            }
        }
        #endregion

        private void CloseDoor()
        {
            isOpened = false;
            RotateDoor(rotationPurpose.Close);
            //doorCollider.enabled = true;
        }

        private void OpenDoor(float interactorXPosition)
        {
            isOpened = true;
            if (interactorXPosition - transform.position.x > 0)
            {
                RotateDoor(rotationPurpose.OpenLeft);
            }
            else
            {
                RotateDoor(rotationPurpose.OpenRight);
            }
            //doorCollider.enabled = false;
        }

        public void Interact(float interactorXPosition)
        {
            if (isOpened)
            {
                CloseDoor();
                return;
            }

            OpenDoor(interactorXPosition);
        }
    }
}