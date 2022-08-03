using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    RaycastHit2D hit;
    public Camera cam;
    Vector3 pos;
    Vector3 mPos;
    Transform focus;
    public bool IsDrag;

    private void Start()
    {
        IsDrag = false;
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(Input.mousePosition));

            if (hit.collider != null && hit.collider.gameObject.tag != "Hole" && hit.collider.gameObject.layer != 6 && hit.collider.gameObject.layer != 7 && hit.collider.gameObject.layer != 8 && hit.collider.gameObject.layer != 13)
            {
                focus = hit.transform;
                IsDrag = true;
            }
        }
        else if (Input.GetMouseButtonUp(0) && IsDrag == true)
        {
            IsDrag = false;
        }
        else if (IsDrag == true)
        {
            mPos = Input.mousePosition;
            mPos.z = cam.transform.position.z;
            pos = cam.ScreenToWorldPoint(mPos);

            focus.position = new Vector3(pos.x, pos.y, focus.position.z);
        }
    }
}
