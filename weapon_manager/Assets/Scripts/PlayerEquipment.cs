using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemData
{
    // public int Id;
    public string Name;
    public string Description;
    public GameObject Model;
}

public class PlayerEquipment : MonoBehaviour
{
    [field: SerializeField]
    public List<ItemData> PlayerItems { get; private set; }

    public ItemData CurrentEquippedItem;

    [SerializeField]
    Transform _weaponHoldTransform;

    void Start()
    {
        EquipItem(0);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipItem(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) EquipItem(1);
    }

    public void EquipItem(int itemId)
    {
        if (itemId >= PlayerItems.Count) return;

        CurrentEquippedItem = PlayerItems[itemId];

        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        // create weapons if hand is empty 
        if (_weaponHoldTransform.childCount == 0)
        {
            foreach (var item in PlayerItems)
            {
                var spawnedWeap = Instantiate(item.Model, _weaponHoldTransform);
                spawnedWeap.name = item.Name;
                spawnedWeap.SetActive(false);
            }
        }

        foreach (Transform weaps in _weaponHoldTransform)
        {
            weaps.gameObject.SetActive(weaps.name == CurrentEquippedItem.Name);
        }
    }
}

