using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    public TotemType currentCursorType { get; private set; } // water = standart

    [SerializeField] private Texture2D standartCursor;
    [SerializeField] private Texture2D fireCursor;
    [SerializeField] private Texture2D airCursor;
    [SerializeField] private Texture2D earthCursor;

    private int fireballTargetAreaMask;
    private Dictionary<TotemType, Texture2D> cursorByType;

    void Start()
    {
        Instance = this;

        currentCursorType = TotemType.Water;
        cursorByType = new() { {TotemType.Water, standartCursor}, {TotemType.Fire, fireCursor},
                               {TotemType.Air, airCursor}, {TotemType.Earth, earthCursor} };
        fireballTargetAreaMask = LayerMask.NameToLayer("fireball-target-area");
        fireballTargetAreaMask = 1 << fireballTargetAreaMask;

        Cursor.SetCursor(standartCursor, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        var targeting = TargetSelection.Instance;
        if (Input.GetMouseButtonDown(0) && targeting.isOccupied && targeting.initiatorType == TotemType.Fire)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out RaycastHit hit, 50f, fireballTargetAreaMask))
            {
                var temp = new GameObject("temp-fireball-target");
                temp.transform.position = hit.point;
                targeting.EndTargetSelection(temp);
            }
        }
    }

    public void ChangeCursorType(TotemType type)
    {
        if (type == currentCursorType) return;

        currentCursorType = type;
        Cursor.SetCursor(cursorByType[type], Vector2.zero, CursorMode.Auto);
    }
}
