using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
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
    public override void Deactivate()
    {
        this.enabled = false;
        varStats.currentActivationHealth = 0;
    }
    public override void Detach()
    {
        varStats.currentAttachmentHealth = 0;
        gameObject.GetComponent<Collider2D>().isTrigger = false;
        transform.parent.GetComponent<Rigidbody2D>().mass -= varStats.currentWeight;
        transform.SetParent(null,true);
    }
    
}
