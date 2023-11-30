using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotemButton : MonoBehaviour
{
    private Button btn;
    [SerializeField] private int placeId;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => { TotemSelection.Instance.StartTotemSelection(placeId); });
    }

    void Update()
    {

    }
}
