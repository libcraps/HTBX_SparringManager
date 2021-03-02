using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Device
{
    public class Movuino : Device
    {
        private GameObject _oscGameObject;

        /// <summary>
        /// Accelerometer data.
        /// </summary>
        private Vector3 _accelerometer;
        /// <summary>
        /// Gysocope data.
        /// </summary>
        private Vector3 _gyroscope;
        /// <summary>
        /// Magnetometer data.
        /// </summary>
        private Vector3 _magnetometer;

        private List<Vector3> _listAccelerometer;
        private List<Vector3> _listMagnetometer;
        private List<Vector3> _listGyroscope;

        

        private OSC _oscManager;
        private string _id;
        private void Awake()
        {
            _listAccelerometer = new List<Vector3>();
            _listGyroscope = new List<Vector3>();
            _listMagnetometer = new List<Vector3>();
        }
        void Start()
        {
            _oscGameObject = GameObject.Find("OSCManager");
            _oscManager = _oscGameObject.GetComponent<OSC>();

            _oscManager.SetAddressHandler(_id + "data", ToMovuinoData);
            _oscManager.SetAllMessageHandler(GetMessages);
            Debug.Log(_id + "data");
        }

        private void FixedUpdate()
        {
            StockData(_accelerometer, _gyroscope, _magnetometer);
        }

        private void OnDestroy()
        {
            
        }

        public void Init(string id)
        {
            _id = id;
        }
        private void GetMessages(OscMessage message)
        {
            Debug.Log(message.address);
            Debug.Log(message.values);
        }
        public override void ToMovuinoData(OscMessage message)
        {
            float ax = message.GetFloat(0);
            float ay = message.GetFloat(1);
            float az = message.GetFloat(2);
            float gx = message.GetFloat(3);
            float gy = message.GetFloat(4);
            float gz = message.GetFloat(5);
            float mx = message.GetFloat(6);
            float my = message.GetFloat(7);
            float mz = message.GetFloat(8);
            _accelerometer = new Vector3(ax, ay, az);
            _gyroscope = new Vector3(gx, gy, gz);
            _magnetometer = new Vector3(mx, my, mz);
        }
        private void StockData(Vector3 accleration, Vector3 gyroscope, Vector3 magnetometre)
        {
            _listAccelerometer.Add(accleration);
            _listGyroscope.Add(gyroscope);
            _listMagnetometer.Add(magnetometre);
        }


        public override string ToString()
        {
            return string.Format("[MovuinoSensorData] = "
            + "Accelerometer = "
            + _accelerometer.ToString()
            + " Gyroscope = "
            + _gyroscope.ToString()
            + " Magnetometer = "
            + _magnetometer.ToString());
        }


    }

}