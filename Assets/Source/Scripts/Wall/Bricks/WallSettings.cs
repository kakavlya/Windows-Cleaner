using UnityEngine;
using UnityEngine.Serialization;

namespace WindowsCleaner.WallNs
{
    [CreateAssetMenu(fileName = "WallSettings", menuName = "Game/Wall Settings")]
    public class WallSettings : ScriptableObject
    {
        [Header("Wall Size")]
        [FormerlySerializedAs("rowsCurve")]
        public AnimationCurve RowsCurve;
        [FormerlySerializedAs("columnsCurve")]
        public AnimationCurve ColumnsCurve;

        public int GetRows(int level) => Mathf.RoundToInt(RowsCurve.Evaluate(level));
        public int GetColumns(int level) => Mathf.RoundToInt(ColumnsCurve.Evaluate(level));
    }
}