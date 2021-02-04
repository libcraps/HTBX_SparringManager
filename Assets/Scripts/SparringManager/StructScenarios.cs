using UnityEngine;
using SparringManager.SimpleLine;
using SparringManager.HitLine;
using SparringManager.SimpleHit;
using SparringManager.CrossLine;
using SparringManager.SplHitLine;
using System.Collections.Generic;

namespace SparringManager.Scenarios
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
        private GameObject _scenarioPrefab;
        [SerializeField]
        private int _timerScenario;
        [SerializeField]
        private SimpleLineStruct _simpleLineStruct;
        [SerializeField]
        private SimpleHitStruct _simpleHitStruct;
        [SerializeField]
        private HitLineStruct _hitLineStruct;
        [SerializeField]
        private CrossLineStruct _crossLineStruct;
        [SerializeField]
        private SplHitLineStruct _splHitLineStruct;

        public GameObject ScenarioPrefab
        {
            get
            {
                return _scenarioPrefab;
            }
            set
            {
                _scenarioPrefab = value;
            }
        }
        public int TimerScenario
        {
            get
            {
                return _timerScenario;
            }
            set
            {
                _timerScenario = value;
            }
        }
        public SimpleHitStruct SimpleHitStruct
        {
            get
            {
                return _simpleHitStruct;
            }
            set
            {
                _simpleHitStruct = value;
            }
        }
        public SimpleLineStruct SimpleLineStruct
        {
            get
            {
                return _simpleLineStruct;
            }
            set
            {
                _simpleLineStruct = value;
            }
        }
        public CrossLineStruct CrossLineStruct
        {
            get
            {
                return _crossLineStruct;
            }
            set
            {
                _crossLineStruct = value;
            }
        }
        public HitLineStruct HitLineStruct
        {
            get
            {
                return _hitLineStruct;
            }
            set
            {
                _hitLineStruct = value;
            }
        }
        public SplHitLineStruct SplHitLineStruct
        {
            get
            {
                return _splHitLineStruct;
            }
            set
            {
                _splHitLineStruct = value;
            }
        }

        public StructScenarios(GameObject Prefab, SimpleLineStruct simpleLineStruct, SimpleHitStruct simpleHitStruct, HitLineStruct hitLineStruct, CrossLineStruct crossLineStruct, SplHitLineStruct splHitLine, int timer)
        {
            this._scenarioPrefab = Prefab;
            this._simpleLineStruct = simpleLineStruct;
            this._simpleHitStruct = simpleHitStruct;
            this._crossLineStruct = crossLineStruct;
            this._hitLineStruct = hitLineStruct;
            this._splHitLineStruct = splHitLine;
            this._timerScenario = timer;
        }
    }
}