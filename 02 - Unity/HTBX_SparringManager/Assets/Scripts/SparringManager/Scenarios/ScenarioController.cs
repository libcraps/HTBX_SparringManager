using System.Collections;
using System.Collections.Generic;
using SparringManager.Data;
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
    public abstract class ScenarioController : MonoBehaviour
    {
        #region Attributs
        [SerializeField]
        protected GameObject _prefabScenarioComposant;

        protected GameObject playerPrefab;


        #region PlayerScene
        /// <summary>
        /// PlayerSceneController component of the playerScene
        /// </summary>
        protected PlayerSceneController playerSceneController;

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
        #endregion



        /// <summary>
        /// Scenario Object
        /// </summary>
        protected Scenario scenario;


        protected ScenarioDisplayBehaviour scenarioBehaviour;


        /// <summary>
        /// Arc where the hitbox is operational (from the center to +/- operationalArc/2)
        /// </summary>
        protected int operationalArea;
        /// <summary>
        /// number of apparition of the scenario
        /// </summary>
        protected static int nbApparition;

        /// <summary>
        /// Stock all the data of the scenario (movuinos, consigne, player mouvement, hit...etc) cf doc of DataSessionPlayerq
        /// </summary>
        protected DataSessionPlayer dataSessionPlayer;

        /// <summary>
        /// Component DataManager of the PlayerScene object
        /// </summary>
        protected DataManager dataManagerComponent;



        /// <summary>
        /// RenderCamera
        /// </summary>
        protected GameObject _renderCameraObject;

        


        /// <summary>
        /// range of the render camera
        /// </summary>
        protected float _rangeSize;

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
        #endregion

        #region Properties
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

        public ScenarioDisplayBehaviour scenarioBehavior
        {
            get { return scenarioBehaviour; }
        }

        public Scenario Scenario { get { return scenario; } }

        /// <summary>
        /// ImpactManagerComponent of the renderCamera
        /// </summary>
        public ImpactManager renderCameraIM { get { return _renderCameraObject.GetComponent<ImpactManager>(); } }

        /// <summary>
        /// hit variable, True if hitted, " " if not
        /// </summary>
        protected virtual object hitDataValue
        {
            get
            {
                if (scenarioBehaviour.hitted == true)
                {
                    return true;
                }
                else
                {
                    return " ";
                }
            }
        }

        /// <value>Start time of the scenario</value>
        public float startTimeScenario { get { return Scenario.startTimeScenario; } }

        /// <value>Get the consigne of the scenario</value>
        public abstract float consigne { get; }

        public float rangeSize { get { return _rangeSize; } }
        public GameObject renderCameraObject { get { return _renderCameraObject; } }

        #endregion

        #region Methods

        #region Unity implemented methods
        /// <summary>
        /// Initialize some variables
        /// </summary>
        protected virtual void Awake()
        {
            playerPrefab = this.gameObject.transform.parent.gameObject;
            playerSceneController = playerPrefab.GetComponent<DeviceManager>().PlayerScene.GetComponent<PlayerSceneController>();

            operationalArea = playerPrefab.GetComponent<SessionManager>().OperationalArea;
            nbMovuino = playerSceneController.NbMovuino;
            _renderCameraObject = playerPrefab.GetComponent<DeviceManager>().RenderCamera;
            _rangeSize = _renderCameraObject.GetComponent<Camera>().orthographicSize;
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
        }
        protected virtual void OnDestroy()
        {
            dataManagerComponent.DataBase.Add(dataSessionPlayer.DataTable);
            dataManagerComponent.EndScenarioForData = true;

            playerPrefab.GetComponent<SessionManager>().EndScenario = true;

            Debug.Log(this.gameObject.name + "has been destroyed");
        }
        #endregion



        /// <summary>
        /// Initialize parameters of the scenario.
        /// </summary>
        /// <para>Generaly it initializes :
        /// <list type="ParametersInit">
        /// <item><paramref name="scenario"/></item>
        /// <item><paramref name="dataSessionlayer"/></item>
        /// <item>And it has complete the DataManager</item>
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
            dataSessionPlayer.DataSessionScenario.scenarioSumUp = DataManager.StructToDictionary<GeneriqueScenarioStruct>(Scenario.structScenari);
            dataManagerComponent = GetComponentInParent<DataManager>();
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataSessionPlayer.DataSessionScenario.scenarioSumUp);
        }

        /// <summary>
        /// Search and get other devices in the scene
        /// </summary>
        protected virtual void GetDevices()
        {
            //movuino
            movuino = new Movuino[nbMovuino];
            for (int i = 0; i < nbMovuino; i++)
            {
                movuino[i] = playerSceneController.movuino[i].GetComponent<Movuino>();
                dataSessionPlayer.DataSessionMovuino[i].id = movuino[i].id; //We need the id of the movuino to have different column names and identifie thmen
                dataSessionPlayer.DataSessionMovuinoXMM[i].id = movuino[i].id;
            }

            //Polar
            polar = playerSceneController.polar.GetComponent<Polar>();

            //ViveTracker
            viveTrackerManager = playerSceneController.viveTracker.GetComponent<ViveTrackerManager>();
        }

        /// <summary>
        /// Stock Data in the DataSessionPlayer
        /// </summary>
        protected virtual void StockData()
        {
            dataSessionPlayer.DataSessionScenario.StockData(tTime, consigne);
            dataSessionPlayer.DataSessionViveTracker.StockData(tTime, viveTrackerManager.angle);
            dataSessionPlayer.DataSessionHit.StockData(tTime, hitDataValue);
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

