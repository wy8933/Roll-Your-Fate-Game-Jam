using UnityEngine;

/// <summary>
/// 让物体沿路径点依次移动的脚本。
/// 可设置循环或往返移动。
/// </summary>
public class PathMover : MonoBehaviour
{
    public Transform[] waypoints;    // 路径点
    public float speed = 3f;         // 移动速度
    public bool loop = true;         // 是否循环
    public bool pingPong = false;    // 是否往返
    public bool lookForward = true;  // 是否面向移动方向

    private int index = 0;
    private int direction = 1;

    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("PathMover：请在 Inspector 中设置路径点！");
            enabled = false;
        }
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[index];
        Vector3 moveDir = target.position - transform.position;
        float step = speed * Time.deltaTime;

        // 到达当前路径点
        if (moveDir.magnitude <= step)
        {
            transform.position = target.position;
            AdvanceIndex();
        }
        else
        {
            transform.position += moveDir.normalized * step;

            if (lookForward && moveDir.sqrMagnitude > 0.0001f)
            {
                Quaternion lookRot = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 10f);
            }
        }
    }

    void AdvanceIndex()
    {
        if (pingPong)
        {
            if (index == waypoints.Length - 1)
                direction = -1;
            else if (index == 0)
                direction = 1;

            index += direction;
        }
        else if (loop)
        {
            index = (index + 1) % waypoints.Length;
        }
        else
        {
            if (index < waypoints.Length - 1)
                index++;
            else
                enabled = false;
        }
    }

    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Gizmos.color = Color.cyan;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] == null) continue;
            Gizmos.DrawSphere(waypoints[i].position, 0.1f);
            if (i + 1 < waypoints.Length && waypoints[i + 1] != null)
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }
}
