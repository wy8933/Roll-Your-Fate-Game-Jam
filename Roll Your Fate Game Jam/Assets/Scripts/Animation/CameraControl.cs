using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("注视目标")]
    public Transform target;            // 要注视的物体

    [Header("旋转平滑度")]
    [Tooltip("旋转插值速度，数值越大越快。0 表示瞬间转向。")]
    public float rotationSmooth = 5f;

    [Header("偏移角度 (度数)")]
    [Tooltip("X=上下俯仰，Y=左右偏转，Z=滚转")]
    public Vector3 rotationOffset = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // 计算目标方向
        Vector3 direction = target.position - transform.position;
        if (direction.sqrMagnitude < 0.0001f) return;

        // 计算基础朝向
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);

        // 应用额外角度偏移（欧拉角）
        Quaternion offsetRotation = Quaternion.Euler(rotationOffset);
        Quaternion targetRotation = lookRotation * offsetRotation;

        // 平滑或立即旋转
        if (rotationSmooth > 0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);
        }
        else
        {
            transform.rotation = targetRotation;
        }
    }

    void OnDrawGizmos()
    {
        if (target)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, target.position);
            Gizmos.DrawSphere(target.position, 0.1f);
        }
    }
}
