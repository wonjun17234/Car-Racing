using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("car set")]
    public float driftFactor = 0.95f; //�巡��Ʈ �ӵ�
    public float speed = 30.0f; //���ӵ�
    public float maxSpeed = 30f; //�ִ� �ӵ�
    public float turnFactor = 3.5f; //ȸ�� �ӵ�

    float accelerationInput = 0; // ws horizontal
    float steerintInput = 0; // ad vertical

    float rotationAngle = 0; //ȸ���� �󸶳� �Ұ��� ������ ����

    float velocityVsUp = 0;

    Rigidbody2D rigid;

    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }

    void ApplyEngineForce() //������ ���� �Լ�
    {
        velocityVsUp = Vector2.Dot(transform.up, rigid.velocity);

        if(velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }

        if (rigid.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }


        if (accelerationInput == 0) // ���� ws�� �ȴ����� ������ ���װ� ������Ű��
        {
            rigid.drag = Mathf.Lerp(rigid.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            rigid.drag = 0; //������ ���װ� ���ֱ�
        }
        Vector2 engineForceVector = transform.up * accelerationInput * speed; //���� �������� �����̱�

        rigid.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void KillOrthogonalVelocity() //�帮��Ʈ ����
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rigid.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rigid.velocity, transform.right);

        rigid.velocity = forwardVelocity + rightVelocity * driftFactor;
    }


    void ApplySteering() //ȸ���Ѵ� �Լ�, �ּ� ���ǵ庸�� ������ ȸ�� X
    {
        float minSpeed = (rigid.velocity.magnitude / 8);

        minSpeed = Mathf.Clamp01(minSpeed);

        rotationAngle -= steerintInput * turnFactor * minSpeed;

        rigid.MoveRotation(rotationAngle);
    }


    float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, rigid.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBreaking)
    {
        lateralVelocity = GetLateralVelocity();
        isBreaking = false;

        if(accelerationInput < 0 && velocityVsUp > 0)
        {
            isBreaking = true;
            return true;
        }

        if(Mathf.Abs(GetLateralVelocity()) > 4.0f)
        {
            return true;
        }


        return false;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steerintInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
