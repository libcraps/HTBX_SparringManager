using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SparringManager.Data;
using SparringManager.Device;


namespace SparringManager.Scenarios
{
    /// <summary>
    /// Abstract class that represents a scenario
    /// </summary>
    public abstract class ScenarioControllerBase : MonoBehaviour
    {
        #region Attributs
        /// <summary>
        /// Prefab of the scenario that is instantiated
        /// </summary>
        [SerializeField] protected GameObject _prefabScenarioComposant;

        /// <summary>
        /// ScenarioBehaviour of this scenario
        /// </summary>
        protected ScenarioBehaviourBase _scenarioBehaviour;

        /// <summary>
        /// tTime
        /// </summary>
        protected float tTime;

        /// <summary>
        /// RenderCamera
        /// </summary>
        protected GameObject _renderCameraObject;

        /// <summary>
        /// Range of the RenderCamera.
        /// </summary>
        protected float _rangeSize;

        /// <summary>
        /// Scenario Object
        /// </summary>
        protected Scenario _scenario;

        /// <summary>
        /// number of apparition of the scenario
        /// </summary>
        protected static int _nbApparition;

        #region PlayerPrefab
        /// <summary>
        /// PlayerPrefab 
        /// </summary>
        protected GameObject _playerPrefab;

        /// <summary>
        /// Stock all the data of the scenario (movuinos, consigne, player mouvement, hit...etc) cf doc of DataSessionPlayerq
        /// </summary>
        protected DataSessionPlayer dataSessionPlayer;

        /// <summary>
        /// Component DataManager of the PlayerPrefab object
        /// </summary>
        protected DataManager _dataManagerComponent;

        /// <summary>
        /// Component DeviceManager of the PlayerPrefab Object
        /// </summary>
        protected DeviceManager _deviceManagerComponent;

        /// <summary>
        /// Component SessionManager of the PlayerPrefab Object
        /// </summary>
        protected SessionManager _sessionManagerComponent;
        #endregion



        #region PlayerScene
        /// <summary>
        /// PlayerSceneController component of the playerScene
        /// </summary>
        protected PlayerSceneController _playerSceneController;

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

        #endregion

        #region Properties
        public GameObject renderCameraObject { get { return _renderCameraObject;  } }

        public float rangeSize { get { return _rangeSize;  } }
        public Scenario scenario { get { return _scenario; } }

        /// <summary>
        /// Component DataManager of the PlayerPrefab object
        /// </summary>
        public DataManager dataManagerComponent { get { return _dataManagerComponent; } }

        /// <summary>
        /// Component DeviceManager of the PlayerPrefab Object
        /// </summary>
        public DeviceManager deviceManagerComponent { get { return _deviceManagerComponent; } }

        /// <summary>
        /// Component SessionManager of the PlayerPrefab Object
        /// </summary>
        public SessionManager sessionManagerComponent { get { return _sessionManagerComponent;  } }

        /// <summary>
        /// ScenarioBehaviour of the scenario
        /// </summary>
        public ScenarioBehaviourBase scenarioBehaviour { get { return _scenarioBehaviour;  } }


        #endregion

        #region Methods

        /// <summary>
        /// Method that inits the scenario controller
        /// </summary>
        /// <param name="structScenarios"></param>
        public virtual void Init(GeneriqueScenarioStruct structScenarios)
        {
            _nbApparition += 1;
            //Initialize this Class
            //Scenario controller
            _scenario = new Scenario(structScenarios);

            //Device
            _playerPrefab = transform.parent.gameObject;
            _deviceManagerComponent = _playerPrefab.GetComponent<DeviceManager>();
            _sessionManagerComponent = _playerPrefab.GetComponent<SessionManager>();
            _dataManagerComponent = _playerPrefab.GetComponent< DataManager>();

            //Camera
            _renderCameraObject = _deviceManagerComponent.RenderCamera;
            _rangeSize = _renderCameraObject.GetComponent<Camera>().orthographicSize;

            //PlayerScene
            _playerSceneController = _deviceManagerComponent.PlayerScene.GetComponent<PlayerSceneController>();
            nbMovuino = _playerSceneController.movuino.Length;

            //Data
            dataSessionPlayer = new DataSessionPlayer(nbMovuino);
            dataSessionPlayer.DataSessionScenario.scenarioSumUp = DataManager.StructToDictionary<GeneriqueScenarioStruct>(scenario.structScenari);
            _dataManagerComponent.AddContentToSumUp(this.name + "_" + _nbApparition, dataSessionPlayer.DataSessionScenario.scenarioSumUp);
        }

        /// <summary>
        /// Method to handle data
        /// </summary>
        protected abstract void StockData();

        /// <summary>
        /// Search and get other devices in the scene
        /// </summary>
        protected virtual void GetDevices()
        {
            //movuino
            movuino = new Movuino[nbMovuino];
            for (int i = 0; i < nbMovuino; i++)
            {
                movuino[i] = _playerSceneController.movuino[i].GetComponent<Movuino>();
                dataSessionPlayer.DataSessionMovuino[i].id = movuino[i].id; //We need the id of the movuino to have different column names and identifie thmen
                dataSessionPlayer.DataSessionMovuinoXMM[i].id = movuino[i].id;
            }

            //Polar
            polar = _playerSceneController.polar.GetComponent<Polar>();

            //ViveTracker
            viveTrackerManager = _playerSceneController.viveTracker.GetComponent<ViveTrackerManager>();
        }
        #endregion
    }

}