﻿using SparringManager;
using SparringManager.DataManager;
using SparringManager.DataManager.HitLine;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Text;
using UnityEngine;

namespace SparringManager.HitLine
{
    /*
     * Class of the HitLineController, it manage the HitLine and is in interaction with the Session Manager and the DataManager
     */
    public class HitLineController : MonoBehaviour
    {
        //Usefull parameters of the scenario, they are in the hitLineStructure
        private int _accelerationMax;
        private int _deltaTimeMax;
        private int _deltaTimeMin;
        private float _deltaHit;
        private float _timeBeforeHit;

        private float _previousTime;
        private float _tTime;
        private float _reactTime;
        private float _startTimeScenario;
        private float _timerScenario;
        private bool _hitted = false;

        //Object that contain datas (structures)
        private ScenarioController _scenarioControllerComponent;
        private StructScenarios _controllerStruct;
        private HitLineStruct _hitLineControllerStruct;
        private HitLineDataStruct _hitLineData;

        [SerializeField]
        private GameObject _scenarioComposant; //HitLine Prefab
        private HitLineBehaviour _hitLineComponent;

        //List of the data that we will export 
        private List<float> mouvementConsign;
        private List<float> timeListScenario;

        private void Awake()
        {
            //INITIALISATION OF VARIABLES 
            _scenarioControllerComponent = GetComponent<ScenarioController>();
            _controllerStruct = _scenarioControllerComponent.ControllerStruct;
            _hitLineControllerStruct = _controllerStruct.HitLineStruct;
            SetControllerVariables();

            _hitted = false;
            mouvementConsign = new List<float>();
            timeListScenario = new List<float>();

            //Initialisation of the time and the acceleration
            _startTimeScenario = Time.time;
            _tTime = Time.time - _startTimeScenario;
            _previousTime = _tTime;

            Debug.Log(this.gameObject.name + " for " + _timerScenario + " seconds");
        }
        void Start()
        {
            Vector3 _pos3d = new Vector3();
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            Destroy(Instantiate(_scenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform), _timerScenario);

            _hitLineComponent = this.gameObject.GetComponentInChildren<HitLineBehaviour>();
            SetComponentVariables();
        }

        private void FixedUpdate()
        {
            //Update the "situation" of the line
            _tTime = Time.time - _startTimeScenario;
            RandomizeParametersLineMovement(_accelerationMax, _deltaTimeMin, _deltaTimeMax);
            GetConsigne(_tTime, _hitLineComponent.transform.position.x);
        }

        private void OnEnable()
        {
            ImpactManager.onInteractPoint += GetHit;
        }

        private void OnDisable()
        {
            ImpactManager.onInteractPoint -= GetHit;
        }

        public void GetHit(Vector2 position2d_)
        {
            RaycastHit hit;
            Vector3 rayCastOrigin = new Vector3(position2d_.x, position2d_.y, this.gameObject.transform.position.z);
            Vector3 rayCastDirection = new Vector3(0, 0, 1);

            bool rayOnTarget = Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, 250);
            bool canHit = (_tTime > _timeBeforeHit && (_tTime - _timeBeforeHit) < _deltaHit);

            if (rayOnTarget && canHit && _hitted == false)
            {
                _reactTime = _tTime - _timeBeforeHit;
                _hitted = true;
                _hitLineComponent.Hitted = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + _reactTime);
            }
        }

        void OnDestroy()
        {
            _reactTime = 0;
            _hitted = false;
            _hitLineComponent.Hitted = true;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }
        /*
        void GetData()
        {

        }
        */

        private void GetConsigne(float time, float pos)
        {
            mouvementConsign.Add(pos);
            timeListScenario.Add(time);
        }
        private void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax)
        {
            System.Random random = new System.Random();
            //Randomize the movement of the line every deltaTime seconds
            if ((_tTime - _previousTime) > _hitLineComponent.DeltaTimeChangeAcceleration)
            {
                _hitLineComponent.LineAcceleration = random.Next(-accelerationMax, accelerationMax);
                _hitLineComponent.DeltaTimeChangeAcceleration = random.Next(deltaTimeMin, deltaTimeMax);

                _previousTime = _tTime;
            }
        }

        private void SetComponentVariables()
        {
            _hitLineComponent.TimeBeforeHit = _hitLineControllerStruct.TimeBeforeHit;
            _hitLineComponent.DeltaHit = _hitLineControllerStruct.DeltaHit;
            _hitLineComponent.FixPosHit = _hitLineControllerStruct.FixPosHit;
        }

        private void SetControllerVariables()
        {
            _timerScenario = _controllerStruct.TimerScenario;
            _accelerationMax = _hitLineControllerStruct.AccelerationMax;
            _deltaTimeMax = _hitLineControllerStruct.DeltaTimeMax;
            _deltaTimeMin = _hitLineControllerStruct.DeltaTimeMin;
            _timeBeforeHit = _hitLineControllerStruct.TimeBeforeHit;
            _deltaHit = _hitLineControllerStruct.DeltaHit;
        }


    }
}