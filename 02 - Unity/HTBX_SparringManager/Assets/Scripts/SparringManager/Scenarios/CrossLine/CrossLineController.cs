using SparringManager.DataManager;
using SparringManager.Device;
using SparringManager.Structures;
using UnityEngine;


/// <summary>
/// Namespace relative to the scenario CrossLine
/// </summary>
namespace SparringManager.Scenarios.CrossLine
{
    /// <summary>
    /// Manage the scenario CrossLine.
    /// </summary>
    /// <inheritdoc cref="ScenarioControllerBehaviour"/>
    public class CrossLineController : ScenarioControllerBehaviour
    {
        #region Attributs
        //----------- ATTRIBUTS ----------------------
        //Scenario
        public CrossLineBehaviour scenarioBehaviour { get; set; }
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
            
            Debug.Log(nbMovuino);

            //Instantiation of scenario behaviour display
            Vector3 pos3d;
            pos3d.x = this.gameObject.transform.position.x;
            pos3d.y = this.gameObject.transform.position.y;
            pos3d.z = this.gameObject.transform.position.z + 100f;

            var go = Instantiate(PrefabScenarioComposant, pos3d, Quaternion.identity, this.gameObject.transform);
            scenarioBehaviour = go.GetComponent<CrossLineBehaviour>();
            scenarioBehaviour.Init(scenario.structScenari);
            Destroy(go, scenario.timerScenario);

            Debug.Log(this.gameObject.name + " for " + scenario.timerScenario + " seconds");

        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
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
        public override void Init(GeneriqueScenarioStruct structScenari)
        {
            scenario = new Scenario(structScenari);
            dataSessionPlayer = new DataSessionPlayer(nbMovuino);

            dataSessionPlayer.DataSessionScenario.scenarioSumUp = DataController.StructToDictionary<GeneriqueScenarioStruct>(scenario.structScenari);
            dataManagerComponent = GetComponentInParent<DataController>();
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataSessionPlayer.DataSessionScenario.scenarioSumUp); //Mettre dans 
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