using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PPVController : MonoBehaviour
{
    public static PPVController instance;
    private Volume v;
    private Bloom b;
    private Vignette vg;
    private DepthOfField dof;

    [SerializeField]
    [Range(0.1f, 0.2f)]
    private float baseVig;

    [SerializeField]
    [Range(0.3f, 0.7f)]
    private float maxVig;



    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        v = GetComponent<Volume>();
        v.profile.TryGet(out b);
        v.profile.TryGet(out vg);
        v.profile.TryGet(out dof);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVignette(float intensity)
    {
        vg.intensity.value = baseVig + intensity * maxVig;
    }

    public void SetDoF(bool b)
    {
        dof.active = b;
    }
}
