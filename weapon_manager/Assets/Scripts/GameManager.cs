using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ExpHandler Exp { get; private set; }

    void OnValidate()
    {
        Exp = new ExpHandler();
    }

}
