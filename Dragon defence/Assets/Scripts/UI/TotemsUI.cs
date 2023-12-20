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
        { {0, -180}, {1, -276}, {2, -378}, {3, -476}, {4, -578}, {5, -678} };

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
