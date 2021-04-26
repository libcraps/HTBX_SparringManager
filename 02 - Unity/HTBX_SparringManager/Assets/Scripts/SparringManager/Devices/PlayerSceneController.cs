using System.Collections;
using System.Collections.Generic;
using SparringManager.Structures;
using SparringManager.Scenarios;
using SparringManager.Data;
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

        public GameObject player;
        public GameObject bag;
        public GameObject ground;

        private GameObject _GymnaseRep;
        private GameObject _movuino;
        private GameObject _polar;
        private GameObject _viveTracker;
        private GameObject _scenarioPlayedController;

        public GameObject movuino { get { return _movuino;  } }
        public GameObject polar { get { return _polar;  } }
        public GameObject viveTracker { get { return _viveTracker;  } }

        private string _idPlayer;

        public int NbMovuino { get { return _structPlayerScene.StructMovuino.Length; } }

        //---------------------------     METHODS    -------------------------------
        private void Awake()
        {
            player = this.gameObject.transform.Find("Player").gameObject;
            bag = this.gameObject.transform.Find("BagTracker").gameObject;
            ground = GameObject.Find("PlaneReferenceGymnase");
            _idPlayer = _structPlayerScene.IdPlayer;
        }
        private void Start()
        {
            //Instantiation of Devices if Struct.OnOff = On
            _viveTracker = InstantiateDevice<ViveTrackerManager>(_structPlayerScene.StructViveTracker, this.gameObject, _idPlayer);
            _polar = InstantiateDevice<Polar>(_structPlayerScene.StructPolar, player, "/" + _idPlayer);
            
            foreach (StructMovuino mov in _structPlayerScene.StructMovuino)
            {
                _movuino = InstantiateDevice<Movuino>(mov, player, "/" + _idPlayer + mov.Id);
            }

            _GymnaseRep = InstantiateDevice<GymnaseBasicProjection>(_structPlayerScene.StructGymnaseRepresentation, this.gameObject, _idPlayer);

        }
        private void FixedUpdate()
        {

            
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