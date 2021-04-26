using SparringManager.Scenarios;
using SparringManager.Data;
using SparringManager.Structures;
using UnityEngine;
using System;

namespace SparringManager
{
    /// <summary>
    /// Class of the SessionManager, it manages the session. 
    /// <para>It manages sthe session by instantiating scenarios</para>
    /// </summary>
    /// <remarks>It is a component of each PlayerCamera prefab.</remarks>
    public class SessionManager : MonoBehaviour
    {
        #region Attributs
        //----------------------    ATTRIBUTS    --------------------------
        private string _name;
        private GeneriqueScenarioStruct[] _scenarios; //List of StructScenarios, it contains every parameters of the session of the scenario
        private int _operationalArea;
        public bool EndScenario { get; set; }
        private int _indexScenario = 0;

        //Variables for the DataManager
        private DataManager _dataManager;
        private DeviceManager _deviceManager;

        //Properties
        public int NbScenarios { get { return _scenarios.Length; } }
        public int OperationalArea { get { return _operationalArea; } }

        public GameObject scenarioPlayed;

        public ImpactManager renderCameraIM;

        #endregion
        #region Methods
        //----------------------    METHODS    -------------------------------
        // ---> General Methods
        private void Awake()
        {
            _dataManager = GetComponent<DataManager>();
            _deviceManager = GetComponent<DeviceManager>();
        }
        void Start()
        {
            //DATA MANAGER
            _dataManager.InitGeneralSectionSumUp(_name, _dataManager.FilePath, NbScenarios); //DataManager completed
            _indexScenario = 0;
            EndScenario = true; //We initialise to true in order to go in the loop
        }

        private void OnEnable()
        {
            ImpactManager.onInteractPoint += LaunchScenario; //Launch scenario when Hit
        }

        private void OnDisable()
        {
            ImpactManager.onInteractPoint -= LaunchScenario; //Launch scenario when Hit
        }

        /// <summary>
        /// Method that instantaiates scenarios
        /// </summary>
        /// <para>Function that instatiate an object, the prefab of this object is in the structureScenarios, it contains all the data that is usefull for the scenarios</para>
        /// <param name="strucObject">Structure that contains scenario settings</param>
        /// <param name="referenceGameObject">Use to choose the parent of our object, it often this.gameObjetct</param>
        /// <param name="pos3d">Position where we want to instantiate our object</param>
        private void InstantiateAndBuildScenario(GeneriqueScenarioStruct strucObject, GameObject referenceGameObject, Vector3 pos3d)
        {
            scenarioPlayed = Instantiate(strucObject.ScenarioPrefab, pos3d, Quaternion.identity, referenceGameObject.transform);

            scenarioPlayed.GetComponent<ScenarioController>().Init(strucObject);
            Destroy(scenarioPlayed, strucObject.TimerScenario);

            Debug.Log(scenarioPlayed.name + " has been instantiated");
        }


        /// <summary>
        /// Method that launch a scenario when it is called
        /// </summary>
        /// <param name="position2d_">Position of the scenario</param>
        public void LaunchScenario(Vector2 position2d_)
        {
            Vector3 pos3d_ = this.gameObject.transform.position;
            pos3d_.x = position2d_.x;


            if (EndScenario == true) //(Time.time - _timeStartScenarioI) > _timerScenarioI)
            {
                //Deal with the instantiation of scenarios
                if (_indexScenario < (_scenarios.Length))
                {
                    //InstantiateAndBuildScenario(_scenarios[_indexScenario], this.gameObject, pos3d_);   ---> TO CHOOSE <---
                    InstantiateAndBuildScenario(_scenarios[_indexScenario], this.gameObject, this.gameObject.transform.position);
                    _indexScenario += 1;
                }
                EndScenario = false;
            }
        }

        /// <summary>
        /// Init session's settings
        /// </summary>
        /// <param name="scenarios"></param>
        /// <param name="actionAngle"></param>
        /// <param name="name"></param>
        /// <param name="export"></param>
        public void Init(GeneriqueScenarioStruct[] scenarios, int actionAngle, string name, bool export)
        {
            _scenarios = scenarios;
            _operationalArea = actionAngle;
            _name = name;
        }
        #endregion
    }
}