using SparringManager;
using UnityEngine;

namespace SparringManager.SimpleLine 
{
    public class SimpleLineController : MonoBehaviour
    {

        [SerializeField]
        private GameObject _simpleLine;
        [SerializeField]
        public int _accelerationMax;
        [SerializeField]
        public int _deltaTimeMax;
        [SerializeField]
        public int _deltaTimeMin;

        void Start()
        {
            GameObject gameObject = GameObject.Find(this.gameObject.transform.parent.name);
            SessionManager session = gameObject.GetComponent<SessionManager>();

            float _timer = session._timer;
            Debug.Log(this.gameObject.name + " timer " + _timer);
            
            Vector3 _pos3d;
            
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            Destroy(Instantiate(_simpleLine, _pos3d, Quaternion.identity, this.gameObject.transform.parent), _timer);
        }
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + "has been destroyed");
        }
    }   
}
