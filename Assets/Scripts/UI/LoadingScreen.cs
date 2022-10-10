using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LoadingScreen : UIPanel
{
    [SerializeField] private TMP_Text _loadingText;

    protected override void OnShow()
    {
        StartCoroutine(ShowLoadnig());
    }

    protected override void OnClose()
    {
    }

    protected IEnumerator ShowLoadnig()
    {
        var str = "Loading";
        while (true)
        {
            _loadingText.text = str + new string('.', Mathf.FloorToInt(Time.time * 2) % 4);
            yield return new WaitForSeconds(0.5f);
        }
    }

}
