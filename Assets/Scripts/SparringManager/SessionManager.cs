using System.Collections;
using System.Collections.Generic;
using System;
using SparringManager.SimpleLine;
using SparringManager.HitLine;
using SparringManager.CrossLine;
using UnityEngine;

namespace SparringManager
{
    public class SessionManager : MonoBehaviour
    {
        [SerializeField]
        private StructScenarios[] scenarios;

        private int indexScenario;
        private string nameSenarioI;

        string path;

        //Variables temporaires de scénarios
        private int _timerScenarioI;
        private float _timeStartScenarioI;

        void Start()
        {
            indexScenario = 0;

            _timerScenarioI = scenarios[indexScenario]._timerScenario;
            nameSenarioI = scenarios[indexScenario]._scenarioPrefab.name;
            _timeStartScenarioI = Time.time;
            
            InstantiateAndBuildScenario(scenarios[indexScenario], this.gameObject, this.gameObject.transform.position);
        }
        private void Update()
        {
            if (((Time.time - _timeStartScenarioI) > _timerScenarioI) && (indexScenario < (scenarios.Length -1)))
            {
                indexScenario += 1;

                _timerScenarioI = scenarios[indexScenario]._timerScenario;
                _timeStartScenarioI = Time.time;

                InstantiateAndBuildScenario(scenarios[indexScenario], this.gameObject, this.gameObject.transform.position);
            }
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

            ScenarioController scenarioControllerComponent = prefabObject.GetComponent<ScenarioController>();

            scenarioControllerComponent._controllerStruct = strucObject;
            Destroy(Instantiate(prefabObject, _pos3d, Quaternion.identity, referenceGameObject.transform), strucObject._timerScenario);

            Debug.Log(prefabObject.name + " has been instantiated");
        }
    }
}