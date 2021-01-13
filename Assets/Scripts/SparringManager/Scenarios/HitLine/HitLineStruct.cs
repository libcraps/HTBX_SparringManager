using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.HitLine
{
    [System.Serializable]
    public struct HitLineStruct
    {
        [SerializeField]
        private GameObject _hitPrefab;
        [SerializeField]
        private int _accelerationMax;
        [SerializeField]
        private int _deltaTimeMax;
        [SerializeField]
        private int _deltaTimeMin;
        [SerializeField]
        private float _deltaHit;
        [SerializeField]
        private float _timeBeforeHit;
        [SerializeField]
        private bool _fixPosHit;

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
        public int DeltaTimeMax
        {
            get
            {
                return _deltaTimeMax;
            }
            set
            {
                _deltaTimeMax = value;
            }
        }
        public int DeltaTimeMin
        {
            get
            {
                return _deltaTimeMin;
            }
            set
            {
                _deltaTimeMin = value;
            }
        }

        public float TimeBeforeHit
        {
            get
            {
                return _timeBeforeHit;
            }
            set
            {
                _timeBeforeHit = value;
            }
        }
        public float DeltaHit
        {
            get
            {
                return _deltaHit;
            }
            set
            {
                _deltaHit = value;
            }
        }
        public bool FixPosHit
        {
            get
            {
                return _fixPosHit;
            }
            set
            {
                _fixPosHit = value;
            }
        }

        public HitLineStruct(GameObject hitPrefab, int acceleration, int deltaMin, int deltaMax, float timeHit, float deltaHit, bool fixPosHit)
        {
            this._hitPrefab = hitPrefab;
            this._accelerationMax = acceleration;
            this._deltaTimeMin = deltaMin;
            this._deltaTimeMax = deltaMax;
            this._timeBeforeHit = timeHit;
            this._deltaHit = deltaHit;
            this._fixPosHit = fixPosHit;
        }
    }
}
