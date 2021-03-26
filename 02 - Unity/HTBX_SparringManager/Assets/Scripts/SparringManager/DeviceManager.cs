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
     * MonoBehaviour component that manages devices of a player in the scene
     * Attributs :        
     *      StructHitBox _structHitBox : Structure of the Bag
     *      StructPlayerScene _structPlayerScene : Structure of the playerScene
     *      GameObject _renderCamera : Render camera of the PlayerCamera
     *      GameObject _playerScene : Pbject player scene
     *      int _indexSac : Number of the sac <=> id
     *      Vector3 _posRenderCamera : Position of the RenderCamera
     *      Vector3 _posPlayerScene : Position of the PlayerScene
     *      public int NbMovuino { get { return _structPlayerScene.StructMovuino.Length; } }
     *      private string _namePlayer : name of the player
     * 
     * Methods :
     *      void Init(StructHitBox hitBox, StructPlayerScene playerScene, int i) : Initialize Devices Structures
     *      GameObject InstantiatePrefab(GameObject prefab, Vector3 pos, GameObject parent = null) : 
     */

    /// <summary>
    /// MonoBehaviour component that manages devices of a player in the scene.
    /// <para>It instantiates the RenderCamera and the PlayerScene</para>
    /// <list type="Attributs">
    /// <item>The structure of the hitbox</item>
    /// <item>The structure of the hitbox</item>
    /// </list>
    /// </summary>
    public class DeviceManager : MonoBehaviour
    {
        #region Attributs
        //----------------    ATTRIBUTS    ------------------
        [SerializeField]
        private StructHitBox _structHitBox;
        [SerializeField]
        private StructPlayerScene _structPlayerScene;

        private GameObject _renderCamera;
        private GameObject _playerScene;

        private string _namePlayer;

        private int _indexSac;

        /// <value>Structure of the PlayerScene</value>
        public StructPlayerScene StructPlayerScene { get { return _structPlayerScene; } }

        /// <value>Get the RenderCamera unity object of the PlayerCamera</value>
        public GameObject RenderCamera
        {
            get
            {
                return _renderCamera;
            }
        }
        /// <value>Get the PlayerScene unity object of the PlayerCamera</value>
        public GameObject PlayerScene
        {
            get
            {
                return _playerScene;
            }
        }

        /// <value>Get the number of movuinos of the PlayerScene</value>
        public int NbMovuino { get { return _structPlayerScene.StructMovuino.Length; } }


        Vector3 _posRenderCamera; //Position of the RenderCamera
        Vector3 _posPlayerScene; //Position of the PlayerScene
        #endregion

        #region Methods
        private void Awake()
        {
            
        }
        private void Start()
        {
             int nbPlayer = GetComponentInParent<MainEnvironnement>().NbPlayer;

            this.gameObject.name += "_" + _indexSac;

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
                if (nbPlayer > 1 && nbPlayer <= 5)
                {
                    Debug.Log(_indexSac);
                    _posPlayerScene.x += _posPlayerScene.x + 2 * (float)Math.Cos(_indexSac * 2 * Math.PI / nbPlayer);
                    _posPlayerScene.z += _posPlayerScene.z + 2 * (float)Math.Sin(_indexSac * 2 * Math.PI / nbPlayer);
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
        /// <summary>
        /// Initialise structure of the bag and the PlayerScene.
        /// </summary>
        /// <param name="hitBox">Structure of the hitbox</param>
        /// <param name="playerScene">Structure of the PlayerScene</param>
        /// <param name="name">Name of the player</param>
        /// <param name="i">Index of the bag</param>
        public void Init(StructHitBox hitBox, StructPlayerScene playerScene, string name, int i)
        {
            _structHitBox = hitBox;
            _structPlayerScene = playerScene;
            _namePlayer = name;
            _indexSac = i;
        }

        /// <summary>
        /// Instantiate the prefab that is in argmuent (GameObject prefab) and return his clone that is in the Unity scene.
        /// </summary>
        /// <param name="prefab">Prefab to instantiate</param>
        /// <param name="pos">Coordinates to position the prefab</param>
        /// <param name="parent">Parent of the prefab (if you want to specified)</param>
        /// <returns>Return the clone that is in the Unity scene</returns>
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
        #endregion
    }

}