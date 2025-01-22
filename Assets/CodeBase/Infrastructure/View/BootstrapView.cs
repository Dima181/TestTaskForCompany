using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.View
{
    public class BootstrapView : MonoBehaviour
    {
        [SerializeField] private Slider _progressFill;
        [SerializeField] private TextMeshProUGUI _progressText;

        public void SetProgress(float progress)
        {
            _progressFill.value = progress;
            _progressText.text = ((int)(progress * 100)) + "%";
        }
    }
}
