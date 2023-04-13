using MG_BlocksEngine2.Core;
using UnityEngine;

namespace Level
{
    public class LevelItemBehaviour : MonoBehaviour
    {
        private ItemStorage _itemStorage;
        private void Awake()
        {
            _itemStorage = new ItemStorage(transform);
            BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnStop, _itemStorage.ResetItem);
        }
        private void OnDestroy()
        {
            BE2_MainEventsManager.Instance.StopListening(BE2EventTypes.OnStop, _itemStorage.ResetItem);
        }
    }
}
