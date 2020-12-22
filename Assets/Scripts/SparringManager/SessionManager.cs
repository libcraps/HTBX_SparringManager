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

        private int indexScenario = 0;

        private StructScenarios _structScenarioI;
        private GameObject _scenarioPrefabI;
        private int _timerScenarioI;
        public float _timeStartScenarioI;
        private ScenarioController scen;

        void Start()
        {
            _structScenarioI = scenarios[indexScenario];
            _scenarioPrefabI = _structScenarioI._scenarioPrefab;
            _timerScenarioI = _structScenarioI._timerScenario;
            _timeStartScenarioI = Time.time;

            Debug.Log("SessionManager timer " + _structScenarioI);

            _scenarioPrefabI.AddComponent<ScenarioController>();
            scen = _scenarioPrefabI.GetComponent<ScenarioController>();
            scen._controllerStruct = _structScenarioI;
            Destroy(Instantiate(_scenarioPrefabI, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform), _timerScenarioI);
        }
        private void Update()
        {
            float _tTime = Time.time;
            if (((_tTime - _timeStartScenarioI) > _timerScenarioI) && (indexScenario < (scenarios.Length -1)))
            {
                indexScenario += 1;
                _structScenarioI = scenarios[indexScenario];
                _scenarioPrefabI = _structScenarioI._scenarioPrefab;
                _timerScenarioI = _structScenarioI._timerScenario;
                _timeStartScenarioI = Time.time;

                Destroy(Instantiate(_scenarioPrefabI, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform), _timerScenarioI);
                
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
        public void InstantiateAndBuildScenario()
        {

        }
    }
}
//Utiliser update() pour actualiser la génération de nouveaux scénarios quand un est finie