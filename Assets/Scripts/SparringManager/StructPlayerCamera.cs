using System.Collections;
using System.Collections.Generic;
using SparringManager.Serial;
using SparringManager.Scenarios;
using UnityEngine;

namespace SparringManager.Structures
{
    public interface IStructDevice
    {
        GameObject Prefab { get; }
        bool OnOff { get; set; }
    }

    [System.Serializable]
    public struct StructPlayerCamera
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private int _operationalArea; //angle
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
        public int operationalArea
        {
            get { return _operationalArea; }
            set { _operationalArea = value; }
        }

        public StructPlayerCamera(string name, int operationalArea,StructHitBox structHitBox, StructPlayerScene structPlayerScene, StructScenarios[] scenarios)
        {
            _name = name;
            _structHitBox = structHitBox;
            _structPlayerScene = structPlayerScene;
            _scenarios = scenarios;
            _operationalArea = operationalArea;
        }
    }

    [System.Serializable]
    public struct StructHitBox : IStructDevice
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
    public struct StructPlayerScene : IStructDevice
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private bool _onOff;
        [SerializeField]
        private string _idPlayer;
        [SerializeField]
        private StructViveTracker _structViveTracker;
        [SerializeField]
        private StructMovuino[] _structMovuino;
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
        public string IdPlayer
        {
            get
            {
                return _idPlayer;
            }
            set
            {
                _idPlayer = value;
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
        public StructMovuino[] StructMovuino
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

        public StructPlayerScene(GameObject prefab, bool onOff, string idPlayer, StructViveTracker viveTracker, StructMovuino[] movuino, StructPolar polar)
        {
            _prefab = prefab;
            _onOff = onOff;
            _idPlayer = idPlayer;
            _structViveTracker = viveTracker;
            _structMovuino = movuino;
            _structPolar = polar;
        }
    }

    [System.Serializable]
    public struct StructMovuino : IStructDevice
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private bool _onOff;
        [SerializeField]
        private string _id;

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

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public StructMovuino(GameObject prefab, bool onOff, string id)
        {
            _prefab = prefab;
            _onOff = onOff;
            _id = id;
        }
    }
    [System.Serializable]
    public struct StructPolar : IStructDevice
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
    public struct StructViveTracker : IStructDevice
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private bool _onOff;

        public enum EIndex
        {
            None = -1,
            Hmd = (int)Valve.VR.OpenVR.k_unTrackedDeviceIndex_Hmd,
            Device1,
            Device2,
            Device3,
            Device4,
            Device5,
            Device6,
            Device7,
            Device8,
            Device9,
            Device10,
            Device11,
            Device12,
            Device13,
            Device14,
            Device15,
            Device16
        }

        public EIndex indexBag;
        public EIndex indexPlayer;

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
        public StructViveTracker(GameObject prefab, EIndex bag, EIndex player, bool onOff)
        {
            _prefab = prefab;
            _onOff = onOff;
            indexBag = bag;
            indexPlayer = player;
        }
    }

}
