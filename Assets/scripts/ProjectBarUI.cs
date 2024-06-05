using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private UnityEngine.UI.Image BarImage;
    private void Start()
    {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
        BarImage.fillAmount = 0f;
        hide();
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnprogressChangedEventArgs e)
    {
        BarImage.fillAmount = e.progressNormalized;
        if(e.progressNormalized== 0f ||  e.progressNormalized== 1f)
        {
            hide();
        }
        else
        {
            show();
        }
    }

    private void show()
    {
        gameObject.SetActive(true);
    }
    private void hide()
    {
        gameObject.SetActive(false);
    }
} 
