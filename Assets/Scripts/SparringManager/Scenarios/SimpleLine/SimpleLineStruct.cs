using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.SimpleLine
{
    [System.Serializable]
    public struct SimpleLineStruct
    {
        //Parameters of the SimpleLineStruct scenario
        [SerializeField]
        private int _timerScenario;
        [SerializeField]
        private int _accelerationMax;
        [SerializeField]
        private int _deltaTimeMax;
        [SerializeField]
        private int _deltaTimeMin;

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


        public SimpleLineStruct(int timerScenario, int acceleration, int deltaMin, int deltaMax)
        {
            this._timerScenario = timerScenario;
            this._accelerationMax = acceleration;
            this._deltaTimeMin = deltaMin;
            this._deltaTimeMax = deltaMax;
        }
    }
}
