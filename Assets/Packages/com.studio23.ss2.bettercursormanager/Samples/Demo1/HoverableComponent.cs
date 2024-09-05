using Studio23.SS2.BetterCursor.Core;
using Studio23.SS2.BetterCursor.Data;
using Studio23.SS2.BetterCursor.Interfaces;
using UnityEngine;

public class HoverableComponent : MonoBehaviour,IHoverable
{
    public CursorData CursorData;
    [SerializeField] private BetterCursor _betterCursor;
    private CursorData _tempData;


    public void OnHoverEnter()
    {
        _tempData = _betterCursor.CurrentCursor;
        _betterCursor.ChangeCursor(CursorData);
        Debug.Log($"<color=gray>{name}</color> : <color=yellow>On Hover Enter</color>");
    }

    public void OnHoverExit()
    {
        _betterCursor.ChangeCursor(_tempData);
        Debug.Log($"<color=gray>{name}</color> : <color=red>On Hover Exit</color>");
    }
}
