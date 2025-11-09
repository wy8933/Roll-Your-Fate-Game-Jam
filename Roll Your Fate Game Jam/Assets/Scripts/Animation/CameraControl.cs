using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("跟随目标")]
    public Transform target;            // 要注视的物体

    [Header("旋转平滑度")]
    [Tooltip("旋转插值速度，数值越大越快。0 表示瞬间转向。")]
    public float rotationSmooth = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // 计算目标方向
        Vector3 direction = target.position - transform.position;
        if (direction.sqrMagnitude < 0.0001f) return; // 防止除零错误

        // 计算目标旋转
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

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

    // 场景中可视化线条
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
