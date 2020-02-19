using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVolume : MonoBehaviour
{
    //手のオブジェクトを入れる箱
    public GameObject volumeHand; //左手
    public GameObject pitchHand; // 右手
    //アンテナのオブジェクトを入れる箱
    public GameObject volumeAntenna;
    public GameObject pitchAntenna;
    //音源を入れる箱
    public AudioSource audioSource;
    public AudioClip sound1;

    //変数
    public float volumeHandDistance;
    private float volume;
    public float pitchHandDistance;
    private float pitch;
    public int isSoundOn; // 0: OFF 1: ON

    //属性をつける場合定義前に[]で指定する
    [Range(0.1f, 50)]
    public float volumeSensitivity; //高いほど付近での音量変化が大きくなる(デフォルト: 0.1) ????
    [Range(0.1f, 50)]
    public float pitchSensitivity; //高いほど付近でのピッチ変化が大きくなる(デフォルト: 5) ????
    [Range(0,10)]
    public float pitchMax; //ピッチの最大値(デフォルト: 4)
    [Range(0, 10)]
    public float pitchMin; //ピッチの最小値(デフォルト: 0.25)


    // Start is called before the first frame update
    void Start()
    {
        //初期値
        isSoundOn = 0;


        //音源の指定
        audioSource = gameObject.GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        //音量の変更をする
        /*Mathf.Maxで左手とアンテナの距離を出す
         * .Maxで()内の数値の一番大きい数が入る
         * 今回の場合左手のy軸の高さからアンテナのy軸の高さを引きその差の数値を入れる
         * 左手の高さがアンテナよりも上だった場合正の数値が出力され
         * 手がアンテナよりも低い場合マイナスになり隣の0が出力される
        */
        volumeHandDistance = Mathf.Max(volumeHand.transform.position.y - volumeAntenna.transform.position.y, 0);
        /*音量の大きさを決める
         * AudioSource内のVolume(AudioSource.Volume)を変更することで音のおおきさを変えることができる
         * Max: 1 Min: 0
         * 
         * 今回の場合最大値の1から左手の距離と　音量変化の変数？　で決まる
         * .Expで乗数を得る　e^(カッコ内の数値)
         */
        volume = 1 - Mathf.Exp(-volumeHandDistance * volumeSensitivity);
        audioSource.volume = volume;

        //ピッチを変更する
        /*HolizontalDistanceでアンテナとの距離を出す
         * HolizontalDistanceはUpdate関数の下に記述
         * 左手とほぼ同様に計算し出力
         * 式はよくわからんきっといい感じなんだろう
         */
         
        pitchHandDistance = HorizontalDistance(pitchHand.transform.position, pitchAntenna.transform.position);
        pitch = Mathf.Exp(-pitchHandDistance * pitchSensitivity) * (pitchMax - pitchMin) + pitchMin;
        audioSource.pitch = pitch;

        /*
         コリダー内に入っているときに音が鳴るようにしたら戻せなくなった
         IsSoundOnスクリプトで衝突判定を行っている
         */
        if(isSoundOn == 1)
        {
            audioSource.PlayOneShot(sound1);
        }
        if(isSoundOn == 0)
        {
            audioSource.Stop();
        }
        
    }


    //水平方向の距離を求める
    //x,y,zの３つの情報を持っていたものを二つに変換する関数
    float HorizontalDistance(Vector3 v1, Vector3 v2)
    {
        return Vector2.Distance(new Vector2(v1.x, v1.z), new Vector2(v2.x, v2.z));
    }



}
