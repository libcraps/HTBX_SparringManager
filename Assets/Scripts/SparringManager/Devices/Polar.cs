using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Device
{
    public class Polar : MonoBehaviour
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
        private List<float> _listBPM;
        public PolarBPM PolarBPM { get { return polarBPM; } }
        private string _id;
        private string _idDataBpm;

        private void Awake()
        {
            polarBPM = OSCDataHandler.CreateOSCDataHandler<PolarBPM>();

            _idDataBpm = _id + polarBPM.OSCAddress;
            _listBPM = new List<float>();
        }
        void Start()
        {
            _oscGameObject = GameObject.Find("OSCManager");
            _oscManager = _oscGameObject.GetComponent<OSC>();

            _oscManager.SetAddressHandler(_id + "bpm", polarBPM.ToOSCDataHandler);
        }

        private void FixedUpdate()
        {
        }

        public void Init(string id)
        {
            _id = id;
        }
    }

}