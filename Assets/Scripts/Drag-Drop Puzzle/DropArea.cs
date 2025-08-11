using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public void OnDrop (PointerEventData eventData)
    {
        Debug.Log("Word is dropped");

        GameObject wordDropped = eventData.pointerDrag;
        wordDropped.transform.SetParent(transform); //set the DropWordArea as the parent

        int closestIndex = transform.childCount;

        for (int i = 0; i < closestIndex; i++)
        {
            Transform child = transform.GetChild(i);

            if (child == wordDropped.transform) continue;

            //Check whether word is dropped on the left or right of the word thats currently in the DropWordArea
            if (wordDropped.transform.position.x < child.position.x)
            {
                closestIndex = i;
                break;
            }
        }
        wordDropped.transform.SetParent(transform);
        wordDropped.transform.SetSiblingIndex(closestIndex); //allows re-arranging in the DropWordArea
    }
}
