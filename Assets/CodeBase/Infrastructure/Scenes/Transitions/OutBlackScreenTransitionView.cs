using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Infrastructure.Scenes.Transitions
{
    public class OutBlackScreenTransitionView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _alphaGroup;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        public async UniTask Prepare()
        {
            gameObject.SetActive(true);
            _alphaGroup.alpha = 0;
            await UniTask.Yield();
        }

        public async UniTask Apply()
        {
            _alphaGroup.alpha = 1;

            // Используем AsyncWaitForCompletion для корректного ожидания завершения анимации
            await _alphaGroup.DOFade(0, _duration).SetEase(_ease).AsyncWaitForCompletion();

            /*await _alphaGroup.DOFade(0, _duration).SetEase(_ease);// Надо будет проверить работает или нет, я хз что будет если так привести TweenerCore к IEnumerator*/
            gameObject.SetActive(false);
        }

        public void ResetAlpha()
        {
            _alphaGroup.alpha = 0;
        }
    }
}