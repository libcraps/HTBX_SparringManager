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
        private SimpleLineStruct structScenari;

        //Global Time variable
        private float _startTimeScenario;
        private float _tTime;

        void Start()
        {
            //Initialisation of the time
            _startTimeScenario = Time.time;
            _tTime = Time.time - _startTimeScenario;
        }

        void FixedUpdate()
        {
            _tTime = Time.time - _startTimeScenario;
            ObjectInCameraRange();
            MoveObject(objectVelocity);
        }
        public override void Init(IStructScenario structScenari)
        {
            this.structScenari = (SimpleLineStruct)structScenari;
        }

    }
}
