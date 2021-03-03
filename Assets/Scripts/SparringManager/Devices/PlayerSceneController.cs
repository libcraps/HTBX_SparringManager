using System.Collections;
using System.Collections.Generic;
using SparringManager.Structures;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.Device
{
    /*
     * Summary :
     * MonoBehaviour class that is used to manages the player scene
     * 
     *  Attributs :
     *      StructPlayerScene _structPlayerScene : _Structure of the Player scene
     *      DataController _dataManagerComponent : DataController Component (it is in the PlayerCamera Prefab)
     *      GameObject _player:  GameObject that represent the player
     *      GameObject _bag: GameObject that represent the bag
     *      List<Vector3> _mouvementPlayer : List of the positions of the Player
     *      List<Vector3> _mouvementBag : List of the positions of the bag
     *      
     *  Methods :
     *      void Start(): used for the first frame
     */
    public class PlayerSceneController : MonoBehaviour
    {
        //----------------------    ATTRIBUTS    --------------------------
        [SerializeField]
        private StructPlayerScene _structPlayerScene;

        DataController _dataManagerComponent;

        private GameObject _player;
        private GameObject _bag;

        private GameObject _movuino;
        private GameObject _polar;
        private GameObject _viveTracker;

        private string _idPlayer;

        private List<Vector3> _mouvementPlayer;
        private List<Vector3> _mouvementBag;


        //---------------------------     METHODS    -------------------------------
        private void Start()
        {
            _player = GameObject.Find("Player");
            _bag = GameObject.Find("BagTracker");
            _idPlayer = _structPlayerScene.IdPlayer;

            _dataManagerComponent = gameObject.GetComponentInParent<DataController>();

            _mouvementPlayer = new List<Vector3>();
            _mouvementBag = new List<Vector3>();

            //Instantiation of Devices if Struct.OnOff = On
            _viveTracker = InstantiateDevice<StructViveTracker>(_structPlayerScene.StructViveTracker, this.gameObject);

            if (_structPlayerScene.StructPolar.OnOff)
            {
                _polar = InstantiateDevice<StructPolar>(_structPlayerScene.StructPolar, _player);
                _polar.GetComponent<Polar>().Init(_idPlayer);
            }


            foreach (StructMovuino mov in _structPlayerScene.StructMovuino)
            {
                _movuino = InstantiateDevice<StructMovuino>(mov, _player);
                if (mov.OnOff == true)
                {
                    _movuino.GetComponent<Movuino>().Init("/" + _idPlayer + "/" +  mov.Id);
                }
            }


        }
        private void FixedUpdate()
        {
            if (_dataManagerComponent.EndScenarioForData == true) //If the scenario ended and data a completed
            {
                _dataManagerComponent.GetSceneExportDataInStructure(_mouvementPlayer, _mouvementBag);
                _dataManagerComponent.EditDataTable = true; //The data managar can now transform all the data in the datatbale

                _mouvementBag = new List<Vector3>();
                _mouvementPlayer = new List<Vector3>();
            }

            if (GetComponentInParent<SessionManager>().EndScenario == false)
            {
                GetPlayerSceneData(_player.transform.localPosition, _bag.transform.localPosition); // We only stock the data of the scene hen a scenario is running
            }
        }
        
        public void Init(StructPlayerScene structPlayerScene)
        {
            //Init the structure of the Playercene
            _structPlayerScene = structPlayerScene;
        }

        private void GetPlayerSceneData(Vector3 posPlayer, Vector3 posBag)
        {
            //Stock Data in the list
            _mouvementBag.Add(posBag);
            _mouvementPlayer.Add(posPlayer);
        }
        GameObject InstantiateDevice<StructDevice>(StructDevice structure, GameObject parent) where StructDevice : IStructDevice
        {
            GameObject prefab = null;
            //Instantiate Devices
            if (structure.OnOff)
            {
                prefab = Instantiate(structure.Prefab, parent.transform);
                Debug.Log("Instantiation of " + structure.Prefab.name);
            }
            return prefab;
        }
    }

}