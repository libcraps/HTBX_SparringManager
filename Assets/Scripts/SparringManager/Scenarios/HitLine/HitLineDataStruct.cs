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
    }
}
