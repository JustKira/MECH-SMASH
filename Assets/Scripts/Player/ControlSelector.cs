using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSelector : MonoBehaviour
{

    private static ControlSelector instance;
    public static ControlSelector Instance
    {
        get
        {
            if (instance == null)
            {
                if (GameObject.Find("ControlsManager") == null)
                {

                    var g = (new GameObject("ControlsManager"));
                    g.AddComponent<ControlSelector>();
                    instance = g.GetComponent<ControlSelector>();
                }
                else
                {
                    if (GameObject.Find("ControlsManager").GetComponent<ControlSelector>() != null)
                    {
                        instance = GameObject.Find("ControlsManager").GetComponent<ControlSelector>();
                    }
                    else
                    {
                        var g = (new GameObject("ControlsManager"));
                        g.AddComponent<ControlSelector>();
                        instance = g.GetComponent<ControlSelector>();
                    }


                }
            }
            DontDestroyOnLoad(instance);
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public List<Controls> controls;
    public Transform playerParent;
    private void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(7, 8, true);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        if (controls == null || controls.Count == 0)
        {
            controls = new List<Controls>(2);
            controls.Add (new Controls());
            controls[0].up = KeyCode.UpArrow;
            controls[0].down = KeyCode.DownArrow;
            controls[0].left = KeyCode.LeftArrow;
            controls[0].right=KeyCode.RightArrow;
            controls[0].attack = KeyCode.Z;

            controls.Add(new Controls());
            controls[1].up = KeyCode.W;
            controls[1].down = KeyCode.S;
            controls[1].left = KeyCode.A;
            controls[1].right = KeyCode.D;
            controls[1].attack = KeyCode.K;
        }
    }
}

[System.Serializable]
public class Controls
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode attack;
    public KeyCode interact;
}