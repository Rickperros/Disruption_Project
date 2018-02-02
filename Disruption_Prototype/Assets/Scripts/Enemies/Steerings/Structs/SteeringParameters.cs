using UnityEngine;

namespace Steerings
{
    public enum EFacingPolicy { LWYG, LWYGI, FT, FTI, NONE };

    public struct SSeekParameters
    {
        public EFacingPolicy m_facingPolicy;
        public Transform m_target;
    }

    public struct SArriveParameters
    {
        public EFacingPolicy m_facingPolicy;
        public Transform m_target;
        public float m_closeEnoughRadius;
        public float m_slowDownRadius;
        public float m_timeToDesiredSpeed;
    }

    public struct SVelocityMatchingParameters
    {
        public EFacingPolicy m_facingPolicy;
        public Vector3 m_targetLinearVelocity;
        public float m_timeToDesiredVelocity;
    }

    public struct SAlignParameters
    {
        public Transform m_target;
        public float m_closeEnoughAngle;
        public float m_slowDownAngle;
        public float m_timeToDesiredAngularSpeed;
    }

    public struct SInterceptParameters
    {
        public EFacingPolicy m_facingPolicy;
        public float m_maxPredictionTime;
        public Vector3 m_targetLinearVelocity;
        public Transform m_target;
    }

    public struct SEvadeParameters
    {
        public EFacingPolicy m_facingPolicy;
        public float m_maxPredictionTime;
        public Vector3 m_targetLinearVelocity;
    }

    public struct SNaiveWanderParameters
    {
        public EFacingPolicy m_facingPolicy;
        public float m_wanderRate;
    }

    public struct SWanderParameters
    {
        public EFacingPolicy m_facingPolicy;
        public float m_wanderRate;
        public float m_wanderRadius;
        public float m_wanderOffset;
        public float m_targetOrientation;
    }

    public struct SSingleWhiskerObstacleAvoidanceParameters
    {
        public EFacingPolicy m_facingPolicy;
        public float m_whiskerLenght;
        public float m_avoidDistance;
    }

    public struct SMultipleWhiskerObstacleAvoidanceParameters
    {
        public EFacingPolicy m_facingPolicy;
        public float m_primaryWhiskerLenght;
        public float m_avoidDistance;
        public float m_secondaryWhiskerRatio;
        public float m_secondaryWhiskerAngle;
    }

    public struct SWanderAroundParameters
    {
        public float m_seekWeight;
    }


}
