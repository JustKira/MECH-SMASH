using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RectTransform))]
[RequireComponent (typeof(Collider2D))]
public abstract class PartController : MonoBehaviour
{
    public int maxStatPoints;
    public VariableStats varStats;
    public Stat attachmentHealth;
    public Stat activationHealth;
    public Stat weight;
    [HideInInspector]
    public BoxCollider2D hitBox;

    public abstract void Deactivate();
    public abstract void Detach();
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        if (other.transform.parent != null && other.transform.parent.GetComponent<MechController>() != null)
        {

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

        }
    }

}
