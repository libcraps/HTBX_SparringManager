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
    /// <inheritdoc cref="LineScenarioBehaviour"/>
    public class HitLineBehaviour : LineScenarioBehaviour
    {

        protected override void Start()
        {
            base.Start();
            _objectToHit = this.gameObject;
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            MoveObject(this.gameObject, fixPosHitValue * _objectVelocity);
            HitManager(this.gameObject);
        }


    }
}
