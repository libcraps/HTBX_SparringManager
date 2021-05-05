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
    /// Abstract class of a ScenarioController of a scenario line, each scenario controller will dispose this attributs and methods (public and protected) 
    /// </summary>
    /// <inheritdoc cref="ScenarioControllerBase"/>
    public abstract class LineScenarioController : ScenarioControllerBase
    {
        #region Attributs
        public new LineScenarioBehaviour scenarioBehaviour { get { return (LineScenarioBehaviour)_scenarioBehaviour;  } }

        /// <summary>
        /// tTime
        /// </summary>
        protected float tTime;

        #endregion

        #region Properties

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


        /// <value>Get the consigne of the scenario</value>
        public abstract float consigne { get; }
        #endregion

        #region Methods

        #region Unity implemented methods
        /// <summary>
        /// Default : Get Devices from the PlayerScene and Instatiation of the scenario
        /// </summary>
        protected virtual void Start()
        {
            //Devices 
            GetDevices();
            //Initialisation of the time and the acceleration
            tTime = Time.time - scenario.startTimeScenario;

            //Instantiation BehaviourDisplay
            Vector3 _pos3d = new Vector3();
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            var go = Instantiate(_prefabScenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform);
            _scenarioBehaviour = go.GetComponent<LineScenarioBehaviour>();
            _scenarioBehaviour.Init();
            Destroy(go, scenario.timerScenario);
        }

        /// <summary>
        /// Default LineScenario : update time and StockData
        /// </summary>
        protected virtual void FixedUpdate()
        {
            tTime = Time.time - scenario.startTimeScenario;

            StockData();
        }
        protected virtual void OnDestroy()
        {
            dataManagerComponent.DataBase.Add(dataSessionPlayer.DataTable);

            _playerPrefab.GetComponent<SessionManager>().EndScenario = true;

            Debug.Log(this.gameObject.name + "has been destroyed");
        }
        #endregion

        /// <summary>
        /// Stock Data in the DataSessionPlayer
        /// </summary>
        protected override void StockData()
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

