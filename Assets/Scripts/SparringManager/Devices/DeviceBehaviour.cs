using System.Collections;
using System.Collections.Generic;
using SparringManager.Structures;
using UnityEngine;

namespace SparringManager.Device
{

    public class DeviceBehaviour : MonoBehaviour
    {
        public string id { get; set; }

        public virtual void Init(IStructDevice structure, string id)
        {
            this.id = id;
        }
    }

}