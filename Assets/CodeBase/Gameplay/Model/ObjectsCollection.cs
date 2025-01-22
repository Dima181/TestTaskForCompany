using System.Collections;
using System.Collections.Generic;
using UniRx;

namespace Gameplay.Model
{
    public class ObjectsCollection : IEnumerable<ObjectModel>
    {
        private readonly IReadOnlyDictionary<EObject, ObjectModel> _objects;

        public ObjectsCollection(IReadOnlyDictionary<EObject, ObjectModel> buildings) =>
            _objects = buildings;

        public ObjectModel Get(EObject Id) =>
            _objects[Id];

        public IEnumerator<ObjectModel> GetEnumerator() =>
            _objects.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            _objects.Values.GetEnumerator();
    }

    public class PlayerCollection
    {

    }
}
