using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.Device
{
    public class Movuino : MonoBehaviour
    {
        private GameObject _oscGameObject;
        private OSC _oscManager;

        private string _addressSensorData;
        private string _addressGesture;

        private string _id;

        MovuinoSensorData movuinoSensorData = MovuinoData.CreateMovuinoData<MovuinoSensorData>();
        MovuinoXMM movuinoXMMdata = MovuinoData.CreateMovuinoData<MovuinoXMM>();


        private List<int> _listGesture;
        private List<float> _listProgression;

        private void Awake()
        {
            _listGesture = new List<int>();
            _listProgression = new List<float>();
        }
        void Start()
        {
            _addressSensorData = _id + movuinoSensorData.movuinoAddress;
            _addressGesture = _id + movuinoXMMdata.movuinoAddress;

            _oscGameObject = GameObject.Find("OSCManager");
            _oscManager = _oscGameObject.GetComponent<OSC>();

            _oscManager.SetAddressHandler(_addressSensorData, movuinoSensorData.ToMovuinoData);
            _oscManager.SetAddressHandler(_addressGesture, movuinoXMMdata.ToMovuinoData);
            _oscManager.SetAllMessageHandler(GetMessages);
        }

        private void FixedUpdate()
        {
            
        }

        public void Init(string id)
        {
            _id = id;
        }
        private void GetMessages(OscMessage message)
        {
            Debug.Log(message.address);
        }

        private void StockDataGesture(int idGesture, float progression)
        {
            _listGesture.Add(idGesture);
            _listProgression.Add(progression);
        }


    }

}