using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(Collider2D))]
public class MechController : MonoBehaviour
{
    public float initialArmsRotation = 30;
    public HeadController headController;
    public BodyController bodyController;
    public ArmsController armsController;
    public WheelsController wheelsController;
    //private BoxCollider Collider2D;
    public Vector2 armsOffset = new Vector2(0,0);
    [HideInInspector]
    public int totalWeight;
    // Start is called before the first frame update
    void Awake()
    {
        headController = GetComponentInChildren<HeadController>();
        bodyController = GetComponentInChildren<BodyController>();
        armsController = GetComponentInChildren<ArmsController>();
        wheelsController = GetComponentInChildren<WheelsController>();
        RectTransform Head = headController.gameObject.GetComponent<RectTransform>();
        RectTransform Body = bodyController.gameObject.GetComponent<RectTransform>();
        RectTransform Arms = armsController.gameObject.GetComponent<RectTransform>();
        RectTransform Wheels = wheelsController.gameObject.GetComponent<RectTransform>();
        Body.localEulerAngles = Vector3.zero;
        Head.localEulerAngles = Vector3.zero;
        Arms.localEulerAngles = new Vector3(initialArmsRotation, 0, 0);
        Wheels.localEulerAngles = Vector3.zero;
        Body.localPosition = Vector2.zero;
        Body.localPosition = Body.localPosition+ new Vector3(0, Wheels.localScale.y*Wheels.rect.height, 0);
        Head.localPosition = Body.localPosition+ new Vector3(0, (Body.localScale.y *Body.rect.height)/2 + (Head.localScale.y*Head.rect.height)/2, 0);
        Arms.localPosition = new Vector3(armsOffset.x-Arms.localScale.x*Arms.rect.x/2, armsOffset.y+(Body.localPosition.y*Body.localScale.y*Body.rect.height)/4, 0);
        Arms.localEulerAngles = Vector3.zero;
        Wheels.localPosition= Body.localPosition- new Vector3(0, (Body.localScale.y*Body.rect.height)/ 2 + (Wheels.localScale.y*Wheels.rect.height)/ 2, 0);
        GetComponent<BoxCollider2D>().size= new Vector2(
            Wheels.rect.width * Wheels.localScale.x>Body.rect.width*Body.localScale.x? Wheels.rect.width * Wheels.localScale.x: Body.rect.width * Body.localScale.x,
            Head.position.y + Head.localScale.y * Head.rect.height/2 - (Wheels.position.y - Wheels.rect.height * Wheels.localScale.y/2));
        GetComponent<BoxCollider2D>().offset = Body.localPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
