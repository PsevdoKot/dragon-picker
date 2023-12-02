using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TotemsRow : MonoBehaviour
{
    public static Totem[] Totems { get; private set; }
    public static TotemsRow Instance { get; private set; }

    [SerializeField] private GameObject waterTotemPrefab;
    [SerializeField] private GameObject fireTotemPrefab;
    [SerializeField] private GameObject airTotemPrefab;
    [SerializeField] private GameObject earthTotemPrefab;

    [SerializeField] public float timeToTotemAppear { get; private set; } = 2f;
    private readonly Dictionary<int, float> xTotemLocalPosByPlaceId = new()
        { {0, 0}, {1, 0.85f}, {2, 1.75f}, {3, 2.65f}, {4, 3.6f}, {5, 4.55f} };

    void Start()
    {
        Totems = new Totem[6];
        Instance = this;
    }

    public void AddTotem(TotemType type, int placeId)
    {
        GameObject totemPrefab = type switch
        {
            TotemType.Fire => fireTotemPrefab,
            TotemType.Air => airTotemPrefab,
            TotemType.Earth => earthTotemPrefab,
            TotemType.Water => waterTotemPrefab,
            _ => throw new System.Exception("Wrong totem type to create"),
        };
        GameObject totemGO = Instantiate(totemPrefab, transform);
        totemGO.transform.localPosition = new Vector3(xTotemLocalPosByPlaceId[placeId], -5, 0);
        StartCoroutine(totemGO.MoveObjectSmoothly(new Vector3(totemGO.transform.localPosition.x, 0, 0), timeToTotemAppear));

        var totem = totemGO.GetComponent<Totem>();
        totem.placeId = placeId;
        Totems[placeId] = totem;

        TotemButtons.Instance.HandleTotemAppearance(placeId);

        Player.Instance.PlaceTotem(totem.manaCost);
    }

    public void DestroyTotem(int placeId)
    {
        // var totem = Totems[placeId];
        // Totems[placeId].AsUnityNull();
        TotemButtons.Instance.HandleTotemDestroy(placeId);
        Destroy(Totems[placeId]);
    }
}
