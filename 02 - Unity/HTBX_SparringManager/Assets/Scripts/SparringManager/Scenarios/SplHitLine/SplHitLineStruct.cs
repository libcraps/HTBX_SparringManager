using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Scenarios
{
    [System.Serializable]
    public struct SplHitLineStruct : IStructScenario
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
        private int _scaleMaxValue;
        [SerializeField]
        private float _scaleSpeed;
        [SerializeField]
        private bool _fixPosHit;

        public int ScaleMaxValue
        {
            get
            {
                return _scaleMaxValue;
            }
            set
            {
                _scaleMaxValue = value;
            }
        }
        public float ScaleSpeed
        {
            get
            {
                return _scaleSpeed;
            }
            set
            {
                _scaleSpeed = value;
            }
        }
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

        public SplHitLineStruct(GameObject hitPrefab, int acceleration, int deltaMin, int deltaMax, float timeHit, float deltaHit, int scaleMaxValue, int scaleSpeed, bool fixPosHit)
        {
            this._hitPrefab = hitPrefab;
            this._accelerationMax = acceleration;
            this._deltaTimeMin = deltaMin;
            this._deltaTimeMax = deltaMax;
            this._timeBeforeHit = timeHit;
            this._deltaHit = deltaHit;
            this._scaleSpeed = scaleSpeed;
            this._scaleMaxValue = scaleMaxValue;
            this._fixPosHit = fixPosHit;
        }
    }
}
