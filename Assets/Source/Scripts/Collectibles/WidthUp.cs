using UnityEngine;
using WindowsCleaner.PlayerNs;

namespace WindowsCleaner.Collectibles
{
    public class WidthUp : MonoBehaviour
    {
        [SerializeField] private float _widthBuff;

        private void Start()
        {
            _widthBuff = _widthBuff == 0 ? 1.2f : _widthBuff;
        }

        private void OnTriggerEnter(Collider other)
        {

            var mopHead = other.GetComponent<MopHead>();
            if (mopHead)
            {
                Remove();
                mopHead.WidthUp(_widthBuff);

            }
        }

        private void Remove()
        {
            Destroy(gameObject);
        }
    }
}