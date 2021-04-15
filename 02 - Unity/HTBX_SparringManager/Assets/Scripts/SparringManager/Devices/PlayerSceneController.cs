using System.Collections;
using System.Collections.Generic;
using SparringManager.Structures;
using SparringManager.Scenarios;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.Device
{
    /// <summary>
    /// Manage the PlayerScene object.
    /// </summary>
    /// <remarks>Instantiate other devices like movuinos, vivetrackers...</remarks>
    public class PlayerSceneController : MonoBehaviour
    {
        //----------------------    ATTRIBUTS    --------------------------
        [SerializeField]
        private StructPlayerScene _structPlayerScene;
        [SerializeField]
        private GameObject _playerZone;
        [SerializeField]
        private GameObject _vectorBagPlayer;
        [SerializeField]
        private GameObject _playerDistRepresentationOnGymnase;
        [SerializeField]
        private GameObject _vectorConsigne;


        private GameObject _player;
        private GameObject _bag;
        private GameObject _ground;

        private GameObject _movuino;
        private GameObject _polar;
        private GameObject _viveTracker;
        private GameObject _scenarioPlayedController;

        private string _idPlayer;

        public int NbMovuino { get { return _structPlayerScene.StructMovuino.Length; } }

        //---------------------------     METHODS    -------------------------------
        private void Awake()
        {
            _player = this.gameObject.transform.Find("Player").gameObject;
            _bag = this.gameObject.transform.Find("BagTracker").gameObject;
            _ground = GameObject.Find("PlaneReferenceGymnase");
            _idPlayer = _structPlayerScene.IdPlayer;

            

        }
        private void Start()
        {
            //Instantiation of Devices if Struct.OnOff = On
            _viveTracker = InstantiateDevice<ViveTrackerManager>(_structPlayerScene.StructViveTracker, this.gameObject, _idPlayer);
            _polar = InstantiateDevice<Polar>(_structPlayerScene.StructPolar, _player, "/" + _idPlayer);
            
            foreach (StructMovuino mov in _structPlayerScene.StructMovuino)
            {
                _movuino = InstantiateDevice<Movuino>(mov, _player, "/" + _idPlayer + mov.Id);
            }

            Vector3 posPlayerCircle = new Vector3(_player.transform.localPosition.x,  _ground.transform.localPosition.y, _player.transform.localPosition.z);
            Vector3 posBagCircle = new Vector3(_bag.transform.localPosition.x, _ground.transform.localPosition.y, _bag.transform.localPosition.z);
            Vector3 posVector = new Vector3(_bag.transform.localPosition.x, _ground.transform.localPosition.y, _bag.transform.localPosition.z);
            Vector3 posConsigne = new Vector3(_bag.transform.localPosition.x, _ground.transform.localPosition.y, _bag.transform.localPosition.z);

            _playerZone = Instantiate(_playerZone, posPlayerCircle, Quaternion.identity, this.gameObject.transform);
            _playerDistRepresentationOnGymnase = Instantiate(_playerDistRepresentationOnGymnase, posBagCircle, Quaternion.identity, this.gameObject.transform);
            _vectorBagPlayer = Instantiate(_vectorBagPlayer, posVector, Quaternion.identity, this.gameObject.transform);
            _vectorConsigne = Instantiate(_vectorConsigne, posConsigne, Quaternion.identity, this.gameObject.transform);

            
        }
        private void FixedUpdate()
        {
            if (GameObject.FindObjectOfType<ScenarioControllerBehaviour>())
            {
                _scenarioPlayedController = GameObject.FindObjectOfType<ScenarioControllerBehaviour>().gameObject;
                Debug.Log(_scenarioPlayedController.GetComponent<ScenarioControllerBehaviour>().consigne.GetType());
                _vectorBagPlayer.transform.localEulerAngles = new Vector3(0, (float)_scenarioPlayedController.GetComponent<ScenarioControllerBehaviour>().consigne, 0);

            }
            Vector3 posPlayerCircle = new Vector3(_player.transform.localPosition.x, _ground.transform.localPosition.y, _player.transform.localPosition.z);
            Vector3 posBagCircle = new Vector3(_bag.transform.localPosition.x, _ground.transform.localPosition.y-0.02f, _bag.transform.localPosition.z);
            Vector3 posVectorPlayer = new Vector3(_bag.transform.localPosition.x, _ground.transform.localPosition.y-0.015f, _bag.transform.localPosition.z);
            Vector3 posVectorConsign= new Vector3(_bag.transform.localPosition.x, _ground.transform.localPosition.y-0.015f, _bag.transform.localPosition.z);

            Vector3 _bagDir = posBagCircle - posPlayerCircle;
            Vector3 _bagDirNormalized = Vector3.Normalize(new Vector3(_bagDir.x, 0, _bagDir.z));
            Vector3 vectorAngle = _vectorBagPlayer.transform.localEulerAngles;
            float radius = _bagDir.magnitude;

            _playerZone.transform.localPosition = posPlayerCircle;
            _playerDistRepresentationOnGymnase.transform.localPosition = posBagCircle;
            _vectorBagPlayer.transform.localPosition = posVectorPlayer;
            _vectorConsigne.transform.localPosition = posVectorConsign;

            _playerDistRepresentationOnGymnase.transform.localScale = new Vector3(2 * radius, 0.001f, 2 * radius);
            _vectorBagPlayer.transform.localScale = new Vector3(radius + 0.1f , 0.001f, _vectorBagPlayer.transform.localScale.z);
            _vectorConsigne.transform.localScale = new Vector3(radius + 0.1f , 0.001f, _vectorConsigne.transform.localScale.z);

            _vectorBagPlayer.transform.localEulerAngles = new Vector3(0, Vector3.SignedAngle(_bagDirNormalized, -_bag.transform.right, -_bag.transform.forward), 0);
            
        }
        
        public void Init(StructPlayerScene structPlayerScene)
        {
            //Init the structure of the Playercene
            _structPlayerScene = structPlayerScene;
        }

        GameObject InstantiateDevice<ClassDevice>(IStructDevice structure, GameObject parent, string id) 
            where ClassDevice : DeviceBehaviour
        {
            GameObject prefab = null;
            //Instantiate Devices
            if (structure.OnOff)
            {
                prefab = Instantiate(structure.Prefab, parent.transform);
                prefab.GetComponent<ClassDevice>().Init(structure, id);
                Debug.Log("Instantiation of " + structure.Prefab.name);
            }
            return prefab;
        }
    }

}