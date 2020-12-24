using SparringManager;
using UnityEngine;

namespace SparringManager.HitLine
{
    //Classe du Controller du scenario HitLine
    public class HitLineController : MonoBehaviour
    {
        private StructScenarios _controllerStruct;
        private ScenarioController _scenarioControllerComponent;

        [SerializeField]
        private GameObject _scenarioPrefab;

        private float _reactTime;
        private float _startScenario;
        private bool _hitted;

        void Start()
        {
            _scenarioControllerComponent = GetComponent<ScenarioController>();
            _controllerStruct = _scenarioControllerComponent._controllerStruct;
            
            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            SessionManager.InstantiateAndBuildScenario(_controllerStruct, this.gameObject, _pos3d, _scenarioPrefab);
        }

        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + "has been destroyed");
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
            float _timeBeforeHit = _controllerStruct._timeBeforeHit;
            float _deltaHit = _controllerStruct._deltaHit;
            

            RaycastHit hit;
            Vector3 rayCastOrigin = new Vector3 (position2d_.x, position2d_.y, this.gameObject.transform.position.z);
            Vector3 rayCastDirection = new Vector3 (0,0,1);

            bool rayOnTarget = Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, 250);
            bool canHit = (tTime > _timeBeforeHit && (tTime - _timeBeforeHit) < _deltaHit);
            
            if (rayOnTarget && canHit && _controllerStruct._hitted == false)
            {
                _reactTime = tTime - _timeBeforeHit;
                _controllerStruct._hitted = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + _reactTime);
            }
        }

    }
}