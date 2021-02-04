using System.Collections;
using System.Collections.Generic;
using SparringManager.Serial;
using SparringManager.Scenarios;
using UnityEngine;

namespace SparringManager.Structures
{
    [System.Serializable]
    public struct StructPlayerCamera
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private StructHitBox _structHitBox;
        [SerializeField]
        private StructPlayerScene _structPlayerScene;
        [SerializeField]
        private StructScenarios[] _scenarios;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public StructHitBox StructHitBox
        {
            get
            {
                return _structHitBox;
            }
            set
            {
                _structHitBox = value;
            }
        }
        public StructPlayerScene StructPlayerScene
        {
            get
            {
                return _structPlayerScene;
            }
            set
            {
                _structPlayerScene = value;
            }
        }
        public StructScenarios[] Scenarios
        {
            get
            {
                return _scenarios;
            }
            set
            {
                _scenarios = value;
            }
        }
        public StructPlayerCamera(string name, StructHitBox structHitBox, StructPlayerScene structPlayerScene, StructScenarios[] scenarios)
        {
            _name = name;
            _structHitBox = structHitBox;
            _structPlayerScene = structPlayerScene;
            _scenarios = scenarios;
        }
    }

    [System.Serializable]
    public struct StructHitBox
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private SerialSettings _serialSettings;
        [SerializeField]
        private bool _onOff;

        public GameObject Prefab
        {
            get
            {
                return _prefab;
            }
        }
        public SerialSettings SerialSettings
        {
            get
            {
                return _serialSettings;
            }
            set
            {
                _serialSettings = value;
            }
        }
        public bool OnOff
        {
            get
            {
                return _onOff;
            }
            set
            {
                _onOff = value;
            }
        }

        public StructHitBox(GameObject prefab, SerialSettings serialSettings, bool onOff)
        {
            _prefab = prefab;
            _serialSettings = serialSettings;
            _onOff = onOff;
        }
    }
    [System.Serializable]
    public struct StructPlayerScene
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private bool _onOff;
        [SerializeField]
        private StructViveTracker _structViveTracker;
        [SerializeField]
        private StructMovuino _structMovuino;
        [SerializeField]
        private StructPolar _structPolar;

        public GameObject Prefab
        {
            get
            {
                return _prefab;
            }
        }
        public bool OnOff
        {
            get
            {
                return _onOff;
            }
            set
            {
                _onOff = value;
            }
        }
        public StructViveTracker StructViveTracker
        {
            get
            {
                return _structViveTracker;
            }
            set
            {
                _structViveTracker = value;
            }
        }
        public StructMovuino StructMovuino
        {
            get
            {
                return _structMovuino;
            }
            set
            {
                _structMovuino = value;
            }
        }
        public StructPolar StructPolar
        {
            get
            {
                return _structPolar;
            }
            set
            {
                _structPolar = value;
            }
        }

        public StructPlayerScene(GameObject prefab, bool onOff, StructViveTracker viveTracker, StructMovuino movuino, StructPolar polar)
        {
            _prefab = prefab;
            _onOff = onOff;
            _structViveTracker = viveTracker;
            _structMovuino = movuino;
            _structPolar = polar;
        }
    }

    [System.Serializable]
    public struct StructMovuino
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private bool _onOff;

        public GameObject Prefab
        {
            get
            {
                return _prefab;
            }
        }

        public bool OnOff
        {
            get
            {
                return _onOff;
            }
            set
            {
                _onOff = value;
            }
        }

        public StructMovuino(GameObject prefab, bool onOff)
        {
            _prefab = prefab;
            _onOff = onOff;
        }
    }
    [System.Serializable]
    public struct StructPolar
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private bool _onOff;
        public GameObject Prefab
        {
            get
            {
                return _prefab;
            }
        }

        public bool OnOff
        {
            get
            {
                return _onOff;
            }
            set
            {
                _onOff = value;
            }
        }
        public StructPolar(GameObject prefab, bool onOff)
        {
            _prefab = prefab;
            _onOff = onOff;
        }
    }
    [System.Serializable]
    public struct StructViveTracker
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private bool _onOff;
        public GameObject Prefab
        {
            get
            {
                return _prefab;
            }
        }

        public bool OnOff
        {
            get
            {
                return _onOff;
            }
            set
            {
                _onOff = value;
            }
        }
        public StructViveTracker(GameObject prefab, bool onOff)
        {
            _prefab = prefab;
            _onOff = onOff;
        }
    }

}
