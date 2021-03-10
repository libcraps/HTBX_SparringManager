using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Scenarios
{
    [System.Serializable]
    public struct SimpleLineStruct : IStructScenario
    {
        //Parameters of the SimpleLineStruct scenario
        [SerializeField]
        private int _accelerationMax;
        [SerializeField]
        private int _deltaTimeMax;
        [SerializeField]
        private int _deltaTimeMin;

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
                _deltaTimeMax  = value;
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


        public SimpleLineStruct(int acceleration, int deltaMin, int deltaMax)
        {
            this._accelerationMax = acceleration;
            this._deltaTimeMin = deltaMin;
            this._deltaTimeMax = deltaMax;
        }
    }
}
