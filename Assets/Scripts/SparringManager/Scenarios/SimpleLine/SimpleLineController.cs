using SparringManager;
using UnityEngine;

namespace SparringManager.SimpleLine 
{
    public class SimpleLineController : MonoBehaviour
    {
        private StructScenarios _controllerStruct;
        private ScenarioController _scenario;

        [SerializeField]
        private GameObject _scenarioPrefab;


        void Start()
        {
            _scenario = GetComponent<ScenarioController>();

            _controllerStruct = _scenario._controllerStruct;
            float _timer = _controllerStruct._timerScenario;
            

            Debug.Log(this.gameObject.name + " timer " + _timer);
            
            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            Destroy(Instantiate(_scenarioPrefab, _pos3d, Quaternion.identity, this.gameObject.transform), _timer);
        }
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + " has been destroyed");

        }
    }   
}
