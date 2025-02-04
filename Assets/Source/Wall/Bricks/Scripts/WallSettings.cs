using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WallSettings", menuName ="Game/Wall Settings")]
public class WallSettings : ScriptableObject
{
    [Header("Wall Size")]
    public AnimationCurve rowsCurve;
    public AnimationCurve columnsCurve;

    public int GetRows(int level) => Mathf.RoundToInt(rowsCurve.Evaluate(level));
    public int GetColumns(int level) => Mathf.RoundToInt(columnsCurve.Evaluate(level));
}
