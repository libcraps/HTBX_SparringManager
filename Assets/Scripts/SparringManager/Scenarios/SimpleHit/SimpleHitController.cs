using SparringManager.DataManager;
using SparringManager.Device;
using UnityEngine;

namespace SparringManager.Scenarios
{
    /*
     * 
     */
    public class SimpleHitController : ScenarioControllerBehaviour
    {
        #region Attributs
        //----------- ATTRIBUTS ----------------------
        public static int nbApparition;
        //Scenario
        public ScenarioSimpleHit scenario { get; set; }

        protected override float startTimeScenario { get { return scenario.startTimeScenario; } set { scenario.startTimeScenario = value; } }
        protected override object consigne { get { return " "; } }

        #endregion

        //---------- METHODS ----------
        private void Awake()
        {
            nbApparition += 1;
        }
        //General Methods
        protected override void Start()
        {
            base.Start();
            GetDevices();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate(); //StockData
            hit = " ";
        }
        private void OnDestroy()
        {

            dataManagerComponent.DataBase.Add(dataSessionPlayer.DataTable);

            dataManagerComponent.EndScenarioForData = true;
            GetComponentInParent<SessionManager>().EndScenario = true;

            Debug.Log(this.gameObject.name + "has been destroyed");
        }
        public override void Init(StructScenarios structScenarios)
        {
            //Initialize this Class
            //Scenario controller
            scenario = Scenario<SimpleHitStruct>.CreateScenarioObject<ScenarioSimpleHit>();
            scenario.Init(structScenarios);

            dataSessionPlayer = new DataSessionPlayer(NbMovuino);

            dataSessionPlayer.DataSessionScenario.scenarioSumUp = DataController.StructToDictionary<SimpleHitStruct>(scenario.structScenario);
            dataManagerComponent = GetComponentInParent<DataController>();
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataSessionPlayer.DataSessionScenario.scenarioSumUp); //Mettre dans 
        }
        //Method for an hitting object
        private void OnEnable()
        {
            ImpactManager.onInteractPoint += SetImpactPosition;
            
        }
        private void OnDisable()
        {
            ImpactManager.onInteractPoint -= SetImpactPosition;
            
        }
        public void SetImpactPosition(Vector2 position2d_)
        {
            Vector3 pos3d_ = new Vector3(position2d_.x, position2d_.y, this.gameObject.transform.position.z + 20f);
            Instantiate(_prefabScenarioComposant, pos3d_, Quaternion.identity, this.gameObject.transform);
            hit = true;
        }
    }
}
