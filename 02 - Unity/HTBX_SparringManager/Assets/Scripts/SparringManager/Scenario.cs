using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Scenarios
{
    /*
     * Class that allows us tu control scenarios
     * Mother class : Scenario<StructScenario>
     * 
     * Test interface : IScenarioClass
     * 
     * Scenario created by inheretent of Scenario<StructScenario>:
     *      ScenarioCrossLine : Scenario<CrossLineStruct>
     *      ScenarioHitLine : Scenario<HitLineStruct>
     *      ScenarioSimpleLine : Scenario<SimpleLineStruct>
     *      ScenarioSplHitLine : Scenario<SplHitLineStruct>
     *      ScenarioSimpleHit : Scenario<SimpleHitStruct>
     */


    /// <summary>
    /// Interface test for scenarios
    /// </summary>
    public interface IScenarioClass
    {
        float timerScenario { get; set; }
        float startTimeScenario { get; set; }
    }

    /// <summary>
    /// <para>Abstract class that manage general scenarios caracterisics and methods.</para>
    /// <para>Mother class of scenarios, also it allows us to stock easily data of scenarios.</para>
    /// </summary>
    /// <typeparam name="StructScenario">Structure that need to be IStructScenario</typeparam>
    public abstract class Scenario<StructScenario> : IScenarioClass where StructScenario: IStructScenario
    {
        /*
         * Mother class of scenarios, it allows us to stock easily data of scenarios
         * 
         * Static methods : 
         *      public static T  CreateScenarioObject<T>() where T : Scenario<StructScenario> and StructScenario : IStructScenario
         * 
         * Attributs:
         *      public StructScenario structScenario { get; set; }
         *      public float timerScenario { get; set; }
         *      public float startTimeScenario { get; set; }
         * 
         * Methods : 
         */

        /// <summary>
        /// Create an object of the type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>The object of type <typeparamref name="T"/></returns>
        public static T CreateScenarioObject<T>() where T : Scenario<StructScenario>, new()
        {
            T myObj = new T();
            return myObj;
        }

        #region Attributs/properties
        /// <summary>
        /// Structure that contain scenario parameters
        /// </summary>
        public StructScenario structScenario { get; set; }
        public float timerScenario { get; set; }
        public float startTimeScenario { get; set; }
        #endregion

        #region Methods
        public virtual void Init(StructScenarios structScenarios)
        {
            /*
             * Initialize the structure
             */
        }
        public virtual object PosToAngle(float screenSize, object coord)
        {
            /*
             * Function that allows us to convert a (x,y,z) consigne in a degree consigne
             * 
             * Screen size -> reference
             * 
             */
            object angle;
            angle = (float)coord * 180.0/ screenSize;
            return angle;
        }
        #endregion
    }

    public class ScenarioCrossLine : Scenario<CrossLineStruct>
    {

        //Usefull parameters of the scenario, they are in the crossLineStructure
        public int accelerationMax { get { return structScenario.AccelerationMax; } }
        public int deltaTimeMax { get { return structScenario.DeltaTimeMax; } }
        public int deltaTimeMin { get { return structScenario.DeltaTimeMin; } }
        public float deltaHit { get { return structScenario.DeltaHit; } }
        public float timeBeforeHit { get { return structScenario.TimeBeforeHit; } }

        public override void Init(StructScenarios structScenarios)
        {
            structScenario = new CrossLineStruct();
            structScenario = structScenarios.CrossLineStruct;

            timerScenario = structScenarios.TimerScenario;
        }
    }
    public class ScenarioHitLine : Scenario<HitLineStruct>
    {
        //Usefull parameters of the scenario, they are in the crossLineStructure
        public int accelerationMax { get { return structScenario.AccelerationMax; } }
        public int deltaTimeMax { get { return structScenario.DeltaTimeMax; } }
        public int deltaTimeMin { get { return structScenario.DeltaTimeMin; } }
        public float deltaHit { get { return structScenario.DeltaHit; } }
        public float timeBeforeHit { get { return structScenario.TimeBeforeHit; } }

        public override void Init(StructScenarios structScenarios)
        {
            structScenario = new HitLineStruct();
            structScenario = structScenarios.HitLineStruct;

            timerScenario = structScenarios.TimerScenario;
        }

    }
    public class ScenarioSimpleLine : Scenario<SimpleLineStruct>
    {
        //Usefull parameters of the scenario, they are in the crossLineStructure
        public int accelerationMax { get { return structScenario.AccelerationMax; } }
        public int deltaTimeMax { get { return structScenario.DeltaTimeMax; } }
        public int deltaTimeMin { get { return structScenario.DeltaTimeMin; } }

        public override void Init(StructScenarios structScenarios)
        {
            structScenario = new SimpleLineStruct();
            structScenario = structScenarios.SimpleLineStruct;

            timerScenario = structScenarios.TimerScenario;
        }

    }
    public class ScenarioSplHitLine : Scenario<SplHitLineStruct>
    {
        //Usefull parameters of the scenario, they are in the crossLineStructure
        public int accelerationMax { get { return structScenario.AccelerationMax; } }
        public int deltaTimeMax { get { return structScenario.DeltaTimeMax; } }
        public int deltaTimeMin { get { return structScenario.DeltaTimeMin; } }
        public float deltaHit { get { return structScenario.DeltaHit; } }
        public float timeBeforeHit { get { return structScenario.TimeBeforeHit; } }
        public int ScaleMaxValue { get { return structScenario.ScaleMaxValue; } }
        public float ScaleSpeed { get { return structScenario.ScaleSpeed; } }

        public override void Init(StructScenarios structScenarios)
        {
            //_structScenario = new SplHitLineStruct();
            structScenario = structScenarios.SplHitLineStruct;

            timerScenario = structScenarios.TimerScenario;
        }

    }
    public class ScenarioSimpleHit : Scenario<SimpleHitStruct>
    {

        public override void Init(StructScenarios structScenarios)
        {
            structScenario = new SimpleHitStruct();
            structScenario = structScenarios.SimpleHitStruct;

            timerScenario = structScenarios.TimerScenario;
        }
    }
}