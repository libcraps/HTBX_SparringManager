using System.Collections;
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
        private StructPlayerCamera[] _mainStructure;
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

        void Start()
        {
            GameObject clonePlayerCamera;
            Vector3 posPlayerCamera = new Vector3();
            float rangeSize = GameObject.Find("CoachCamera").GetComponent<Camera>().orthographicSize;
            float sizeSection = rangeSize * 2 / (NbPlayer*2);

            posPlayerCamera.x = this.gameObject.transform.localPosition.x - sizeSection*(NbPlayer-1);
            posPlayerCamera.y = this.gameObject.transform.localPosition.y;
            posPlayerCamera.z = this.gameObject.transform.localPosition.z;

            for (int i = 0; i< NbPlayer; i++)
            {
                clonePlayerCamera = Instantiate(_prefabPlayerCamera, posPlayerCamera, Quaternion.identity, this.gameObject.transform);
                clonePlayerCamera.GetComponent<SessionManager>().Init(_mainStructure[i].Scenarios, _mainStructure[i].Name);
                clonePlayerCamera.GetComponent<DeviceManager>().Init(_mainStructure[i].StructHitBox, _mainStructure[i].StructPlayerScene, i);

                posPlayerCamera.x += sizeSection*2;
            }
        }

    }

}