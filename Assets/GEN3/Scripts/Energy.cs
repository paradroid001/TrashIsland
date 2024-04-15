using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public static Energy instance;
    public int maxEnergy;
    public int currentEnergy;
    void Start()
    {
        instance = this;
        currentEnergy = maxEnergy;
    }
}
