using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Bezier : MonoBehaviour
{
    public Transform p0; //시작점(고정)
    public Transform p3; //도착점(고정)


    [Header("Random Ranges")]
    public float p1Radius = 2f;    //p0 근처에서 뽑는 반경
    public float p2Radius = 2f;    //p3 근처에서 뽑는 반경
    public float p1Height = 3f;    //p1 Y축 추가 높이(선택)
    public float p2Height = 3f;    //p2 Y축 추가 높이(선택)

    //결과 제어점
    [HideInInspector] public Vector3 p1;
    [HideInInspector] public Vector3 p2;

    List<Vector3> points;
    float time = 0f;

    void Awake()
    {
    }
    void Update()
    {
        time += Time.deltaTime / 2f;
        transform.position = DeCastljau(points, time);
    }
    public void StartShooting()
    {
        GenerateRandomControlPoints();
        points = new List<Vector3> { p0.position, p1, p2, p3.position };
    }
    void GenerateRandomControlPoints()
    {
        Vector2 rand1 = Random.insideUnitCircle * p1Radius;
        p1 = p0.position + new Vector3(rand1.x, 0f, rand1.y);
        p1.y += p1Height;

        Vector2 rand2 = Random.insideUnitCircle * p2Radius;
        p2 = p3.position + new Vector3(rand2.x, 0f, rand2.y);
        p2.y += p2Height;
    }
    Vector3 DeCastljau(List<Vector3> p, float t)
    {
        while (p.Count > 1)
        {
            int last = p.Count - 1;

            var next = new List<Vector3>(last);
            for (int i = 0; i < last; i++)
                next.Add(Vector3.Lerp(p[i], p[i + 1], t));
            p = next;
        }
        return p[0];
    }
    //--------------------------------------------------------------------------
    //de Casteljau 알고리즘

    //public List<Transform> points = new List<Transform>();
    //List<Vector3> pointPositions = new List<Vector3>();

    //float timeValue = 0f;

    //void Awake()
    //{
    //    foreach (var pt in points)
    //    {
    //        if (pt !=  null)
    //            pointPositions.Add(pt.position);
    //    }
    //}
    //void Update()
    //{
    //    timeValue += Time.deltaTime / 2f;
    //    transform.position = DeCasteljau(pointPositions, timeValue);
    //}
    //Vector3 DeCasteljau(List<Vector3> p, float t)
    //{
    //    while (p.Count > 1)
    //    {
    //        int last = p.Count - 1; //마지막 점의 인덱스

    //        var next = new List<Vector3>(last);
    //        for (int i = 0; i < last; i++)
    //        {
    //            next.Add(Vector3.Lerp(p[i], p[i + 1], t));
    //        }
    //        p = next;              //한 단계 줄이기
    //    }
    //    //count가 1이 되면, p[0]에 남은 점이 곡선의 위치
    //    return p[0];                 //남은 한 점이 곡선 위치
    //}

    //--------------------------------------------------------------------------
    //3차 베지어


    //public Transform point0;
    //public Transform point1;
    //public Transform point2;
    //public Transform point3;

    //float timeValue = 0f;


    //void Update()
    //{
    //    timeValue += Time.deltaTime / 2f; //2초동안 애니메이션
    //    transform.position = GetPointOnBezierCurve(point0.position, point1.position, point2.position, point3.position, timeValue);
    //}
    //Vector3 GetPointOnBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    //{
    //    Vector3 a = Vector3.Lerp(p0, p1, t);
    //    Vector3 b = Vector3.Lerp(p1, p2, t);
    //    Vector3 c = Vector3.Lerp(p2, p3, t);
    //    Vector3 ab = Vector3.Lerp(a, b, t);
    //    Vector3 bc = Vector3.Lerp(b, c, t);
    //    Vector3 abc = Vector3.Lerp(ab, bc, t);

    //    return abc;
    //}
}
