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
    public class SimpleLineBehaviour : LineScenarioBehaviour
    {
        //General variables of a MovingLine
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            MoveObject(this.gameObject, _objectVelocity);
        }

    }
}
