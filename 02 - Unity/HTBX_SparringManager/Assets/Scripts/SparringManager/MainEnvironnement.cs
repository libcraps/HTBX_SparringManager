using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
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
        private GameObject _prefabPlayerCamera;


        [SerializeField]
        private bool _exportInFile;

        [SerializeField]
        private StructPlayerCamera[] _mainStructure;
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
        /// Instantiates PlayerCameras
        /// </summary>
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
                clonePlayerCamera.GetComponent<SessionManager>().Init(_mainStructure[i].Scenarios, _mainStructure[i].operationalArea, _mainStructure[i].Name, _exportInFile);
                clonePlayerCamera.GetComponent<DeviceManager>().Init(_mainStructure[i].StructHitBox, _mainStructure[i].StructPlayerScene, _mainStructure[i].Name, i);
                clonePlayerCamera.GetComponent<DataController>().Init(_exportInFile, ".\\_data\\" + _mainStructure[i].Name+"\\");

                posPlayerCamera.x += sizeSection*2;
            }
        }

    }

}