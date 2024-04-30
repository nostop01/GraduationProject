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
        // 마우스의 Y축 입력에 민감도를 곱합니다.
        float rotationY = Input.GetAxis("Mouse Y") * sensitivityY;

        // mouseY 값을 -45에서 45 사이로 제한합니다.
        mouseY = Mathf.Clamp(mouseY + rotationY, -80, 45);

        // Y축 회전값만 변경하고, X와 Z 축 회전값은 그대로 유지합니다.
        Quaternion targetRotation = Quaternion.Euler(-mouseY, 0, 0);

        // 회전을 적용합니다.
        transform.localRotation = targetRotation;
    }
}
