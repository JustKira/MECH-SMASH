using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class MechController : MonoBehaviour
{
    public float initialArmsRotation = 30;
    [SerializeField]
    private bool autoManageParts = false;
    public HeadController headController;
    public BodyController bodyController;
    public ArmsController armsController;
    public WheelsController wheelsController;
    //private BoxCollider Collider2D;
    public Vector2 armsOffset = new Vector2(0,0);
    [HideInInspector]
    public int totalWeight;



    public int playerIndex = 0;

    public Vector2 dir = Vector2.zero;
    
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
        if (autoManageParts)
        {
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
            /*  GetComponent<BoxCollider2D>().size= new Vector2(
                  Wheels.rect.width * Wheels.localScale.x>Body.rect.width*Body.localScale.x? Wheels.rect.width * Wheels.localScale.x: Body.rect.width * Body.localScale.x,
                  Head.position.y + Head.localScale.y * Head.rect.height/2 - (Wheels.position.y - Wheels.rect.height * Wheels.localScale.y/2));
              GetComponent<BoxCollider2D>().offset = Body.localPosition;
            */
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        dir.x += Input.GetKey(ControlSelector.Instance.controls[playerIndex].right) ? 1*(transform.rotation.eulerAngles.y==0?-1:1) : 0;
        dir.x += Input.GetKey(ControlSelector.Instance.controls[playerIndex].left) ? -1 * (transform.rotation.eulerAngles.y == 0 ? -1 : 1) : 0;
        dir.y += Input.GetKey(ControlSelector.Instance.controls[playerIndex].up) ? 1 : 0;
        dir.y += Input.GetKey(ControlSelector.Instance.controls[playerIndex].down) ? -1 : 0;
        
        if (dir.x != 0)
            wheelsController.Move(dir.x > 0);
        if(dir.y != 0)
            armsController.RotateArm(dir.y > 0);
        dir = Vector2.zero;
    }
}
