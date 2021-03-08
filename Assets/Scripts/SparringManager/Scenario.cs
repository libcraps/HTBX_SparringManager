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
    public abstract class Scenario<StructScenario> where StructScenario: IStructScenario
    {
        public abstract GameObject scenarioComposant { get; set; }
        public abstract StructScenario structScenario { get; set; }

        public static T CreateScenarioObject<T>(StructScenario structScenario) where T: Scenario<StructScenario>, new()
        {
            T myObj = new T();
            myObj.structScenario = structScenario;

            return myObj;
        }

        public abstract void Init(StructScenarios structScenarios);
    }

    public class ScenarioCrossLine : Scenario<CrossLineStruct>
    {
        public override GameObject scenarioComposant { get; set; }
        public override CrossLineStruct structScenario { get; set; }

        public override void Init(StructScenarios structScenarios)
        {
            structScenario = structScenarios.CrossLineStruct;
        }
        
    }

}