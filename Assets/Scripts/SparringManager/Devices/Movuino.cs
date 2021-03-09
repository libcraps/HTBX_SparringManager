using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.Device
{
    public class Movuino : MonoBehaviour
    {
        /*
         * Classe that manage the movuino object in the scene
         */
        private GameObject _oscGameObject;
        private OSC _oscManager;

        private string _addressSensorData;
        private string _addressGesture;

        private string _id;

        MovuinoSensorData movuinoSensorData = OSCDataHandler.CreateOSCDataHandler<MovuinoSensorData>();
        MovuinoXMM movuinoXMMdata = OSCDataHandler.CreateOSCDataHandler<MovuinoXMM>();

        public MovuinoSensorData MovuinoSensorData { get { return movuinoSensorData; } }
        public MovuinoXMM MovuinoXMM { get { return movuinoXMMdata; } }

        private List<int> _listGesture;
        private List<float> _listProgression;

        private void Awake()
        {
            _listGesture = new List<int>();
            _listProgression = new List<float>();
        }
        void Start()
        {
            _addressSensorData = _id + movuinoSensorData.OSCAddress;
            _addressGesture = _id + movuinoXMMdata.OSCAddress;

            _oscGameObject = GameObject.Find("OSCManager");
            _oscManager = _oscGameObject.GetComponent<OSC>();

            _oscManager.SetAddressHandler(_addressSensorData, movuinoSensorData.ToOSCDataHandler);
            _oscManager.SetAddressHandler(_addressGesture, movuinoXMMdata.ToOSCDataHandler);
        }

        public void Init(string id)
        {
            _id = id;
        }

    }

}