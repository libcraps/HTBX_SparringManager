using SparringManager.DataManager;
using SparringManager.Device;
using SparringManager.Structures;
using UnityEngine;

namespace SparringManager.Scenarios.CrossLine
{
    /* Class nof the CrossLine Scenario Controller
     * 
     *  Summary :
     *  This class manage the behaviour of the CrossLine prefab.
     *  The CrossLine moves lateraly and vertically and it instantiates an hit after _timeBeforeHit seconds
     *  And the player will have to hit it less than _deltaHit seconds.
     *  
     *  Methods :
     *      GameObject _scenarioComposant : Prefab of the line
     *      CrossLineBehaviour _crossLineComponent : SplHitLineBehaviour component of the prefab, it gives u acces ti different variable of the splHitLine Prefab
     *      
     *  Methods :
     *      //Method that change parameters of a moving object
     *      void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax) : Randomize the movement of the cross
     *      
     *      Method for an hitting object
     *      void GetHit(Vector2 position2d_) : Get the Hit of the position2d_ (use events)
     *      
     */


    /// <summary>
    /// Manage the scenario CrossLine.
    /// </summary>
    /// <inheritdoc cref="ScenarioControllerBehaviour"/>
    public class CrossLineController : ScenarioControllerBehaviour
    {
        #region Attributs
        //----------- ATTRIBUTS ----------------------
        //Scenario
        public ScenarioCrossLine scenario { get; set; }
        public CrossLineBehaviour scenarioBehaviour { get; set; }

        public override float startTimeScenario { get { return scenario.startTimeScenario; } set { scenario.startTimeScenario = value; } }
        protected override object consigne { get { return scenario.PosToAngle(rangeSize, scenarioBehaviour.transform.localPosition.x); } }
        #endregion

        #region Methods
        //------------ METHODS -------------------
        //General Methods
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            GetDevices();
            
            Debug.Log(NbMovuino);

            //Instantiation of scenario behaviour display
            Vector3 pos3d;
            pos3d.x = this.gameObject.transform.position.x;
            pos3d.y = this.gameObject.transform.position.y;
            pos3d.z = this.gameObject.transform.position.z + 100f;

            var go = Instantiate(PrefabScenarioComposant, pos3d, Quaternion.identity, this.gameObject.transform);
            scenarioBehaviour = go.GetComponent<CrossLineBehaviour>();
            scenarioBehaviour.Init(scenario.structScenario);
            Destroy(go, scenario.timerScenario);

            Debug.Log(this.gameObject.name + " for " + scenario.timerScenario + " seconds");

        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
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
            scenarioBehaviour.hitted = false;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        //Methods that set variables
        public override void Init(StructScenarios structScenarios)
        {
            //Initialize this Class
            //Scenario controller
            scenario = Scenario<CrossLineStruct>.CreateScenarioObject<ScenarioCrossLine>();
            scenario.Init(structScenarios);

            //StructPlayerSCene
            dataSessionPlayer = new DataSessionPlayer(NbMovuino);
            dataSessionPlayer.DataSessionScenario.scenarioSumUp = DataController.StructToDictionary<CrossLineStruct>(scenario.structScenario);
            dataManagerComponent = GetComponentInParent<DataController>();
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataSessionPlayer.DataSessionScenario.scenarioSumUp); //Mettre dans 

        }
        //Method that change parameters of a moving object
        void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax)
        {
            //Randomize the movement of the line every deltaTime seconds
            if ((tTime - previousTime) > scenarioBehaviour.DeltaTimeChangeMovement)
            {
                scenarioBehaviour.objectVelocity[0] = Random.Range(-accelerationMax, accelerationMax);
                scenarioBehaviour.objectVelocity[1] = Random.Range(-accelerationMax, accelerationMax);
                scenarioBehaviour.DeltaTimeChangeMovement = Random.Range(deltaTimeMin, deltaTimeMax);

                previousTime = tTime;

            }
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

            if (rayOnTarget && canHit && scenarioBehaviour.hitted == false)
            {
                reactTime = tTime - scenario.timeBeforeHit;
                scenarioBehaviour.hitted = true;
                this.hit = true;
                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + reactTime);
            }
        }
        #endregion
    }
}