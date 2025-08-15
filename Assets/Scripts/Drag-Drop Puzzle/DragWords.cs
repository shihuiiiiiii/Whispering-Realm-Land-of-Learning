using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWords : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent; //original parent = WordArea, so if the words not place correctly it will go back here
    private Vector3 originalPosition; //original position in the parent object(WordArea)
    private RectTransform rectTransform; // needed to move the words
    private CanvasGroup canvasGroup; // for raycast blocking
    private Canvas canvas;

    public Transform parentAfterDrag;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start Drag");
        originalParent = transform.parent; //store original parent

        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling(); //so the word is on top visually
        canvasGroup.blocksRaycasts = false; //so the word can be dropped on DropArea
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor; //move the word
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Stop Drag");
        canvasGroup.blocksRaycasts = true;

        if (transform.parent == canvas.transform)
        {
            transform.SetParent(originalParent, false);
            rectTransform.localPosition = Vector3.zero;
        }
    }
}
