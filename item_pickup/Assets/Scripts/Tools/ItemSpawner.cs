using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject _objToSpawn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var t = PlayerInteraction.Instance.PlayerHand;
            Instantiate(_objToSpawn, t.position, Quaternion.identity);
        }
    }
}
