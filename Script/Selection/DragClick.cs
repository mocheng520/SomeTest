using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragClick : MonoBehaviour
{
    Camera myCam;

    [SerializeField]
    RectTransform boxVisual;
    
    Rect selectionBox;
    Vector2 startPosition;
    Vector2 endPosition;

    void Start()
    {
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
    }

    // Update is called once per frame
    void Update()
    {
        //单单击
        if(Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            selectionBox = new Rect();
        }
        //拖动
        if(Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }
        

        //释放点击
        if(Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
        }

    }

    void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
        boxVisual.sizeDelta = boxSize;

    }

    void DrawSelection()
    {
        selectionBox.xMin = Mathf.Min(Input.mousePosition.x, startPosition.x);
        selectionBox.xMax = Mathf.Max(Input.mousePosition.x, startPosition.x);
        selectionBox.yMin = Mathf.Min(Input.mousePosition.y, startPosition.y);
        selectionBox.yMax = Mathf.Max(Input.mousePosition.y, startPosition.y);

    }

    void SelectUnits(){
        foreach( var unit in UnitSelections.Instance.unitList)
        {
            if(selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                UnitSelections.Instance.DragSelect(unit);
            }
        }



    }









}
