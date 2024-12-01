using UnityEngine;
using UnityEngine.UI;
using Utility.Interactable;

public class DisplayInteraction : MonoBehaviour
{
    [SerializeField] private GameObject interactionCursor;
    private Interactionen playerInteractions;
    Transform dispalyPos;

    private void Update()
    {
        UpdateDisplay();
    }

    private bool HasInfo(){
        if(playerInteractions != null) return true;
        playerInteractions = FindAnyObjectByType<Interactionen>();
        return playerInteractions != null;
    }

    private void UpdateDisplay(){
        if(!HasInfo()){
            interactionCursor.SetActive(false);
        }
        else{
            dispalyPos = playerInteractions.GetInteraction();
            if(dispalyPos != null){
                interactionCursor.SetActive(true);
                UpdateCursorPosition(dispalyPos);
            }
            else{
                interactionCursor.SetActive(false);
            }
        }
    }

    private void UpdateCursorPosition(Transform worldPos){
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos.position);
        interactionCursor.transform.position = screenPos;
    }
}
