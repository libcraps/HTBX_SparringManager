using UnityEngine;
using SparringManager.Scenarios;

namespace SparringManager.SimpleHit
{
    /*
     * 
     */
    public class SimpleHitController : MonoBehaviour
    {
        //---------- ATTRIBUTS ----------
        private StructScenarios _simpleHitStruct;
        private ScenarioController scenarioControllerComponent;

        [SerializeField]
        private GameObject _hitPrefab;
        public GameObject HitPrefab
        {
            get
            {
                return _hitPrefab;
            }
            set
            {
                _hitPrefab = value;
            }
        }

        //---------- METHODS ----------
        //General Methods
        private void Start()
        {
            scenarioControllerComponent = GetComponent<ScenarioController>();
            _simpleHitStruct = scenarioControllerComponent.ControllerStruct;
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
            Instantiate(_hitPrefab, pos3d_, Quaternion.identity, this.gameObject.transform);
        }
    }
}
