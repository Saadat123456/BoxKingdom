using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinesController : MonoBehaviour
{

    private LineRenderer lineRend;
    private Vector2 mousePos;
    private Vector2 startMousePos;
    private GameObject[] dots;
    private Boolean drawing;
    private Boolean drawingDone;


    // Start is called before the first frame update
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        dots = GameObject.FindGameObjectsWithTag("Dots");
        drawing = false;
        drawingDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (hitInfo.collider.gameObject.CompareTag("Dots"))
                {
                    Debug.Log(hitInfo.collider.gameObject.transform.position);
                    lineRend.positionCount = 2;
                    startMousePos = hitInfo.collider.gameObject.transform.position;
                    drawing = true;
                }
            }
        }

        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))
        {
            if (drawing && !drawingDone)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lineRend.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
                lineRend.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
            }
        }
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (!hitInfo.collider.gameObject.CompareTag("Dots"))
                {
                    lineRend.SetPosition(0, new Vector3(0f, 0f, 0f));
                    lineRend.SetPosition(1, new Vector3(0f, 0f, 0f));
                    drawing = false;
                }
                else
                {
                    lineRend.SetPosition(1, new Vector3(hitInfo.collider.gameObject.transform.position.x, hitInfo.collider.gameObject.transform.position.y, 0f));
                    drawingDone = true;
                    Instantiate(Resources.Load("Prefabs/LineRenderer"));
                    enabled = false;
                }
            }
            else
            {
                lineRend.SetPosition(0, new Vector3(0f, 0f, 0f));
                lineRend.SetPosition(1, new Vector3(0f, 0f, 0f));
                drawing = false;
            }
            
        }
    }

}
