using SparringManager.DataManager;
using SparringManager.Device;
using SparringManager.Scenarios;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Text;
using UnityEngine;

/// <summary>
/// Namespace relative to the scenario SplHitLine
/// </summary>
namespace SparringManager.SplHitLine
{

    /// <summary>
    /// Manage the scenario SplHitLine.
    /// </summary>
    /// <inheritdoc cref="ScenarioControllerBehaviour"/>
    public class SplHitLineController : ScenarioControllerBehaviour
    {
        #region Attributs
        //----------- ATTRIBUTS ----------------------
        private SplHitLineBehaviour scenarioBehaviour;
        protected override object consigne { get { return scenario.PosToAngle(rangeSize, scenarioBehaviour.transform.localPosition.x); } }

        #endregion

        //--------------------------    METHODS     ----------------------------------------
        // ---> General Methods
        protected override void Awake()
        {
            base.Awake();
            //INITIALISATION OF VARIABLES 
        }
        protected override void Start()
        {
            base.Start();
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
            scenarioBehaviour = go.GetComponent<SplHitLineBehaviour>();
            scenarioBehaviour.Init(scenario.structScenari);
            Destroy(go, scenario.timerScenario);
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate(); //Stock Data
            
            //Update the "situation" of the line
            
            //RandomizeParametersLineMovement(scenario.accelerationMax, scenario.deltaTimeMin, scenario.deltaTimeMax);
            hit = " ";

        }
        void OnDestroy()
        {
            dataManagerComponent.DataBase.Add(dataSessionPlayer.DataTable);
            dataManagerComponent.EndScenarioForData = true;

            GetComponentInParent<SessionManager>().EndScenario = true;

            reactTime = 0;
            scenarioBehaviour.Hitted = false;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        // ---> Methods that set variables
        public override void Init(GeneriqueScenarioStruct structScenari)
        {
            //Initialize this Class
            //Scenario controller
            scenario = new Scenario(structScenari);
            dataSessionPlayer = new DataSessionPlayer(nbMovuino);

            dataSessionPlayer.DataSessionScenario.scenarioSumUp = DataController.StructToDictionary<GeneriqueScenarioStruct>(scenario.structScenari);
            dataManagerComponent = GetComponentInParent<DataController>();
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataSessionPlayer.DataSessionScenario.scenarioSumUp); //Mettre dans 

        }

// ---> Method for an hitting object
        private void OnEnable()
        {
            ImpactManager.onInteractPoint += GetHit;
        }
        private void OnDisable()
        {
            ImpactManager.onInteractPoint -= GetHit;
        }

        /// <summary>
        /// Get the hit and react to it
        /// </summary>
        /// <remarks>It is called by the event ImpactManager.InteractPoint</remarks>
        /// <param name="position2d_">Position of the hit</param>
        public void GetHit(Vector2 position2d_)
        {
            RaycastHit hit;
            Vector3 rayCastOrigin = new Vector3(position2d_.x, position2d_.y, this.gameObject.transform.position.z);
            Vector3 rayCastDirection = new Vector3(0, 0, 1);

            bool rayOnTarget = Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, 250);
            bool canHit = (tTime > scenario.timeBeforeHit && (tTime - scenario.timeBeforeHit) < scenario.deltaHit);

            if (rayOnTarget && canHit && scenarioBehaviour.Hitted == false)
            {
                reactTime = tTime - scenario.timeBeforeHit;
                scenarioBehaviour.Hitted = true;
                this.hit = true;
                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + reactTime);
            }
        }

    }
}