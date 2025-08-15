using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public Transform dropWordsContent; //DropWordsArea
    public void OnDrop (PointerEventData eventData)
    {
        var word = eventData.pointerDrag; //the word object that the player is dragging

        if (word != null)
        {
            //Parent the word to the content in viewport
            word.transform.SetParent(dropWordsContent, false );
            RectTransform rect = word.GetComponent<RectTransform>();

            rect.localPosition = Vector3.zero; //snap word into exact position in DropWordsArea
            rect.localScale = Vector3.one; //reset the scale so its not too big

            word.transform.SetAsLastSibling(); //add the next word to the right in the DropWordsArea
        }
    }
}
