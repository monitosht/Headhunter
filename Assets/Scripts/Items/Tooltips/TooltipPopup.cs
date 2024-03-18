using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipPopup : MonoBehaviour
{
    [SerializeField] private GameObject popupCanvasObject;
    [SerializeField] private RectTransform popupObject;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemRarity;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Vector3 offset;
    //[SerializeField] private float padding;

    private void Update()
    {
        FollowCursor();
        EscPressed();
    }

    void FollowCursor()
    {
        if (!popupCanvasObject.activeSelf) { return; }

        Vector3 newPos = Input.mousePosition + offset;
        newPos.z = 0f;

        popupObject.transform.position = newPos;
    }

    void EscPressed()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideInfo();
        }
    }

    public void DisplayInfo(ItemObject item)
    {
        itemName.text = item.itemName;

        itemRarity.text = item.Rarity.name;
        itemRarity.color = item.Rarity.TextColour;

        itemDescription.text = item.itemDescription;

        popupCanvasObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }

    public void HideInfo()
    {
        popupCanvasObject.SetActive(false);
    }
}
