using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private float _currentLife;

    void Start()
    {
        InvokeRepeating(nameof(CountDown), 1f, 1f);
    }

    private void CountDown()
    {
        _currentLife--;
        //timePuntos.text = _currentTime.ToString(); //
        //if (_currentTime <= 0) { CountEnd(); }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
