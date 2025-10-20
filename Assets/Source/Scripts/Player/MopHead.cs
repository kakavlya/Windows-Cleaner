using UnityEngine;
using WindowsCleaner.AudioNs;

namespace WindowsCleaner.PlayerNs
{
    public class MopHead : MonoBehaviour
    {
        [SerializeField] private AudioClip _growSound;

        public void WidthUp(float widthScale)
        {
            var curLocalScale = this.transform.localScale;
            var localXUpdated = new Vector3(
                curLocalScale.x * widthScale,
                curLocalScale.y,
                curLocalScale.z);
            this.transform.localScale = localXUpdated;

            Audio.Instance.PlaySfx(_growSound);
        }
    }
}