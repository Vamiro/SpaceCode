using MG_BlocksEngine2.Core;
using UnityEngine;

namespace Level
{
    public class LevelItemBehaviour : MonoBehaviour, IResetable
    {
        private ItemStorage _itemStorage;
        private Color _color;
        private Transform _transform;
        
        private void Awake()
        {
            if (transform.GetComponent<IResetColor>() != null)
            {
                _color = (_transform = transform).GetComponent<Renderer>().material.color;
                _itemStorage = new ItemStorage(_transform, _color);
               
            }
            else
            {
                _itemStorage = new ItemStorage(transform);
            }
            BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnStop, Reset);
        }
        
        private void OnDestroy()
        {
            BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnStop, Reset);
        }
        
        public void Reset()
        {
            if(!LevelManager.Instance.IsFinished) _itemStorage.ResetItem();
        }
    }
}
