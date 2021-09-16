using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Player.StateMachine
{
    public class OverHeatState : PlayerState
    {
        protected override IEnumerator StateRoutine()
        {
            Debug.Log("Enter OverHeat State");

            condition.GetHit(model.OverheatDamage);

            animator.SetTrigger("Overheat");
            playerRigidbody.velocity = Vector2.zero;
            float coolingRate = model.TemperatureCap / model.CoolingTime;

            while (condition.Temperature > 0)
            {
                //Debug.Log(condition.Temperature);
                condition.RaiseTemperature(-coolingRate * Time.fixedDeltaTime, false);
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            condition.RaiseTemperature(0, true);
            stateController.ChangeState(PlayerStateController.StateNames.idle);
            yield return null;
            
        }
    }
}

