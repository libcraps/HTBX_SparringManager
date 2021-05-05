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

        protected ScenarioBehaviourBase scenarioBehaviour;

        /// <summary>
        /// RenderCamera
        /// </summary>
        protected GameObject _renderCameraObject;

        /// <summary>
        /// Scenario Object
        /// </summary>
        protected Scenario scenario;

        /// <summary>
        /// number of apparition of the scenario
        /// </summary>
        protected static int nbApparition;

        #region PlayerPrefab
        /// <summary>
        /// PlayerPrefab 
        /// </summary>
        protected GameObject playerPrefab;

        /// <summary>
        /// Stock all the data of the scenario (movuinos, consigne, player mouvement, hit...etc) cf doc of DataSessionPlayerq
        /// </summary>
        protected DataSessionPlayer dataSessionPlayer;

        /// <summary>
        /// Component DataManager of the PlayerPrefab object
        /// </summary>
        protected DataManager dataManagerComponent;

        /// <summary>
        /// Component DeviceManager of the PlayerPrefab Object
        /// </summary>
        protected DeviceManager deviceManagerComponent;

        /// <summary>
        /// Component SessionManager of the PlayerPrefab Object
        /// </summary>
        protected SessionManager sessionManagerComponent;
        #endregion



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
            scenario = new Scenario(structScenarios);

            //Device
            playerPrefab = this.gameObject.transform.parent.gameObject;
            deviceManagerComponent = playerPrefab.GetComponent<DeviceManager>();
            sessionManagerComponent = playerPrefab.GetComponent<SessionManager>();
            dataManagerComponent = playerPrefab.GetComponent< DataManager>();

            //PlayerScene
            playerSceneController = deviceManagerComponent.PlayerScene.GetComponent<PlayerSceneController>();

            //Data
            dataSessionPlayer = new DataSessionPlayer(nbMovuino);
            dataSessionPlayer.DataSessionScenario.scenarioSumUp = DataManager.StructToDictionary<GeneriqueScenarioStruct>(scenario.structScenari);
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataSessionPlayer.DataSessionScenario.scenarioSumUp);
        }


        /// <summary>
        /// Method to handle data
        /// </summary>
        public abstract void StockData();
        #endregion
    }

}