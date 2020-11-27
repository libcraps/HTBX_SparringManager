using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.SimpleLine
{
    public class SimpleLine : MonoBehaviour
    {
        [SerializeField]
        private int _accelerationMax;
        [SerializeField]
        private int _deltaTimeMax;
        [SerializeField]
        private int _deltaTimeMin;
        
        private Rigidbody _lineRigidComponent;
        private float _lineAcceleration = 5.0f; 
        private float _deltaTime;
        private System.Random _randomTime = new System.Random();
        private System.Random _randomAcceleration = new System.Random();
        private float _previousTime;
        private float _tTime = 0f;


        // Start is called before the first frame update
        void Start()
        {
            _lineRigidComponent = GetComponent<Rigidbody>();

            _tTime = Time.time;
            _previousTime = _tTime;
            _deltaTime = _randomTime.Next(_deltaTimeMin, _deltaTimeMax);
        }

        void FixedUpdate()
        {
            _tTime = Time.time;

            if ((_tTime - _previousTime) > _deltaTime)
            {
                _lineAcceleration = _randomAcceleration.Next(-_accelerationMax, _accelerationMax);
                _previousTime = _tTime;
                _deltaTime = _randomTime.Next(_deltaTimeMin, _deltaTimeMax);

            }
            MoveLine(_lineAcceleration);
        }

        void MoveLine(float lineHorizontalAcceleration)
        {
            //_lineRigidComponent.AddForce(new Vector3 (lineHorizontalAcceleration, 0, 0), ForceMode.Acceleration);

            _lineRigidComponent.velocity = new Vector3 (lineHorizontalAcceleration, 0, 0);
        }
    }
}