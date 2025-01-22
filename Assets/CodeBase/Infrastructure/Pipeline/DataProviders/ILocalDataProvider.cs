using Cysharp.Threading.Tasks;
using Zenject;

namespace Infrastructure.Pipeline.DataProviders
{
    public interface ILocalDataProvider : IDataProvider
    {
        UniTask<object> Load(DiContainer di, DisposableManager disposableManager);
    }
}
