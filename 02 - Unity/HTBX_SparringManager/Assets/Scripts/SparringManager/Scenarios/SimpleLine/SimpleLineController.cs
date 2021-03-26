using SparringManager.DataManager;
using SparringManager.Device;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Scenarios
{
    /* Class nof the SimpleLine Scenario Controller
     * 
     *  Summary :
     *  This class manage the behaviour of the SimpleLine prefab, and the data handling of the scenario.
     *  
     *  Attributs :
     *      public ScenarioSimpleLine scenario { get; set; } : scenario object inherited from Scenario<Hitline>
     *      public SimpleLineBehaviour scenarioBehaviour : HitlineBehaviour componnent of the scenario
     *      protected override float startTimeScenario : startTime of the scenario;
     *      protected override object consigne : get the consigne of the session
     *      
     *  Methods :
     *      protected virtual void Awake() : Unity Function launch a the instance of the script     -> base.Awake()
     *      protected virtual void Start() : Unity Function launch for the firts frame              -> base.Start(), instantiation of the behaviour
     *      protected virtual void FixedUpdate() : Unity Function called at each phyical iteration  -> base.FixedUpdate(), RandomizeLineMovement() ?
     *      public virtual void Init(StructScenarios structScenarios) : Function that is called after the instantiation of the scenario controller, it initialised parameters of the scenario
     *      void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax) : Randomize the movement of the cross
     *      
     */


    /// <summary>
    /// Manage the scenario SimpleLine
    /// </summary>
    public class SimpleLineController : ScenarioControllerBehaviour
    {
        #region Attributs
        //----------- ATTRIBUTS ----------------------
        //Usefull parameters of the scenario, they are in the SimpleLineStructure
        //Object that contain datas (structures)
        public ScenarioSimpleLine scenario { get; set; }
        public SimpleLineBehaviour scenarioBehaviour;

        protected override float startTimeScenario { get { return scenario.startTimeScenario; } set { scenario.startTimeScenario = value; } }
        protected override object consigne { get { return scenario.PosToAngle(rangeSize, scenarioBehaviour.transform.localPosition.x); }}
        #endregion

        #region Methods
        //------------ METHODS -------------------
        //General Methods
        protected override void Awake()
        {
            base.Awake();
            hit = " ";
        }
        protected override void Start()
        {
            GetDevices();
            //Initialisation of the time
            startTimeScenario = Time.time;
            tTime = Time.time - startTimeScenario;
            previousTime = tTime;

            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            var go = Instantiate(_prefabScenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform);
            scenarioBehaviour = go.GetComponent<SimpleLineBehaviour>();
            scenarioBehaviour.Init(scenario.structScenario);
            Destroy(go, scenario.timerScenario);

            scenarioBehaviour.LineVelocity = scenario.accelerationMax;
            Debug.Log(this.gameObject.name + " for " + scenario.timerScenario + " seconds");
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate(); //StockData

            //Behaviour Management
            //RandomizeParametersLineMovement(scenario.accelerationMax, scenario.deltaTimeMin, scenario.deltaTimeMax);
        }
        void OnDestroy()
        {
            dataManagerComponent.DataBase.Add(dataSessionPlayer.DataTable);
            dataManagerComponent.EndScenarioForData = true;
            GetComponentInParent<SessionManager>().EndScenario = true;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }
        //Methods that set variables
        public override void Init(StructScenarios structScenarios)
        {
            //Initialize this Class
            //Scenario controller
            scenario = Scenario<SimpleLineStruct>.CreateScenarioObject<ScenarioSimpleLine>();
            scenario.Init(structScenarios);

            dataSessionPlayer = new DataSessionPlayer(NbMovuino);
            dataSessionPlayer.DataSessionScenario.scenarioSumUp = DataController.StructToDictionary<SimpleLineStruct>(scenario.structScenario);
            dataManagerComponent = GetComponentInParent<DataController>();
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataSessionPlayer.DataSessionScenario.scenarioSumUp); //Mettre dans 
        }

        //Method that changes parameters of a moving object
        public void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax)
        {
            System.Random random = new System.Random();
            //Randomize the movement of the line every deltaTime seconds
            if ((tTime - previousTime) > scenarioBehaviour.DeltaTimeChangeVelocity)
            {
                scenarioBehaviour.LineVelocity = random.Next(-accelerationMax, accelerationMax);
                scenarioBehaviour.DeltaTimeChangeVelocity = random.Next(deltaTimeMin, deltaTimeMax);

                previousTime = tTime;
            }
        }
        #endregion
    }
}