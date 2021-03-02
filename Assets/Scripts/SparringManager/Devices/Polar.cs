using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Device
{
    public class Polar : Device
    {
        private GameObject _oscGameObject;

        /// <summary>
        /// BPM data.
        /// </summary>
        private float _bpm;

        private List<float> _listBPM;

        private OSC _oscManager;
        private string _id;
        private void Awake()
        {
            _listBPM = new List<float>();
        }
        void Start()
        {
            _oscGameObject = GameObject.Find("OSCManager");
            _oscManager = _oscGameObject.GetComponent<OSC>();

            _oscManager.SetAddressHandler(_id + "bpm", ToMovuinoData);
        }

        private void FixedUpdate()
        {
            StockData(_bpm);
        }

        public void Init(string id)
        {
            _id = id;
        }
        public override void ToMovuinoData(OscMessage message)
        {
            _bpm = message.GetFloat(0);
            Debug.Log("BPM : " + _bpm);
        }
        private void StockData(float bpm)
        {
            _listBPM.Add(bpm);
        }

        public override string ToString()
        {
            return string.Format("[PolarData] = "
            + "BPM = "
            + _bpm.ToString());
        }


    }

}