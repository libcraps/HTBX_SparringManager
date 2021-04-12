﻿using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using SparringManager.Device;
using UnityEngine;

/// <summary>
/// Namespace relative to hitbox's scenarios 
/// <para>It concerns all Scenrio controllers, scenraios<structSCenarios> object, scenario behaviour display</structSCenarios></para>
/// </summary>
namespace SparringManager.Scenarios
{

    /// <summary>
    /// Abstract class of a ScenarioController, each scenario controller will dispose this attributs and methods (public and protected) 
    /// </summary>
    public abstract class ScenarioControllerBehaviour : MonoBehaviour
    {
        #region Attributs
        [SerializeField]
        protected GameObject _prefabScenarioComposant;

        /// <summary>
        /// Composant of the scenario that have the scenario behaviour
        /// </summary>
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
        /// <summary>
        /// Scenario Object
        /// </summary>
        protected Scenario scenario;

        public Scenario Scenario { get { return scenario;  } }

        protected ScenarioDisplayBehaviour scenarioBehaviour;
        /// <summary>
        /// Arc where the hitbox is operational (from the center to +/- operationalArc/2)
        /// </summary>
        protected int operationalArea;
        /// <summary>
        /// number of apparition of the scenario
        /// </summary>
        protected static int nbApparition;
        //Data

        /// <summary>
        /// Stock all the data of the scenario (movuinos, consigne, player mouvement, hit...etc) cf doc of DataSessionPlayerq
        /// </summary>
        protected DataSessionPlayer dataSessionPlayer;

        /// <summary>
        /// Component DataController of the PlayerScene object
        /// </summary>
        protected DataController dataManagerComponent;

        //Devices
        /// <summary>
        /// Movuinos present in the scene for the scenario
        /// </summary>
        protected Movuino[] movuino;

        /// <summary>
        /// polar presents in the scene for the scenario
        /// </summary>
        protected Polar polar;

        /// <summary>
        /// Manage all vivetrackers present in the scene for the scenario
        /// </summary>
        protected ViveTrackerManager viveTrackerManager;

        /// <summary>
        /// Number of movuinos
        /// </summary>
        protected int nbMovuino;

        /// <summary>
        /// RenderCamera
        /// </summary>
        protected GameObject renderCameraObject;

        /// <summary>
        /// range of the render camera
        /// </summary>
        protected float rangeSize;

        //time
        /// <summary>
        /// time variable where we stock the previous time
        /// </summary>
        protected float previousTime;

        /// <summary>
        /// tTime
        /// </summary>
        protected float tTime;

        /// <summary>
        /// reactionTime of the player
        /// </summary>
        protected float reactTime;

        /// <summary>
        /// hit variable, True if hitted, " " if not
        /// </summary>
        protected object hit;

        /// <value>Start time of the scenario</value>
        public float startTimeScenario { get { return Scenario.startTimeScenario; } }

        /// <value>Get the consigne of the scenario</value>
        protected abstract object consigne { get; }

        public float RangeSize { get { return rangeSize; } }
        public GameObject RenderCameraObject { get { return renderCameraObject; } }
        #endregion

        #region Methods

        #region Unity implemented Methods
        /// <summary>
        /// Initialize some variables
        /// </summary>
        protected virtual void Awake()
        {
            operationalArea = this.gameObject.GetComponentInParent<SessionManager>().OperationalArea;
            nbMovuino = this.gameObject.GetComponentInParent<DeviceManager>().NbMovuino;
            renderCameraObject = this.gameObject.GetComponentInParent<DeviceManager>().RenderCamera;
            rangeSize = renderCameraObject.GetComponent<Camera>().orthographicSize;
            nbApparition += 1;
        }
        protected virtual void Start()
        {
            GetDevices();
            //Initialisation of the time and the acceleration
            tTime = Time.time - startTimeScenario;
            previousTime = tTime;

            //Instantiation BehaviourDisplay
            Vector3 _pos3d = new Vector3();
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            var go = Instantiate(_prefabScenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform);
            scenarioBehaviour = go.GetComponent<ScenarioDisplayBehaviour>();
            scenarioBehaviour.Init(Scenario.structScenari);
            Destroy(go, Scenario.timerScenario);
        }
        protected virtual void FixedUpdate()
        {
            tTime = Time.time - startTimeScenario;

            //Data management
            StockData();
            hit = " ";
        }
        protected virtual void OnDestroy()
        {
            dataManagerComponent.DataBase.Add(dataSessionPlayer.DataTable);
            dataManagerComponent.EndScenarioForData = true;

            GetComponentInParent<SessionManager>().EndScenario = true;

            reactTime = 0;
            scenarioBehaviour.hitted = false;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }
        #endregion

