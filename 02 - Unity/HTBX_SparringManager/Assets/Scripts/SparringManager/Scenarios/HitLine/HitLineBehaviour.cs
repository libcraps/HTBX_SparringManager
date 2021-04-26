using System.Collections;
using System.Collections.Generic;
using SparringManager.Data;
using UnityEngine;

namespace SparringManager.Scenarios.HitLine
{
    /// <summary>
    /// Manage the behaviour of the HitLine.
    /// </summary>
    /// <remarks>Essentialy it moves the line, instantiates the hit and it makes sure that the line stays in the range of the camera</remarks>
    /// <inheritdoc cref="ScenarioDisplayBehaviour"/>
    public class HitLineBehaviour : ScenarioDisplayBehaviour
    {

        protected override void Awake()
        {
            base.Awake();
            SetObjectToHit();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            MoveObject(fixPosHitValue * objectVelocity);
            SetHit(this.gameObject);
        }


        protected override void SetObjectToHit()
        {
            _objectToHit = this.gameObject;
        }

    }
}
