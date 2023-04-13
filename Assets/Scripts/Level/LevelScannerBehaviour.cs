using UnityEngine;
using Color = UnityEngine.Color;

namespace Level
{
    public class LevelScannerBehaviour : MonoBehaviour
    {
        private Color newColor;
        private Renderer _renderer;
        public ColorNames currentScannerHueColor = ColorNames.Red;
        
        private void Awake()
        {
            _renderer = gameObject.GetComponent<Renderer>();
            newColor = currentScannerHueColor.ConvertColor();
            newColor.a = 0.5019608f;
            _renderer.material.color = newColor;
        }
    }
}