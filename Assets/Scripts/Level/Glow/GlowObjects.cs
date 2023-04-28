using System.Collections;
using UnityEngine;

namespace Level
{
    public class GlowObjects : MonoBehaviour
    {
        public IEnumerator coroutineOffGlow;
        public IEnumerator coroutineOnGlow;
        public bool _isOffGlowInProcess = false;
        public bool _isOnGlowInProcess = false;

        private MaterialPropertyBlock _m_MaterialPropertyBlock;

        private Renderer _m_Renderer;

        float HInHSV = 0f;
        float SInHSV = 0.64f;
        float VInHSV = 0.64f;

        private void Awake()
        {
            if (gameObject.GetComponent<Renderer>() != null)
            {
                _m_Renderer = gameObject.GetComponent<Renderer>();

                if (_m_MaterialPropertyBlock == null)
                {
                    _m_MaterialPropertyBlock = new MaterialPropertyBlock();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "TargetObject" && _m_Renderer != null)
            {
                CancelInvoke("OffGlowing");
                VInHSV = 0.84f;
                ChangeEmission();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "TargetObject" && _m_Renderer != null)
            {
                Invoke("OffGlowing", 0f);
            }
        }

        private void OffGlowing()
        {
            CancelInvoke("OnGlowing");
            if (VInHSV <= 0.64f)
            {
                VInHSV = 0.64f;
                ChangeEmission();
                CancelInvoke();
                return;
            }
            else
            {
                VInHSV -= 0.0005f;
                ChangeEmission();
                Invoke("OffGlowing", 0.01f);
            }
        }

        public void OnGlowing()
        {
            CancelInvoke("OffGlowing");
            if (VInHSV >= 0.84f)
            {
                VInHSV = 0.84f;
                ChangeEmission();
                CancelInvoke();
                return;
            }
            else
            {
                VInHSV += 0.0008f;
                ChangeEmission();
                Invoke("OnGlowing", 0.01f);
            }
        }

        private void ChangeEmission()
        {
            _m_MaterialPropertyBlock.SetColor("_EmissionColor", Color.HSVToRGB(HInHSV, SInHSV, VInHSV));
            _m_Renderer.SetPropertyBlock(_m_MaterialPropertyBlock);
        }

        public void ChangeEmission(float HInHSV, float SInHSV, float VInHSV)
        {
            _m_MaterialPropertyBlock.SetColor("_EmissionColor", Color.HSVToRGB(HInHSV, SInHSV, VInHSV));
            _m_Renderer.SetPropertyBlock(_m_MaterialPropertyBlock);
        }
    }
}
