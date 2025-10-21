using UnityEngine;

namespace WindowsCleaner.Core
{
    public class Helpers
    {
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

        public static Vector3 GetRandomPos(float spread)
        {
            return new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
        }

        public static Vector3 GetRandomPosXY(float spread)
        {
            return new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0f);
        }
    }
}