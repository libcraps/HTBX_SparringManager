using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Device
{
    public abstract class Device : MonoBehaviour
    {
        public virtual void ToMovuinoData(OscMessage message)
        {
        }
        
        
    }
}

