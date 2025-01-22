using Cysharp.Threading.Tasks;
using System;
using Zenject;

namespace Infrastructure.Pipeline.DataProviders
{
    public abstract class LocalDataProvider<T> : ILocalDataProvider
    {
        Type IDataProvider.ModelType => typeof(T);
        async UniTask<object> ILocalDataProvider.Load(DiContainer di, DisposableManager disposableManager) =>
            await Load(di, disposableManager);

        protected abstract UniTask<T> Load(DiContainer di, DisposableManager disposableManager);
    }
    
    public abstract class LocalSingleDataProvider<T> : ILocalDataProvider
    {
        Type IDataProvider.ModelType => typeof(T);
        async UniTask<object> ILocalDataProvider.Load(DiContainer di, DisposableManager disposableManager) =>
            await Load(di, disposableManager);

        protected abstract UniTask<T> Load(DiContainer di, DisposableManager disposableManager);
    }
}
