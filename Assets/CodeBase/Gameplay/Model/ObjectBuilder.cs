namespace Gameplay.Model
{
    public class ObjectBuilder
    {
        private EObject _id;
        private string _name;
        private ObjectConfig _config;

        public ObjectBuilder WithId(EObject id)
        {
            _id = id;
            return this;
        }

        public ObjectBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ObjectBuilder WithConfig(ObjectConfig config)
        {
            _config = config;
            return this;
        }

        public ObjectModel Build()
        {
            return new ObjectModel(
                _id,
                _name,
                "Description",
                _config);
        }
    }
}
