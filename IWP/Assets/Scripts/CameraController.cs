using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool move = true;

    public float panSpeed = 30f;
    public float mouseBoarderThickness = 10f;
    public float scrollSpeed = 5000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            move = !move;
        }

        if (!move)
        {
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - mouseBoarderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= mouseBoarderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= mouseBoarderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - mouseBoarderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        //when not using, mousewheel scrolling = 0;
        float scrolling = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;

        pos.y -= scrolling * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, 10f, 100f);
        transform.position = pos;
    }
}
