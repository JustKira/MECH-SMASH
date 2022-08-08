using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : PartController
{
    [HideInInspector]
    public BodyStatsManager activeStats;
    private Transform head;
    private Transform arms;
    [HideInInspector]
    public bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        varStats = gameObject.AddComponent<VariableStats>();
        activeStats = new BodyStatsManager(maxStatPoints, attachmentHealth, activationHealth, weight, varStats);
        head = transform.parent.Find("Head");
        arms = transform.parent.Find("Arms");
        varStats.currentAttachmentHealth = activeStats.attachmentHealth.value;
        varStats.currentActivationHealth = activeStats.activationHealth.value;
        varStats.currentWeight = activeStats.weight.value;
        varStats.currentSpeed = 0;
        varStats.currentAcceleration = 0;
        hitBox = gameObject.GetComponent<BoxCollider2D>();
        hitBox.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null && transform.parent.name.ToLower().Contains("mech"))
        {
            
            varStats.currentSpeed = transform.parent.GetComponent<MechController>().wheelsController.varStats.currentSpeed;
            varStats.currentAcceleration = transform.parent.GetComponent<MechController>().wheelsController.varStats.currentAcceleration;
        }

    }
    public void Deactivate()
    {
        this.enabled = false;
        varStats.currentActivationHealth = 0;
    }
    public void Detach()
    {
        varStats.currentAttachmentHealth = 0;
        gameObject.GetComponent<Rigidbody2D>().mass = varStats.currentWeight;
        gameObject.GetComponent<Collider2D>().isTrigger = false;
        transform.parent.GetComponent<Rigidbody2D>().mass -= varStats.currentWeight;
        transform.parent = null;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent != null && other.transform.parent.GetComponent<MechController>() != null)
        {
            /*int damage = DamageCalculator.instance.CalculateDamage(varStats, other.GetComponent<VariableStats>());
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
