using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : PartController
{
    public HeadStatsManager activeStats;
    // Start is called before the first frame update
    void Start()
    {
        varStats = gameObject.AddComponent<VariableStats>();
        activeStats = new HeadStatsManager(maxStatPoints, attachmentHealth, activationHealth, weight, varStats);
        varStats.currentAttachmentHealth = activeStats.attachmentHealth.value;
        varStats.currentActivationHealth = activeStats.activationHealth.value;
        varStats.currentWeight = activeStats.weight.value;
        varStats.currentSpeed = 0;
        varStats.currentAcceleration = 0;
        hitBox = gameObject.GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null && transform.parent.name.Contains("Mech"))
        {
            varStats.currentSpeed = transform.parent.GetComponent<MechController>().wheelsController.varStats.currentSpeed;
            varStats.currentAcceleration = transform.parent.GetComponent<MechController>().wheelsController.varStats.currentAcceleration;
        }
    }
    public override void Deactivate()
    {
        varStats.currentActivationHealth = 0;
        MechController mech = transform.parent.GetComponent<MechController>();
        mech.armsController.Deactivate();
        mech.bodyController.Deactivate();
        //mech.wheelsController.Deactivate();
    }

    public override void Detach()
    {
        transform.parent.GetComponent<Rigidbody2D>().mass -= varStats.currentWeight;
        varStats.currentAttachmentHealth = 0;
        gameObject.GetComponent<Collider2D>().isTrigger = false;
        transform.SetParent(transform.parent.parent, true);
    }
    
}
