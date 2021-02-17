using System.Collections;
using System.Collections.Generic;
using SparringManager.Structures;
using SparringManager.Serial;
using SparringManager.Device;
using System;
using UnityEngine;

namespace SparringManager
{
    /*
     * 
     * Attributs :        
     *      StructHitBox _structHitBox : Structure of the Bag
     *      StructPlayerScene _structPlayerScene : Structure of the playerScene
     *      GameObject _renderCamera :
     *      GameObject _playerScene : 
     *      int _indexSac : 
     *      int _nbPlayer : 
     *      Vector3 _posRenderCamera : Position of the RenderCamera
     *      Vector3 _posPlayerScene : Position of the PlayerScene
     * 
     * Methods :
     *      void Init(StructHitBox hitBox, StructPlayerScene playerScene, int i) : Initialize Devices Structures
     *      GameObject InstantiatePrefab(GameObject prefab, Vector3 pos, GameObject parent = null) : 
     */
    public class DeviceManager : MonoBehaviour
    {
        //----------------    ATTRIBUTS    ------------------
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

        //------------------    METHODS    -------------------
        //General Methods
        private void Start()
        {
            _nbPlayer = GetComponentInParent<MainEnvironnement>().NbPlayer;
            this.gameObject.name += "_"+ _indexSac;
            _posRenderCamera = new Vector3();
            _posRenderCamera = this.gameObject.transform.position;

            _posPlayerScene = new Vector3();
            _posPlayerScene = GameObject.Find("PlaneReferenceGymnase").transform.position;

            if (_structHitBox.OnOff == true)
            {
                _renderCamera = InstantiatePrefab(_structHitBox.Prefab, _posRenderCamera);
                _renderCamera.GetComponent<SerialControllerCameraHitBox>().InitSerialController(_structHitBox.SerialSettings, SerialControllerCameraHitBox.i);
            }

            if (_structPlayerScene.OnOff == true)
            {
                if (_nbPlayer > 1 && _nbPlayer <= 5)
                {
                    Debug.Log(_indexSac);
                    _posPlayerScene.x += _posPlayerScene.x + 2 * (float)Math.Cos(_indexSac * 2 * Math.PI / _nbPlayer);
                    _posPlayerScene.z += _posPlayerScene.z + 2 * (float)Math.Sin(_indexSac * 2 * Math.PI / _nbPlayer);
                }
                _playerScene = InstantiatePrefab(_structPlayerScene.Prefab, _posPlayerScene);
                _playerScene.GetComponent<PlayerSceneController>().Init(_structPlayerScene);
            }


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
             * Initialise structure of the bag and the PlayerScene
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