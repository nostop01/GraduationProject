using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange = 5; // 상호작용 거리
    [SerializeField] private float interactionSize = 0.2f; // 상호작용 가능한 크기
    public float interactTimer = 0f; 

    InteractableObject interactableObject;
    HotBar _hotBar;
    InteractionUI _interactionUI;
    Door _door;

    // Start is called before the first frame update
    void Start()
    {
        _hotBar = GameObject.Find("HotBarSystem").GetComponent<HotBar>();
        _interactionUI = GetComponent<InteractionUI>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKey(KeyCode.F) && _hotBar.itemSlots[_hotBar.currentSlotIndex] == null)
        {
            PerformInteraction();
        }

        else if(Input.GetKeyUp(KeyCode.F))
        {
            interactTimer = 0f;
            _interactionUI.CancleInteraction();
        }

        else if(Input.GetKeyDown(KeyCode.F) && _hotBar.itemSlots[_hotBar.currentSlotIndex] != null)         
        {
            PerformBackInteraction();
        }

        if(Input.GetMouseButtonDown(0))
        {
            
            OpenDoor();
        }
    }

    private void PerformInteraction() // 줍기 상호작용
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
        {
            Debug.DrawRay(transform.position, transform.forward, Color.green);

            interactableObject = hit.collider.GetComponent<InteractableObject>();

            if (interactableObject != null)
            {
                interactTimer += Time.deltaTime;
                _interactionUI.InteractionGauge();

                if (interactTimer >= 0.75)
                {
                    interactableObject.Interact();
                }
            }

            else if(hit.collider != GetComponent<InteractableObject>())
            {
                _interactionUI.CancleInteraction();
            }
        }

        else
        {
            _interactionUI.CancleInteraction();
        }
    }

    public void PerformBackInteraction() // 놓기 상호작용
    {
        if(_hotBar.itemSlots[_hotBar.currentSlotIndex] != null) // 핫바 오브젝트의 현재 index 값이 NULL이 아닐때 작동
        {
            _hotBar.itemSlots[_hotBar.currentSlotIndex].SetActive(true); // 해당하는 Index의 오브젝트 활성화

            InteractableObject interactable = _hotBar.itemSlots[_hotBar.currentSlotIndex].GetComponent<InteractableObject>();
            // HotBar 시스템의 아이템 슬롯 안에있는 게임 오브젝트가 가지고 있는 InteractableObject 스크립트에 접근
            interactable.BackInteract(); // BackInteract() 함수 실행

            _hotBar.itemSlots[_hotBar.currentSlotIndex] = null; // HotBar의 아이템 슬롯 배열의 currentSlotIndex값을 NULL로 변경
            _hotBar.itemIcons[_hotBar.currentSlotIndex].sprite = null; // 아이콘 슬롯 배열의 currentSlotIndex의 sprite Source 값을 null로 변경
            _hotBar.itemIcons[_hotBar.currentSlotIndex].color = Color.white; // 아이콘 슬롯 배열의 currentSlotIndex 값의 색을 하얀색으로 변경
        }
    }

    void OpenDoor() //문 열기 상호작용
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
        {
            _door = hit.collider.GetComponent<Door>();

            if (_door != null && _hotBar.itemSlots[_hotBar.currentSlotIndex].gameObject.CompareTag("Key"))
            {
                _door.OpenDoor = true;

                Destroy(_hotBar.itemSlots[_hotBar.currentSlotIndex].gameObject);
                _hotBar.itemSlots[_hotBar.currentSlotIndex] = null; //아이템 슬롯 비우기
                _hotBar.itemIcons[_hotBar.currentSlotIndex].sprite = null; //아이템 슬롯 아이콘 비우기
            }

            else if (_hotBar.itemSlots[_hotBar.currentSlotIndex] == null)
            {
                Debug.Log("해당 슬롯은 비어있습니다.");
            }
        }
    }
}
