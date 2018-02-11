using UnityEngine;
using System.Collections.Generic;

namespace Steerings
{
    public enum EFacingPolicy { LWYG, LWYGI, FT, FTI, NONE };

    [System.Serializable]
    public struct SSeek
    {
        public EFacingPolicy m_facingPolicy;
        public Transform m_target;
    }

    [System.Serializable]
    public struct SArrive
    {
        public EFacingPolicy m_facingPolicy;
        public Transform m_target;
        public float m_closeEnoughRadius;
        public float m_slowDownRadius;
        public float m_timeToDesiredSpeed;
    }

    [System.Serializable]
    public struct SVelocityMatching
    {
        public EFacingPolicy m_facingPolicy;
        public Vector3 m_targetLinearVelocity;
        public float m_timeToDesiredVelocity;
    }

    [System.Serializable]
    public struct SAlign
    {
        public Transform m_target;
        public float m_closeEnoughAngle;
        public float m_slowDownAngle;
        public float m_timeToDesiredAngularSpeed;
    }

    [System.Serializable]
    public struct SIntercept
    {
        public EFacingPolicy m_facingPolicy;
        public float m_maxPredictionTime;
        public Vector3 m_targetLinearVelocity;
        public Transform m_target;
    }

    [System.Serializable]
    public struct SEvade
    {
        public EFacingPolicy m_facingPolicy;
        public float m_maxPredictionTime;
        public Vector3 m_targetLinearVelocity;
        public Transform m_target;
    }

    [System.Serializable]
    public struct SNaiveWander
    {
        public EFacingPolicy m_facingPolicy;
        public float m_wanderRate;
    }

    [System.Serializable]
    public struct SWander
    {
        public EFacingPolicy m_facingPolicy;
        public float m_wanderRate;
        public float m_wanderRadius;
        public float m_wanderOffset;
        public float m_targetOrientation;
    }

    [System.Serializable]
    public struct SSimpleObstacleAvoidance
    {
        public EFacingPolicy m_facingPolicy;
        public float m_whiskerLenght;
        public float m_avoidDistance;
    }

    [System.Serializable]
    public struct SObstacleAvoidance
    {
        public EFacingPolicy m_facingPolicy;
        public float m_primaryWhiskerLenght;
        public float m_avoidDistance;
        public float m_RatioSecondWhisk;
        public float m_AngleSecondWhisk;
    }

    [System.Serializable]
    public struct SKeepPosition
    {
        public EFacingPolicy m_facingPolicy;
        public Transform m_target;
        public float m_requiredDistance;
        public float m_requiredAngle;
    }

    [System.Serializable]
    public struct SPathFollowing
    {
        public EFacingPolicy m_facingPolicy;
        public float m_closeEnoughRadius;
        public int m_currentWaypointIndex;
        public List<Transform> m_path;
    }

    [System.Serializable]
    public struct SInterpose
    {
        public EFacingPolicy m_facingPolicy;
        public Transform m_target;
        public Transform m_coveredObject;
        [Range(0, 1)]
        [Tooltip("Range between 0, 1 meaning 0 the point to cover and 1 the target position")]
        public float m_distanceToInterpose;

    }
}
