using UnityEngine;

namespace SparringManager.Scenarios
{
    /// <summary>
    /// Structure that contains parameters of the specified scenario
    /// </summary>
    public interface IStructScenario
    {
    }

    /// <summary>
    /// Structure that sets secnarios settings
    /// </summary>
    [System.Serializable]
    public struct GeneriqueScenarioStruct
    {
        
        [SerializeField] //Scenatio_Name prefab
        [Tooltip("Prefab of the scenario : scenario_NAME")]
        private GameObject _scenarioPrefab;
        [SerializeField]
        [Tooltip("Scenario duration")]
        private float _timerScenario;
        [SerializeField]
        [Tooltip("Speed of the object")]
        private int _speed;
        [SerializeField]
        [Tooltip("Quantity of hit")]
        private int _rythme;
        [SerializeField]
        [Tooltip("Stop the Object for the hit")]
        private bool _fixPosHit;

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
        public float TimerScenario
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
        public int Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }
        public int Rythme
        {
            get
            {
                return _rythme;
            }
            set
            {
                _rythme = value;
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

    }
}