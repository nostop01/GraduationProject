using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraRotation : MonoBehaviour
{
    [SerializeField] private float mouseY = 10f;
    [SerializeField] private float sensitivityY = 1f;

    public Transform _playerTrm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerRotateY();
    }

    void PlayerRotateY()
    {
        // ���콺�� Y�� �Է¿� �ΰ����� ���մϴ�.
        float rotationY = Input.GetAxis("Mouse Y") * sensitivityY;

        // mouseY ���� -45���� 45 ���̷� �����մϴ�.
        mouseY = Mathf.Clamp(mouseY + rotationY, -80, 45);

        // Y�� ȸ������ �����ϰ�, X�� Z �� ȸ������ �״�� �����մϴ�.
        Quaternion targetRotation = Quaternion.Euler(-mouseY, 0, 0);

        // ȸ���� �����մϴ�.
        transform.localRotation = targetRotation;
    }
}
