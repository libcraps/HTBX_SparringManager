using SparringManager.Scenarios;
using SparringManager.DataManager;
using SparringManager.Structures;
using UnityEngine;
using System;

namespace SparringManager
{
    /* Class of the Session Manager
     * 
     *  Summary :
     *  This class manage the session :
     *      - it instantiates scenarios
     *      - it deals with the DataManager
     *  
     *  Attributs :
     *      //Usefull parameters of the class
     *      string _name :  Name of the player 
     *      StructScenarios[] _scenarios : List of StructScenarios, it contains every parameters of the session of the scenario
     *      int _indexScenario: index of the current secnario that is playing
     *      
     *      //Variables for the DataManager
     *      string _filePath : Path of the data folder, it is initialized to .\_data\
     *      DataManager.DataManager _dataManager : DataManager component
     *      
     *  Methods :
     *      void InstantiateAndBuildScenario(StructScenarios strucObject, GameObject referenceGameObject, Vector3 _pos3d, GameObject prefabObject = null)
     */

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
        private StructPlayerScene _structPlayerScene;
        private int _operationalArea;
        public bool EndScenario { get; set; }
        private int _indexScenario = 0;

        //Variables for the DataManager
        private DataController _dataManager;

        //Properties
        public int NbScenarios { get { return _scenarios.Length; } }
        public int OperationalArea { get { return _operationalArea; } }

        #endregion
        #region Methods
        //----------------------    METHODS    -------------------------------
        // ---> General Methods
        void Start()
        {
            //DATA MANAGER
            _dataManager = GetComponent<DataController>(); 
            _dataManager.InitGeneralSectionSumUp(_name, _dataManager.FilePath, NbScenarios); //DataController completed

            _indexScenario = 0;
            EndScenario = true; //We initialise to true in order to go in the loop
        }
        private void Update()
        {
            if (EndScenario == true) //(Time.time - _timeStartScenarioI) > _timerScenarioI)
            {
                //Deal with the instantiation of scenarios
                if (_indexScenario < (_scenarios.Length))
                {
                    InstantiateAndBuildScenario(_scenarios[_indexScenario], this.gameObject, this.gameObject.transform.position);
                    
                    _indexScenario += 1;
                }
                EndScenario = false;
            }
        }
        private void OnDestroy()
        {

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
            GameObject prefabObject = strucObject.ScenarioPrefab;
            GameObject scenario = Instantiate(prefabObject, pos3d, Quaternion.identity, referenceGameObject.transform);

            scenario.GetComponent<ScenarioControllerBehaviour>().Init(strucObject);
            Destroy(scenario, strucObject.TimerScenario);

            Debug.Log(prefabObject.name + " has been instantiated");
        }

        public void Init(GeneriqueScenarioStruct[] scenarios,StructPlayerScene structPlayerScene, int actionAngle, string name, bool export)
        {
            _scenarios = scenarios;
            _structPlayerScene = structPlayerScene;
            _operationalArea = actionAngle;
            _name = name;
        }
        #endregion
    }
}