using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExaminableObject : MonoBehaviour
{
    public Vector3 startingPosition;
    public Quaternion startingRotation;

    private Material defaultMaterial;
    public Material highlightMaterial;

    public string examineMessage;
    public string objectName;

    private Renderer materialRenderer;



    private void Awake()
    {
        materialRenderer = GetComponent<Renderer>();
        defaultMaterial = materialRenderer.material;
        
    }

    private void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }

    public void Select(Vector3 focusPosition, Vector3 target)
    {
        transform.position = focusPosition;
        transform.LookAt(target);

    }

    public void DeSelect()
    {
        transform.position = startingPosition;
        transform.rotation = startingRotation;

    }

    public void OnHover()
    {
        materialRenderer.material = highlightMaterial;

    }

    public void OnHoverExit()
    {

        materialRenderer.material = defaultMaterial;
        //HUDManager.Instance.ResetInteractionMessage();
    }
}
