using System.Collections;
using System.Collections.Generic;
using SparringManager.Data;
using SparringManager.Structures;
using UnityEngine;


/// <summary>
/// Namespace regrouping avery object : functions, classes, structures that are usefull or important for the good launch of the project
/// </summary>
namespace SparringManager
{
    /// <summary>
    /// MonoBehaviour class that manage the MainEnvironnement
    /// </summary>
    /// <para>Instantiates every PlayerCamera from the <paramref name="StructPlayerCamera[]">MainStructure</paramref></para>
    public class MainEnvironnement : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefabPlayerPrefab;

        [SerializeField]
        private bool _exportInFile;

        [SerializeField]
        private StructPlayerPrefab[] _mainStructure;

        /// <summary>
        /// Bool to confirm if we want to export/save session's data in file
        /// </summary>
        public bool ExportIntoFile
        {
            get
            {
                return _exportInFile;
            }
        }

        /// <summary>
        /// Get the number of player for the session.
        /// </summary>
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
        /// <summary>
        /// Instantiates PlayerPrefabs
        /// </summary>
        void Awake()
        {
            //Instantiate and initailize component of the player camera
            _coachCamera = GameObject.Find("CoachCamera");
            GameObject clonePlayerPrefab;
            Vector3 posPlayerPrefab = new Vector3();

            float rangeSize = _coachCamera.GetComponent<Camera>().orthographicSize;
            float sizeSection = rangeSize * 2 / (NbPlayer*2);

            posPlayerPrefab.x = this.gameObject.transform.localPosition.x - sizeSection*(NbPlayer-1);
            posPlayerPrefab.y = this.gameObject.transform.localPosition.y;
            posPlayerPrefab.z = this.gameObject.transform.localPosition.z;

            for (int i = 0; i< NbPlayer; i++)
            {
                clonePlayerPrefab = Instantiate(_prefabPlayerPrefab, posPlayerPrefab, Quaternion.identity, this.gameObject.transform); 
                clonePlayerPrefab.GetComponent<SessionManager>().Init(_mainStructure[i].Scenarios, _mainStructure[i].operationalArea, _mainStructure[i].Name, _exportInFile);
                clonePlayerPrefab.GetComponent<DeviceManager>().Init(_mainStructure[i].StructHitBox, _mainStructure[i].StructPlayerScene, _mainStructure[i].Name, i);
                clonePlayerPrefab.GetComponent<DataManager>().Init(_exportInFile, ".\\_data\\" + _mainStructure[i].Name+"\\");

                posPlayerPrefab.x += sizeSection*2;
            }
        }

    }

}