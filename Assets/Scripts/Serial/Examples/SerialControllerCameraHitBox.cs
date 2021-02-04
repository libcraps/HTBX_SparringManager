using System;
using System.IO.Ports;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SparringManager.Serial
{
    public class SerialControllerCameraHitBox : MonoBehaviour
    {
        [SerializeField]
        private SerialSettings _serialSettings;
        [SerializeField]
        private GameObject _serialControllerPrefab = null;
        private GameObject _serialControllers;

        public static int i = 0;

        public Vector3 acceleration
        {
            get
            {
                return _serialControllers.GetComponent<SerialTouchController>().acceleration;
            }
        }

        public const int touchControlGridRows = 24;
        public const int touchControlGridCols = 24;
        public const int ledControlGridRows = 30;
        public const int ledControlGridCols = 60;
        public const int baudRate = 38400;
        public const int readTimeout = 400;
        public const Handshake handshake = Handshake.None;

        private void Start()
        {
            i += 1;

            //test            
            GameObject cameraObject = GameObject.FindGameObjectWithTag("HitboxCamera");
            _serialSettings.PlayerCamera = cameraObject.GetComponent<Camera>();
            //_serialControllers = InitSerialController(_serialSettings, i);

        }

        public GameObject InitSerialController(SerialSettings serialSettings, int p)
        {
            var go = GameObject.Instantiate(_serialControllerPrefab, this.transform);
            try
            {
                go.name = "Serial Controller" + p;
                go.GetComponent<SerialLedController>().Init(p,
                    ledControlGridRows,
                    ledControlGridCols,
                    serialSettings.ledControlSerialPortName,
                    baudRate,
                    readTimeout,
                    handshake,
                    this.gameObject.GetComponent<Camera>()
                    );
                go.GetComponent<SerialTouchController>().Init(p,
                    touchControlGridRows,
                    touchControlGridCols,
                    serialSettings.touchControlSerialPortName,
                    baudRate,
                    readTimeout,
                    handshake,
                    this.gameObject.GetComponent<Camera>(),
                    serialSettings.impactThreshold,
                    serialSettings.delayOffHit,
                    serialSettings.displayDatapoints,
                    serialSettings.ignoreBlackBackground
                    );
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                Debug.Log("Connection aborted.");
                Destroy(go);
                go = null;
            }
            return go;
        }

        /// <summary>
        /// Plays the End Game animation.
        /// </summary>
        public void EndGame()
        {
            _serialControllers.GetComponent<SerialLedController>().EndGame();
        }

        /// <summary>
        /// Plays the Hit animation.
        /// </summary>
        public void Hit()
        {
            _serialControllers.GetComponent<SerialLedController>().Hit();
        }

        /// <summary>
        /// Plays the Display Grid animation.
        /// </summary>
        public void DisplayGrid()
        {
            _serialControllers.GetComponent<SerialLedController>().DisplayGrid();
        }

        /// <summary>
        /// Stop all animations and turn the leds off.
        /// </summary>
        public void ShutDown()
        {
            _serialControllers.GetComponent<SerialLedController>().ShutDown();
        }

        /// <summary>
        /// Plays the Screen Saver animation.
        /// </summary>
        public void ScreenSaver()
        {
            _serialControllers.GetComponent<SerialLedController>().ScreenSaver();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(SerialControllerCameraHitBox))]
    public class ExampleSerialControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (Application.isPlaying)
            {
                var exampleController = target as SerialControllerCameraHitBox;
                EditorGUILayout.LabelField("Send Messages", EditorStyles.centeredGreyMiniLabel);
                if (GUILayout.Button("End Game"))
                    exampleController.EndGame();
                if (GUILayout.Button("Hit"))
                    exampleController.Hit();
                if (GUILayout.Button("Display Grid"))
                    exampleController.DisplayGrid();
                if (GUILayout.Button("Shut Down"))
                    exampleController.ShutDown();
                if (GUILayout.Button("Screen Saver"))
                    exampleController.ScreenSaver();
            }
        }
    }
#endif
}
