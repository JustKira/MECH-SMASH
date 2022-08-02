using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsController : PartController
{
    public ArmStatsManager activeStats;
    public Stat speed;
    public Stat acceleration;
    public Stat maxAngle;
    public float startingAngle;
    private bool rebound = false;
    // Start is called before the first frame update
    void Start()
    {
        varStats = gameObject.AddComponent<VariableStats>();
        activeStats = new ArmStatsManager(maxStatPoints, attachmentHealth, activationHealth, weight, speed, maxAngle, acceleration, varStats);
        startingAngle = transform.localRotation.eulerAngles.x > 180 ? -(360 - transform.localRotation.eulerAngles.x) : ((transform.localRotation.eulerAngles.x < 0 && transform.localRotation.eulerAngles.x > -180) ? transform.localRotation.eulerAngles.x : 360 + transform.localRotation.eulerAngles.x) % 360;
        varStats.currentAttachmentHealth = activeStats.attachmentHealth.value;
        varStats.currentActivationHealth = activeStats.activationHealth.value;
        varStats.currentWeight = activeStats.weight.value;
        varStats.currentSpeed = 0;
        varStats.currentAcceleration = activeStats.acceleration.value;
        hitBox = gameObject.GetComponent<BoxCollider2D>();
        hitBox.isTrigger = true;
    }
    public void RotateArm(bool Reverse, bool fromTrigger = false)
    {
        if (rebound && !fromTrigger || varStats.currentActivationHealth <= 0)
        {
            return;
        }
        //Makes sure the angle is in range of -180 to 180 and applies adequate value shift for each case
        float currentAngle = transform.localRotation.eulerAngles.x > 180 ? -(360 - transform.localRotation.eulerAngles.x) : ((transform.localRotation.eulerAngles.x < 0 && transform.localRotation.eulerAngles.x > -180) ? transform.localRotation.eulerAngles.x : 360 + transform.localRotation.eulerAngles.x) % 360;
        float accel = activeStats.acceleration.value * (Reverse ? -1 : 1);
        if ((varStats.currentSpeed < activeStats.speed.value && !Reverse) || varStats.currentSpeed > -activeStats.speed.value && Reverse)
            varStats.currentSpeed += accel * Time.smoothDeltaTime;
        if (!Reverse && currentAngle < (activeStats.maxRotation.value / 2 + startingAngle))
            transform.Rotate((new Vector3(varStats.currentSpeed * Time.smoothDeltaTime, 0, 0)), Space.Self);
        else if (Reverse && currentAngle > -(activeStats.maxRotation.value / 2 + startingAngle))
            transform.Rotate((new Vector3(varStats.currentSpeed * Time.smoothDeltaTime, 0, 0)), Space.Self);
        else
            varStats.currentSpeed = 0;
    }
    public IEnumerator Recoil(int speed)
    {
        rebound = true;
        varStats.currentSpeed = 0;
        for (float i = 0; i < Mathf.Abs(speed * Time.smoothDeltaTime); i += Time.smoothDeltaTime)
        {
            RotateArm(speed > 0, true);
            yield return null;
        }
        rebound = false;
    }
    public void Deactivate()
    {

        StartCoroutine(deactivate());
    }
    private IEnumerator deactivate()
    {
        for (float i = 0; i < Mathf.Abs(activeStats.speed.value * Time.smoothDeltaTime); i += Time.smoothDeltaTime)
        {
            RotateArm(false);
            yield return null;
        }
        varStats.currentActivationHealth = 0;
        this.enabled = false;
    }
    public void Detach()
    {
        varStats.currentAttachmentHealth = 0;
        transform.parent = null;
        gameObject.AddComponent<Rigidbody2D>().isKinematic = false;
        gameObject.GetComponent<Rigidbody2D>().mass = varStats.currentWeight;
        gameObject.GetComponent<Collider2D>().isTrigger = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
