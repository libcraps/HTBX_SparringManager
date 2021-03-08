using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Scenarios
{
    public abstract class ScenarioControllerBehaviour : MonoBehaviour
    {
        public abstract GameObject PrefabScenarioComposant { get; set; }
        public abstract void Init(StructScenarios structScenarios);

    }

    public abstract class ScenarioBehaviour : MonoBehaviour
    {

    }
}

