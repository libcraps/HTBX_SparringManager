using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererBehaviour : MonoBehaviour
{
    LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        Debug.Log(lr.GetPosition(0));
        Debug.Log("OOOOOOOOOOOOOOOOOK");
        lr.SetPosition(2, new Vector3(2, 2, 2));
    }
}
