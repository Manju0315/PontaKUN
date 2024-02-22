using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace CollectReflect.Scripts
{
    public class ReflectRender : MonoBehaviour
    {
        //発射方向
        private Vector2 _launchDirection;
        //ドラッグ開始位置を取得する
        private Vector2 _dragStart = Vector2.zero;
        //リジッドボディ
        private Rigidbody2D _rigidBody;
        //反射予測線の最大値
        [SerializeField] private float maxMagnitude = 30f;
        //予測線の描画
        [SerializeField] private LineRenderer predictionLineRenderer;
        //wallLayerを指定
        [SerializeField] private LayerMask wallLayer;

        /// <summary>
        /// 起動時に呼び出される関数
        /// </summary>
        private void Awake()
        {
            //オブジェクトの位置を取得するためにリジッドボディの取得
            _rigidBody = GetComponent<Rigidbody2D>();
        }
        /// <summary>
        /// ドラッグ開始イベントハンドラ
        /// </summary>
        public void MouseDown()
        {
            //描画線の予測を有効にする
            predictionLineRenderer.enabled = true;
            //ドラッグの開始位置をワールド座標で取得する
            _dragStart = GetMousePosition();
        }
        /// <summary>
        /// ドラッグ中イベントハンドラ
        /// </summary>
        public void MouseDrag()
        {
            //ドラッグ中のマウスの位置をワールド座標で取得する。
            var position = GetMousePosition();
            //ドラッグ開始点からの距離を取得する
            var currentForce = _dragStart - position;
            // MaxMagnitudeに直線の長さの制限を指定しておきそれを超える場合は、最大値となるようにします。
            if (currentForce.magnitude > maxMagnitude)
            {
                currentForce *= maxMagnitude / currentForce.magnitude;
            }
            //反射予測線を描画する
            DrawLineOfReflection(currentForce);
        }
        /// <summary>
        /// ドラッグ終了イベントハンドラ
        /// </summary>
        public void MouseUp()
        {
            predictionLineRenderer.enabled = false;
        }
        /// <summary>
        /// ワールド座標のマウスの場所を取得
        /// </summary>
        /// <returns>マウスポジション</returns>
        private Vector2 GetMousePosition()
        {
            //マウスの場所を取得
            Vector2 position = Input.mousePosition;
            //ワールド座標に変換
            return Camera.main.ScreenToWorldPoint(position);
        }
        /// <summary>
        /// 反射予測線の描画
        /// </summary>
        /// <param name="currentForce">反射予測線の方向と大きさ</param>
        private void DrawLineOfReflection(Vector2 currentForce)
        {
            var poses = ReflectPointer.RefrectionLinePoses(_rigidBody.position, currentForce.normalized, currentForce.magnitude, wallLayer).ToArray();
            predictionLineRenderer.positionCount = poses.Length;
            for (var i = 0; i < poses.Length; i++)
            {
                predictionLineRenderer.SetPosition(i, poses[i]);
            }
        }
    }
}