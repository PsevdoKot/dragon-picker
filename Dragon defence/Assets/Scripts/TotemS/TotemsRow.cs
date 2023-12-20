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

    public int totemsCount = 6;
    [SerializeField] public float timeToTotemAppear { get; private set; } = 2f;
    private readonly Dictionary<int, float> xTotemLocalPosByPlaceId = new()
        { {0, 0}, {1, 0.85f}, {2, 1.75f}, {3, 2.65f}, {4, 3.6f}, {5, 4.55f} };

    void Start()
    {
        Totems = new Totem[totemsCount];
        Instance = this;
    }

    public void AddTotem(TotemType type, int placeId)
    {
        GameObject totemPrefab = type switch
        {
            TotemType.Water => waterTotemPrefab,
            TotemType.Fire => fireTotemPrefab,
            TotemType.Air => airTotemPrefab,
            TotemType.Earth => earthTotemPrefab,
            _ => throw new System.Exception("The new totem type has not been processed"),
        };
        GameObject totemGO = Instantiate(totemPrefab, transform);
        totemGO.transform.localPosition = new Vector3(xTotemLocalPosByPlaceId[placeId], -5, 0);
        StartCoroutine(totemGO.MoveObjectSmoothly(new Vector3(totemGO.transform.localPosition.x, 0, 0), timeToTotemAppear));

        var totem = totemGO.GetComponent<Totem>();
        totem.placeId = placeId;
        Totems[placeId] = totem;

        TotemButtons.Instance.HideButton(placeId);

        var manaCost = totem.type switch
        {
            TotemType.Water => WaterTotem.ManaCost,
            TotemType.Fire => FireTotem.ManaCost,
            TotemType.Air => AirTotem.ManaCost,
            TotemType.Earth => EarthTotem.ManaCost,
            _ => throw new System.Exception("The new totem type has not been processed"),
        };

        StartCoroutine(Player.Instance.PlaceTotem(manaCost));

        AudioManager.Instance.Play("totem-apperance");
    }

    public List<int> GetExistingTotemsId()
    {
        var res = new List<int>();
        for (var i = 0; i < totemsCount; i++)
        {
            var totem = Totems[i];
            if (!totem.IsUnityNull())
            {
                res.Add(totem.placeId);
            }
        }
        return res;
    }

    public void DestroyTotem(int placeId)
    {
        TargetSelection.Instance.InterruptTargetSelection(placeId);
        TotemButtons.Instance.ShowButton(placeId);
        Destroy(Totems[placeId].gameObject);
    }
}
