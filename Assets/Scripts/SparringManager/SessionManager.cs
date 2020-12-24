using System.Collections;
using System.Collections.Generic;
using System;
using SparringManager.SimpleLine;
using SparringManager.HitLine;
using UnityEngine;

namespace SparringManager
{
    public class SessionManager : MonoBehaviour
    {
        [SerializeField]
        private StructScenarios[] scenarios;

        private int indexScenario;

        //Variables temporaires de scénarios
        private int _timerScenarioI;
        public float _timeStartScenarioI;

        void Start()
        {
            indexScenario = 0;

            _timerScenarioI = scenarios[indexScenario]._timerScenario;
            _timeStartScenarioI = Time.time;

            SessionManager.InstantiateAndBuildScenario(scenarios[indexScenario], this.gameObject, this.gameObject.transform.position);
        }
        private void Update()
        {
            float _tTime = Time.time;
            if (((_tTime - _timeStartScenarioI) > _timerScenarioI) && (indexScenario < (scenarios.Length -1)))
            {
                indexScenario += 1;

                _timerScenarioI = scenarios[indexScenario]._timerScenario;
                _timeStartScenarioI = Time.time;

                SessionManager.InstantiateAndBuildScenario(scenarios[indexScenario], this.gameObject, this.gameObject.transform.position);
            }
        }

        public StructScenarios InstantiateScenarioStruct()
        {
            return scenarios[indexScenario];
        }

        public void DisplayDataScenari(StructScenarios scenario)
        {
            Debug.Log("--- Scenario name : " + scenario._scenarioPrefab.name);
            Debug.Log("--- Scenario Duration : " + scenario._timerScenario);
        }
        public static void InstantiateAndBuildScenario(StructScenarios strucObject, GameObject referenceGameObject, Vector3 _pos3d, GameObject prefabObject = null)
        {
            /*
             * Function that instatiate an object, the prefab of this object is in the structureScenarios, 
             * it contains all the data that is usefull for the scenarios
             * 
             * referenceGameObject : is often this.gameObject
             */
            if (prefabObject == false)
            {
                prefabObject = strucObject._scenarioPrefab;
            }

            if (_pos3d == null)
            {
                _pos3d = referenceGameObject.transform.position;
            }
            if (strucObject._startScenario == 0)
            {
                strucObject._startScenario = Time.time;
            }

            float _timerScenarioI = strucObject._timerScenario;
            ScenarioController scenarioControllerComponent;

            prefabObject.AddComponent<ScenarioController>();
            scenarioControllerComponent = prefabObject.GetComponent<ScenarioController>();
            scenarioControllerComponent._controllerStruct = strucObject;
            Destroy(Instantiate(prefabObject, _pos3d, Quaternion.identity, referenceGameObject.transform), _timerScenarioI);

            Debug.Log(prefabObject.name + " has been instantiated");
        }
    }
}
//Utiliser update() pour actualiser la génération de nouveaux scénarios quand un est finie