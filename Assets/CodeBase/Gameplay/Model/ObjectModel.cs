using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Gameplay.Model
{
    public class ObjectModel
    {
        public EObject Id => _id;
        public string Name => _name;
        public string Description => _description;
        public ObjectConfig Config => _config;
        public IReadOnlyReactiveProperty<float> CurrentMass => _config.Mass;


        private readonly EObject _id;
        private readonly string _name;
        private readonly string _description;
        private readonly ObjectConfig _config;

        public ObjectModel(
            EObject id,
            string name,
            string description,
            ObjectConfig config)
        {
            _id = id;
            _name = name;
            _description = description;
            _config = config;
        }
    }
}
