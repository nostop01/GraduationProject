using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public Slider _interactionSlider;
    public TMP_Text _interactText;
    public GameObject _player;

    PlayerInteraction _playerInteraction;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Main Camera");
        _playerInteraction = _player.GetComponent<PlayerInteraction>();
        _interactionSlider = GameObject.Find("interactionGauge").GetComponent<Slider>();
        _interactionSlider.gameObject.SetActive(false);
        _interactText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractionGauge()
    {
        _interactionSlider.gameObject.SetActive(true);
        _interactionSlider.value = _playerInteraction.interactTimer;

        if(_interactionSlider.value >= _interactionSlider.maxValue)
        {
            _interactionSlider.gameObject.SetActive(false);
        }
    }

    public void CancleInteraction()
    {
        _interactionSlider.gameObject.SetActive(false);
        _playerInteraction.interactTimer = 0;
    }

    public void CanInteractionUI()
    {
        _interactText.gameObject.SetActive(true);
    }

    public void CannotInteractionUI()
    {
        _interactionSlider.gameObject.SetActive(false);
    }
}
