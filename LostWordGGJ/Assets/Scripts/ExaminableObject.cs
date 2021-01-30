using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExaminableObject : MonoBehaviour
{
    public Vector3 startingPosition;
    public Quaternion startingRotation;

    private Material defaultMaterial;
    public Material highlightMaterial;

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
        if (objectName == "")
        {
            objectName = transform.name;
        }
    }

    public void Select(Vector3 focusPosition, Vector3 target)
    {
        transform.position = focusPosition;
        transform.LookAt(target);
        StartCoroutine(LearnWord());

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

    private IEnumerator LearnWord()
    {
        yield return new WaitForSeconds(2f);
        if (!GameManager.Instance.wordsLearned.Contains(objectName))
        {
            GameManager.Instance.wordsLearned.Add(objectName);
            char[] tempCharArray = objectName.ToCharArray();
            foreach(char c in tempCharArray)
            {
                GameManager.Instance.lettersLearned.Add(c);
            }
            //UIManager.Instance.UpdateWordCollectionDisplay(objectName);
            UIManager.Instance.DisplayLearnedWord(objectName);
        }
        
    }
}
