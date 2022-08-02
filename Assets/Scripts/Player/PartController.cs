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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
