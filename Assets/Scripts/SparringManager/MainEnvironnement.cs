﻿using System.Collections;
using System.Collections.Generic;
using SparringManager.Structures;
using SparringManager.Scenarios;
using UnityEngine;

namespace SparringManager
{
    public class MainEnvironnement : MonoBehaviour
    {
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
        private StructPlayerCamera[] _mainStructure; //TODO Pb si scénarios trop identiques -> mvt == pareil
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

        void Start()
        {
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
                clonePlayerCamera.GetComponent<SessionManager>().Init(_mainStructure[i].Scenarios, _mainStructure[i].Name, _exportInFile);
                clonePlayerCamera.GetComponent<DeviceManager>().Init(_mainStructure[i].StructHitBox, _mainStructure[i].StructPlayerScene, i);
                clonePlayerCamera.GetComponent<DataManager.DataManager>().Init(_exportInFile, ".\\_data\\" + _mainStructure[i].Name+"\\");


                posPlayerCamera.x += sizeSection*2;
            }
        }

    }

}