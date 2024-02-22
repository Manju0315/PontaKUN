using CollectReflect.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditor.PlayerSettings;


public class PlayerController : MonoBehaviour
{

    Rigidbody2D rigid2d;
    Vector2 startPos;
    Vector2 direction;
    float speed;
    bool moveMode;

    void Start()
    {
        this.rigid2d = GetComponent<Rigidbody2D>();
        //this.speed = 1000;

    }

  
    void Update()
    {

        // �}�E�X���������n�_�̍��W���L�^
        if (Input.GetMouseButtonDown(0))
        {
            this.startPos = Input.mousePosition;
            GetComponent<ReflectRender>().MouseDown();

            //this.rigid2d.velocity *= 0;
            moveMode = false;
        }

        // �}�E�X�𗣂����n�_�̍��W����A���˕������v�Z
        if (Input.GetMouseButtonUp(0))
        {
            moveMode = true;

            Vector2 endPos = Input.mousePosition;
            direction = -1 * (endPos - startPos).normalized;
            //speed = endpos.magnitude;

            /* �^�[�Q�b�g�ƃv���C���[�̋������擾 */
            speed = Vector2.Distance(this.startPos, endPos);

            //transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);



            this.rigid2d.AddForce(direction * speed);
            Debug.Log(speed);
        }


        // �e�X�g�p�F�X�y�[�X�L�[�����Œ�~
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.rigid2d.velocity *= 0;
        }


        var pos = Camera.main.WorldToScreenPoint(transform.localPosition);
        var rotation = Quaternion.LookRotation(Vector3.forward, -Input.mousePosition + pos);

        if (moveMode)
        {
            Time.timeScale = 1.0f;

            //transform.rotation = Quaternion.FromToRotation(Vector3.up, this.transform.position);
            transform.localRotation = Quaternion.FromToRotation(Vector3.up, rigid2d.velocity);
            GetComponent<ReflectRender>().MouseUp();
        }
        else
        {
            Time.timeScale = 0.05f;
            transform.localRotation = rotation;
            GetComponent<ReflectRender>().MouseDrag();
        }
    }


    void FixedUpdate()
    {

        this.rigid2d.velocity *= 0.995f;

    }

}