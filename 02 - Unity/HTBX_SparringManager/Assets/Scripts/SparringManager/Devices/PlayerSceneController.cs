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
     *      GameObject _player:  GameObject that represent the player
     *      GameObject _bag: GameObject that represent the bag
     *      GameObject _movuino
     *      GameObject _polar
     *      GameObject _viveTracker
     *      string _idPlayer
     *      
     *  Methods :
     *      void Start(): used for the first frame
     *      GameObject InstantiateDevice<StructDevice>(StructDevice structure, GameObject parent) where StructDevice : IStructDevice
     *      void Init(StructPlayerScene structPlayerScene)
     *      
     */
    public class PlayerSceneController : MonoBehaviour
    {
        //----------------------    ATTRIBUTS    --------------------------
        [SerializeField]
        private StructPlayerScene _structPlayerScene;

        private GameObject _player;
        private GameObject _bag;

        private GameObject _movuino;
        private GameObject _polar;
        private GameObject _viveTracker;

        private string _idPlayer;

        public int NbMovuino { get { return _structPlayerScene.StructMovuino.Length; } }

        //---------------------------     METHODS    -------------------------------
        private void Start()
        {
            _player = GameObject.Find("Player");
            _bag = GameObject.Find("BagTracker");
            _idPlayer = _structPlayerScene.IdPlayer;

            //Instantiation of Devices if Struct.OnOff = On
            _viveTracker = InstantiateDevice<ViveTrackerManager>(_structPlayerScene.StructViveTracker, this.gameObject, _idPlayer);
            _polar = InstantiateDevice<Polar>(_structPlayerScene.StructPolar, _player, "/" + _idPlayer);

            foreach (StructMovuino mov in _structPlayerScene.StructMovuino)
            {
                _movuino = InstantiateDevice<Movuino>(mov, _player, "/" + _idPlayer + mov.Id);
            }

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