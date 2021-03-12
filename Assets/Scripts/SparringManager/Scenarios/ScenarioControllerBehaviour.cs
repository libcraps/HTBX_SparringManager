using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using SparringManager.Device;
using UnityEngine;

namespace SparringManager.Scenarios
{
    public abstract class ScenarioControllerBehaviour : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _prefabScenarioComposant;
        public virtual GameObject PrefabScenarioComposant
        {
            get
            {
                return _prefabScenarioComposant;
            }
            set
            {
                _prefabScenarioComposant = value;
            }
        }
        //Scenario
        //TOCHECK
        public static int nbApparition;
        //Data
        protected DataSessionPlayer dataSessionPlayer;
        protected DataController dataManagerComponent;

        //Devices
        protected Movuino[] movuino;
        protected Polar polar;
        protected ViveTrackerManager viveTrackerManager;
        protected int NbMovuino;
        protected GameObject cameraObject;
        protected float rangeSize;
        //time
        protected float previousTime;
        protected float tTime;
        protected float reactTime;

        protected object hit;
        protected abstract float startTimeScenario { get; set; }
        protected abstract object consigne { get;}

        protected virtual void Awake()
        {
            NbMovuino = this.gameObject.GetComponentInParent<DeviceManager>().NbMovuino;
            cameraObject = this.gameObject.transform.GetComponentInParent<DeviceManager>().RenderCamera;
            rangeSize = cameraObject.GetComponent<Camera>().orthographicSize;
            nbApparition += 1;
        }

        protected virtual void Start()
        {
            Debug.Log("------------" + " ScenarioControllerBehaviour Start" + "---------------");
            
            //Initialisation of the time and the acceleration
            startTimeScenario = Time.time;
            previousTime = 0;


        }
        protected virtual void FixedUpdate()
        {
            tTime = Time.time - startTimeScenario;

            //Data management
            dataSessionPlayer.DataSessionScenario.StockData(tTime, consigne);
            dataSessionPlayer.DataSessionViveTracker.StockData(tTime, viveTrackerManager.angle);
            dataSessionPlayer.DataSessionHit.StockData(tTime, hit);
            dataSessionPlayer.DataSessionPolar.StockData(polar.polarBPM.bpm);//test angle
            for (int i = 0; i < NbMovuino; i++)
            {
                dataSessionPlayer.DataSessionMovuino[i].StockData(tTime, movuino[i].MovuinoSensorData.accelerometer, movuino[i].MovuinoSensorData.gyroscope, movuino[i].MovuinoSensorData.magnetometer);
            }
        }
        public virtual void Init(StructScenarios structScenarios)
        {
        }
        protected virtual void GetDevices()
        {
            //Search other devices in the scene
            //movuino Part
            
            movuino = new Movuino[NbMovuino]; //TODO know how many movuino we have
            for (int i = 0; i < NbMovuino; i++)
            {
                movuino[i] = GameObject.FindGameObjectsWithTag("Movuino")[i].GetComponent<Movuino>();
                dataSessionPlayer.DataSessionMovuino[i].id = movuino[i].id;
            }

            //Polar part
            polar = GameObject.FindGameObjectWithTag("Polar").GetComponent<Polar>();
            //ViveTrackerPart
            viveTrackerManager = GameObject.Find("ViveTrackerManager(Clone)").GetComponent<ViveTrackerManager>();
        }

        protected virtual void StockData()
        {

        }

    }
}

