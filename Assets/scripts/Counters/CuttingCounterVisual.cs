using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual: MonoBehaviour
{
    [SerializeField] private CuttingCounter CuttingCounter;
    private Animator animator;
    private const string CUT = "Cut";

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Start()
    {
        CuttingCounter.OnCut += CuttingCounter_OnCut;
       
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
