using UnityEngine;

namespace SparringManager.SimpleHit
{
    public class SimpleHitController : MonoBehaviour
    {
        private StructScenarios _simpleHitStruct;
        private ScenarioController _scenarioControllerComponent;
        [SerializeField]
        public GameObject _hitPrefab;

        private void Start()
        {
            _scenarioControllerComponent = GetComponent<ScenarioController>();
            _simpleHitStruct = _scenarioControllerComponent._controllerStruct;
        }

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
            SessionManager.InstantiateAndBuildScenario(_simpleHitStruct, this.gameObject, pos3d_, _hitPrefab);
        }
    }
}
