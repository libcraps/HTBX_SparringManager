using SparringManager;
using UnityEngine;

namespace SparringManager.SimpleLine 
{
    public class SimpleLineController : MonoBehaviour
    {
        private StructScenarios _controllerStruct;
        private ScenarioController _scenarioControllerComponent;

        [SerializeField]
        private GameObject _scenarioComposant;

        void Start()
        {
            //Initialisation des varibales pour lé scénarios
            _scenarioControllerComponent = GetComponent<ScenarioController>();
            _controllerStruct = _scenarioControllerComponent._controllerStruct;

            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            SessionManager.InstantiateAndBuildScenario(_controllerStruct, this.gameObject, _pos3d, _scenarioComposant);
        }
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + " has been destroyed");

        }
    }   
}
