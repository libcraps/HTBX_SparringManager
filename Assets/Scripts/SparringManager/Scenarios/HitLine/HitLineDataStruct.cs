using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.DataManager.HitLine
{
    public struct HitLineDataStruct
    {
        public bool hitted;
        public float reactionTime;
        public List<float> followingTarget;
        public List<float> timeListSession;

        public HitLineDataStruct(bool hitted, float reactionTime, List<float> followingTarget, List<float> timeListSession)
        {
            this.hitted = hitted;
            this.reactionTime = reactionTime;
            this.timeListSession = timeListSession;
            this.followingTarget = followingTarget;
        }

        public override string ToString()
        {
            Debug.Log("HitLineData structure : ");
            Debug.Log("Hitted : " + hitted);
            Debug.Log("Reaction Time : " + reactionTime);
            Debug.Log("List length : " + timeListSession.Count);
            return "HitLine Data";
        }
    }
}
