using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsCleaner.Core;

namespace WindowsCleaner.UI
{
    public class DustExplosion : MonoBehaviour
    {
        [SerializeField] private DustResizing _dustObject;
        [SerializeField] private int _amountOfDusts = 10;
        [SerializeField] private float _spread = 5f;
        [SerializeField] private float _maxRandomRotationAngle = 360f;
        [SerializeField] private ParticleSystem _dustParticle;
        [SerializeField] private Vector3 _startPos = new Vector3(10f, 0f, -2f);

        private List<ParticleSystem> _dustParticles;
        private List<DustResizing> _dustObjects;
        private List<Vector3> _positions;
        private float _duration;

        private void Start()
        {
            _dustParticles = new List<ParticleSystem>();
            _dustObjects = new List<DustResizing>();
            _positions = new List<Vector3>();

            CreateRandomPosList();
            CreateDustObjects();
            CreateDustParticles();

        }

        public void PlayEffect()
        {
            foreach (var dust in _dustObjects)
            {
                dust.IncreaseAndDecreaseSizeWithDelay();
            }

            foreach (var dust in _dustParticles)
            {
                dust.Play();
            }

            StartCoroutine(DisableAfterDurationIE(_duration));
        }

        public void SetDuration(float duration)
        {
            _duration = duration;
        }

        private void CreateRandomPosList()
        {
            for (int i = 0; i < _amountOfDusts; i++)
            {
                _positions.Add(_startPos + Helpers.GetRandomPos(_spread));
            }
        }

        private IEnumerator DisableAfterDurationIE(float duration)
        {
            yield return new WaitForSeconds(duration);

            foreach (var dust in _dustParticles)
            {
                dust.Stop();
                dust.gameObject.SetActive(false);
            }
        }

        private void CreateDustObjects()
        {
            for (int i = 0; i < _amountOfDusts; i++)
            {
                GameObject dust = GameObject.Instantiate(_dustObject.gameObject, _positions[i], Helpers.GetRandomRotation(_maxRandomRotationAngle));
                _dustObjects.Add(dust.GetComponent<DustResizing>());
            }
        }

        private void CreateDustParticles()
        {
            for (int i = 0; i < _amountOfDusts; i++)
            {
                var dustParticle = Instantiate(_dustParticle, _positions[i], Quaternion.identity);
                dustParticle.Stop();
                _dustParticles.Add(dustParticle);
            }
        }
    }
}