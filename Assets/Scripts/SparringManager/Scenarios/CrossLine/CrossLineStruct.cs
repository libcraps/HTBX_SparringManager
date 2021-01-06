using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.CrossLine
{
    [System.Serializable]
    public struct CrossLineStruct
    {
        //Paramters of the CrossLine structure
        [SerializeField]
        private GameObject _hitPrefab;
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

        public GameObject HitPrefab
        {
            get
            {
                return _hitPrefab;
            }
            set
            {
                _hitPrefab = value;
            }
        }

        public int AccelerationMax
        {
            get
            {
                return _accelerationMax;
            }
            set
            {
                _accelerationMax = value;
            }
        }

        public CrossLineStruct(GameObject hitPrefab, int acceleration, int deltaMin, int deltaMax, float timeHit, float deltaHit)
        {
            this._hitPrefab = hitPrefab;
            this._accelerationMax = acceleration;
            this._deltaTimeMin = deltaMin;
            this._deltaTimeMax = deltaMax;
            this._timeBeforeHit = timeHit;
            this._deltaHit = deltaHit;
        }
    }
}
