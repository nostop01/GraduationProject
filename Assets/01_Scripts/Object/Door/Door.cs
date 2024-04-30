using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject R_Door;
    public GameObject L_Door;
    public GameObject R_Criteria;
    public GameObject L_Criteria;

    public bool DoorRightAngle = false;
    public bool OpenDoor = false;

    HotBar _hotBar;

    BoxCollider _boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        _hotBar = GameObject.Find("HotBarSystem").GetComponent<HotBar>();
        DoorRightAngle = false;
        OpenDoor = false;
        _boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateDoor();
    }

    public void RotateDoor()
    {
        if(!DoorRightAngle && OpenDoor)
        {
            R_Door.transform.RotateAround(R_Criteria.transform.position, new Vector3(0f, 1f, 0f), 90f * Time.deltaTime);
            L_Door.transform.RotateAround(L_Criteria.transform.position, new Vector3(0f, 1f, 0f), -90f * Time.deltaTime);

            _boxCollider.isTrigger = true;
        }

        float angle = Mathf.Round(R_Door.transform.rotation.eulerAngles.y);

        if(angle >= 89f )
        {
            DoorRightAngle = true;
            OpenDoor = false;
        }
    }
}
