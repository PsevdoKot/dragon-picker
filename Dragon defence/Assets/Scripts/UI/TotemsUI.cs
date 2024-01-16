using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemsUI : MonoBehaviour
{
    public static TotemUI[] TotemsUIs;
    public static TotemsUI Instance;

    [SerializeField] private GameObject totemUIPrefab;

    [SerializeField] private Vector2 offset;

    private int totemsCount = 6;

    private readonly Dictionary<int, float> xTotemLocalPosByPlaceId = new()
        { {0, 475}, {1, 380}, {2, 285}, {3, 190}, {4, 95}, {5, 0} };

    void Start()
    {
        TotemsUIs = new TotemUI[totemsCount];
        Instance = this;
    }

    void Update()
    {

    }

    public IEnumerator CreateTotemUI(int placeId)
    {
        var totemUIGO = Instantiate(totemUIPrefab, transform);
        totemUIGO.transform.localPosition = new Vector3(xTotemLocalPosByPlaceId[placeId], 0, 0);

        // var totemUIGO = Instantiate(totemUIPrefab, transform); x -85 xsize 0.97
        // var rectTransform = totemUIGO.GetComponent<RectTransform>();
        // var totemXLocalPos = TotemsRow.Instance.xTotemLocalPosByPlaceId[placeId];
        // var totemPos = TotemsRow.Instance.transform.position + new Vector3(totemXLocalPos, 0, 0);
        // var totemPosOnScreen = Camera.main.WorldToScreenPoint(totemPos);
        // rectTransform.anchoredPosition = new Vector3(totemPosOnScreen.x + offset.x, totemPosOnScreen.y + offset.y, 0);

        totemUIGO.SetActive(false);
        yield return new WaitForSecondsRealtime(TotemsRow.Instance.timeToTotemAppear);
        totemUIGO.SetActive(true);

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