        #region Hitting Methods
        protected virtual void OnEnable()
        {
            ImpactManager.onInteractPoint += GetHit;
        }
        protected virtual void OnDisable()
        {
            ImpactManager.onInteractPoint -= GetHit;
        }
        /// <summary>
        /// Get the hit of the player
        /// </summary>
        /// <param name="position2d_">Position of the hit</param>
        protected virtual void GetHit(Vector2 position2d_)
        {
            RaycastHit hit;
            Vector3 rayCastOrigin = new Vector3(position2d_.x, position2d_.y, this.gameObject.transform.position.z);
            Vector3 rayCastDirection = new Vector3(0, 0, 1);

            bool rayOnTarget = Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, 250);
            bool canHit = (tTime > Scenario.timeBeforeHit && (tTime - Scenario.timeBeforeHit) < Scenario.deltaHit);

            if (rayOnTarget && canHit && scenarioBehaviour.hitted == false)
            {
                reactTime = tTime - Scenario.timeBeforeHit;
                scenarioBehaviour.hitted = true;
                this.hit = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + reactTime);
            }
        }
        #endregion

        /// <summary>
        /// Initialize parameters of the scenario.
        /// </summary>
        /// <para>Generaly it initializes :
        /// <list type="ParametersInit">
        /// <item><paramref name="scenario"/></item>
        /// <item><paramref name="dataSessionlayer"/></item>
        /// <item>And it has complete the DataController</item>
        /// </list></para>
        /// <remarks>It is called after his instantiation.</remarks>
        /// <param name="structScenarios">Structure that parameterize different settings of a scenario</param>
        public virtual void Init(GeneriqueScenarioStruct structScenarios)
        {
            //Initialize this Class
            //Scenario controller
            scenario = new Scenario(structScenarios);
            //Data
            dataSessionPlayer = new DataSessionPlayer(nbMovuino);
            dataSessionPlayer.DataSessionScenario.scenarioSumUp = DataController.StructToDictionary<GeneriqueScenarioStruct>(Scenario.structScenari);
            dataManagerComponent = GetComponentInParent<DataController>();
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataSessionPlayer.DataSessionScenario.scenarioSumUp);
        }

        /// <summary>
        /// Search and get other devices in the scene
        /// </summary>
        protected virtual void GetDevices()
        {
            /*
             * Search other devices in the scene
             */

            //movuino
            movuino = new Movuino[nbMovuino];
            for (int i = 0; i < nbMovuino; i++)
            {
                movuino[i] = GameObject.FindGameObjectsWithTag("Movuino")[i].GetComponent<Movuino>();
                dataSessionPlayer.DataSessionMovuino[i].id = movuino[i].id; //We need the id of the movuino to have different column names and identifie thmen
                dataSessionPlayer.DataSessionMovuinoXMM[i].id = movuino[i].id;
            }

            //Polar
            polar = GameObject.FindGameObjectWithTag("Polar").GetComponent<Polar>();

            //ViveTracker
            viveTrackerManager = GameObject.Find("ViveTrackerManager(Clone)").GetComponent<ViveTrackerManager>();
        }

        /// <summary>
        /// Stock Data in the DataSessionPlayer
        /// </summary>
        protected virtual void StockData()
        {
            /*
             * Stock Data in the DataSessionPlayer
             */

            dataSessionPlayer.DataSessionScenario.StockData(tTime, consigne);
            dataSessionPlayer.DataSessionViveTracker.StockData(tTime, viveTrackerManager.angle);
            dataSessionPlayer.DataSessionHit.StockData(tTime, hit);
            dataSessionPlayer.DataSessionPolar.StockData(tTime, polar.oscData.bpm);
            for (int i = 0; i < nbMovuino; i++)
            {
                dataSessionPlayer.DataSessionMovuino[i].StockData(tTime, movuino[i].MovuinoSensorData.accelerometer, movuino[i].MovuinoSensorData.gyroscope, movuino[i].MovuinoSensorData.magnetometer);
                dataSessionPlayer.DataSessionMovuinoXMM[i].StockData(tTime, movuino[i].MovuinoXMM.gestId, movuino[i].MovuinoXMM.gestProg);
            }
        }



        #endregion

    }

}

