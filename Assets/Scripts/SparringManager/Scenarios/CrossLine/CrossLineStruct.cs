using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.CrossLine
{
    [System.Serializable]
    public struct CrossLineStruct
    {
        [SerializeField]
        public GameObject _hitPrefab;
        [SerializeField]
        public GameObject _hitLine;
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

        public CrossLineStruct(GameObject linePrefab,GameObject hitPrefab, int acceleration, int deltaMin, int deltaMax, float timeHit, float deltaHit)
        {
            this._hitLine = linePrefab;
            this._hitPrefab = hitPrefab;
            this._accelerationMax = acceleration;
            this._deltaTimeMin = deltaMin;
            this._deltaTimeMax = deltaMax;
            this._timeBeforeHit = timeHit;
            this._deltaHit = deltaHit;
        }
    }
}
