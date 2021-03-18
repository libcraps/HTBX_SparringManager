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
        private PolarBPM _polarBPM;
        public PolarBPM polarBPM { get { return _polarBPM; } }

        private void Awake()
        {
            _polarBPM = OSCDataHandler.CreateOSCDataHandler<PolarBPM>();
        }
        void Start()
        {
            _oscGameObject = GameObject.Find("OSCManager");
            _oscManager = _oscGameObject.GetComponent<OSC>();

            _oscManager.SetAddressHandler(id + PolarBPM.address, _polarBPM.ToOSCDataHandler);
        }

        private void FixedUpdate()
        {
            Debug.Log(id + PolarBPM.address);
        }

    }

}