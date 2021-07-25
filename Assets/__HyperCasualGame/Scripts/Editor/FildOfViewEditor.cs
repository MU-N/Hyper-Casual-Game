
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FeildOfView))]
public class FildOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FeildOfView fov = (FeildOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position , Vector3.up , Vector3.forward, 360 , fov.viewRadius);
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle/2 , false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle/2 , false);
        Handles.DrawLine(fov.transform.position, fov.transform.position+viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position+viewAngleB * fov.viewRadius);

        Handles.color = Color.grey;
        foreach (Transform item in fov.visableTargets)
        {
            Handles.DrawLine(fov.transform.position, item.position);
        }
    }
}
