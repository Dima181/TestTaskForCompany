using Cysharp.Threading.Tasks;
using Infrastructure.Scenes;
using System;

namespace Infrastructure.Scenes.Transitions
{
    public class NoTransition : ITransition
    {
        public ETransition Type => ETransition.None;
        public async UniTask ApplyTransition(Func<UniTask> func) =>
            await func();
    }
}