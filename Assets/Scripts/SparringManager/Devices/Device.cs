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
    public static class Test
    {
        public static int a;
        public static void Affiche()
        {
            Debug.Log(a);
        }

    }
}

