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
    /// <inheritdoc cref="LineDisplayBehaviour"/>
    public class HitLineBehaviour : LineDisplayBehaviour
    {

        protected override void Awake()
        {
            base.Awake();
            SetObjectToHit();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            MoveObject(this.gameObject, fixPosHitValue * objectVelocity);
            HitManager(this.gameObject);
        }


        protected override void SetObjectToHit()
        {
            _objectToHit = this.gameObject;
        }

    }
}
