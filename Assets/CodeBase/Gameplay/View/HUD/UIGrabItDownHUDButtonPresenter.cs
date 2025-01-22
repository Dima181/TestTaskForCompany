using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.View.HUD
{
    [RequireComponent(typeof(Button))]
    public class UIGrabItDownHUDButtonPresenter : MonoBehaviour
    {
        public IObservable<Unit> OnDownClicked => _buttonDown.OnClickAsObservable();
     
        private Button _buttonDown;
        private void Awake()
        {
            _buttonDown = GetComponent<Button>();
        }
    }
}
