using UnityEngine;
using DG.Tweening;
using TMPro;

namespace WeatherDogs
{
    public class LoadingPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform _paw;
        [SerializeField] private TextMeshProUGUI _textFact;

        private Tween _rotationTween;

        private void Start()
        {
            StartSpinning();
        }

        public void Initialize(string fact)
        {
            Debug.Log(fact);
            _textFact.text = fact;
        }

        public void DestroyPanel()
        {
            StopSpinning();
            Destroy(gameObject);
        }

        private void StartSpinning()
        {
            _rotationTween?.Kill();

            _rotationTween = _paw
                .DORotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1); 
        }

        private void StopSpinning()
        {
            _rotationTween?.Kill();
            _paw.rotation = Quaternion.identity;
        }
    }
}

