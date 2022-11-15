using UnityEngine;

public abstract class UIPanelCore : MonoBehaviour
{
    protected void ShowInternal()
    {
        gameObject.SetActive(true);
        OnShow();
    }

    protected abstract void OnShow();

    public void Close()
    {
        OnClose();
        gameObject.SetActive(false);
    }

    protected abstract void OnClose();
}

public abstract class UIPanel : UIPanelCore
{
    public void Show()
    {
        ShowInternal();
    }
}

public abstract class UIPanel<Targ> : UIPanelCore
{
    protected Targ _argument;
    public void Show(Targ argument)
    {
        _argument = argument;
        SetUp(_argument);
        ShowInternal();
    }
    protected virtual void SetUp(Targ argument)
    {

    }
}
