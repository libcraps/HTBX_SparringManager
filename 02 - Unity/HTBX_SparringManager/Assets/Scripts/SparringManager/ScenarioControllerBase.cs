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
        [SerializeField]
        protected GameObject _prefabScenarioComposant;

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
        /// RenderCamera
        /// </summary>
        protected GameObject _renderCameraObject;

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

        public abstract void Init(GeneriqueScenarioStruct structScenarios);

        public abstract void StockData();
    }

}