using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PD_Teremin : MonoBehaviour
{
    private string pdPatchName = "../sine.pd"; //Patch that have HRIR spatializer
                                                /// <summary>
                                                /// The scale of distance respectively between sound source and listener in Cm.
                                                /// </summary>
    public float scale = 1f; //Scale of Cm
    private int dollarzero = -999; //Reference of patch    
    private bool _isPlaying = false; //Spatializer is working or not
    public GameObject left;
    public GameObject rigth;
    public GameObject VolumeAntenna;
    public GameObject pitchAntenna;
    [Range(0.1f, 50)]
    public float volumeSensitivity; //高いほど付近での音量変化が大きくなる(デフォルト: 0.1) ????
    [Range(0.1f, 50)]
    public float pitchSensitivity; //高いほど付近でのピッチ変化が大きくなる(デフォルト: 5) ????
    [Range(0, 10)]
    public float pitchMax; //ピッチの最大値(デフォルト: 4)
    [Range(0, 10)]
    public float pitchMin; //ピッチの最小値(デフォルト: 0.25)
    [Range(0, 2)]
    public int type=0;

    public bool isPlaying
    {
        get
        {
            return _isPlaying;
        }
    }
    public void Update_left(float f)
    {
        PdManager.Instance.Send(dollarzero.ToString() + "-left", f);
    }
    public void Update_right(float f)
    {
        PdManager.Instance.Send(dollarzero.ToString() + "-right", f);
    }
    public void Select_source(float f)
    {        
        PdManager.Instance.Send(dollarzero.ToString() + "-type", f);
    }

    /// <summary>
    /// Dispose sound spatializer for sound sources object
    /// </summary>
    public void Available()
    {
        if (_isPlaying)
        {
            Debug.LogWarning("pdteremin was already enabled");
            return;
        }
        if (scale <= 0f) scale = 1f;
        dollarzero = PdManager.Instance.OpenNewPdPatch(PdManager.Instance.APIPath(pdPatchName));
        _isPlaying = true;
    }

    /// <summary>
    /// Disable sound spatializer for sound sources object
    /// </summary>
    public void Disable()
    {
        if (!_isPlaying)
        {
            Debug.LogWarning("pdtermin was already disable");
            return;
        }        
        PdManager.Instance.ClosePdPatch(dollarzero);        
        _isPlaying = false;
    }

    void OnDestroy()
    {
        if (PdManager.Instance.pdDsp)
            Disable();
    }

    private void Start()
    {
        Available();
        Select_source((float)type);
    }

    // Update is called once per frame
    void Update()
    {
        //here insert the updates
        //float volume = 1 - Mathf.Exp(-Mathf.Max(left.transform.position.y - VolumeAntenna.transform.position.y, 0) * volumeSensitivity);
        Update_left(Mathf.Min(Mathf.Abs(left.transform.position.y - VolumeAntenna.transform.position.y)*scale,100));
        //float pitchHandDistance = Mathf.Min(Mathf.Abs(HorizontalDistance(rigth.transform.position, pitchAntenna.transform.position)*scale),127);
        //float pitch = Mathf.Exp(-pitchHandDistance * pitchSensitivity) * (pitchMax - pitchMin) + pitchMin;
        Update_right(Mathf.Min(Mathf.Abs(rigth.transform.position.x - pitchAntenna.transform.position.x) * scale, 127));
        //Select_source();//CHANGE THE TYPE OF SOUND SOURCE
    }

    float HorizontalDistance(Vector3 v1, Vector3 v2)
    {
        return Vector2.Distance(new Vector2(v1.x, v1.z), new Vector2(v2.x, v2.z));
    }
}
