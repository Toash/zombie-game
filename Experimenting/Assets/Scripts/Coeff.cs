using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coeff : MonoBehaviour
{
    //simulates risk of rain 2 coefficient behavior
    public float _coefficient;
    public void Update()
    {
        _coefficient = (Time.time * 0.046f);
    }
}
