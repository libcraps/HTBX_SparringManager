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

        private int _gesteId;
        private float _progression;

        private List<int> _listGesture;
        private List<float> _listProgression;

        private string _addressData;
        private string _addressGesture;

        private OSC _oscManager;
        private string _id;
        private void Awake()
        {
            _listAccelerometer = new List<Vector3>();
            _listGyroscope = new List<Vector3>();
            _listMagnetometer = new List<Vector3>();

            _listGesture = new List<int>();
            _listProgression = new List<float>();
    }
        void Start()
        {
            _addressData = _id + "data";
            _addressGesture = _id + "gesture";

            _oscGameObject = GameObject.Find("OSCManager");
            _oscManager = _oscGameObject.GetComponent<OSC>();

            _oscManager.SetAddressHandler(_addressData, ToMovuinoData); 
            _oscManager.SetAddressHandler(_addressGesture, ToMovuinoData);
            _oscManager.SetAllMessageHandler(GetMessages);
        }

        private void FixedUpdate()
        {
            
        }

        private void OnDestroy()
        {
            Debug.Log("Data size : " + _listAccelerometer.Count);
            Debug.Log("Data Gesture size : " + _listGesture.Count);
        }

        public void Init(string id)
        {
            _id = id;
        }
        private void GetMessages(OscMessage message)
        {
            Debug.Log(message.address);
        }
        public override void ToMovuinoData(OscMessage message)
        {
            string key = message.address;
            
            if (key == _addressData)
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
                StockData(_accelerometer, _gyroscope, _magnetometer);

            }
            else if (key == _addressGesture)
            {
                _gesteId = message.GetInt(0);
                _progression = message.GetFloat(1);
                StockDataGesture(_gesteId, _progression);
                Debug.Log("Gesture : " + _gesteId + " " + _progression);
            }
            Debug.Log(key == _addressData);
            Debug.Log(key == _addressGesture);
        }

        private void StockData(Vector3 accleration, Vector3 gyroscope, Vector3 magnetometre)
        {
            _listAccelerometer.Add(accleration);
            _listGyroscope.Add(gyroscope);
            _listMagnetometer.Add(magnetometre);
        }

        private void StockDataGesture(int idGesture, float progression)
        {
            _listGesture.Add(idGesture);
            _listProgression.Add(progression);
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