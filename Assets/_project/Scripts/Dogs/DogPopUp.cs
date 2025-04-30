using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;


namespace WeatherDogs
{
    public class DogPopUp : MonoBehaviour
    {
        [SerializeField] private RectTransform _popup;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private TextMeshProUGUI _textDescription;
        [SerializeField] private Button _closeButton;

        private Tween _showTween;
        private Tween _hideTween;

        private void Awake()
        {
            _popup.localScale = Vector3.zero;
            _canvasGroup.alpha = 0;
            _popup.gameObject.SetActive(false);

            _closeButton.onClick.AddListener(Hide);
        }

        public void Initialize(string title, string description)
        {
            _popup.gameObject.SetActive(true);
            _textName.text = title;
            _textDescription.text = description;

            _showTween = DOTween.Sequence()
                .Append(_popup.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack))
                .Join(_canvasGroup.DOFade(1f, 0.2f));
        }

        public void Hide()
        {
            _hideTween?.Kill();
            _showTween?.Kill();

            _hideTween = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(0f, 0.2f))
                .Join(_popup.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack))
                .OnComplete(() => DestoyPanel());
        }

        private void DestoyPanel() 
        { 
            _closeButton.onClick.RemoveAllListeners();
            Destroy(gameObject); 
        }
    }
}

