using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Device
{
    public class Polar : DeviceBehaviour
    {
        /*
         * Classe that manage the movuino object in the scene
         */
        private GameObject _oscGameObject;
        private OSC _oscManager;

        /// <summary>
        /// BPM data.
        /// </summary>
        private OSCPolarBPM _polarBPM;
        public OSCPolarBPM polarBPM { get { return _polarBPM; } }

        private void Awake()
        {
            _polarBPM = OSCDataHandler.CreateOSCDataHandler<OSCPolarBPM>();
        }
        void Start()
        {
            _oscGameObject = GameObject.Find("OSCManager");
            _oscManager = _oscGameObject.GetComponent<OSC>();

            _oscManager.SetAddressHandler(id + OSCPolarBPM.address, _polarBPM.ToOSCDataHandler);
        }

        private void FixedUpdate()
        {
            Debug.Log(id + OSCPolarBPM.address);
        }

    }

}