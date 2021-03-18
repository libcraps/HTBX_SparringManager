﻿using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.Scenarios
{
    /* Class of the HitLine Prefab
     * 
     *  Summary :
     *  This class leads the behaviour of the HitLine prefab.
     *  The Line only moves lateraly and it instantiates the hit after _timeBeforeHit seconds. 
     *  
     *  Attributs :
     *      float _lineAcceleration : Acceleration at a tTime of the Line
     *      int _deltaTimeChangeAcceleration : Time during which the line will keep tis acceleration
     *      float _timeBeforeHit : Time when the hit will be setted
     *      float _deltaHit : Time during which the player will be able to hit the line
     *      bool _hitted : Boolean that indicates fi the line is hitted or not
     *      bool _fixPosHit : Boolean that indicates if the line stop during the hit
     *      int _fixPosHitValue : if the boolean _fixPoshit is true we fix the value to 0 in order to have an acceleration null
     *      float _startTimeScenario : absolut time of the beginning of the scenario
     *      float _tTime : tTime
     *      
     *  Methods :
     *      void MoveLine(int lineAcceleration) : moves the line at the lineAcceleration
     *      Void LineInCameraRange() : Verifie that the line stay in the camera range
     *      void SetHit() : Indicates when the playe can hit by changing the color of the line
     */
    public class HitLineBehaviour : Scenario1DLineDisplay
    {
        //General variables of a MovingLine
        private HitLineStruct structScenari;
        public ScenarioHitLine scenario;

        //Variables of an Hitting Line
        private bool _hitted;
        private int _fixPosHitValue = 1; // if fix Pos hit == true we fix the value to 0 in order to have an acceleration null
        public float DeltaHit { get { return structScenari.DeltaHit; } }
        public float TimeBeforeHit { get { return structScenari.TimeBeforeHit; } }
        public bool FixPosHit { get { return structScenari.FixPosHit; } } //Boolean to indicate if the line continue to move when the hit is setted 
        public bool Hitted
        {
            get
            {
                return _hitted;
            }
            set
            {
                _hitted = value;
            }
        }

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
            MoveLine(_fixPosHitValue * _lineAcceleration);
            SetHit();
        }

        public override void Init(IStructScenario structScenari)
        {
            this.structScenari = (HitLineStruct)structScenari;
        }

        private void MoveLine(float lineHorizontalAcceleration)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (lineHorizontalAcceleration, 0, 0);
        }
        private void SetHit()
        {
            //change the color of the line if the player have to hit
            bool canHit = (_tTime > TimeBeforeHit && (_tTime - TimeBeforeHit) < DeltaHit);

            if (canHit && _hitted == false) // warning if hitLine controller == instantiated 2 times -> problem need to be solved
            {
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                if (FixPosHit == true)
                {
                    _fixPosHitValue = 0;
                }
            }
            else
            {
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                _fixPosHitValue = 1;
            }
        }

        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + "has been destroyed");
        }
    }
}