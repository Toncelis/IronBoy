using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.StateMachine
{
    public class PlayerState : MonoBehaviour
    {
        /// <summary>
        /// ���������� ��������� ������
        /// </summary>
        protected PlayerStateController stateController;
        /// <summary>
        /// ������ ������, ���������� ��������� ��� ��.
        /// </summary>
        protected PlayerV2.PlayerModel           model;
        /// <summary>
        /// ��������� ������, ���������� �� ��� ����������� � �������.
        /// </summary>
        protected PlayerCondition       condition;

        protected Animator              animator;
        protected Rigidbody2D           playerRigidbody;

        protected Coroutine _coroutine;

        public void Construct (
            PlayerStateController newStateController, PlayerV2.PlayerModel newModel, PlayerCondition newCondition, 
            Animator newAnimator, Rigidbody2D newRigidbody)
        {
            stateController = newStateController;
            model           = newModel;
            condition       = newCondition;

            animator        = newAnimator;
            playerRigidbody = newRigidbody;
        }

        public virtual void StartState ()
        {
            _coroutine = StartCoroutine(StateRoutine());
        }

        public virtual void StopState ()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        protected virtual IEnumerator StateRoutine ()
        {
            yield return null;
        }
    }
}
