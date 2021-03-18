using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using SparringManager.Structures;
using UnityEngine;

namespace SparringManager
{
    /*
     * Summary :
     * 
     * MonoBehaviour class that is used to generate the main environnement : instantiate scenarios and gymnase scene
     * 
     *  Attributs :
     *      GameObject _prefabPlayerCamera : Prefab of the player environnement, at the moment it contains : a player scene (gymnase environnement) and a renderCamera for the bag
     *      GameObject _coachCamera : GameObject of the Coach Camera
     *      StructPlayerCamera[] _mainStructure : List of StructPlayerCamera that contain all the usefull data for the Gymnase scene, scenario,... for every player 
     *      bool _exportInFile : Boolean for the exportation data
     *      
     *  Methods :
     *      void Awake(): Instantiate and initailize component of the player camera
     */
    public class MainEnvironnement : MonoBehaviour
    {
        //----------------------    ATTRIBUTS    --------------------------
        [SerializeField]
        private GameObject _prefabPlayerCamera;

        [SerializeField]
        private bool _exportInFile;
        public bool ExportIntoFile
        {
            get
            {
                return _exportInFile;
            }
        }

        [SerializeField]
        private StructPlayerCamera[] _mainStructure; //TODO Pb si scénarios trop identiques -> mvt == pareil -> pb de system.random peut etre
        public StructPlayerCamera[] MainStructure
        {
            get
            {
                return _mainStructure;
            }
            set
            {
                _mainStructure = value;
            }
        }

        public int NbPlayer
        {
            get
            {
                return _mainStructure.Length;
            }
        }

        private GameObject _coachCamera;
        public GameObject CoachCamera
        {
            get
            {
                return _coachCamera;
            }
        }

        //----------------------    Methods    --------------------------
        void Awake()
        {
            //Instantiate and initailize component of the player camera
            GameObject _coachCamera = GameObject.Find("CoachCamera");
            GameObject clonePlayerCamera;

            Vector3 posPlayerCamera = new Vector3();
            float rangeSize = _coachCamera.GetComponent<Camera>().orthographicSize;
            float sizeSection = rangeSize * 2 / (NbPlayer*2);

            posPlayerCamera.x = this.gameObject.transform.localPosition.x - sizeSection*(NbPlayer-1);
            posPlayerCamera.y = this.gameObject.transform.localPosition.y;
            posPlayerCamera.z = this.gameObject.transform.localPosition.z;

            for (int i = 0; i< NbPlayer; i++)
            {

                clonePlayerCamera = Instantiate(_prefabPlayerCamera, posPlayerCamera, Quaternion.identity, this.gameObject.transform); 
                clonePlayerCamera.GetComponent<SessionManager>().Init(_mainStructure[i].Scenarios, _mainStructure[i].StructPlayerScene, _mainStructure[i].operationalArea, _mainStructure[i].Name, _exportInFile);
                clonePlayerCamera.GetComponent<DeviceManager>().Init(_mainStructure[i].StructHitBox, _mainStructure[i].StructPlayerScene, _mainStructure[i].Name, i);
                clonePlayerCamera.GetComponent<DataController>().Init(_exportInFile, ".\\_data\\" + _mainStructure[i].Name+"\\");

                posPlayerCamera.x += sizeSection*2;
            }
        }

    }

}