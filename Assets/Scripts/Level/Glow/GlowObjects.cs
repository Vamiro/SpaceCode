using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GlowObjects : MonoBehaviour
{
    public IEnumerator coroutineOffGlow;
    public IEnumerator coroutineOnGlow;

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
            coroutineOffGlow = OffGlowing(_m_MaterialPropertyBlock);
            coroutineOnGlow = OnGlowing(_m_MaterialPropertyBlock);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TargetObject" && _m_Renderer != null)
        {
            StopCoroutine(coroutineOffGlow);
            VInHSV = 0.84f;
            ChangeEmission();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TargetObject" && _m_Renderer != null)
        {
            StartCoroutine(coroutineOffGlow);
        }
    }

    private IEnumerator OffGlowing(MaterialPropertyBlock materialPropertyBlock)
    {
        while (VInHSV >= 0.64f)
        {
            VInHSV -= 0.01f;
            ChangeEmission();
            yield return new WaitForSeconds(0.05f);
        }
        VInHSV = 0.64f;
        ChangeEmission();
        StopCoroutine(coroutineOffGlow);
    }
    private IEnumerator OnGlowing(MaterialPropertyBlock materialPropertyBlock)
    {
        StopCoroutine(coroutineOffGlow);
        while (VInHSV <= 0.84f)
        {
                VInHSV += 0.01f;
                ChangeEmission();
                yield return new WaitForSeconds(0.05f);
        }
        VInHSV = 0.84f;
        ChangeEmission();
        StopCoroutine(coroutineOnGlow);
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
