using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using SparringManager.Device;
using UnityEngine;

namespace SparringManager.Scenarios
{
    /*
     *  Abstract class of a ScenarioController, each scenario controller will dispose this attributs and methods (public and protected) 
     *  Attributs :
     *      protected GameObject _prefabScenarioComposant : Composant of the scenario that have the scenario behaviour
     *      protected int operationalArea : Arc where the hitbox is operational (from the center to +/- operationalArc/2)
     *      protected static int nbApparition : number of apparition of the scenario
     *      protected DataSessionPlayer dataSessionPlayer : Stock all the data of the scenario (movuinos, consigne, player mouvement, hit...etc) cf doc of DataSessionPlayer
     *      protected DataController dataManagerComponent : Component DataController of the PlayerScene object
     *      protected Movuino[] movuino : Movuinos present in the scene for the scenario
     *      protected Polar polar : polar presents in the scene for the scenario
     *      protected ViveTrackerManager viveTrackerManager : Manage all vivetrackers present in the scene for the scenario
     *      protected int NbMovuino : Numberof movuinos
     *      protected GameObject cameraObject : RenderCamera
     *      protected float rangeSize : range of the render camera
     *      protected float previousTime : time variable where we stock the previous time
     *      protected float tTime : tTime
     *      protected float reactTime : reactionTime of the player
     *      protected object hit : hit variable, True if hitted, " " if not
     *      protected abstract float startTimeScenario { get; set; } : startTimeScenario
     *      protected abstract object consigne { get; } : getter to have the right format of the consigne 
     *  
     *  Methods : 
     *      protected virtual void Awake() : Unity Function launch a the instance of the script     -> Search of every component we need 
     *      protected virtual void Start() : Unity Function launch for the firts frame              -> time variables initialisation
     *      protected virtual void FixedUpdate() : Unity Function called at each phyical iteration -> StockData() is called, tTime is updated
     *      public virtual void Init(StructScenarios structScenarios) : Function that is called after the instantiation of the scenario controller, it initialised parameters of the scenario
     *      protected virtual void GetDevices() : Search other devices in the scene -> movuinos, poler, vive etc...
     *      protected virtual void StockData() : Stock Data in the DataSessionPlayer
     */

    /// <summary>
    /// Abstract class of a ScenarioController, each scenario controller will dispose this attributs and methods (public and protected) 
    /// </summary>
    public abstract class ScenarioControllerBehaviour : MonoBehaviour
    {
        #region Attributs
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
        protected int operationalArea;
        protected static int nbApparition;
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
        protected abstract object consigne { get; }
        #endregion

        #region Methods
        protected virtual void Awake()
        {
            operationalArea = this.gameObject.GetComponentInParent<SessionManager>().OperationalArea;
            NbMovuino = this.gameObject.GetComponentInParent<DeviceManager>().NbMovuino;
            cameraObject = this.gameObject.GetComponentInParent<DeviceManager>().RenderCamera;
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
            StockData();
        }
        public virtual void Init(StructScenarios structScenarios)
        {
        }
        protected virtual void GetDevices()
        {
            /*
             * Search other devices in the scene
             */

            //movuino
            movuino = new Movuino[NbMovuino];
            for (int i = 0; i < NbMovuino; i++)
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

        protected virtual void StockData()
        {

            /*
             * Stock Data in the dataessionPlayer
             */

            dataSessionPlayer.DataSessionScenario.StockData(tTime, consigne);
            dataSessionPlayer.DataSessionViveTracker.StockData(tTime, viveTrackerManager.angle);
            dataSessionPlayer.DataSessionHit.StockData(tTime, hit);
            dataSessionPlayer.DataSessionPolar.StockData(tTime, polar.polarBPM.bpm);
            for (int i = 0; i < NbMovuino; i++)
            {
                dataSessionPlayer.DataSessionMovuino[i].StockData(tTime, movuino[i].MovuinoSensorData.accelerometer, movuino[i].MovuinoSensorData.gyroscope, movuino[i].MovuinoSensorData.magnetometer);
                dataSessionPlayer.DataSessionMovuinoXMM[i].StockData(tTime, movuino[i].MovuinoXMM.gestId, movuino[i].MovuinoXMM.gestProg);
            }
        }
        #endregion

    }

}

