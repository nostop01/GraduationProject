using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange = 5; // ��ȣ�ۿ� �Ÿ�
    [SerializeField] private float interactionSize = 0.2f; // ��ȣ�ۿ� ������ ũ��
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

    private void PerformInteraction() // �ݱ� ��ȣ�ۿ�
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

    public void PerformBackInteraction() // ���� ��ȣ�ۿ�
    {
        if(_hotBar.itemSlots[_hotBar.currentSlotIndex] != null) // �ֹ� ������Ʈ�� ���� index ���� NULL�� �ƴҶ� �۵�
        {
            _hotBar.itemSlots[_hotBar.currentSlotIndex].SetActive(true); // �ش��ϴ� Index�� ������Ʈ Ȱ��ȭ

            InteractableObject interactable = _hotBar.itemSlots[_hotBar.currentSlotIndex].GetComponent<InteractableObject>();
            // HotBar �ý����� ������ ���� �ȿ��ִ� ���� ������Ʈ�� ������ �ִ� InteractableObject ��ũ��Ʈ�� ����
            interactable.BackInteract(); // BackInteract() �Լ� ����

            _hotBar.itemSlots[_hotBar.currentSlotIndex] = null; // HotBar�� ������ ���� �迭�� currentSlotIndex���� NULL�� ����
            _hotBar.itemIcons[_hotBar.currentSlotIndex].sprite = null; // ������ ���� �迭�� currentSlotIndex�� sprite Source ���� null�� ����
            _hotBar.itemIcons[_hotBar.currentSlotIndex].color = Color.white; // ������ ���� �迭�� currentSlotIndex ���� ���� �Ͼ������ ����
        }
    }

    void OpenDoor() //�� ���� ��ȣ�ۿ�
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
        {
            _door = hit.collider.GetComponent<Door>();

            if (_door != null && _hotBar.itemSlots[_hotBar.currentSlotIndex].gameObject.CompareTag("Key"))
            {
                _door.OpenDoor = true;

                Destroy(_hotBar.itemSlots[_hotBar.currentSlotIndex].gameObject);
                _hotBar.itemSlots[_hotBar.currentSlotIndex] = null; //������ ���� ����
                _hotBar.itemIcons[_hotBar.currentSlotIndex].sprite = null; //������ ���� ������ ����
            }

            else if (_hotBar.itemSlots[_hotBar.currentSlotIndex] == null)
            {
                Debug.Log("�ش� ������ ����ֽ��ϴ�.");
            }
        }
    }
}
