using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public Transform dropWordsContent;
    public void OnDrop (PointerEventData eventData)
    {
        var word = eventData.pointerDrag;

        if (word != null)
        {
            //Parent the word to the content in viewport
            word.transform.SetParent(dropWordsContent, false );
            RectTransform rect = word.GetComponent<RectTransform>();

            rect.localPosition = Vector3.zero; //
            rect.localScale = Vector3.one; //make sure the scale is one to be visible

            word.transform.SetAsLastSibling();

            ////Allow Rearrange words inside of DropArea
            //int closestIndex = dropWordsContent.childCount;

            //for (int i = 0; i < dropWordsContent.childCount; i++)
            //{
            //    Transform child = dropWordsContent.GetChild(i);
            //    if (child == word.transform) continue;

            //    if (rect.anchoredPosition.x < child.GetComponent<RectTransform>().anchoredPosition.x)
            //    {
            //        closestIndex = i;
            //        break;
            //    }
            //}
            //word.transform.SetSiblingIndex(closestIndex);
        }
    }
}
