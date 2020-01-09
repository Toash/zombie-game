using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public bool _playDustEffect;
    ParticleSystem[] _dustEffect;
    // Start is called before the first frame update
    void Start()
    {
        _dustEffect = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_playDustEffect == true)
        {
            _dustEffect[0].Play();
            _dustEffect[1].Play();
            _playDustEffect = false;
        }
    }
    public void DisableBoard()
    {
        _dustEffect[0].Play();
        _dustEffect[1].Play();
        gameObject.SetActive(false);
    }
}
