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
        private PolarBPM polarBPM;
        public PolarBPM PolarBPM { get { return polarBPM; } }

        private void Awake()
        {
            polarBPM = OSCDataHandler.CreateOSCDataHandler<PolarBPM>();
        }
        void Start()
        {
            _oscGameObject = GameObject.Find("OSCManager");
            _oscManager = _oscGameObject.GetComponent<OSC>();

            _oscManager.SetAddressHandler(id + "bpm", polarBPM.ToOSCDataHandler);
        }

        private void FixedUpdate()
        {
        }

    }

}