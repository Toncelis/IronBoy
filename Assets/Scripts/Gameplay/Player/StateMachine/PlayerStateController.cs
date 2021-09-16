using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.StateMachine
{
    public class PlayerStateController : MonoBehaviour
    {
        PlayerState currentState;
        [Header("Debug field")]
        [SerializeField]
        StateNames currentStateName;

        [Header("Dependencies")]
        [SerializeField]
        private PlayerV2.PlayerModel model;
        [SerializeField]
        private PlayerCondition condition;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private Rigidbody2D playerRigidbody;

        public ActionList actionList;

        #region States
        public enum StateNames
        {
            idle            = 0,
            walk            = 1,
            run             = 2,

            midair          = 3,
            ladder          = 4,
            ledge           = 5,

            overheat        = 6, 

            weakAttack      = 7,
            strongAttack    = 8,
            bush            = 9
        }

        [Header("States")]
        /// <summary>
        /// idle, walk, run,
        /// midair, ladder, ledge,
        /// overheat,
        /// weakAttack, strongAttack, bush
        /// </summary>
        [SerializeField]
        List<PlayerState> playerStates;

        public void ChangeState(StateNames stateName)
        {
            if (currentState != null)
            {
                currentState.StopState();
            }

            if (_overheated)
            {
                _overheated = false;
                EnterOverheatState();
            }
            else
            {
                currentStateName    = stateName;
                currentState        = playerStates[(int)stateName];
                currentState.StartState();
            }
        }

        #region Overheat
        private bool _overheated = false;
        private void Overheat()
        {
            _overheated = true;
        }

        private void EnterOverheatState()
        {
            ChangeState(StateNames.overheat);
        }
        #endregion

        #region Death
        private void Die ()
        {
            //ChangeState(new DyingState(this, model, condition, gameObject));
        }
        #endregion
        #endregion

        private void Start()
        {
            playerRigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;

            condition.overheatEvent += Overheat;
            condition.zeroEnergyEvent += Die;

            foreach (PlayerState state in playerStates)
            {
                state?.Construct(this, model, condition, animator, playerRigidbody);
            }

            ChangeState(StateNames.idle);
        }
    }
}
