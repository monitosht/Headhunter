using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ItemObject item;
    private TooltipPopup tooltip;

    void Awake()
    {
        tooltip = FindObjectOfType<TooltipPopup>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.DisplayInfo(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideInfo();
    }
}
