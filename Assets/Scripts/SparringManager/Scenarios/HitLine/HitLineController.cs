using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.HitLine
{
    public class HitLineController : MonoBehaviour
    {

        [SerializeField]
        public HitLineStruct _hitLineStruct;

        private float _reactTime;

        public bool _hitted = false;
        void Start()
        {
            GameObject _Session = GameObject.Find(this.gameObject.transform.parent.name);
            SessionManager session = _Session.GetComponent<SessionManager>();

            float _timer = session._timer;
            
            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            Destroy(Instantiate(_hitLineStruct._hitLine, _pos3d, Quaternion.identity, this.gameObject.transform), _timer);
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
            float tTime = Time.time;
            float _timeBeforeHit = _hitLineStruct._timeBeforeHit;
            float _deltaHit = _hitLineStruct._deltaHit;
            GameObject _hitPrefab = _hitLineStruct._hitPrefab;


            if (_hitPrefab !=null)
            {
            Vector3 pos3d_ = new Vector3(position2d_.x, position2d_.y, this.gameObject.transform.position.z + 20f);
            Instantiate(_hitPrefab, pos3d_, Quaternion.identity, this.gameObject.transform);
            }

            RaycastHit hit;
            Vector3 rayCastOrigin = new Vector3 (position2d_.x, position2d_.y, this.gameObject.transform.position.z);
            Vector3 rayCastDirection = new Vector3 (0,0,1);

            bool rayOnTarget = Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, 250);
            bool canHit = (tTime > _timeBeforeHit && (tTime - _timeBeforeHit) < _deltaHit);


            if (rayOnTarget && canHit && _hitted == false)
            {
                _reactTime = Time.time - _timeBeforeHit;
                _hitted = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + _reactTime);
            }
        }

    }
}