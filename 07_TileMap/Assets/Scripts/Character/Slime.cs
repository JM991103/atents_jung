using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Slime : MonoBehaviour
{
    public float phaseDuration = 0.5f;
    public float dissolveDuration = 1.0f;
    const float outline_Thickness = 0.005f;

    Material mainMaterial;

    public Action onPhaseEnd;
    public Action onDie;
    public Action onDisable;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        mainMaterial = renderer.material;
    }

    private void OnEnable()
    {
        ShowOutLine(false);
        StartCoroutine(StartPhase());
        mainMaterial.SetFloat("_Dissolve_Fade", 1.0f);
    }

    private void OnDisable()
    {
        onDisable?.Invoke();
    }

    IEnumerator StartPhase()
    {
        mainMaterial.SetFloat("_Phase_Thickness", 0.1f);
        mainMaterial.SetFloat("_Phase_Split", 0.0f);

        float timeElipsed = 0.0f;
        float phaseDurationNormalize = 1 / phaseDuration;

        while (timeElipsed < phaseDuration)
        {
            timeElipsed += Time.deltaTime;

            mainMaterial.SetFloat("_Phase_Split", timeElipsed * phaseDurationNormalize);

            yield return null;
        }

        mainMaterial.SetFloat("_Phase_Thickness", 0.0f);
        onPhaseEnd?.Invoke();
    }

    public void Die()
    {
        StartCoroutine(StartDisslove());
        onDie?.Invoke();
    }

    IEnumerator StartDisslove()
    {

        mainMaterial.SetFloat("_Dissolve_Fade", 1.0f);

        float timeElipsed = 0.0f;
        float dessolveDurationNormalize = 1 / dissolveDuration;

        while (timeElipsed < dissolveDuration)
        {
            timeElipsed += Time.deltaTime;

            mainMaterial.SetFloat("_Dissolve_Fade", 1 - timeElipsed * dessolveDurationNormalize);

            yield return null;
        }

        this.gameObject.SetActive(false);
    }

    public void ShowOutLine(bool isShow)
    {
        if (isShow)
        {
            mainMaterial.SetFloat("_OutLine_Thickness", outline_Thickness);
        }
        else
        {
            mainMaterial.SetFloat("_OutLine_Thickness", 0.0f);
        }
    }
}
