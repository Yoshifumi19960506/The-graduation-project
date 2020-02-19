using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSoundOn : MonoBehaviour
{

    //音のするしないを制御するスクリプト

    //親オブジェクトを入れておく箱
    private GameObject _parent;
    //スクリプト参照に必要な箱
    public PD_Teremin CVscript;

    //親の音源参照する箱
    public AudioClip sound1;
    AudioSource audioSource;

    /*コリダーを使い左手との衝突判定を行う
     左手のモデルにタグを設定しそれが触れている間音が鳴るようにしている
     */

    /*
     * 物を貫通しながら衝突判定を行いたい場合トリガーを使う
     * トリガーはUnity側でチェックボックスをオンにしておくことでできる
     * また、静的オブジェクトのトリガーをオンにしてそれ以外はオフにしておく
     */
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Hit");
        if(other.gameObject.tag == "LeftHand")
        {
            Debug.Log("LHand");
            
            if(CVscript.isPlaying == false)
            {
                CVscript.Available();
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CVscript.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        //オブジェクト取得
        _parent = transform.root.gameObject;
        //参照
        CVscript = _parent.GetComponent<PD_Teremin>();
        //音源取得
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
