using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.View.HUD
{
    [RequireComponent(typeof(Button))]
    public class UIGrabItUpHUDButtonPresenter : MonoBehaviour
    {
        public IObservable<Unit> OnUpClicked => _buttonUp.OnClickAsObservable();
        
        private Button _buttonUp;

        private void Awake()
        {
            _buttonUp = GetComponent<Button>();
        }
    }
}
