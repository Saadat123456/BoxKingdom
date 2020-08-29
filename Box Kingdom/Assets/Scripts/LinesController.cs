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
    private GameObject mouseOverObject;
    private GameObject startingObject;
    private Material materialRight, materialNormal, materialWrong;
    public int numberOfColumns;

    GameObject gameController;
    GameControllerScript gameControllerScript;


    // Start is called before the first frame update
    void Start()
    {
        numberOfColumns = 3;
        lineRend = GetComponent<LineRenderer>();
        dots = GameObject.FindGameObjectsWithTag("Dots");
        drawing = false;
        drawingDone = false;
        mouseOverObject = null;

        gameController = GameObject.Find("GameController");
        gameControllerScript = gameController.GetComponent<GameControllerScript>();
        
        materialRight = Resources.Load("Material/Right", typeof(Material)) as Material;
        materialNormal = Resources.Load("Material/Normal", typeof(Material)) as Material;
        materialWrong = Resources.Load("Material/Wrong", typeof(Material)) as Material;
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
                    startingObject = hitInfo.collider.gameObject;
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
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
                if (hit)
                {
                    if (hitInfo.collider.gameObject.CompareTag("Dots"))
                    {
                        if (startingObject != hitInfo.collider.gameObject)
                        {
                            int startingNumber = int.Parse(startingObject.name);
                            int mouseOverObjectNumber = int.Parse(hitInfo.collider.gameObject.name);
                            
                            if((mouseOverObjectNumber == startingNumber+1 && (mouseOverObjectNumber % numberOfColumns) != 1) || (mouseOverObjectNumber == startingNumber - 1 && (mouseOverObjectNumber % numberOfColumns) != 0) || mouseOverObjectNumber == startingNumber + numberOfColumns || mouseOverObjectNumber == startingNumber - numberOfColumns )
                            {
                                mouseOverObject = hitInfo.collider.gameObject;
                                mouseOverObject.GetComponent<Renderer>().material = materialRight;
                            }
                            else
                            {
                                mouseOverObject = hitInfo.collider.gameObject;
                                mouseOverObject.GetComponent<Renderer>().material = materialWrong;
                            }
                                
                        }
                    }
                }
                else
                {
                    if (mouseOverObject != null && mouseOverObject.GetComponent<Renderer>().material.name.Replace(" (Instance)", "").Equals("Right") || mouseOverObject != null && mouseOverObject.GetComponent<Renderer>().material.name.Replace(" (Instance)", "").Equals("Wrong"))
                    {
                        
                        mouseOverObject.GetComponent<Renderer>().material = materialNormal;
                    }
                }
                         
                
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
                    int startingNumber = int.Parse(startingObject.name);
                    int mouseOverObjectNumber = int.Parse(hitInfo.collider.gameObject.name);

                    if ((mouseOverObjectNumber == startingNumber + 1 && (mouseOverObjectNumber % numberOfColumns) != 1) || (mouseOverObjectNumber == startingNumber - 1 && (mouseOverObjectNumber % numberOfColumns) != 0) || mouseOverObjectNumber == startingNumber + numberOfColumns || mouseOverObjectNumber == startingNumber - numberOfColumns)
                    {
                        lineRend.SetPosition(1, new Vector3(hitInfo.collider.gameObject.transform.position.x, hitInfo.collider.gameObject.transform.position.y, 0f));
                        hitInfo.collider.gameObject.GetComponent<Renderer>().material = materialNormal;

                        if(startingNumber<mouseOverObjectNumber)
                            gameControllerScript.addToList(startingNumber.ToString() + mouseOverObjectNumber.ToString());
                        if(startingNumber>mouseOverObjectNumber)
                            gameControllerScript.addToList(mouseOverObjectNumber.ToString() + startingNumber.ToString());
                        
                        drawingDone = true;
                        gameControllerScript.getNumberOfLines(startingNumber,numberOfColumns,mouseOverObjectNumber);
                        Instantiate(Resources.Load("Prefabs/LineRenderer"));
                        enabled = false;
                    }
                    else
                    {
                        lineRend.SetPosition(0, new Vector3(0f, 0f, 0f));
                        lineRend.SetPosition(1, new Vector3(0f, 0f, 0f));
                        drawing = false;
                        mouseOverObject.GetComponent<Renderer>().material = materialNormal;
                    }
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
