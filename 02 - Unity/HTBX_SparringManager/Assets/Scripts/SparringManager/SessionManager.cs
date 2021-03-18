﻿using SparringManager.Scenarios;
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
    public class SessionManager : MonoBehaviour
    {
        #region Attributs
        //----------------------    ATTRIBUTS    --------------------------
        private string _name;
        private StructScenarios[] _scenarios; //List of StructScenarios, it contains every parameters of the session of the scenario
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
                    InstantiateAndBuildScenario(_scenarios[_indexScenario], _structPlayerScene, this.gameObject, this.gameObject.transform.position);
                    
                    _indexScenario += 1;
                }
                EndScenario = false;
            }
        }
        private void OnDestroy()
        {

        }
        //Method that instantiate a scenario
        private void InstantiateAndBuildScenario(StructScenarios strucObject, StructPlayerScene structPlayerScene, GameObject referenceGameObject, Vector3 _pos3d, GameObject prefabObject = null)
        {
            /*
             * Function that instatiate an object, the prefab of this object is in the structureScenarios, it contains all the data that is usefull for the scenarios
             * 
             * Parameters :
             *      strucObject : structure that contains parameters of the scenario, the type is the same for everyone because it allows us to unified the type and to choose a more specified type after
             *      referenceGameObject : use to choose the parent of our object, it often this.gameObjetct
             *      _pos3D : position where we want to instantiate our object
             *      prefabGameObject : it is here to be able to use this fonction for scenario composants (because the scenario_controller is in the structure and the scenario composent in the scenario controller)
             */
            GameObject scenario;
            if (prefabObject == null)
            {
                prefabObject = strucObject.ScenarioPrefab; //if we don't specified the prefab we used the prefab that is in the structure (so the prefab of the scenario)
            }

            if (_pos3d == null)
            {
                _pos3d = referenceGameObject.transform.position; //if the position isn't specified we place the object a the same place of the reference
            }

            scenario = Instantiate(prefabObject, _pos3d, Quaternion.identity, referenceGameObject.transform);
            scenario.GetComponent<ScenarioControllerBehaviour>().Init(strucObject);
            Destroy(scenario, strucObject.TimerScenario);

            Debug.Log(prefabObject.name + " has been instantiated");
        }

        public void Init(StructScenarios[] scenarios,StructPlayerScene structPlayerScene, int actionAngle, string name, bool export)
        {
            _scenarios = scenarios;
            _structPlayerScene = structPlayerScene;
            _operationalArea = actionAngle;
            _name = name;
        }
        #endregion
    }

    [Serializable]
    public struct DeviceStructure
    {
        [SerializeField]
        private bool _viveTracker;
        [SerializeField]
        private bool _polar;
        [SerializeField]
        private bool _movuino;

        public bool ViveTracker
        {
            get
            {
                return _viveTracker;
            }
            set
            {
                _viveTracker = value;
            }
        }
        public bool Polar
        {
            get
            {
                return _polar;
            }
            set
            {
                _polar = value;
            }
        }
        public bool Movuino
        {
            get
            {
                return _movuino;
            }
            set
            {
                _movuino = value;
            }
        }

        public DeviceStructure(bool viveTracker, bool polar, bool movuino)
        {
            _viveTracker = viveTracker;
            _polar = polar;
            _movuino = movuino;
        }
    }
}