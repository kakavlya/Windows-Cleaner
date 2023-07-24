using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers 
{
    public static Vector3 GetRandomVector3(float spread)
    {
        return new Vector3(
            GetRandomPosNegSpread(spread),
            Random.Range(-spread, spread),
            GetRandomPosNegSpread(spread));
    }

    public Vector3 GetRandomPositiveDirection(float minMagnitude, float maxMagnitude)
    {
        // Generate a random point on the surface of a unit sphere
        Vector3 randomDirection = Random.onUnitSphere;

        // Randomize the magnitude of the direction within the specified range
        float randomMagnitude = Random.Range(minMagnitude, maxMagnitude);

        // Normalize the direction and scale it by the random magnitude
        Vector3 randomPositiveDirection = randomDirection.normalized * randomMagnitude;

        return randomPositiveDirection;
    }

    public static Quaternion GetRandomRotation(float maxRandomRotationAngle)
    {
        // Generate a random Euler rotation
        Vector3 randomEulerRotation = new Vector3(Random.Range(0f, maxRandomRotationAngle),
                                                  Random.Range(0f, maxRandomRotationAngle),
                                                  Random.Range(0f, maxRandomRotationAngle));

        // Convert the Euler rotation to a Quaternion
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
}
