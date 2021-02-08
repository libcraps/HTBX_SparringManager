using System.Collections;
using System.Collections.Generic;
using SparringManager.Structures;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.Device
{

    public class PlayerSceneController : MonoBehaviour
    {
        [SerializeField]
        StructPlayerScene _structPlayerScene;

        PlayerSceneDataStruct _playerSceneDataStruct;

        // Start is called before the first frame update
        DataManager.DataManager _dataManagerComponent;
        private List<float> _mouvementPlayerX;
        private List<float> _mouvementPlayerY;
        private List<float> _mouvementPlayerZ;
        private List<float> _mouvementBagX;
        private List<float> _mouvementBagY;
        private List<float> _mouvementBagZ;

        GameObject _player;
        GameObject _bag;
        private void Awake()
        {
            _playerSceneDataStruct = new PlayerSceneDataStruct();
        }
        private void Start()
        {
            _player = GameObject.Find("Player");
            _bag = GameObject.Find("BagTracker");

            _dataManagerComponent = this.gameObject.GetComponentInParent<DataManager.DataManager>();
            _mouvementPlayerX = new List<float>();
            _mouvementPlayerY = new List<float>();
            _mouvementPlayerZ = new List<float>();
            _mouvementBagX = new List<float>();
            _mouvementBagY = new List<float>();
            _mouvementBagZ = new List<float>();

        }
        private void Update()
        {
            GetConsigne(_player.transform.localPosition, _bag.transform.localPosition);
        }
        public void Init(StructPlayerScene structPlayerScene)
        {
            _structPlayerScene = structPlayerScene;
        }

        private void GetConsigne(Vector3 posPlayer, Vector3 posBag)
        {
            //Put tTime's data in list
            _mouvementPlayerX.Add(posPlayer[0]);
            _mouvementPlayerY.Add(posPlayer[1]);
            _mouvementPlayerZ.Add(posPlayer[2]);
            _mouvementBagX.Add(posBag[0]);
            _mouvementBagY.Add(posBag[1]);
            _mouvementBagZ.Add(posBag[2]);
        }
        void OnDestroy()
        {
            GetExportDataInStructure();
            ExportDataInDataManager();

            _dataManagerComponent.EditFile = true;
        }
        private void GetExportDataInStructure()
        {
            //Put the export data in the dataStructure, it is call at the end of the scenario (in the destroy methods)
            _playerSceneDataStruct.MouvementPlayer = _mouvementPlayerX;
            _playerSceneDataStruct.MouvementBag = _mouvementBagX;
        }
        private void ExportDataInDataManager()
        {
            //Export the dataStructure in the datamanager
            _dataManagerComponent.DataBase.Add(_playerSceneDataStruct.PlayerSceneDataTable);
        }
    }

}