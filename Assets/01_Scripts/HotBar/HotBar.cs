using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class HotBar : MonoBehaviour
{
    public GameObject[] itemSlots; // ������ ���� �迭
    public Image[] itemIcons; // ������ ������ �迭
    public Image[] slotIcons; // ���� ���� ������ �迭

    public int currentSlotIndex = 0; // ���� ���õ� ���� �ε���

    // Start is called before the first frame update
    void Start()
    {
        StartClearHotBarUI();
    }

    // Update is called once per frame
    void Update()
    {
        // 1���� 0������ ���� Ű�� ���� ���� ��ȯ
        for (int i = 1; i <= itemSlots.Length; i++)
        {
            if (Input.GetKeyDown(i.ToString()) || Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    currentSlotIndex = i - 1; // �迭 �ε����� 0���� �����ϹǷ� 1�� ����
                }
                else if (Input.GetAxis("Mouse ScrollWheel") > 0 && currentSlotIndex > 0)
                {
                    currentSlotIndex = (currentSlotIndex - 1 + itemSlots.Length) % itemSlots.Length; // ���� ���� ����
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0 && currentSlotIndex < 4)
                {
                    currentSlotIndex = (currentSlotIndex + 1) % itemSlots.Length; // ���� ���� ����
                }

                UpdateSelectionUI(); // ���õ� ������ UI�� ������Ʈ
                break;
            }
        }
    }

    void StartClearHotBarUI() //UI ���� �ʱ�ȭ
    {
        for (int i = 0; i < slotIcons.Length; i++)
        {
            slotIcons[i].color = new Color(slotIcons[i].color.r, slotIcons[i].color.g, slotIcons[i].color.b, 0f);
        }

        slotIcons[0].color = new Color(slotIcons[0].color.r, slotIcons[0].color.g, slotIcons[0].color.b, 1f);
    }

    void UpdateSelectionUI()
    {
        // ��� ������ ���� ���¸� �ʱ�ȭ
        for (int i = 0; i < slotIcons.Length; i++)
        {
            slotIcons[i].color = new Color(slotIcons[i].color.r, slotIcons[i].color.g, slotIcons[i].color.b, 0f);
        }

        // ���� ���õ� ������ ������ ������ �����Ͽ� ���� ���¸� ��Ÿ��
        slotIcons[currentSlotIndex].color = new Color(slotIcons[currentSlotIndex].color.r, slotIcons[currentSlotIndex].color.g, slotIcons[currentSlotIndex].color.b, 1f);
    }

    public void AssignItemToSlot(GameObject item, Sprite image)
    {
        {
            int startIndex = 0; // �Ҵ��� ������ �ε���

            do
            {
                // ���� ���Կ� �������� ���� ��� �������� �Ҵ��ϰ� ����
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

                // ���� ���Կ� �̹� �������� �ִ� ��� ���� �������� �̵�
                startIndex = (startIndex + 1) % itemSlots.Length;

            } while (startIndex != 0); // �� ������ ���Ƽ� �ٽ� �������� ������ ������ �ݺ�
        }
    }
}
