using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsController : PartController
{
    [HideInInspector]
    public WheelStatsManager activeStats;
    private Transform mech;
    public Stat maxSpeed;
    public Stat acceleration;
    public Stat grippiness;
    public bool reverse;
    // Start is called before the first frame update
    void Start()
    {
        varStats = gameObject.AddComponent<VariableStats>();
        activeStats = new WheelStatsManager(maxStatPoints, attachmentHealth, activationHealth, weight, acceleration, maxSpeed, grippiness, varStats);
        mech = transform.parent;
        varStats.currentAttachmentHealth = activeStats.attachmentHealth.value;
        varStats.currentActivationHealth = activeStats.activationHealth.value;
        varStats.currentWeight = activeStats.weight.value;
        varStats.currentSpeed = 0;
        varStats.currentAcceleration = activeStats.acceleration.value;
        hitBox = gameObject.GetComponent<BoxCollider2D>();
        hitBox.isTrigger = true;
    }
    void Update()
    {
        if (transform.parent != null && transform.parent.name.Contains("Mech"))
        {
            Move(reverse);
        }
    }

    public void Move(bool Reverse)
    {
        if (varStats.currentActivationHealth <= 0)
            return;
        transform.parent.GetComponentInChildren<BodyController>().moving = true;
        float accel = activeStats.acceleration.value * (Reverse ? -1 : 1);
        if ((varStats.currentSpeed < activeStats.maxSpeed.value && !Reverse) || (varStats.currentSpeed > -activeStats.maxSpeed.value && Reverse))
        {
            varStats.currentSpeed += accel * Time.smoothDeltaTime;

        }
        RaycastHit2D[] hits;
        hits= Physics2D.BoxCastAll(transform.position,0.75f*new Vector2(transform.localScale.x*GetComponent<RectTransform>().rect.width, transform.localScale.y * GetComponent<RectTransform>().rect.height),transform.eulerAngles.x,transform.right,1.0f);
        foreach(var hit in hits)
        {
            if (hit.transform == transform||hit.transform==transform.parent|| hit.transform == null)
            {
                mech.Translate(mech.right * varStats.currentSpeed * Time.smoothDeltaTime, Space.World);
            }
            else if(hit.transform.name.Contains("Mech"))
            {
                {
                    MechController other = hit.transform.GetComponent<MechController>();
                    mech.GetComponent<Rigidbody2D>().AddForce(mech.right * ((other.wheelsController.varStats.currentAcceleration
                        + other.wheelsController.varStats.currentSpeed) / (varStats.currentAcceleration + varStats.currentSpeed))
                        * -1 * hit.transform.GetComponent<Rigidbody2D>().mass / mech.GetComponent<Rigidbody2D>().mass, ForceMode2D.Impulse); ;
                }
                varStats.currentSpeed = 0;
                break;
            }
            else
            {
                varStats.currentSpeed = 0;
            }
        }
        transform.parent.GetComponentInChildren<BodyController>().moving = true;
    }
    public void Deactivate()
    {
        this.enabled = false;
        varStats.currentActivationHealth = 0;
    }
    public void Detach()
    {
        varStats.currentAttachmentHealth = 0;
        gameObject.AddComponent<Rigidbody2D>().isKinematic = false;
        gameObject.GetComponent<Rigidbody2D>().mass = varStats.currentWeight;
        gameObject.GetComponent<Collider2D>().isTrigger = false;
        transform.parent.GetComponent<Rigidbody2D>().mass -= varStats.currentWeight;
        transform.parent = null;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent != null && other.transform.parent.GetComponent<MechController>() != null)
        {
            /*
            int damage = DamageCalculator.instance.CalculateDamage(varStats, other.GetComponent<VariableStats>());
            if (varStats.currentActivationHealth > 0)
            {
                if (varStats.currentActivationHealth - damage <= 0)
                {
                    Deactivate();
                    return;
                }
                varStats.currentActivationHealth -= damage;
            }
            else
            {
                if (varStats.currentAttachmentHealth - damage <= 0 && varStats.currentAttachmentHealth > 0)
                    Detach();

            }
            */
        }
    }
}
