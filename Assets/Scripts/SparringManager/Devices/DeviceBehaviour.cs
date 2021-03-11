using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Device
{

    public class DeviceBehaviour : MonoBehaviour
    {
        public string id { get; set; }
        public void Init(string id)
        {
            this.id = id;
        }
    }

}