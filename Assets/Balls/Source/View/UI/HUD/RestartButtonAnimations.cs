using UnityEngine;
using UnityEngine.UI;

namespace Balls.Source.View.UI.HUD
{
    public class RestartButtonAnimations : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _accentColor = new Color(0x66, 0x9A, 0xFF);

        private Color _normalColor;
        
        private void Awake()
        {
            _normalColor = _image.color;
        }

        public void FadeToAccentColor()
        {
            _image.color = _accentColor;
        }

        public void FadeToNormalColor()
        {
            _image.color = _normalColor;
        }
    }
}
