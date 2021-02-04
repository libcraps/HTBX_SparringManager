using System.Collections;
using System.Collections.Generic;
using SparringManager.Structures;
using SparringManager.Serial;
using System;
using UnityEngine;

namespace SparringManager
{
    public class DeviceManager : MonoBehaviour
    {
        [SerializeField]
        private StructHitBox _structHitBox;
        [SerializeField]
        private StructPlayerScene _structPlayerScene;

        private GameObject _renderCamera;
        private GameObject _playerScene;

        public int _indexSac;
        private int _nbPlayer;

        public GameObject RenderCamera
        {
            get
            {
                return _renderCamera;
            }
        }
        public GameObject PlayerScene
        {
            get
            {
                return _playerScene;
            }
        }

        Vector3 _posRenderCamera;
        Vector3 _posPlayerScene;

        private void Start()
        {
            _nbPlayer = GetComponentInParent<MainEnvironnement>().NbPlayer;
            this.gameObject.name += "_"+ _indexSac;
            _posRenderCamera = new Vector3();
            _posRenderCamera = this.gameObject.transform.position;

            _posPlayerScene = new Vector3();
            _posPlayerScene = GameObject.Find("PlaneReferenceGymnase").transform.position;

            if (_nbPlayer > 1 && _nbPlayer <5)
            {
                Debug.Log(_indexSac);
                _posPlayerScene.x += _posPlayerScene.x + 2 * (float)Math.Cos(_indexSac * 2 * Math.PI / _nbPlayer);
                _posPlayerScene.z += _posPlayerScene.z + 2 * (float)Math.Sin(_indexSac * 2 * Math.PI / _nbPlayer);
            }
            else if (_nbPlayer == 5)
            {
                
                _posPlayerScene.x += _posPlayerScene.x + 2 * (float)Math.Cos(_indexSac * 2 * Math.PI / (_nbPlayer-1));
                _posPlayerScene.z += _posPlayerScene.z + 2 * (float)Math.Sin(_indexSac * 2 * Math.PI / (_nbPlayer-1));
                Debug.Log("RAAAAAAAAAAAAAA");
                if (_indexSac == 4)
                {
                    Debug.Log("YOOOOOO");
                    _posPlayerScene = GameObject.Find("PlaneReferenceGymnase").transform.position;
                }
            }



            //TODO if on/off
            _renderCamera = InstantiatePrefab(_structHitBox.Prefab, _posRenderCamera);
            _renderCamera.GetComponent<SerialControllerCameraHitBox>().InitSerialController(_structHitBox.SerialSettings, SerialControllerCameraHitBox.i);
            _playerScene = InstantiatePrefab(_structPlayerScene.Prefab, _posPlayerScene);

        }
        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
            }
        }

        public void Init(StructHitBox hitBox, StructPlayerScene playerScene, int i)
        {
            /*
             * Constructeur du DeviceManager => ce sera du style
             * 
             * _structHitBox = arg1
             * _structViveTracker = arg2 
             * ...
             */

            _structHitBox = hitBox;
            _structPlayerScene = playerScene;
            _indexSac = i;
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