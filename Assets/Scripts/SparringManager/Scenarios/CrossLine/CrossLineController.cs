﻿using SparringManager.DataManager.CrossLine;
using UnityEngine;

namespace SparringManager.CrossLine
{
    //Classe du Controller du scenario CrossLine
    public class CrossLineController : MonoBehaviour
    {
        private StructScenarios controllerStruct;
        private CrossLineStruct crossLineStruct;
        private CrossLineDataStruct CrossLineData;

        private ScenarioController scenarioControllerComponent;
        
        [SerializeField]
        private GameObject _scenarioComposant;

        private float _reactTime;
        private float _startScenario;

        public static bool _hitted = false;

        void Start()
        {
            scenarioControllerComponent = GetComponent<ScenarioController>();
            controllerStruct = scenarioControllerComponent._controllerStruct;
            crossLineStruct = scenarioControllerComponent._controllerStruct.CrossLineStruct;
            _startScenario = Time.time;

            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            SessionManager.InstantiateAndBuildScenario(controllerStruct, this.gameObject, _pos3d, _scenarioComposant);
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
            float tTime = Time.time - _startScenario;
            float _timeBeforeHit = crossLineStruct._timeBeforeHit;
            float _deltaHit = crossLineStruct._deltaHit;

            RaycastHit hit;
            Vector3 rayCastOrigin = new Vector3(position2d_.x, position2d_.y, this.gameObject.transform.position.z);
            Vector3 rayCastDirection = new Vector3(0, 0, 1);

            bool rayOnTarget = Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, 250);
            bool canHit = (tTime > _timeBeforeHit && (tTime - _timeBeforeHit) < _deltaHit);

            if (rayOnTarget && canHit && _hitted == false)
            {
                _reactTime = tTime - _timeBeforeHit;
                _hitted = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + _reactTime);
            }
        }

        void OnDestroy()
        {
            GetData();
            DataManager.DataManager.ToCSV(CrossLineData.DataBase, "C:\\Users\\Pierre\\Documents\\Tableau.csv");
            _reactTime = 0;
            _hitted = false;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        void GetData()
        {
            CrossLineData = new CrossLineDataStruct(_hitted, _reactTime, CrossLine.mouvementConsign, CrossLine.timeListScenario);
            DataManager.DataManager.DataBase.Add(CrossLineData);            
            Debug.Log(DataManager.DataManager.DataBase);
        }


        

    }
}