using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SparringManager.Data;
using SparringManager.Device;


namespace SparringManager.Scenarios
{
    public abstract class ScenarioControllerBase : MonoBehaviour
    {
        #region Attributs
        /// <summary>
        /// Prefab of the scenario that is instantiated
        /// </summary>
        [SerializeField]
        protected GameObject _prefabScenarioComposant;

        protected ScenarioBehaviourBase _scenarioBehaviour;

        /// <summary>
        /// RenderCamera
        /// </summary>
        protected GameObject _renderCameraObject;

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

        public Scenario scenario { get { return _scenario; } }

        #endregion

        #region Methods

        /// <summary>
        /// Method that inits the scenario controller
        /// </summary>
        /// <param name="structScenarios"></param>
        public virtual void Init(GeneriqueScenarioStruct structScenarios)
        {
            //Initialize this Class
            //Scenario controller
            _scenario = new Scenario(structScenarios);

            //Device
            _playerPrefab = this.gameObject.transform.parent.gameObject;
            _deviceManagerComponent = _playerPrefab.GetComponent<DeviceManager>();
            _sessionManagerComponent = _playerPrefab.GetComponent<SessionManager>();
            _dataManagerComponent = _playerPrefab.GetComponent< DataManager>();

            //PlayerScene
            _playerSceneController = _deviceManagerComponent.PlayerScene.GetComponent<PlayerSceneController>();

            //Data
            dataSessionPlayer = new DataSessionPlayer(nbMovuino);
            dataSessionPlayer.DataSessionScenario.scenarioSumUp = DataManager.StructToDictionary<GeneriqueScenarioStruct>(scenario.structScenari);
            _dataManagerComponent.AddContentToSumUp(this.name + "_" + _nbApparition, dataSessionPlayer.DataSessionScenario.scenarioSumUp);
        }


        /// <summary>
        /// Method to handle data
        /// </summary>
        public abstract void StockData();
        #endregion
    }

}