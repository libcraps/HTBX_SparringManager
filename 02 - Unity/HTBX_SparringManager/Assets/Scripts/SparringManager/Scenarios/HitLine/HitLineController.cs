using SparringManager.DataManager;
using SparringManager.Device;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Namespace relative to the scenario HitLine
/// </summary>
namespace SparringManager.Scenarios.HitLine
{
    /// <summary>
    /// Manage the scenario HitLine.
    /// </summary>
    public class HitLineController : ScenarioControllerBehaviour
    {
        #region Attributs
        //----------- ATTRIBUTS ---------------------- 

        //Scenario
        private HitLineBehaviour scenarioBehaviour;
        protected override object consigne { get { return scenario.PosToAngle(rangeSize, scenarioBehaviour.transform.localPosition.x); } }

        #endregion

        #region Methods
        //------------ METHODS -------------------
        //General Methods
        protected override void Awake()
        {
            base.Awake();
            //INITIALISATION OF VARIABLES 
        }
        protected override void Start()
        {
            base.Start();
            GetDevices();

            //Instantiation of scenario behaviour display
            Vector3 _pos3d = new Vector3();
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            var go = Instantiate(_prefabScenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform);
            scenarioBehaviour = go.GetComponent<HitLineBehaviour>();
            scenarioBehaviour.Init(scenario.structScenari);
            Destroy(go, scenario.timerScenario);

            Debug.Log(this.gameObject.name + " for " + scenario.timerScenario + " seconds");
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate(); //StockData and time update

            //Behaviour Management
            
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

        //Method that set variables.
        public override void Init(GeneriqueScenarioStruct structScenarios)
        {
            //Initialize this Class
            //Scenario controller
            scenario = new Scenario(structScenarios);
            //Data
            dataSessionPlayer = new DataSessionPlayer(nbMovuino);
            dataSessionPlayer.DataSessionScenario.scenarioSumUp = DataController.StructToDictionary<GeneriqueScenarioStruct>(scenario.structScenari);
            dataManagerComponent = GetComponentInParent<DataController>();
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataSessionPlayer.DataSessionScenario.scenarioSumUp);
        }

        //Method for an hitting object
        private void OnEnable()
        {
            ImpactManager.onInteractPoint += GetHit;
        }
        private void OnDisable()
        {
            ImpactManager.onInteractPoint -= GetHit;
        }
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
        #endregion

    }
}