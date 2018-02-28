using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CCTransformHandle {

    public delegate bool StateCheckerTimeLength(float time, float length);
    public delegate bool StateCheckerTime(float time);

    public static AnimationCurve Linear = AnimationCurve.Linear(0, 0, 1, 1);

    ///CCMove

    public static IEnumerator CCMove(this Transform trans, Vector3 wpos, float sec, AnimationCurve curve = null)
    {
        if (sec <= 0)
            yield break;

        var start = trans.position;
        var c = curve ?? Linear;
        var t = 0f;
        while (t < 1)
        {
            t = Mathf.Min(1f, t + Time.deltaTime / sec);
            trans.transform.position = Vector3.Lerp(start, wpos, c.Evaluate(t));
            yield return null;
        }
    }

    /// <param name="check">움직이는 상태 조건 디폴트는 10초후에 종료</param>
    /// 
    public static IEnumerator CCMove(this Transform trans, Vector3 direction, float speed, StateCheckerTime check = null)
    {
        if (speed <= 0)
            yield break;

        if (check == null)
            check = (t) => { return (t < 10f) ? true : false; };

        var time = 0f;

        while (check(time))
        {
            time += Time.deltaTime;

            trans.Translate(direction * Time.deltaTime * speed);
            yield return null;
        }
    }

    public static IEnumerator CCMove(this Transform trans, Vector3 direction, float speed, float end)
    {
        if (speed <= 0)
            yield break;

        var time = 0f;

        while (time < end)
        {
            time += Time.deltaTime;
            trans.Translate(direction * Time.deltaTime * speed);
            yield return null;
        }
    }

    ///CCFollow

    /// <param name="check">따라가는 상태 조건 디폴트는 10초후에 종료</param>

    public static IEnumerator CCFollow(this Transform trans, Transform target, float speed, StateCheckerTimeLength check = null)
    {
        if (speed <= 0)
            yield break;

        if (check == null)
            check = (t, l) => { return (t < 10f) ? true : false; };

        var time = 0f;
        var length = float.MaxValue;

        while (check(time, length))
        {
            time += Time.deltaTime;
            length = Vector3.Distance(trans.position, target.position);

            var direction = (target.position - trans.position).normalized;
            trans.Translate(direction * Time.deltaTime * speed);
            yield return null;
        }
    }

    public static IEnumerator CCFollow(this Transform trans, Transform target, float speed, float end)
    {
        if (speed <= 0)
            yield break;
        
        var time = 0f;
        var length = float.MaxValue;

        while (time < end)
        {
            time += Time.deltaTime;
            length = Vector3.Distance(trans.position, target.position);

            var direction = (target.position - trans.position).normalized;
            trans.Translate(direction * Time.deltaTime * speed);
            yield return null;
        }
    }

    ///CCRotate
    public static IEnumerator CCRatate(this Transform trans, Vector3 euler, float time, AnimationCurve curve = null)
    {
        if (time <= 0)
            yield break;

        var start = trans.rotation;
        var end = Quaternion.Euler(euler);
        var c = curve ?? Linear;
        var t = 0f;
        while (t < 1)
        {
            t = Mathf.Min(1f, t + Time.deltaTime / time);
            trans.transform.rotation = Quaternion.Lerp(start, end, c.Evaluate(t));
            yield return null;
        }
    }
    
    public static IEnumerator CCRatate(this Transform trans, Quaternion rot, float time, AnimationCurve curve = null)
    {
        if (time <= 0)
            yield break;

        var start = trans.rotation;

        var c = curve ?? Linear;
        var t = 0f;
        while (t < 1)
        {
            t = Mathf.Min(1f, t + Time.deltaTime / time);
            trans.transform.rotation = Quaternion.Lerp(start, rot, c.Evaluate(t));
            yield return null;
        }
    }
}
