using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Device
{
    public class Polar : MonoBehaviour
    {
        private GameObject _oscGameObject;
        private OSC _oscManager;

        /// <summary>
        /// BPM data.
        /// </summary>
        private PolarBPM polarBPM;
        private List<float> _listBPM;

        private string _id;
        private string _idDataBpm;

        private void Awake()
        {
            polarBPM = MovuinoData.CreateMovuinoData<PolarBPM>();

            _idDataBpm = _id + polarBPM.movuinoAddress;
            _listBPM = new List<float>();
        }
        void Start()
        {
            _oscGameObject = GameObject.Find("OSCManager");
            _oscManager = _oscGameObject.GetComponent<OSC>();

            _oscManager.SetAddressHandler(_id + "bpm", polarBPM.ToMovuinoData);
        }

        private void FixedUpdate()
        {
            StockData(polarBPM.bpm);
        }

        public void Init(string id)
        {
            _id = id;
        }

        private void StockData(float bpm)
        {
            _listBPM.Add(bpm);
        }
    }

}