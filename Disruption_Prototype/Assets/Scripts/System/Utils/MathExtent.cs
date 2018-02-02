using UnityEngine;

namespace Utils
{
    public class MathExtent
    {
        /// <summary>
        /// Returns the orientation in deg of a vector3 in the plane x,z in a 3D worldspace
        /// </summary>
        /// <param name="v">Direction</param>
        public static float VectorToAngle(Vector3 v)
        {
            v.Normalize();

            float orientation = Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;

            if (v.x < 0f)
                orientation += 180f;

            return orientation;
        }

        /// <summary>
        /// Returns a Vector3 normalized pointing to the angle alpha on a x,z plane in a 3D worldspace
        /// </summary>
        /// <param name="alpha">orientation</param>
        public static Vector3 AngleToVector(float alpha)
        {
            alpha *= Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(alpha), 0f, Mathf.Sin(alpha));
        }

        /// <summary>
        /// Clips a given value
        /// </summary>
        public static float Clip(float current, float max, bool isAngle = false)
        {
            if (isAngle)
                return ClipAngle(current, max);

            if (current > max)
                return max;
            else
                return current;
        }

        /// <summary>
        /// Clips a given value
        /// </summary>
        public static Vector3 Clip(Vector3 current, float max)
        {
            if (current.magnitude < max)
                return current.normalized * max;
            else
                return current;
        }

        /// <summary>
        /// Returns a random value between -1 to 1
        /// </summary>
        public static float binomial()
        {
            return Random.value - Random.value;
        }

        private static float ClipAngle(float currentAngle, float maxAngle)
        {
            if (Mathf.Abs(currentAngle) > maxAngle)
                return maxAngle * Mathf.Sign(currentAngle);
            else
                return currentAngle;
        }

    }
}
