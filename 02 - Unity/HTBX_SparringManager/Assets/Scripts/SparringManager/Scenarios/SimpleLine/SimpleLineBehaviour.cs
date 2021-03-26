using System.Collections;
using SparringManager;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Scenarios
{
    /* Class nof the SimpleLine prefab
     * 
     *  Summary :
     *  This class leads the behaviour of the simple line prefab.
     *  The Line only moves lateraly
     *  
     *  Attributs :
     *      float _lineVelocity : Acceleration at a tTime of the Line
     *      int _deltaTimeChangeVelocity : Time during which the line will keep tis acceleration
     *      float _startTimeScenario : absolut time of the beginning of the scenario
     *      float _tTime : tTime
     *      
     *  Methods :
     *      void MoveLine(int lineAcceleration) : moves the line at the lineAcceleration
     *      Void LineInCameraRange() : Verifie that the line stay in the camera range
     */

    /// <summary>
    /// Manage the behaviour of the SimpleLine.
    /// </summary>
    /// <remarks>Essentialy it moves the line and it makes sure that the line stays in the range of the camera</remarks>
    public class SimpleLineBehaviour : Scenario1DLineDisplay
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
            MoveLine(_lineVelocity);
        }
        public override void Init(IStructScenario structScenari)
        {
            this.structScenari = (SimpleLineStruct)structScenari;
        }
        void MoveLine(float lineHorizontalAcceleration)
        {
            //_lineRigidComponent.AddForce(new Vector3 (lineHorizontalAcceleration, 0, 0), ForceMode.Acceleration);
            this.gameObject.GetComponent<Rigidbody>().velocity= new Vector3(lineHorizontalAcceleration, 0, 0);
        }
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + " has been destroyed");
        }
    }
}
