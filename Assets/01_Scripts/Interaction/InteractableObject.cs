using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public bool isGrapping = false;

    public GameObject interactDistance;
    public GameObject _hotBarObject;
    public PlayerInteraction _playerInteraction;

    public Sprite _image;

    private Rigidbody _rigid;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _playerInteraction = GameObject.Find("Main Camera").GetComponent<PlayerInteraction>();
        _hotBarObject = GameObject.Find("HotBarSystem");
        interactDistance = GameObject.Find("interactionPos");
    }

    private void Update()
    {
        if (!isGrapping)
        {
            _rigid.isKinematic = false;
        }
    }

    public void Interact() // 줍기 상호작용 시
    {
        HotBar hotBar = _hotBarObject.GetComponent<HotBar>();

        if (hotBar != null)
        {
            hotBar.AssignItemToSlot(this.gameObject, _image);
        }

        gameObject.SetActive(false);

        _playerInteraction.interactTimer = 0;
    }

    public void BackInteract() // 놓기 상호작용 시
    {
        transform.position = interactDistance.transform.position;
    }
}