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
        float timerScenario { get; }
        float startTimeScenario { get;}
    }

    /// <summary>
    /// Abstract class that manage general scenarios caracterisics and methods.
    /// <para>Mother class of scenarios, also it allows us to stock easily data of scenarios.</para>
    /// </summary>
    /// <typeparam name="StructScenario">Structure that need to be IStructScenario</typeparam>
    public class Scenario : IScenarioClass
    {

        #region Attributs/properties

        private float _startTimeScenario;
        private GeneriqueScenarioStruct _structScenari;
        /// <summary>
        /// Structure that contain scenario parameters
        /// </summary>
        public GeneriqueScenarioStruct structScenari { get { return _structScenari; } }

        /// <summary>
        /// Start time of the scenario
        /// </summary>
        public float startTimeScenario { get { return _startTimeScenario;} }

        /// <summary>
        /// Duration of the scenario
        /// </summary>
        public float timerScenario { get { return structScenari.TimerScenario; } }

        /// <summary>
        /// Velocity of the object
        /// </summary>
        public float speed { get { return structScenari.Speed; } }

        /// <summary>
        /// rythme of the session (hit) 
        /// </summary>
        public float rythme { get { return structScenari.Rythme; } }

        public int timeBeforeHit = 2;

        public int deltaHit = 2;
        #endregion

        #region Methods
        /// <summary>
        /// Initialize scenario settings
        /// </summary>
        /// <param name="structScenarios">structure fothe scenai</param>
        public virtual void Init(GeneriqueScenarioStruct structScenarios)
        {
            /*
             * Initialize the structure
             */
            this._structScenari = structScenari;
        }

        /// <summary>
        /// Convert a (x,y,z) consigne in a degree consigne
        /// </summary>
        /// <remarks>The screen is centered/symetrised on 0</remarks>
        /// <param name="screenSize">Size of the screen, it is a reference</param>
        /// <param name="coord">Coordinates</param>
        /// <returns>The angle</returns>
        public virtual object PosToAngle(float screenSize, object coord)
        {
            object angle;
            angle = (float)coord * 180.0 / screenSize;
            return angle;
        }
        #endregion


        /// <summary>
        /// Constructor of the scenario class
        /// </summary>
        /// <param name="structScenari">Settings of the scenari</param>
        public Scenario(GeneriqueScenarioStruct structScenari)
        {
            this._structScenari = structScenari;
            this._startTimeScenario = Time.time;
            Debug.Log(startTimeScenario);
        }
    }
}