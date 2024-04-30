using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class HotBar : MonoBehaviour
{
    public GameObject[] itemSlots; // 아이템 슬롯 배열
    public Image[] itemIcons; // 아이템 아이콘 배열
    public Image[] slotIcons; // 선택 슬롯 아이콘 배열

    public int currentSlotIndex = 0; // 현재 선택된 슬롯 인덱스

    // Start is called before the first frame update
    void Start()
    {
        StartClearHotBarUI();
    }

    // Update is called once per frame
    void Update()
    {
        // 1부터 0까지의 숫자 키를 눌러 슬롯 전환
        for (int i = 1; i <= itemSlots.Length; i++)
        {
            if (Input.GetKeyDown(i.ToString()) || Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    currentSlotIndex = i - 1; // 배열 인덱스는 0부터 시작하므로 1을 빼줌
                }
                else if (Input.GetAxis("Mouse ScrollWheel") > 0 && currentSlotIndex > 0)
                {
                    currentSlotIndex = (currentSlotIndex - 1 + itemSlots.Length) % itemSlots.Length; // 이전 슬롯 선택
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0 && currentSlotIndex < 4)
                {
                    currentSlotIndex = (currentSlotIndex + 1) % itemSlots.Length; // 다음 슬롯 선택
                }

                UpdateSelectionUI(); // 선택된 슬롯을 UI에 업데이트
                break;
            }
        }
    }

    void StartClearHotBarUI() //UI 상태 초기화
    {
        for (int i = 0; i < slotIcons.Length; i++)
        {
            slotIcons[i].color = new Color(slotIcons[i].color.r, slotIcons[i].color.g, slotIcons[i].color.b, 0f);
        }

        slotIcons[0].color = new Color(slotIcons[0].color.r, slotIcons[0].color.g, slotIcons[0].color.b, 1f);
    }

    void UpdateSelectionUI()
    {
        // 모든 슬롯의 선택 상태를 초기화
        for (int i = 0; i < slotIcons.Length; i++)
        {
            slotIcons[i].color = new Color(slotIcons[i].color.r, slotIcons[i].color.g, slotIcons[i].color.b, 0f);
        }

        // 현재 선택된 슬롯의 아이콘 색상을 변경하여 선택 상태를 나타냄
        slotIcons[currentSlotIndex].color = new Color(slotIcons[currentSlotIndex].color.r, slotIcons[currentSlotIndex].color.g, slotIcons[currentSlotIndex].color.b, 1f);
    }

    public void AssignItemToSlot(GameObject item, Sprite image)
    {
        {
            int startIndex = 0; // 할당을 시작할 인덱스

            do
            {
                // 현재 슬롯에 아이템이 없는 경우 아이템을 할당하고 종료
                if (itemSlots[currentSlotIndex] == null)
                {
                    itemSlots[currentSlotIndex] = item;
                    itemIcons[currentSlotIndex].sprite = image;


                    return;
                }

                else if (itemSlots[currentSlotIndex] != null)
                {
                    if (itemSlots[startIndex] == null)
                    {
                        itemSlots[startIndex] = item;
                        itemIcons[startIndex].sprite = image;

                        return;
                    }
                }

                // 현재 슬롯에 이미 아이템이 있는 경우 다음 슬롯으로 이동
                startIndex = (startIndex + 1) % itemSlots.Length;

            } while (startIndex != 0); // 한 바퀴를 돌아서 다시 시작점에 도달할 때까지 반복
        }
    }
}
