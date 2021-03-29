using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Device
{
    /// <summary>
    /// Class that manage a polar object in the scene
    /// </summary>
    /// <remarks>Handle OSC conncetion too</remarks>
    /// <inheritdoc cref="DeviceBehaviour"/>
    public class Polar : DeviceBehaviour
    {
        /*
         * Class that manage the movuino object in the scene
         */
        private GameObject _oscGameObject;
        private OSC _oscManager;

        /// <summary>
        /// BPM data.
        /// </summary>
        public new OSCPolarBPM oscData { get { return (OSCPolarBPM)base.oscData; } }

        private void Awake()
        {
            base.oscData = OSCDataHandler.CreateOSCDataHandler<OSCPolarBPM>();
        }
        void Start()
        {
            _oscGameObject = GameObject.Find("OSCManager");
            _oscManager = _oscGameObject.GetComponent<OSC>();

            _oscManager.SetAddressHandler(id + base.oscData.OSCAddress, base.oscData.ToOSCDataHandler);
        }

        private void FixedUpdate()
        {
            Debug.Log(id + base.oscData.OSCAddress);
        }

    }

}