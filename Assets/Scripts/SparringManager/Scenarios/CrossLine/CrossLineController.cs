using SparringManager.DataManager.CrossLine;
using UnityEngine;

namespace SparringManager.CrossLine
{
   /* Class of the CrossLineController
    * 
    *  Summary :
    *  This class is the  controller of the crossLineScenario
    *  
    *  Importants Attributs :
    *      scenariocontroller scenariocontrollercomponent : It is the component ScenarioController of the prefab object, it allows us to stock specific parameters of the scenario (acceleration, delta hit, etc...) -> it is in the structure controllerstruct
    *      StructScenarios controllerStruct : It is the structure that contains the StructScenarios scenarios[i] (in this structure we can find the structure crossLineStruct that contains the structure CrossLineStruct)
    *      CrossLineStruct crossLineControllerStruct : It is the structure that contain ONLY the CrossLineScenario's parameters
    *      
    *  Methods :
    */
    public class CrossLineController : MonoBehaviour
    {
        private StructScenarios _controllerStruct;
        private CrossLineStruct _crossLineStruct;
        private CrossLineDataStruct _crossLineData;

        private ScenarioController scenarioControllerComponent;
        
        [SerializeField]
        private GameObject _scenarioComposant;

        private float _reactTime;
        private float _startScenario;

        public static bool _hitted = false;

        void Start()
        {
            scenarioControllerComponent = GetComponent<ScenarioController>();
            _controllerStruct = scenarioControllerComponent.ControllerStruct;
            _crossLineStruct = scenarioControllerComponent.ControllerStruct.CrossLineStruct;
            _startScenario = Time.time;

            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            SessionManager.InstantiateAndBuildScenario(_controllerStruct, this.gameObject, _pos3d, _scenarioComposant);
        }

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
            float tTime = Time.time - _startScenario;
            float _timeBeforeHit = _crossLineStruct.TimeBeforeHit;
            float _deltaHit = _crossLineStruct.DeltaHit;

            RaycastHit hit;
            Vector3 rayCastOrigin = new Vector3(position2d_.x, position2d_.y, this.gameObject.transform.position.z);
            Vector3 rayCastDirection = new Vector3(0, 0, 1);

            bool rayOnTarget = Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, 250);
            bool canHit = (tTime > _timeBeforeHit && (tTime - _timeBeforeHit) < _deltaHit);

            if (rayOnTarget && canHit && _hitted == false)
            {
                _reactTime = tTime - _timeBeforeHit;
                _hitted = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + _reactTime);
            }
        }

        void OnDestroy()
        {
            GetData();
            DataManager.DataManager.ToCSV(_crossLineData.DataBase, "C:\\Users\\IIFR\\Documents\\GitHub\\Hitbox_Test\\HTBX_SparringManager\\_data\\Tableau.csv");
            _reactTime = 0;
            _hitted = false;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        void GetData()
        {
            _crossLineData = new CrossLineDataStruct(_hitted, _reactTime, CrossLine.mouvementConsign, CrossLine.timeListScenario);
            DataManager.DataManager.DataBase.Add(_crossLineData);            
            Debug.Log(DataManager.DataManager.DataBase);
        }


        

    }
}