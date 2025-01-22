using Cysharp.Threading.Tasks;
using System;
using Zenject;

namespace Infrastructure.Scenes.Transitions
{
    public class OutBlackScreenTransition : ITransition
    {
        public ETransition Type => ETransition.OutBlackScreen;

        [Inject] private OutBlackScreenTransitionView _view;

        public async UniTask ApplyTransition(Func<UniTask> func)
        {
            await _view.Prepare();
            await func();
            await _view.Apply();
        }
    }
}