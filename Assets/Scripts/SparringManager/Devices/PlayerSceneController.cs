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

        // Start is called before the first frame update
        DataManager.DataManager _dataManagerComponent;

        private List<Vector3> _mouvementPlayer;
        private List<Vector3> _mouvementBag;

        GameObject _player;
        GameObject _bag;
        private void Start()
        {
            _player = GameObject.Find("Player");
            _bag = GameObject.Find("BagTracker");

            _dataManagerComponent = this.gameObject.GetComponentInParent<DataManager.DataManager>();

            _mouvementPlayer = new List<Vector3>();
            _mouvementBag = new List<Vector3>();

            InstantiateDevice<StructViveTracker>(_structPlayerScene.StructViveTracker, _bag);
            InstantiateDevice<StructPolar>(_structPlayerScene.StructPolar, _player);
            InstantiateDevice<StructMovuino>(_structPlayerScene.StructMovuino, _player);
        }
        private void FixedUpdate()
        {
            if (_dataManagerComponent.EndScenarioForData == true)
            {
                _dataManagerComponent.GetSceneExportDataInStructure(_mouvementPlayer, _mouvementBag);
                _dataManagerComponent.EditDataTable = true;

                _mouvementBag = new List<Vector3>();
                _mouvementPlayer = new List<Vector3>();
            }

            if (GetComponentInParent<SessionManager>().EndScenario == false)
            {
                GetPlayerSceneData(_player.transform.localPosition, _bag.transform.localPosition);
            }
        }
        public void Init(StructPlayerScene structPlayerScene)
        {
            _structPlayerScene = structPlayerScene;
        }

        private void GetPlayerSceneData(Vector3 posPlayer, Vector3 posBag)
        {
            _mouvementBag.Add(posBag);
            _mouvementPlayer.Add(posPlayer);
        }
        void OnDestroy()
        {
            //_dataManagerComponent.GetSceneExportDataInStructure(_mouvementPlayer, _mouvementBag);
            //ExportDataInDataManager();
        }

        void InstantiateDevice<StructDevice>(StructDevice structure, GameObject parent) where StructDevice : IStructDevice
        {
            if (structure.OnOff)
            {
                Instantiate(structure.Prefab, parent.transform);
            }
        }
    }

}