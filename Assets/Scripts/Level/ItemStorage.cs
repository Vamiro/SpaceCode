using UnityEngine;

namespace Level
{
    public class ItemStorage
    {
        private Vector3 _position;
        private Quaternion _rotation;
        private Vector3 _scale;
        private Transform _transform;
        private Color _color = Color.clear;
        private Renderer _rendererl;

        public ItemStorage(Transform transform)
        {
            _transform = transform;
            _position = _transform.position;
            _rotation = _transform.rotation;
            _scale = _transform.localScale;
        }
        
        public ItemStorage(Transform transform, Color color)
        {
            _transform = transform;
            _position = _transform.position;
            _rotation = _transform.rotation;
            _scale = _transform.localScale;
            _color = color;
        }

        public void ResetItem()
        {
            _transform.position = _position;
            _transform.rotation = _rotation;
            _transform.localScale = _scale;
            if (!_color.Equals(Color.clear))
            {
                _transform.GetComponent<Renderer>().material.color = _color;
            }
        }
    }
    
    public interface IResetColor{}

    public interface IResetable
    {
        void Reset();
    }
}