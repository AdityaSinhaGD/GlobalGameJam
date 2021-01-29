using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractionController : MonoBehaviour
{
    private Camera playerCamera;
    private Transform playerLookTransform;

    [SerializeField] private float selectionRange = 15f;

    public GameObject objectBeingExamined = null;

    private bool isObjectSelected = false;

    private ExaminableObject currentExaminableObject;
    private IInteractable currentInteractableObject;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = this.GetComponentInChildren<Camera>();
        playerLookTransform = playerCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessObjectInteraction();
    }

    private void ProcessObjectInteraction()
    {
        if (currentExaminableObject != null)
        {
            currentExaminableObject.OnHoverExit();
            currentExaminableObject = null;
        }

        if (currentInteractableObject != null)
        {
            currentInteractableObject.OnHoverExit();
            currentInteractableObject = null;
        }

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, selectionRange))
        {
            if (hit.collider.gameObject.GetComponent<ExaminableObject>())
            {
                currentExaminableObject = hit.collider.gameObject.GetComponent<ExaminableObject>();

                currentExaminableObject.OnHover();

                //If Object is being examined then its default material should be set.
                if (isObjectSelected)
                {
                    currentExaminableObject.OnHoverExit();
                }

                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
                {
                    if (!isObjectSelected)
                    {
                        ExamineHighlightedObject();
                    }
                    else
                    {
                        ReturnObjectToOriginalState();
                    }
                }
            }
            currentInteractableObject = hit.collider.gameObject.GetComponent<IInteractable>();
            if (currentInteractableObject == null)
            {
                return;
            }
            else
            {
                currentInteractableObject.OnHover();
                if (Input.GetMouseButtonDown(0))
                {
                    currentInteractableObject.Interact();
                }
            }
        }
    }

    private void ExamineHighlightedObject()
    {
        objectBeingExamined = currentExaminableObject.gameObject;
        //todo serialize face offset distance for examining the object.
        currentExaminableObject.Select(playerLookTransform.position + playerLookTransform.forward * 1f, playerLookTransform.position);
        isObjectSelected = !isObjectSelected;
    }

    private void ReturnObjectToOriginalState()
    {
        currentExaminableObject.DeSelect();
        objectBeingExamined = null;
        isObjectSelected = !isObjectSelected;
    }
}
