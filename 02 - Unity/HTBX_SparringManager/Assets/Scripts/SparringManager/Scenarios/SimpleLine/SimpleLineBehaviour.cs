using System.Collections;
using SparringManager;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Scenarios.SimpleLine
{

    /// <summary>
    /// Manage the behaviour of the SimpleLine.
    /// </summary>
    /// <remarks>Essentialy it moves the line and it makes sure that the line stays in the range of the camera</remarks>
    public class SimpleLineBehaviour : ScenarioDisplayBehaviour
    {
        //General variables of a MovingLine


        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            ObjectInCameraRange();
            MoveObject(objectVelocity);
            Debug.Log(objectVelocity);
        }
        public override void Init(GeneriqueScenarioStruct structScenari)
        {
            this.structScenari = structScenari;
        }

    }
}
