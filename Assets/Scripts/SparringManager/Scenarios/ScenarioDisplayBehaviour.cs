using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using SparringManager.Device;
using UnityEngine;

namespace SparringManager.Scenarios
{
    public abstract class ScenarioDisplayBehaviour : MonoBehaviour
    {
        public abstract void Init(IStructScenario structScenarios);
    }
}