using System.Collections;
using System.Collections.Generic;
using SparringManager;
using UnityEngine;

namespace SparringManager.SimpleLine 
{
    [RequireComponent(typeof(MeshRenderer))]
    public class SimpleLine : MonoBehaviour
    {

        [SerializeField]
        private MeshRenderer _simpleLine;

        //[SerializeField]
        private float LineSpeed = 5f;

        private float _pos2D;
        private Rigidbody LineRigidComponent;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Debut de Simple Line");
            LineRigidComponent = _simpleLine.GetComponent<Rigidbody>();
            Instantiate(_simpleLine);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            LineRigidComponent.velocity = new Vector3(0, LineSpeed, 0);
        }

    }   
}
