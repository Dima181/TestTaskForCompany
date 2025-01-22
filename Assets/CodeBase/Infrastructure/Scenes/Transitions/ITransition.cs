using Cysharp.Threading.Tasks;
using System;

namespace Infrastructure.Scenes.Transitions
{
    public interface ITransition
    {
        public ETransition Type { get; }

        UniTask ApplyTransition(Func<UniTask> func);
    }
}