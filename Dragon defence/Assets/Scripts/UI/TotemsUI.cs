using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemsUI : MonoBehaviour
{
    public static TotemUI[] TotemsUIs;
    public static TotemsUI Instance;

    [SerializeField] private GameObject totemUIPrefab;

    private int totemsCount = 6;

    private readonly Dictionary<int, float> xTotemLocalPosByPlaceId = new()
        { {0, 270}, {1, 165}, {2, 60}, {3, -50}, {4, -160}, {5, -270} };

    void Start()
    {
        TotemsUIs = new TotemUI[totemsCount];
        Instance = this;
    }

    void Update()
    {

    }

    public void CreateTotemUI(int placeId)
    {
        var totemUIGO = Instantiate(totemUIPrefab, transform);
        totemUIGO.transform.localPosition = new Vector3(xTotemLocalPosByPlaceId[placeId], -500, 0);
        StartCoroutine(totemUIGO.MoveObjectSmoothly(new Vector3(xTotemLocalPosByPlaceId[placeId], 0, 0),
            TotemsRow.Instance.timeToTotemAppear));

        var totemUI = totemUIGO.GetComponent<TotemUI>();
        var totem = TotemsRow.Totems[placeId];
        totemUI.Init(placeId, totem);
        TotemsUIs[placeId] = totemUI;
    }

    public void RemoveTotemUI(int placeId)
    {
        var totemUI = TotemsUIs[placeId];
        Destroy(totemUI.gameObject);
    }
}
