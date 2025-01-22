using Cysharp.Threading.Tasks;
using Gameplay.Model;
using Infrastructure.Pipeline.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Gameplay.Objects.Services
{
    public class LocalObjectsDataProvider : LocalDataProvider<ObjectsCollection>
    {
        protected override async UniTask<ObjectsCollection> Load(
            DiContainer di,
            DisposableManager disposableManager)
        {
            await UniTask.CompletedTask;
            var result = new List<ObjectModel>();

            result.Add(AddEasyObjects());
            result.Add(AddMeadleObjects());
            result.Add(AddHeavyObjects());

            return new ObjectsCollection(result.ToDictionary(builder => builder.Id));
        }


        private ObjectModel AddEasyObjects()
        {
            ObjectConfig stateConfig = new(3f);

            return new ObjectBuilder()
                .WithId(EObject.Easy)
                .WithName("Easy")
                .WithConfig(stateConfig)
                .Build();
        }
        
        private ObjectModel AddMeadleObjects()
        {
            ObjectConfig stateConfig = new(6f);

            return new ObjectBuilder()
                .WithId(EObject.Middle)
                .WithName("Middle")
                .WithConfig(stateConfig)
                .Build();
        }
        
        private ObjectModel AddHeavyObjects()
        {
            ObjectConfig stateConfig = new(15f);

            return new ObjectBuilder()
                .WithId(EObject.Heavy)
                .WithName("Heavy")
                .WithConfig(stateConfig)
                .Build();
        }
    }
}
