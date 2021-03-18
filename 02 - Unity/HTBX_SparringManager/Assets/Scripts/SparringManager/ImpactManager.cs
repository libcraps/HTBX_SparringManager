using SparringManager.Serial;
using SparringManager.Serial.Example;
using UnityEngine;

namespace SparringManager
{
    public class ImpactManager : MonoBehaviour
    {
        private Camera _hitboxCamera; // player camera --> manage real impact
        //[SerializeField]
        private Camera _debugCamera;  // debug camera --> manager mouse click (for debug)

        public delegate void InteractPointEventHandler(Vector2 interactPoint);
        public static event InteractPointEventHandler onInteractPoint;

        private void OnEnable()
        {
            ImpactPointControl.onImpact += OnHit;
        }

        private void OnDisable()
        {
            ImpactPointControl.onImpact -= OnHit;
        }

        private void Awake()
        {
            _debugCamera = GameObject.Find("CoachCamera").GetComponent<Camera>();
            _hitboxCamera = this.gameObject.GetComponent<Camera>(); // get its own camera on awake
        }

        private void OnHit(object sender, ImpactPointControlEventArgs e)
        {
            OnInteract(e.impactPosition);
        }

        private void OnInteract(Vector2 position2D_)
        {
            if (onInteractPoint != null)
                onInteractPoint(position2D_);
        }

#if UNITY_EDITOR
        private void Update()
        {
            // Need to be into Update loop to be trigger without clicking on specific object
            if (Input.GetMouseButtonDown(0))
                OnMouseDown();
        }
        void OnMouseDown()
        {
            Vector3 mousePosition = Input.mousePosition;
            if (!_hitboxCamera.orthographic)
                mousePosition.z = this.transform.position.z;
            OnInteract(_debugCamera.ScreenToWorldPoint(mousePosition));
        }
#endif
    }
}