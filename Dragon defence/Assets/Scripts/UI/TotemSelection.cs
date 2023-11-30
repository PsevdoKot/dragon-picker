using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotemSelection : MonoBehaviour
{
    public static TotemSelection Instance;

    [SerializeField] private GameObject waterTotemPrefab;
    [SerializeField] private GameObject fireTotemPrefab;
    [SerializeField] private GameObject airTotemPrefab;
    [SerializeField] private GameObject earthTotemPrefab;
    [SerializeField] private Button waterBtn;
    [SerializeField] private Button fireBtn;
    [SerializeField] private Button airBtn;
    [SerializeField] private Button earthBtn;

    private int totemPlaceId;
    [SerializeField] private int yTotemLocalPos;
    [SerializeField] private int zTotemLocalPos;

    private readonly Dictionary<int, float> xTotemLocalPosByPlaceId = new()
        { {0, 0}, {1, 0.85f}, {2, 1.75f}, {3, 2.65f}, {4, 3.6f}, {5, 4.55f} };

    void Start()
    {
        Instance = this;
        waterBtn.onClick.AddListener(() => { EndTotemSelection(TotemType.Water); });
        fireBtn.onClick.AddListener(() => { EndTotemSelection(TotemType.Fire); });
        airBtn.onClick.AddListener(() => { EndTotemSelection(TotemType.Air); });
        earthBtn.onClick.AddListener(() => { EndTotemSelection(TotemType.Earth); });
    }

    private void ShowButtons(bool state)
    {
        waterBtn.gameObject.SetActive(state);
        fireBtn.gameObject.SetActive(state);
        airBtn.gameObject.SetActive(state);
        earthBtn.gameObject.SetActive(state);
    }

    public void StartTotemSelection(int totemPlaceId)
    {
        this.totemPlaceId = totemPlaceId;
        ShowButtons(true);
    }

    private void EndTotemSelection(TotemType type)
    {
        GameObject totemGO = type switch
        {
            TotemType.Fire => Instantiate(fireTotemPrefab),
            TotemType.Air => Instantiate(airTotemPrefab),
            TotemType.Earth => Instantiate(earthTotemPrefab),
            _ => Instantiate(waterTotemPrefab),
        };
        totemGO.transform.localPosition = new Vector3(xTotemLocalPosByPlaceId[totemPlaceId], yTotemLocalPos, zTotemLocalPos);

        var totem = totemGO.GetComponent<Totem>();
        totem.Init(totemPlaceId);
        Player.Instance.PlaceTotem(totem.manaCost);

        ShowButtons(false);
    }

}
