using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private float _maxLife = 100f;
    private float _currentLife;
    [SerializeField] private Image _healthImage;
    [SerializeField] private float _amount;


    public Action OnLifeOver;

    private void Awake()
    {
        _currentLife = _maxLife;
    }
    void Start()
    {
        InvokeRepeating(nameof(CountDown), 1f, 1f);
    }

    private void CountDown()
    {
        _currentLife-= _amount;
        _healthImage.fillAmount = _currentLife / _maxLife;
        if (_currentLife <= 0) { CountEnd(); }
    }

    private void CountEnd()
    {
        //winMenuActivation(); //
        CancelInvoke(nameof(CountDown));
        //OnLifeOver?.Invoke();
    }

    public void addLife(float amount)
    {
        _currentLife += amount;
        if(_currentLife >= _maxLife) { _currentLife = _maxLife;}
        if (_currentLife <= 0) { _currentLife = 0;}
        _healthImage.fillAmount = _currentLife / _maxLife;
    }
}
