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
        /* OLD
        [SerializeField] //Scenatio_Name prefab
        public GameObject _scenarioPrefab;
        [SerializeField]
        public int _timerScenario;
        [SerializeField]
        public int _accelerationMax;
        [SerializeField]
        public int _deltaTimeMax;
        [SerializeField]
        public int _deltaTimeMin;
        [SerializeField]
        public float _deltaHit;
        [SerializeField]
        public float _timeBeforeHit;

        public StructScenarios(GameObject Prefab, GameObject hitPrefab, GameObject ObjectPrefab, int acceleration, int deltaMin, int deltaMax, float timeHit, float deltaHit, int timer)
        {
            this._scenarioPrefab = Prefab;
            this._accelerationMax = acceleration;
            this._deltaTimeMin = deltaMin;
            this._deltaTimeMax = deltaMax;
            this._timeBeforeHit = timeHit;
            this._deltaHit = deltaHit;
            this._timerScenario = timer;
        }
        */
    }
}