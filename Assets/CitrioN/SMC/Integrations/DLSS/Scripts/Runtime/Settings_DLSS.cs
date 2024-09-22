#if TND_DLSS
using TND.DLSS;
#endif
using CitrioN.Common;
using System.ComponentModel;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator.Integrations
{
#if TND_DLSS
  [ExcludeFromMenuSelection]
  [MenuPath("Integrations/DLSS")]
  public abstract class Setting_DLSS<T> : Setting_Generic_Reflection_Field<T, DLSS_UTILS>
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
    protected DLSS_UTILS dlss;

    public override bool StoreValueInternally => true;

    protected DLSS_UTILS FindReference()
    {
      var allCameras = Camera.allCameras;
      Camera cam = null;
      DLSS_UTILS script = null;

      for (int i = 0; i < allCameras.Length; i++)
      {
        cam = allCameras[i];
        if (cam == null) { continue; }
        if (cam.TryGetComponent(out script))
        {
          if (script != null)
          {
            return script;
          }
        }
      }

      cam = Camera.main;
      if (cam == null) { return null; }

//#if UNITY_BIRP
//      if (script == null)
//      {
//        script = cam.gameObject.AddComponent<DLSS_UTILS>();
//      }
//#endif

#if UNITY_URP
      if (script == null)
        {
          script = cam.gameObject.AddComponent<DLSS_URP>();
        }
#endif

#if UNITY_HDRP
        if (script == null)
        {
          script = cam.gameObject.AddComponent<DLSS_HDRP>();
        }
#endif

      return script;
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


  [DisplayName("DLSS Quality (URP & HDRP)")]
  public class Setting_DLSS_Quality : Setting_DLSS<DLSS_Quality>
  {
    public override string RuntimeName => "DLSS Quality";

    public override string EditorName => $"{RuntimeName} (URP & HDRP)";

    public override string FieldName => nameof(DLSS_UTILS.DLSSQuality);

    public Setting_DLSS_Quality()
    {
      ConsoleLogger.Log("Creating Quality", Common.LogType.Always);
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

  [DisplayName("DLSS Anti Ghosting (URP & HDRP)")]
  public class Setting_DLSS_AntiGhosting : Setting_DLSS<float>
  {
    public override string RuntimeName => "DLSS Anti Ghosting";

    public override string EditorName => $"{RuntimeName} (URP & HDRP)";

    public override string FieldName => nameof(DLSS_UTILS.m_antiGhosting);

    public Setting_DLSS_AntiGhosting()
    {
      options.AddMinMaxRangeValues("0", "1");
      options.AddStepSize("0.01");
      defaultValue = 0.1f;
    }
  }

  [DisplayName("DLSS Sharpening (URP & HDRP)")]
  public class Setting_DLSS_Sharpening : Setting_DLSS<bool>
  {
    public override string RuntimeName => "DLSS Sharpening";

    public override string EditorName => $"{RuntimeName} (URP & HDRP)";

    public override string FieldName => nameof(DLSS_UTILS.sharpening);
  }

  [DisplayName("DLSS Sharpness (URP & HDRP)")]
  public class Setting_DLSS_Sharpness : Setting_DLSS<float>
  {
    public override string RuntimeName => "DLSS Sharpness";

    public override string EditorName => $"{RuntimeName} (URP & HDRP)";

    public override string FieldName => nameof(DLSS_UTILS.sharpness);

    public Setting_DLSS_Sharpness()
    {
      options.AddMinMaxRangeValues("0", "1");
      options.AddStepSize("0.01");
      defaultValue = 0.5f;
    }
  }
#endif
}