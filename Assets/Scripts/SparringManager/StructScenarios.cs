﻿using UnityEngine;
using SparringManager.SimpleLine;
using SparringManager.HitLine;
using System.Collections.Generic;

namespace SparringManager
{
    [System.Serializable]
    public class StructScenarios
    {
        [SerializeField]
        public GameObject _hitPrefab;
        [SerializeField]
        public GameObject _scenarioPrefab;
        [SerializeField]
        public GameObject _scenarioObjectPrefab;
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

        public StructScenarios(GameObject Prefab, GameObject hitPrefab, GameObject ObjectPrefab, int acceleration, int deltaMin, int deltaMax, float timeHit, float deltaHit)
        {
            this._scenarioPrefab = Prefab;
            this._hitPrefab = hitPrefab;
            this._scenarioObjectPrefab = ObjectPrefab;
            this._accelerationMax = acceleration;
            this._deltaTimeMin = deltaMin;
            this._deltaTimeMax = deltaMax;
            this._timeBeforeHit = timeHit;
            this._deltaHit = deltaHit;
        }
    }
}