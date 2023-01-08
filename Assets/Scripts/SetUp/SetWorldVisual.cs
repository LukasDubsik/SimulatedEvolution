using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine.Rendering.HighDefinition;

public class SetWorldVisual
{
    
    public static void SetUpWaterView(float intensity, float scatter, bool isLight, int lightIntensity, float sunIntensity, float rippleIntensity, float blur) //In future depth and surface should be included, currently only depth view (midnight zone)
    {
        // Calling all functions in order
        SetFlashlight(isLight, lightIntensity);
        SetSun(sunIntensity);
        SetWaterVolume(intensity, scatter);
        SetWaterShader(rippleIntensity, blur);
    }

    public static void TurnOffWaterView() //In future depth and surface should be included, currently only depth view (midnight zone)
    {
        GameObject light = GameObject.Find("Flashlight");
        light.GetComponent<Light>().enabled = false;

        GameObject sun = GameObject.Find("Sun");
        sun.GetComponent<HDAdditionalLightData>().intensity = 100000;

        GameObject waterVolume = GameObject.Find("WaterVolume");
        waterVolume.GetComponent<Volume>().enabled = false;

        GameObject waterShader = GameObject.Find("WaterShader");
        waterShader.GetComponent<CustomPassVolume>().enabled = false;
    }

    public static void SetFlashlight(bool isLight, int lightIntensity)
    {
        //Set the intensity of flashlight
        GameObject light = GameObject.Find("Flashlight");
        light.GetComponent<Light>().enabled = isLight;
        light.GetComponent<HDAdditionalLightData>().intensity = lightIntensity;
    }

    public static void SetSun(float sunIntensity)
    {
        //Set The sun intensity to water (Currently always zero, since depth)
        GameObject sun = GameObject.Find("Sun");
        sun.GetComponent<HDAdditionalLightData>().intensity = sunIntensity;
    }

    public static void SetWaterVolume(float intensity, float scatter)
    {
        //SetUp water volume, for depth of field and bloom
        GameObject waterVolume = GameObject.Find("WaterVolume");
        Bloom bloom = null; Bloom tmp;
        if (waterVolume.GetComponent<Volume>().profile.TryGet<Bloom>(out tmp))
        {
            bloom = tmp;
        }
        bloom.intensity.value = intensity;
        bloom.scatter.value = scatter;
        waterVolume.GetComponent<Volume>().enabled = true;
    }

    public static void SetWaterShader(float rippleIntensity, float blur)
    {

        //Set values for water effect shader
        AssignMaterials materials = GameObject.FindObjectOfType<AssignMaterials>();
        materials.waterEffectMaterial.SetFloat("_RippleIntensity", rippleIntensity);
        materials.waterEffectMaterial.SetFloat("_Blur", blur);

        //Activate the shader volume
        GameObject waterShader = GameObject.Find("WaterShader");
        waterShader.GetComponent<CustomPassVolume>().enabled = true;
    }

}
