using System.Collections;
using System.Collections.Generic;
using SparringManager.CrossLine;
using UnityEngine;

namespace SparringManager.Scenarios
{
    public class Scenario : MonoBehaviour
    {
        /*
         * Class that will be a component of every prefab, it allows us to stock parameters of our scenario
         */
        [SerializeField]
        private StructScenarios _controllerStruct;
        public StructScenarios ControllerStruct
        {
            get
            {
                return _controllerStruct;
            }
            set
            {
                _controllerStruct = value;
            }
        }

    }
    public abstract class ScenarioObj<StructScenario> where StructScenario: IStructScenario
    {
        public abstract GameObject scenarioComposant { get; set; }
        public abstract StructScenario structScenario { get; set; }

        public static T CreateScenarioObject<T>() where T: ScenarioObj<StructScenario>, new()
        {
            T myObj = new T();
            return myObj;
        }
    }

    public class ScenarioCrossLine : ScenarioObj<CrossLineStruct>
    {
        public override GameObject scenarioComposant { get; set; }
        public override CrossLineStruct structScenario { get; set; }

        public ScenarioCrossLine(CrossLineStruct structure)
        {
            structScenario = structure;
        }
    }

}