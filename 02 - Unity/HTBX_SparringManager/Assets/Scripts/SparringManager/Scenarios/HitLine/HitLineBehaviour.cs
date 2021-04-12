﻿using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.Scenarios.HitLine
{
    /* Class of the HitLine Prefab
     * 
     *  Summary :
     *  This class leads the behaviour of the HitLine prefab.
     *  The Line only moves lateraly and it instantiates the hit after _timeBeforeHit seconds. 
     *  
     *  Attributs :
     *      float _lineVelocity : Acceleration at a tTime of the Line
     *      int _deltaTimeChangeVelocity : Time during which the line will keep tis acceleration
     *      float _timeBeforeHit : Time when the hit will be setted
     *      float _deltaHit : Time during which the player will be able to hit the line
     *      bool _hitted : Boolean that indicates fi the line is hitted or not
     *      bool _fixPosHit : Boolean that indicates if the line stop during the hit
     *      int _fixPosHitValue : if the boolean _fixPoshit is true we fix the value to 0 in order to have an acceleration null
     *      float _startTimeScenario : absolut time of the beginning of the scenario
     *      float _tTime : tTime
     *      
     *  Methods :
     *      void MoveLine(int lineAcceleration) : moves the line at the lineAcceleration
     *      Void LineInCameraRange() : Verifie that the line stay in the camera range
     *      void SetHit() : Indicates when the playe can hit by changing the color of the line
     */


    /// <summary>
    /// Manage the behaviour of the HitLine.
    /// </summary>
    /// <remarks>Essentialy it moves the line, instantiates the hit and it makes sure that the line stays in the range of the camera</remarks>
    /// <inheritdoc cref="ScenarioDisplayBehaviour"/>
    public class HitLineBehaviour : ScenarioDisplayBehaviour
    {

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            ObjectInCameraRange();
            MoveObject(fixPosHitValue * objectVelocity);
            SetHit(this.gameObject);
        }

        protected override void SetHit(GameObject LineObject)
        {
            //change the color of the line if the player have to hit
            if (TimeToHit && hitted == false) // warning if hitLine controller == instantiated 2 times -> problem need to be solved
            {
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else
            {
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
    }
}
