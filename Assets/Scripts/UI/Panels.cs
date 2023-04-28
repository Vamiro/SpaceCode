using UnityEngine;

public class Panels : MonoBehaviour
{
    public static Panels Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        CloseAllPanels();
    }

    public void CloseAllPanels()
    {
        var allPanels = GameObject.FindObjectsOfType<UIPanelCore>();
        foreach (var panel in allPanels)
            panel.Close();
    }

    public LoadingScreen loadingScreen;
    public Menu menu;
    public MenuSettnigs menuSettnigs;
    public MenuCredits menuCredits;
    public SaveAndLoad saveAndLoad;
}
