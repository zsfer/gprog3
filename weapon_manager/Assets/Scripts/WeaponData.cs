using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Data")]
    public string WeaponName;
    public float Damage;
}