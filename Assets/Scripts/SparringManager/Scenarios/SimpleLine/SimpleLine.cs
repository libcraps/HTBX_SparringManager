using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.SimpleLine
{
    public class SimpleLine : MonoBehaviour
    {
        private Rigidbody _lineRigidComponent;
        private MeshRenderer _lineRender;
        private float posLineX;
        private float posLineY;
        private float posLineZ;
        private float _lineSpeed = 5.0f;
        private Random _randomTime;
        // Start is called before the first frame update
        void Start()
        {
            _lineRigidComponent = GetComponent<Rigidbody>();
            _lineRender  = GetComponent<MeshRenderer>();
        }

        void FixedUpdate()
        {
            MoveLine(_lineSpeed);
        }

        void MoveLine(float lineHorizontalSpeed)
        {
            Debug.Log("moving line");
            _lineRigidComponent.velocity = new Vector3(lineHorizontalSpeed, 0, 0);
        }

        void RandomMovementSpeed(float timeChangeDir)
        {

        }
    }
}
        