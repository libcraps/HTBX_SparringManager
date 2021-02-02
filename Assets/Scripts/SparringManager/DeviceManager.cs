using System.Collections;
using System.Collections.Generic;
using SparringManager.Serial;
using UnityEngine;

namespace SparringManager
{
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

        public StructHitBox(GameObject prefab,SerialSettings serialSettings, bool onOff)
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

    public class DeviceManager : MonoBehaviour
    {
        [SerializeField]
        private StructHitBox _structHitBox;
        [SerializeField]
        private StructPlayerScene _structPlayerScene;

        private GameObject RenderCamera;
        private GameObject PlayerScene;

        Vector3 _posRenderCamera;
        Vector3 _posPlayerScene;

        private void Start()
        {
            _posRenderCamera = new Vector3();
            _posRenderCamera = this.gameObject.transform.position;

            _posPlayerScene = new Vector3();
            _posPlayerScene.x = this.gameObject.transform.position.x + 200;
            _posPlayerScene.y = this.gameObject.transform.position.y;
            _posPlayerScene.z = this.gameObject.transform.position.z;

            RenderCamera = InstantiatePrefab(_structHitBox.Prefab, _posRenderCamera);
            RenderCamera.GetComponent<SerialControllerCameraHitBox>().InitSerialController(_structHitBox.SerialSettings, SerialControllerCameraHitBox.i);
            PlayerScene = InstantiatePrefab(_structPlayerScene.Prefab, _posPlayerScene);
            
        }
        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.G)) { 
            }
        }

        public void InitStruct()
        {
            /*
             * Constructeur du DeviceManager => ce sera du style
             * 
             * _structHitBox = arg1
             * _structViveTracker = arg2 
             * ...
             */
        }

        public GameObject InstantiatePrefab(GameObject prefab, Vector3 pos, GameObject parent = null)
        {
            GameObject clonePrefab;
            if (parent == null)
            {
                parent = this.gameObject;
            }
            clonePrefab = Instantiate(prefab, pos, Quaternion.identity, parent.transform);

            return clonePrefab;
        }

    }

}