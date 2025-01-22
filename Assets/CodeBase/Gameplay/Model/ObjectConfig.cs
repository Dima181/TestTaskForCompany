using UniRx;

namespace Gameplay.Model
{
    public class ObjectConfig
    {
        public ReactiveProperty<float> Mass { get; }

        public ObjectConfig(float mass) => 
            Mass = new(mass);
    }
}
