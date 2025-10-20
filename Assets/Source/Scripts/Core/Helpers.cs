using UnityEngine;

namespace WindowsCleaner.Core
{
    public class Helpers
    {
        public static Vector3 GetRandomVector3(float spread)
        {
            return new Vector3(
                GetRandomPosNegSpread(spread),
                Random.Range(-spread, spread),
                GetRandomPosNegSpread(spread));
        }

        public static Quaternion GetRandomRotation(float maxRandomRotationAngle)
        {
            Vector3 randomEulerRotation = new Vector3(
                Random.Range(0f, maxRandomRotationAngle),
                Random.Range(0f, maxRandomRotationAngle),
                Random.Range(0f, maxRandomRotationAngle));

            return Quaternion.Euler(randomEulerRotation);
        }

        public static Vector3 GetRandomXZPos(float spread)
        {
            return new Vector3(Random.Range(-spread, spread), 0f, Random.Range(-spread, spread));
        }

        public static float GetRandomPosNegSpread(float spread)
        {
            return Random.Range(-spread, spread);
        }

        public static Vector3 GetRandomPos(float spread)
        {
            return new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
        }

        public static Vector3 GetRandomPosXY(float spread)
        {
            return new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0f);
        }

        public Vector3 GetRandomPositiveDirection(float minMagnitude, float maxMagnitude)
        {
            Vector3 randomDirection = Random.onUnitSphere;

            float randomMagnitude = Random.Range(minMagnitude, maxMagnitude);

            Vector3 randomPositiveDirection = randomDirection.normalized * randomMagnitude;

            return randomPositiveDirection;
        }
    }
}