using Studio23.SS2.BetterCursor.Core;
using Studio23.SS2.BetterCursor.Data;
using Studio23.SS2.BetterCursor.Interfaces;
using UnityEngine;

public class HoverableComponent : MonoBehaviour,IHoverable
{
    public CursorData CursorData;

    private CursorData _tempData;


    public void OnHoverEnter()
    {
        _tempData = BetterCursor.Instance.CurrentCursor;
        BetterCursor.Instance.ChangeCursor(CursorData);
        Debug.Log($"<color=gray>{name}</color> : <color=yellow>On Hover Enter</color>");
    }

    public void OnHoverExit()
    {
        BetterCursor.Instance.ChangeCursor(_tempData);
        Debug.Log($"<color=gray>{name}</color> : <color=red>On Hover Exit</color>");
    }
}
