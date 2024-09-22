using CitrioN.Common;
using UnityEngine;
#if TND_DLSS && UNITY_POST_PROCESSING_STACK_V2 && UNITY_BIRP
using UnityEngine.Rendering.PostProcessing;
using TND.DLSS;
#endif

namespace CitrioN.SettingsMenuCreator.Integrations
{
#if TND_DLSS && UNITY_POST_PROCESSING_STACK_V2 && UNITY_BIRP
  [ExcludeFromMenuSelection]
  [MenuPath("Integrations/DLSS/Post Processing")]
  public abstract class Setting_DLSS_PostProcessing<T> : Setting_Generic_Reflection_Field<T, DLSS>
  {
    public override string EditorNamePrefix => "[DLSS]";

    [SerializeField]
    [Tooltip("Should the DLSS reference be cached for future access?\n\n" +
             "While better for performance it will not work when\n" +
             "the script is attached to a different camera after caching.\n\n" +
             "Default: true")]
    protected bool cacheReference = true;

    protected bool isCached = false;

    // TODO Should some initialization method reset this?
    // like on game start?
    protected DLSS dlss;

    public override bool StoreValueInternally => true;

    protected DLSS FindReference()
    {
      var allCameras = Camera.allCameras;
      Camera cam = null;
      PostProcessLayer postProcessLayer = null;

      for (int i = 0; i < allCameras.Length; i++)
      {
        cam = allCameras[i];
        if (cam == null) { continue; }
        if (cam.TryGetComponent(out postProcessLayer))
        {
          if (postProcessLayer != null)
          {
            postProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.DLSS;
            return postProcessLayer.dlss;
          }
        }
      }

      cam = Camera.main;
      if (cam == null) { return null; }

      postProcessLayer = cam.gameObject.AddComponent<PostProcessLayer>();
      postProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.DLSS;
      return postProcessLayer.dlss;
    }

    public override object GetObject(SettingsCollection settings)
    {
      if (isCached) { return dlss; }
      else
      {
        var reference = FindReference();
        if (cacheReference)
        {
          dlss = reference;
        }
        return reference;
      }
    }
  }


  [System.ComponentModel.DisplayName("DLSS Quality (Builtin)")]
  public class Setting_DLSS_PP_Quality : Setting_DLSS_PostProcessing<DLSS_Quality>
  {
    public override string EditorName => $"{RuntimeName} (Builtin)";

    public override string RuntimeName => "DLSS Quality";

    public override string FieldName => nameof(DLSS.qualityMode);

    public Setting_DLSS_PP_Quality()
    {
      options.Clear();
      options.Add(new StringToStringRelation(nameof(DLSS_Quality.Off), "Off"));
      options.Add(new StringToStringRelation(nameof(DLSS_Quality.DLAA), "DLAA"));
      options.Add(new StringToStringRelation(nameof(DLSS_Quality.MaximumQuality), "Maximum Quality"));
      options.Add(new StringToStringRelation(nameof(DLSS_Quality.Balanced), "Balanced"));
      options.Add(new StringToStringRelation(nameof(DLSS_Quality.MaximumPerformance), "Maximum Performance"));
      options.Add(new StringToStringRelation(nameof(DLSS_Quality.UltraPerformance), "Ultra Performance"));

      defaultValue = DLSS_Quality.Balanced;
    }
  }

  [System.ComponentModel.DisplayName("DLSS Anti Ghosting (Builtin)")]
  public class Setting_DLSS_PP_AntiGhosting : Setting_DLSS_PostProcessing<float>
  {
    public override string EditorName => $"{RuntimeName} (Builtin)";

    public override string RuntimeName => "DLSS Anti Ghosting";

    public override string FieldName => nameof(DLSS.antiGhosting);

    public Setting_DLSS_PP_AntiGhosting()
    {
      options.AddMinMaxRangeValues("0", "1");
      options.AddStepSize("0.01");
      defaultValue = 0.1f;
    }
  }

  [System.ComponentModel.DisplayName("DLSS Sharpening (Builtin)")]
  public class Setting_DLSS_PP_Sharpening : Setting_DLSS_PostProcessing<bool>
  {
    public override string EditorName => $"{RuntimeName} (Builtin)";

    public override string RuntimeName => "DLSS Sharpening";

    public override string FieldName => nameof(DLSS.Sharpening);
  }

  [System.ComponentModel.DisplayName("DLSS Sharpness (Builtin)")]
  public class Setting_DLSS_PP_Sharpness : Setting_DLSS_PostProcessing<float>
  {
    public override string EditorName => $"{RuntimeName} (Builtin)";

    public override string RuntimeName => "DLSS Sharpness";

    public override string FieldName => nameof(DLSS.sharpness);

    public Setting_DLSS_PP_Sharpness()
    {
      options.AddMinMaxRangeValues("0", "1");
      options.AddStepSize("0.01");
      defaultValue = 0.5f;
    }
  }

  [System.ComponentModel.DisplayName("DLSS Auto Texture Update (Builtin)")]
  public class Setting_DLSS_PP_AutoTextureUpdate : Setting_DLSS_PostProcessing<bool>
  {
    public override string EditorName => $"{RuntimeName} (Builtin)";

    public override string RuntimeName => "DLSS Auto Texture Update";

    public override string FieldName => nameof(DLSS.autoTextureUpdate);
  }

  [System.ComponentModel.DisplayName("DLSS Mipmap Bias Override (Builtin)")]
  public class Setting_DLSS_PP_MipmapBiasOverride : Setting_DLSS_PostProcessing<float>
  {
    public override string EditorName => $"{RuntimeName} (Builtin)";

    public override string RuntimeName => "DLSS Mipmap Bias Override";

    public override string FieldName => nameof(DLSS.mipMapBiasOverride);

    public Setting_DLSS_PP_MipmapBiasOverride()
    {
      options.AddMinMaxRangeValues("0", "1");
      options.AddStepSize("0.01");
      defaultValue = 1.0f;
    }
  }
#endif
}