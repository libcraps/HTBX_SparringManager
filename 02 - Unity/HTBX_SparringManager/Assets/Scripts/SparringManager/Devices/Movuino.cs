﻿using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.Device
{

    public class Movuino : DeviceBehaviour
    {
        /*
         * Classe that manage the movuino object in the scene
         */
        private GameObject _oscGameObject;
        private OSC _oscManager;

        private string _addressSensorData;
        private string _addressGesture;

        private MovuinoSensorData movuinoSensorData; //9axes data
        private MovuinoXMM movuinoXMMdata; //Gesture data

        public string Id { get { return id; } }
        public MovuinoSensorData MovuinoSensorData { get { return movuinoSensorData; } }
        public MovuinoXMM MovuinoXMM { get { return movuinoXMMdata; } }


        private void Awake()
        {
            movuinoSensorData = OSCDataHandler.CreateOSCDataHandler<MovuinoSensorData>();
            movuinoXMMdata = OSCDataHandler.CreateOSCDataHandler<MovuinoXMM>();
        }
        void Start()
        {
            _addressSensorData = id + movuinoSensorData.OSCAddress;
            _addressGesture = id + movuinoXMMdata.OSCAddress;

            _oscGameObject = GameObject.Find("OSCManager");
            _oscManager = _oscGameObject.GetComponent<OSC>();

            _oscManager.SetAddressHandler(_addressSensorData, movuinoSensorData.ToOSCDataHandler);
            _oscManager.SetAddressHandler(_addressGesture, movuinoXMMdata.ToOSCDataHandler);
            _oscManager.SetAllMessageHandler(DebugAllMessage);
        }
        
        void DebugAllMessage(OscMessage msg)
        {
            Debug.Log(msg.address);
            Debug.Log(msg.values);
        }

    }

}