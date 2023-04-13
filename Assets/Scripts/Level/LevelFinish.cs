using UnityEngine;

namespace Level
{
    public class LevelFinish : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "TargetObject")
            {
                LevelManager.Instance.LevelPassed();
            }
        }
    }
}
