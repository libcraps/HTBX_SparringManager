using System.Collections;
using System.Collections.Generic;
using SparringManager;
using UnityEngine;

namespace SparringManager.SimpleLine 
{
    public class SimpleLineController : MonoBehaviour
    {

        [SerializeField]
        private MeshRenderer _simpleLine;
        private Vector3 _pos3d;

        // Start is called before the first frame update
        void Start()
        {
            
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            Instantiate(_simpleLine, _pos3d, Quaternion.identity, this.gameObject.transform.parent);
        }
    }   
}
