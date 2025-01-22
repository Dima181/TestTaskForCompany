using Gameplay.Model;
using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public class GameplayObject : MonoInstaller
    {
        public EObject Id => _id;
        [SerializeField] private EObject _id;

        public override void InstallBindings()
        {
            MonoInstaller[] installers = gameObject.GetComponents<MonoInstaller>();

            foreach (var installer in installers)
            {
                if (installer == this || installer == null)
                    continue;

                Debug.Log(installer.ToString());
                Container.Inject(installer);
                installer.InstallBindings();
            }
        }
    }
}
