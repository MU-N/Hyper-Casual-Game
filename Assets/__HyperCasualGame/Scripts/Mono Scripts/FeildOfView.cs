using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeildOfView : MonoBehaviour
{


    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float distance;
        public float angle;


        public ViewCastInfo(bool _hit, Vector3 _point, float _distance, float _angle)
        {
            hit = _hit;
            point = _point;
            distance = _distance;
            angle = _angle;
        }
    };

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }



    [SerializeField] public float viewRadius;
    [Range(0, 360)]
    [SerializeField] public float viewAngle;
    [SerializeField] public LayerMask whatIsTarget;
    [SerializeField] public LayerMask whatIsObstacles;


    [SerializeField] public float meshResultion;
    [SerializeField] public int edgeResolveIterations;
    [SerializeField] public float edgeDstThreshold;

    [SerializeField] public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    [HideInInspector] public List<Transform> visableTargets = new List<Transform>();
    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        StartCoroutine("FindTragetWithDelay", 0.2f);

    }

    private void LateUpdate()
    {
        DrawFildOfView();
    }

    IEnumerator FindTragetWithDelay(float dealy)
    {
        while (true)
        {
            yield return new WaitForSeconds(dealy);
            FindVisableTarget();

        }
    }


    private void FindVisableTarget()
    {
        visableTargets.Clear();
        Collider[] targetsInVeiwRaduis = Physics.OverlapSphere(transform.position, viewRadius, whatIsTarget);
        for (int i = 0; i < targetsInVeiwRaduis.Length; i++)
        {
            Transform target = targetsInVeiwRaduis[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float disToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, disToTarget, whatIsObstacles))
                {
                    // enemy now can shoot traget 
                    visableTargets.Add(target);
                    if(target.GetComponent<PlayerTouchController>()!=null)
                    {
                    //TODO: CALL SHHOT ANIMTION ;
                    //TODO: CALL GAME LOSE
                    }
                    
                }
            }
        }
    }

    private void DrawFildOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResultion);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i < stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDstThresholdExceded = Mathf.Abs(oldViewCast.distance - newViewCast.distance) > edgeDstThreshold; 
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit &&edgeDstThresholdExceded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if(edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();

    }


    public Vector3 DirFromAngle(float angleInDeg, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDeg += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 direction = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, viewRadius, whatIsObstacles))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
            return new ViewCastInfo(false, transform.position + direction * viewRadius, viewRadius, globalAngle);
    }


    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);
            bool edgeDstThresholdExceded = Mathf.Abs(minViewCast.distance - newViewCast.distance) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else 
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }
        return new EdgeInfo(minPoint, maxPoint);
    }

}
