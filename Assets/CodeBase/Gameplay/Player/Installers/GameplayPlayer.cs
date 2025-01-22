using Gameplay.Model;
using Gameplay.Player.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Player.Installers
{
    public class GameplayPlayer : MonoInstaller
    {
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
