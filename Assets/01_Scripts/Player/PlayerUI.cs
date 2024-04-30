using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public PlayerMove _playerMove;
    public GameObject _fillArea;
    public Slider _staminaSlider;

    // Start is called before the first frame update
    private void Start()
    {
        _playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        _fillArea = GameObject.Find("Stamina Fill Area");
        _staminaSlider = GameObject.Find("StaminaSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    private void Update()
    {
        StaminaSlider();
    }

    void StaminaSlider()
    {
        _staminaSlider.value = _playerMove.stamina;

        if(_playerMove.stamina < 0.01f)
        {
            _fillArea.SetActive(false);
        }
        else
        {
            _fillArea.SetActive(true);
        }
    }
}
