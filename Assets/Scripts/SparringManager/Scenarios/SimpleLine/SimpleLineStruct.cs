using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.SimpleLine
{
    [System.Serializable]
    public struct SimpleLineStruct
    {
        [SerializeField]
        public GameObject _simpleLine;
        [SerializeField]
        public int _accelerationMax;
        [SerializeField]
        public int _deltaTimeMax;
        [SerializeField]
        public int _deltaTimeMin;

        public SimpleLineStruct(GameObject prefab, int acceleration, int deltaMin, int deltaMax)
        {
            this._simpleLine = prefab;
            this._accelerationMax = acceleration;
            this._deltaTimeMin = deltaMin;
            this._deltaTimeMax = deltaMax;
        }
    }
}
