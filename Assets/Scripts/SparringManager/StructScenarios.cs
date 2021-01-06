using UnityEngine;
using SparringManager.SimpleLine;
using SparringManager.HitLine;
using SparringManager.SimpleHit;
using SparringManager.CrossLine;
using System.Collections.Generic;

namespace SparringManager
{
    [System.Serializable]
    public struct StructScenarios
    {
        /*
         * Structure that can contains the structure of paramaters of every scenario
         * 
         * _scenarioPrefab : prefab of the scenario
         * _timerScenario : duration of the scenaio
         * 
         * ScenarioNameStructure : Structure of the parameters of the scenarioNAme
         * 
         */
        [SerializeField] //Scenatio_Name prefab
        public GameObject _scenarioPrefab;
        [SerializeField]
        public int _timerScenario;
        [SerializeField]
        public SimpleLineStruct SimpleLineStruct;
        [SerializeField]
        public SimpleHitStruct SimpleHitStruct;
        [SerializeField]
        public HitLineStruct HitLineStruct;
        [SerializeField]
        public CrossLineStruct CrossLineStruct;

        public StructScenarios(GameObject Prefab, SimpleLineStruct simpleLineStruct, SimpleHitStruct simpleHitStruct, HitLineStruct hitLineStruct, CrossLineStruct crossLineStruct, int timer)
        {
            this._scenarioPrefab = Prefab;
            this.SimpleLineStruct = simpleLineStruct;
            this.SimpleHitStruct = simpleHitStruct;
            this.CrossLineStruct = crossLineStruct;
            this.HitLineStruct = hitLineStruct;
            this._timerScenario = timer;
        }
    }
}