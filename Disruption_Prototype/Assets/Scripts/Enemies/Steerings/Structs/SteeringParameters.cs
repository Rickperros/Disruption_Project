using UnityEngine;
using System.Collections.Generic;

namespace Steerings
{
    public enum EFacingPolicy { LWYG, LWYGI, FT, FTI, NONE };

    public struct SSeek
    {
        public EFacingPolicy m_facingPolicy;
        public Transform m_target;
    }

    public struct SArrive
    {
        public EFacingPolicy m_facingPolicy;
        public Transform m_target;
        public float m_closeEnoughRadius;
        public float m_slowDownRadius;
        public float m_timeToDesiredSpeed;
    }

    public struct SVelocityMatching
    {
        public EFacingPolicy m_facingPolicy;
        public Vector3 m_targetLinearVelocity;
        public float m_timeToDesiredVelocity;
    }

    public struct SAlign
    {
        public Transform m_target;
        public float m_closeEnoughAngle;
        public float m_slowDownAngle;
        public float m_timeToDesiredAngularSpeed;
    }

    public struct SIntercept
    {
        public EFacingPolicy m_facingPolicy;
        public float m_maxPredictionTime;
        public Vector3 m_targetLinearVelocity;
        public Transform m_target;
    }

    public struct SEvade
    {
        public EFacingPolicy m_facingPolicy;
        public float m_maxPredictionTime;
        public Vector3 m_targetLinearVelocity;
        public Transform m_target;
    }

    public struct SNaiveWander
    {
        public EFacingPolicy m_facingPolicy;
        public float m_wanderRate;
    }

    public struct SWander
    {
        public EFacingPolicy m_facingPolicy;
        public float m_wanderRate;
        public float m_wanderRadius;
        public float m_wanderOffset;
        public float m_targetOrientation;
    }

    public struct SSimpleObstacleAvoidance
    {
        public EFacingPolicy m_facingPolicy;
        public float m_whiskerLenght;
        public float m_avoidDistance;
    }

    public struct SObstacleAvoidance
    {
        public EFacingPolicy m_facingPolicy;
        public float m_primaryWhiskerLenght;
        public float m_avoidDistance;
        public float m_secondaryWhiskerRatio;
        public float m_secondaryWhiskerAngle;
    }

    public struct SKeepPosition
    {
        public EFacingPolicy m_facingPolicy;
        public Transform m_target;
        public float m_requiredDistance;
        public float m_requiredAngle;
    }

    public struct SPathFollowing
    {
        public EFacingPolicy m_facingPolicy;
        public float m_closeEnoughRadius;
        public int m_currentWaypointIndex;
        public List<Transform> m_path;
    }
}
