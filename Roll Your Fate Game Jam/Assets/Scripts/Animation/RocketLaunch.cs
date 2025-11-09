using UnityEngine;

public class RocketLaunch : MonoBehaviour
{
   public Transform[] waypoints;      // 路径点（至少 2 个）
    public float speed = 3f;           // 移动速度（单位/s）
    public bool loop = true;           // 到最后一个点后回到第一个点（循环）
    public bool pingPong = false;      // 往返（与 loop 互斥；若同时为 true，以 pingPong 为准）
    public bool lookForward = true;    // 是否朝向移动方向旋转
    public float rotateSpeed = 10f;    // 旋转插值速度
    public float waitAtPoint = 0f;     // 到达每个点的停顿时间（秒）

    int index = 0;
    int direction = 1;                // pingpong 时使用（1 或 -1）
    bool waiting = false;
    float waitTimer = 0f;

    void Start()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("PathMover: 未设置任何 waypoints。脚本将停用。", this);
            enabled = false;
            return;
        }

        // 将物体初始放到第一个点（可选，注释掉保持当前位置）
        // transform.position = waypoints[0].position;
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        if (waiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f) waiting = false;
            else return;
        }

        Transform targetPoint = waypoints[index];
        Vector3 targetPos = targetPoint.position;
        // 移动
        Vector3 moveDir = (targetPos - transform.position);
        float distanceThisFrame = speed * Time.deltaTime;

        if (moveDir.sqrMagnitude <= distanceThisFrame * distanceThisFrame)
        {
            // 到达
            transform.position = targetPos;
            if (lookForward && moveDir.sqrMagnitude > 0.0001f)
            {
                // 最后一小段也朝向目标
                Quaternion finalRot = Quaternion.LookRotation(moveDir.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, finalRot, Time.deltaTime * rotateSpeed);
            }

            // 处理等待
            if (waitAtPoint > 0f)
            {
                waiting = true;
                waitTimer = waitAtPoint;
            }

            // 选择下一个 index
            AdvanceIndex();
        }
        else
        {
            // 正常移动
            Vector3 step = moveDir.normalized * distanceThisFrame;
            transform.position += step;

            if (lookForward)
            {
                Quaternion targetRot = Quaternion.LookRotation(moveDir.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotateSpeed);
            }
        }
    }

    void AdvanceIndex()
    {
        if (pingPong)
        {
            // 往返
            if (index == waypoints.Length - 1)
            {
                direction = -1;
            }
            else if (index == 0)
            {
                direction = 1;
            }
            index += direction;
        }
        else if (loop)
        {
            index = (index + 1) % waypoints.Length;
        }
        else
        {
            // 非循环且非往返：到达最后一个点后停在最后一个点（不再移动）
            if (index < waypoints.Length - 1)
                index++;
            else
                enabled = false;
        }
    }

    // 在编辑器中绘制路径线段，便于调试
    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0) return;
        Gizmos.color = Color.cyan;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] == null) continue;
            Gizmos.DrawSphere(waypoints[i].position, 0.08f);
            if (i + 1 < waypoints.Length && waypoints[i + 1] != null)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
            else if (loop && i == waypoints.Length - 1 && waypoints[0] != null)
            {
                // loop 时把最后连回第一个
                Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
            }
        }
    } 
}
