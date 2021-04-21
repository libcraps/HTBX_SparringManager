using UnityEngine;

namespace SparringManager.Scenarios.SimpleHit
{
    /// <summary>
    /// Class that manage the prefab object that will represent an hit
    /// </summary>
    public class SimpleHitBehaviour : ScenarioDisplayBehaviour
    {
        [SerializeField]
        private GameObject _prefabObject;

        public GameObject prefabObject { get { return _prefabObject; } }

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        /// <summary>
        /// Insantiate an impact on the bag when it is hitted.
        /// </summary>
        /// <remarks>It is called by the event ImpactManager.onInteractPoint</remarks>
        /// <param name="position2d_">Position of the hit</param>
        protected override void GetHit(Vector2 position2d_)
        {
            Vector3 pos3d_ = new Vector3(position2d_.x, position2d_.y, this.gameObject.transform.position.z + 20f);
            Instantiate(prefabObject, pos3d_, Quaternion.identity, this.gameObject.transform);
            HitManager();
        }

        public override void HitManager()
        {
            hitted = true;
        }
    }
}