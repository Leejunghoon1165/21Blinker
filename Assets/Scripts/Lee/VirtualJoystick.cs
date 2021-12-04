using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10, 150)]
    private float leverRange;

    private Vector3 inputDirection;
    private bool isInput;

    [SerializeField]
    private Player controller;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        ControllJoystickLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControllJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector3.zero;
        isInput = false;
       // controller.Move(Vector3.zero);
    }

    public void ControllJoystickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
        //inputDirection = new Vector3(inputVector / leverRange, 0f, 0f);
        //inputDirection = new Vector3(inputVector / leverRange, 0, inputVector / leverRange);
    }

    private void InputControlVector()
    {
       // controller.Move(inputDirection);
        Debug.Log(inputDirection.x + " / " + inputDirection.z);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInput)
        {
            InputControlVector();
        }
    }
}
