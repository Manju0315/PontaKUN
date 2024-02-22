using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace CollectReflect.Scripts
{
    public class ReflectRender : MonoBehaviour
    {
        //���˕���
        private Vector2 _launchDirection;
        //�h���b�O�J�n�ʒu���擾����
        private Vector2 _dragStart = Vector2.zero;
        //���W�b�h�{�f�B
        private Rigidbody2D _rigidBody;
        //���˗\�����̍ő�l
        [SerializeField] private float maxMagnitude = 30f;
        //�\�����̕`��
        [SerializeField] private LineRenderer predictionLineRenderer;
        //wallLayer���w��
        [SerializeField] private LayerMask wallLayer;

        /// <summary>
        /// �N�����ɌĂяo�����֐�
        /// </summary>
        private void Awake()
        {
            //�I�u�W�F�N�g�̈ʒu���擾���邽�߂Ƀ��W�b�h�{�f�B�̎擾
            _rigidBody = GetComponent<Rigidbody2D>();
        }
        /// <summary>
        /// �h���b�O�J�n�C�x���g�n���h��
        /// </summary>
        public void MouseDown()
        {
            //�`����̗\����L���ɂ���
            predictionLineRenderer.enabled = true;
            //�h���b�O�̊J�n�ʒu�����[���h���W�Ŏ擾����
            _dragStart = GetMousePosition();
        }
        /// <summary>
        /// �h���b�O���C�x���g�n���h��
        /// </summary>
        public void MouseDrag()
        {
            //�h���b�O���̃}�E�X�̈ʒu�����[���h���W�Ŏ擾����B
            var position = GetMousePosition();
            //�h���b�O�J�n�_����̋������擾����
            var currentForce = _dragStart - position;
            // MaxMagnitude�ɒ����̒����̐������w�肵�Ă�������𒴂���ꍇ�́A�ő�l�ƂȂ�悤�ɂ��܂��B
            if (currentForce.magnitude > maxMagnitude)
            {
                currentForce *= maxMagnitude / currentForce.magnitude;
            }
            //���˗\������`�悷��
            DrawLineOfReflection(currentForce);
        }
        /// <summary>
        /// �h���b�O�I���C�x���g�n���h��
        /// </summary>
        public void MouseUp()
        {
            predictionLineRenderer.enabled = false;
        }
        /// <summary>
        /// ���[���h���W�̃}�E�X�̏ꏊ���擾
        /// </summary>
        /// <returns>�}�E�X�|�W�V����</returns>
        private Vector2 GetMousePosition()
        {
            //�}�E�X�̏ꏊ���擾
            Vector2 position = Input.mousePosition;
            //���[���h���W�ɕϊ�
            return Camera.main.ScreenToWorldPoint(position);
        }
        /// <summary>
        /// ���˗\�����̕`��
        /// </summary>
        /// <param name="currentForce">���˗\�����̕����Ƒ傫��</param>
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