using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWords : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent; //original parent = WordArea, so if the words not place correctly it will go back here
    private Vector3 originalPosition; //original position in the parent object(WordArea)
    private RectTransform rectTransform; // needed to move the words

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start Drag");
        originalPosition = rectTransform.anchoredPosition; //where the word's original position is
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        rectTransform.anchoredPosition += eventData.delta; //move the word
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Stop Drag");

        if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<DropArea>() == null)
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
