using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerGun;
    public GameObject player;
    public Camera mainCamera;
    public float depth = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos1 = Input.mousePosition;
        mousePos1.z = depth;
        Vector3 objectPos1 = mainCamera.WorldToScreenPoint(transform.position);
        mousePos1.x = mousePos1.x - objectPos1.x;
        mousePos1.y = mousePos1.y - objectPos1.y;
        float angle1 = Mathf.Atan2(mousePos1.y, mousePos1.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(new Vector3(0, angle1, 0));

        //make player gun rotate towards the mouse direction left or right
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = depth;
        Vector3 objectPos = mainCamera.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        playerGun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle  + 90));
    }
}
