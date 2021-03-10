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
        //---------- ATTRIBUTS ----------
        [SerializeField]
        private GameObject _prefabScenarioComposant;
        public override GameObject PrefabScenarioComposant
        {
            get
            {
                return _prefabScenarioComposant;
            }
            set
            {
                _prefabScenarioComposant = value;
            }
        }
        public ScenarioCrossLine scenario { get; set; }

        public DataSessionPlayer dataSessionPlayer;
        public Movuino[] movuino;
        public DataSessionMovuino dataSessionMovuino;
        public DataSessionScenario dataScenario;

        //list of the data that we will export
        private DataController dataManagerComponent;
        private float tTime;
        private float startTimeScenario { get { return scenario.startTimeScenario; } set { scenario.startTimeScenario = value; } }

        public static int nbApparition;

        //---------- METHODS ----------
        private void Awake()
        {
            movuino = new Movuino[2];
            nbApparition += 1;
        }
        //General Methods
        private void Start()
        {
            movuino[0] = GameObject.FindGameObjectsWithTag("Movuino")[0].GetComponent<Movuino>();
            movuino[1] = GameObject.FindGameObjectsWithTag("Movuino")[1].GetComponent<Movuino>();
        }

        private void FixedUpdate()
        {
            //Data management
            dataSessionMovuino.StockData(tTime, movuino[0].MovuinoSensorData.accelerometer, movuino[0].MovuinoSensorData.gyroscope, movuino[0].MovuinoSensorData.magnetometer);
        }
        private void OnDestroy()
        {

            dataManagerComponent.DataBase.Add(DataSession.JoinDataTable(dataScenario.DataTable, dataSessionMovuino.DataTable));

            dataManagerComponent.EndScenarioForData = true;
            GetComponentInParent<SessionManager>().EndScenario = true;

            Debug.Log(this.gameObject.name + "has been destroyed");
        }
        public override void Init(StructScenarios structScenarios)
        {
            //Initialize this Class
            //Scenario controller
            scenario = Scenario<CrossLineStruct>.CreateScenarioObject<ScenarioCrossLine>();
            scenario.Init(structScenarios);


            dataSessionPlayer = DataSession.CreateDataObject<DataSessionPlayer>();
            dataScenario = DataSession.CreateDataObject<DataSessionScenario>();
            dataSessionMovuino = DataSession.CreateDataObject<DataSessionMovuino>();

            dataScenario.scenarioSumUp = DataController.StructToDictionary<CrossLineStruct>(scenario.structScenario);
            dataManagerComponent = GetComponentInParent<DataController>();
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataScenario.scenarioSumUp); //Mettre dans 

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
        }
    }
}
