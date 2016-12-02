using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

public struct Mathf
{
    public const float PI = 3.141593f;
    public const float Infinity = float.PositiveInfinity;
    public const float NegativeInfinity = float.NegativeInfinity;
    public const float Deg2Rad = 0.01745329f;
    public const float Rad2Deg = 57.29578f;
    public const float Epsilon = float.Epsilon;

    /// <summary>
    /// Returns the sine of angle f in radians.
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static float Sin(float f)
    {
        return (float)Math.Sin(f);
    }
    public static float Cos(float f)
    {
        return (float)Math.Cos(f);
    }
    public static float Tan(float f)
    {
        return (float)Math.Tan(f);
    }
    public static float Asin(float f)
    {
        return (float)Math.Asin((double)f);
    }
    public static float Acos(float f)
    {
        return (float)Math.Acos((double)f);
    }
    public static float Atan(float f)
    {
        return (float)Math.Atan((double)f);
    }
    public static float Atan2(float y, float x)
    {
        return (float)Math.Atan2((double)y, (double)x);
    }
    public static float Sqrt(float f)
    {
        return (float)Math.Sqrt((double)f);
    }
    public static float Abs(float f)
    {
        return Math.Abs(f);
    }
    public static int Abs(int value)
    {
        return Math.Abs(value);
    }

    /// <summary>
    ///  Gives you the smaller value.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float Min(float a, float b)
    {
        return ((a >= b) ? b : a);
    }

    /// <summary>
    /// Gives you ths Smalles Value from the values table.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static float Min(params float[] values)
    {
        int length = values.Length;
        if (length == 0)
        {
            return 0.0f;
        }

        float num2 = values[0];
        for (int i = 1; i < length; i++)
        {
            if (values[i] < num2)
            {
                num2 = values[i];
            }
        }
        return num2;
    }

    /// <summary>
    ///  Gives you the smaller value.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int Min(int a, int b)
    {
        return ((a >= b) ? b : a);
    }

    /// <summary>
    /// Gives you ths Smalles Value from the values table.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static int Min(params int[] values)
    {
        int length = values.Length;
        if (length == 0)
        {
            return 0;
        }
        int num2 = values[0];
        for (int i = 1; i < length; i++)
        {
            if (values[i] < num2)
            {
                num2 = values[i];
            }
        }
        return num2;
    }

    /// <summary>
    ///  Gives you the bigger value.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float Max(float a, float b)
    {
        return ((a <= b) ? b : a);
    }

    /// <summary>
    /// Gives you ths Bigger Value from the values table.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static float Max(params float[] values)
    {
        int length = values.Length;
        if (length == 0)
        {
            return 0f;
        }
        float num2 = values[0];
        for (int i = 1; i < length; i++)
        {
            if (values[i] > num2)
            {
                num2 = values[i];
            }
        }
        return num2;
    }

    /// <summary>
    ///  Gives you the bigger value.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int Max(int a, int b)
    {
        return ((a <= b) ? b : a);
    }

    /// <summary>
    /// Gives you ths Bigger Value from the values table.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static int Max(params int[] values)
    {
        int length = values.Length;
        if (length == 0)
        {
            return 0;
        }
        int num2 = values[0];
        for (int i = 1; i < length; i++)
        {
            if (values[i] > num2)
            {
                num2 = values[i];
            }
        }
        return num2;
    }


    public static float Pow(float f, float p)
    {
        return (float)Math.Pow(f, p);
    }
    public static float Exp(float power)
    {
        return (float)Math.Exp(power);
    }
    public static float Log(float f, float p)
    {
        return (float)Math.Log(f, p);
    }
    public static float Log(float f)
    {
        return (float)Math.Log(f);
    }
    public static float Log10(float f)
    {
        return (float)Math.Log10(f);
    }
    public static float Ceil(float f)
    {
        return (float)Math.Ceiling(f);
    }

    /// <summary>
    /// Returns the largest integer smaller to or equal to f.
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static float Floor(float f)
    {
        return (float)Math.Floor(f);
    }

    /// <summary>
    /// This Round your Value to the Next best value Possible
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static float Round(float f)
    {
        return (float)Math.Round(f);
    }

    /// <summary>
    /// Returns the smallest integer greater to or equal to f.
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static int CeilToInt(float f)
    {
        return (int)Math.Ceiling(f);
    }

    /// <summary>
    /// Returns the largest integer smaller to or equal to f.
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static int FloorToInt(float f)
    {
        return (int)Math.Floor(f);
    }

    /// <summary>
    /// Returns f rounded to the nearest integer.
    /// If the number ends in .5 so it is halfway between two integers, one of which is even and the other odd, the even number is returned.
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static int RoundToInt(float f)
    {
        return (int)Math.Round(f);
    }

    /// <summary>
    /// Returns the sign of f.
    /// Return value is 1 when f is positive or zero, -1 when f is negative.
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static float Sign(float f)
    {
        return ((f < 0f) ? -1f : 1f);
    }

    /// <summary>
    /// Clamps a value between a minimum float and maximum float value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float Clamp(float value, float min, float max)
    {
        if (value < min)
        {
            value = min;
            return value;
        }
        if (value > max)
        {
            value = max;
        }
        return value;
    }

    /// <summary>
    /// Clamps a value between a minimum float and maximum float value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int Clamp(int value, int min, int max)
    {
        if (value < min)
        {
            value = min;
            return value;
        }
        if (value > max)
        {
            value = max;
        }
        return value;
    }

    /// <summary>
    /// Clamps value between 0 and 1 and returns value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Clamp01(float value)
    {
        if (value < 0f)
        {
            return 0f;
        }
        if (value > 1f)
        {
            return 1f;
        }
        return value;
    }

    /// <summary>
    /// Linearly interpolates between a and b by t.
    ///
    /// The parameter t is clamped to the range [0, 1].
    ///
    /// When t = 0 returns a. 
    /// When t = 1 return b. 
    /// When t = 0.5 returns the midpoint of a and b.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static float Lerp(float from, float to, float t)
    {
        return (from + ((to - from) * Clamp01(t)));
    }

    /// <summary>
    /// Same as Lerp but makes sure the values interpolate correctly when they wrap around 360 degrees.
    ///
    /// The parameter t is clamped to the range[0, 1]. Variables a and b are assumed to be in degrees.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static float LerpAngle(float a, float b, float t)
    {
        float num = Repeat(b - a, 360f);
        if (num > 180f)
        {
            num -= 360f;
        }
        return (a + (num * Clamp01(t)));
    }
    public static float MoveTowards(float current, float target, float maxDelta)
    {
        if (Abs((float)(target - current)) <= maxDelta)
        {
            return target;
        }
        return (current + (Sign(target - current) * maxDelta));
    }
    public static float MoveTowardsAngle(float current, float target, float maxDelta)
    {
        target = current + DeltaAngle(current, target);
        return MoveTowards(current, target, maxDelta);
    }
    public static float SmoothStep(float from, float to, float t)
    {
        t = Clamp01(t);
        t = (((-2f * t) * t) * t) + ((3f * t) * t);
        return ((to * t) + (from * (1f - t)));
    }
    public static float Gamma(float value, float absmax, float gamma)
    {
        bool flag = false;
        if (value < 0f)
        {
            flag = true;
        }
        float num = Abs(value);
        if (num > absmax)
        {
            return (!flag ? num : -num);
        }
        float num2 = Pow(num / absmax, gamma) * absmax;
        return (!flag ? num2 : -num2);
    }
    public static bool Approximately(float a, float b)
    {
        return (Abs((float)(b - a)) < Max((float)(1E-06f * Max(Abs(a), Abs(b))), (float)1.121039E-44f));
    }
    public static float Repeat(float t, float length)
    {
        return (t - (Floor(t / length) * length));
    }
    public static float PingPong(float t, float length)
    {
        t = Repeat(t, length * 2f);
        return (length - Abs((float)(t - length)));
    }
    public static float InverseLerp(float from, float to, float value)
    {
        if (from < to)
        {
            if (value < from)
            {
                return 0f;
            }
            if (value > to)
            {
                return 1f;
            }
            value -= from;
            value /= to - from;
            return value;
        }
        if (from <= to)
        {
            return 0f;
        }
        if (value < to)
        {
            return 1f;
        }
        if (value > from)
        {
            return 0f;
        }
        return (1f - ((value - to) / (from - to)));
    }
    public static float DeltaAngle(float current, float target)
    {
        float num = Repeat(target - current, 360f);
        if (num > 180f)
        {
            num -= 360f;
        }
        return num;
    }
    internal static bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
    {
        float num = p2.x - p1.x;
        float num2 = p2.y - p1.y;
        float num3 = p4.x - p3.x;
        float num4 = p4.y - p3.y;
        float num5 = (num * num4) - (num2 * num3);
        if (num5 == 0f)
        {
            return false;
        }
        float num6 = p3.x - p1.x;
        float num7 = p3.y - p1.y;
        float num8 = ((num6 * num4) - (num7 * num3)) / num5;
        result = new Vector2(p1.x + (num8 * num), p1.y + (num8 * num2));
        return true;
    }
    internal static bool LineSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
    {
        float num = p2.x - p1.x;
        float num2 = p2.y - p1.y;
        float num3 = p4.x - p3.x;
        float num4 = p4.y - p3.y;
        float num5 = (num * num4) - (num2 * num3);
        if (num5 == 0f)
        {
            return false;
        }
        float num6 = p3.x - p1.x;
        float num7 = p3.y - p1.y;
        float num8 = ((num6 * num4) - (num7 * num3)) / num5;
        if ((num8 < 0f) || (num8 > 1f))
        {
            return false;
        }
        float num9 = ((num6 * num2) - (num7 * num)) / num5;
        if ((num9 < 0f) || (num9 > 1f))
        {
            return false;
        }
        result = new Vector2(p1.x + (num8 * num), p1.y + (num8 * num2));
        return true;
    }
}
public struct Vector2
{
    public const float kEpsilon = 1E-05f;
    public float x;
    public float y;

    public Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public void Set(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public void Scale(Vector2 scale)
    {
        x *= scale.x;
        y *= scale.y;
    }
    public override string ToString()
    {
        object[] args = new object[] { x, y };
        return string.Format("({0:F1}, {1:F1})", args);
    }
    public string ToString(string format)
    {
        object[] args = new object[] { x.ToString(format), y.ToString(format) };
        return string.Format("({0}, {1})", args);
    }
    public override bool Equals(object other)
    {
        if (!(other is Vector2))
        {
            return false;
        }
        Vector2 vector = (Vector2)other;
        return (x.Equals(vector.x) && y.Equals(vector.y));
    }
    public static float Dot(Vector2 lhs, Vector2 rhs)
    {
        return ((lhs.x * rhs.x) + (lhs.y * rhs.y));
    }
    public float magnitude
    {
        get
        {
            return Mathf.Sqrt((x * x) + (y * y));
        }
    }
    public float sqrMagnitude
    {
        get
        {
            return ((x * x) + (y * y));
        }
    }
    public static float Distance(Vector2 a, Vector2 b)
    {
        return (a - b).magnitude;
    }
    public static float SqrMagnitude(Vector2 a)
    {
        return ((a.x * a.x) + (a.y * a.y));
    }
    public float SqrMagnitude()
    {
        return ((x * x) + (y * y));
    }
    public static Vector2 Min(Vector2 lhs, Vector2 rhs)
    {
        return new Vector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
    }
    public static Vector2 Max(Vector2 lhs, Vector2 rhs)
    {
        return new Vector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
    }
    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x + b.x, a.y + b.y);
    }
    public static Vector2 operator -(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x - b.x, a.y - b.y);
    }
    public static Vector2 operator -(Vector2 a)
    {
        return new Vector2(-a.x, -a.y);
    }
    public static Vector2 operator *(Vector2 a, float d)
    {
        return new Vector2(a.x * d, a.y * d);
    }
    public static Vector2 operator *(float d, Vector2 a)
    {
        return new Vector2(a.x * d, a.y * d);
    }
    public static Vector2 operator /(Vector2 a, float d)
    {
        return new Vector2(a.x / d, a.y / d);
    }

    /// <summary>
    /// Check if Vector2 is same as the other Vector2
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static bool operator ==(Vector2 lhs, Vector2 rhs)
    {
        return (SqrMagnitude(lhs - rhs) < 9.999999E-11f);
    }

    /// <summary>
    /// Check if Vector2 is not same as the other Vector2
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static bool operator !=(Vector2 lhs, Vector2 rhs)
    {
        return (SqrMagnitude(lhs - rhs) >= 9.999999E-11f);
    }

    /// <summary>
    /// Convert Vector3 to Vector2
    /// </summary>
    /// <param name="v"></param>
    public static implicit operator Vector2(Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    /// <summary>
    /// Convert Vector2 to Vector3
    /// </summary>
    /// <param name="v"></param>
    public static implicit operator Vector3(Vector2 v)
    {
        return new Vector3(v.x, v.y, 0f);
    }
}
public struct Vector3
{
    public const float kEpsilon = 1E-05f;
    public float x;
    public float y;
    public float z;
    public Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public Vector3(float x, float y)
    {
        this.x = x;
        this.y = y;
        this.z = 0f;
    }
    public static Vector3 Lerp(Vector3 from, Vector3 to, float t)
    {
        t = Mathf.Clamp01(t);
        return new Vector3(from.x + ((to.x - from.x) * t), from.y + ((to.y - from.y) * t), from.z + ((to.z - from.z) * t));
    }
    public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
    {
        Vector3 vector = target - current;
        float magnitude = vector.magnitude;
        if ((magnitude > maxDistanceDelta) && (magnitude != 0f))
        {
            return (current + ((Vector3)((vector / magnitude) * maxDistanceDelta)));
        }
        return target;
    }
    public float this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                return this.x;

                case 1:
                return this.y;

                case 2:
                return this.z;
            }
            throw new IndexOutOfRangeException("Invalid Vector3 index!");
        }
        set
        {
            switch (index)
            {
                case 0:
                this.x = value;
                break;

                case 1:
                this.y = value;
                break;

                case 2:
                this.z = value;
                break;

                default:
                throw new IndexOutOfRangeException("Invalid Vector3 index!");
            }
        }
    }
    public void Set(float new_x, float new_y, float new_z)
    {
        this.x = new_x;
        this.y = new_y;
        this.z = new_z;
    }
    public static Vector3 Scale(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
    public void Scale(Vector3 scale)
    {
        this.x *= scale.x;
        this.y *= scale.y;
        this.z *= scale.z;
    }
    public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
    {
        return new Vector3((lhs.y * rhs.z) - (lhs.z * rhs.y), (lhs.z * rhs.x) - (lhs.x * rhs.z), (lhs.x * rhs.y) - (lhs.y * rhs.x));
    }
    public override int GetHashCode()
    {
        return ((this.x.GetHashCode() ^ (this.y.GetHashCode() << 2)) ^ (this.z.GetHashCode() >> 2));
    }
    public override bool Equals(object other)
    {
        if (!(other is Vector3))
        {
            return false;
        }
        Vector3 vector = (Vector3)other;
        return ((this.x.Equals(vector.x) && this.y.Equals(vector.y)) && this.z.Equals(vector.z));
    }
    public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
    {
        return (((Vector3)((-2f * Dot(inNormal, inDirection)) * inNormal)) + inDirection);
    }
    public static Vector3 Normalize(Vector3 value)
    {
        float num = Magnitude(value);
        if (num > 1E-05f)
        {
            return (Vector3)(value / num);
        }
        return zero;
    }
    public void Normalize()
    {
        float num = Magnitude(this);
        if (num > 1E-05f)
        {
            this = (Vector3)(this / num);
        }
        else
        {
            this = zero;
        }
    }
    public Vector3 normalized
    {
        get
        {
            return Normalize(this);
        }
    }
    public override string ToString()
    {
        object[] args = new object[] { this.x, this.y, this.z };
        return string.Format("({0:F1}, {1:F1}, {2:F1})", args);
    }
    public string ToString(string format)
    {
        object[] args = new object[] { this.x.ToString(format), this.y.ToString(format), this.z.ToString(format) };
        return string.Format("({0}, {1}, {2})", args);
    }
    public static float Dot(Vector3 lhs, Vector3 rhs)
    {
        return (((lhs.x * rhs.x) + (lhs.y * rhs.y)) + (lhs.z * rhs.z));
    }
    public static Vector3 Project(Vector3 vector, Vector3 onNormal)
    {
        float num = Dot(onNormal, onNormal);
        if (num < float.Epsilon)
        {
            return zero;
        }
        return (Vector3)((onNormal * Dot(vector, onNormal)) / num);
    }
    public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
    {
        return (vector - Project(vector, planeNormal));
    }
    [Obsolete("Use Vector3.ProjectOnPlane instead.")]
    public static Vector3 Exclude(Vector3 excludeThis, Vector3 fromThat)
    {
        return (fromThat - Project(fromThat, excludeThis));
    }
    public static float Angle(Vector3 from, Vector3 to)
    {
        return (Mathf.Acos(Mathf.Clamp(Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f);
    }
    public static float Distance(Vector3 a, Vector3 b)
    {
        Vector3 vector = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        return Mathf.Sqrt(((vector.x * vector.x) + (vector.y * vector.y)) + (vector.z * vector.z));
    }
    public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
    {
        if (vector.sqrMagnitude > (maxLength * maxLength))
        {
            return (Vector3)(vector.normalized * maxLength);
        }
        return vector;
    }
    public static float Magnitude(Vector3 a)
    {
        return Mathf.Sqrt(((a.x * a.x) + (a.y * a.y)) + (a.z * a.z));
    }
    public float magnitude
    {
        get
        {
            return Mathf.Sqrt(((this.x * this.x) + (this.y * this.y)) + (this.z * this.z));
        }
    }
    public static float SqrMagnitude(Vector3 a)
    {
        return (((a.x * a.x) + (a.y * a.y)) + (a.z * a.z));
    }
    public float sqrMagnitude
    {
        get
        {
            return (((this.x * this.x) + (this.y * this.y)) + (this.z * this.z));
        }
    }
    public static Vector3 Min(Vector3 lhs, Vector3 rhs)
    {
        return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
    }
    public static Vector3 Max(Vector3 lhs, Vector3 rhs)
    {
        return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
    }
    public static Vector3 zero
    {
        get
        {
            return new Vector3(0f, 0f, 0f);
        }
    }
    public static Vector3 one
    {
        get
        {
            return new Vector3(1f, 1f, 1f);
        }
    }
    public static Vector3 forward
    {
        get
        {
            return new Vector3(0f, 0f, 1f);
        }
    }
    public static Vector3 back
    {
        get
        {
            return new Vector3(0f, 0f, -1f);
        }
    }
    public static Vector3 up
    {
        get
        {
            return new Vector3(0f, 1f, 0f);
        }
    }
    public static Vector3 down
    {
        get
        {
            return new Vector3(0f, -1f, 0f);
        }
    }
    public static Vector3 left
    {
        get
        {
            return new Vector3(-1f, 0f, 0f);
        }
    }
    public static Vector3 right
    {
        get
        {
            return new Vector3(1f, 0f, 0f);
        }
    }
    [Obsolete("Use Vector3.forward instead.")]
    public static Vector3 fwd
    {
        get
        {
            return new Vector3(0f, 0f, 1f);
        }
    }
    [Obsolete("Use Vector3.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
    public static float AngleBetween(Vector3 from, Vector3 to)
    {
        return Mathf.Acos(Mathf.Clamp(Dot(from.normalized, to.normalized), -1f, 1f));
    }
    public static Vector3 operator +(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }
    public static Vector3 operator -(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }
    public static Vector3 operator -(Vector3 a)
    {
        return new Vector3(-a.x, -a.y, -a.z);
    }
    public static Vector3 operator *(Vector3 a, float d)
    {
        return new Vector3(a.x * d, a.y * d, a.z * d);
    }
    public static Vector3 operator *(float d, Vector3 a)
    {
        return new Vector3(a.x * d, a.y * d, a.z * d);
    }
    public static Vector3 operator /(Vector3 a, float d)
    {
        return new Vector3(a.x / d, a.y / d, a.z / d);
    }
    public static bool operator ==(Vector3 lhs, Vector3 rhs)
    {
        return (SqrMagnitude(lhs - rhs) < 9.999999E-11f);
    }
    public static bool operator !=(Vector3 lhs, Vector3 rhs)
    {
        return (SqrMagnitude(lhs - rhs) >= 9.999999E-11f);
    }
}
public struct Vector4
{
    public const float kEpsilon = 1E-05f;
    public float x;
    public float y;
    public float z;
    public float w;
    public Vector4(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    public Vector4(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = 0f;
    }
    public Vector4(float x, float y)
    {
        this.x = x;
        this.y = y;
        this.z = 0f;
        this.w = 0f;
    }
    public float this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                return this.x;

                case 1:
                return this.y;

                case 2:
                return this.z;

                case 3:
                return this.w;
            }
            throw new IndexOutOfRangeException("Invalid Vector4 index!");
        }
        set
        {
            switch (index)
            {
                case 0:
                this.x = value;
                break;

                case 1:
                this.y = value;
                break;

                case 2:
                this.z = value;
                break;

                case 3:
                this.w = value;
                break;

                default:
                throw new IndexOutOfRangeException("Invalid Vector4 index!");
            }
        }
    }
    public void Set(float new_x, float new_y, float new_z, float new_w)
    {
        this.x = new_x;
        this.y = new_y;
        this.z = new_z;
        this.w = new_w;
    }
    public static Vector4 Lerp(Vector4 from, Vector4 to, float t)
    {
        t = Mathf.Clamp01(t);
        return new Vector4(from.x + ((to.x - from.x) * t), from.y + ((to.y - from.y) * t), from.z + ((to.z - from.z) * t), from.w + ((to.w - from.w) * t));
    }
    public static Vector4 MoveTowards(Vector4 current, Vector4 target, float maxDistanceDelta)
    {
        Vector4 vector = target - current;
        float magnitude = vector.magnitude;
        if ((magnitude > maxDistanceDelta) && (magnitude != 0f))
        {
            return (current + ((Vector4)((vector / magnitude) * maxDistanceDelta)));
        }
        return target;
    }
    public static Vector4 Scale(Vector4 a, Vector4 b)
    {
        return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    }
    public void Scale(Vector4 scale)
    {
        this.x *= scale.x;
        this.y *= scale.y;
        this.z *= scale.z;
        this.w *= scale.w;
    }
    public override int GetHashCode()
    {
        return (((x.GetHashCode() ^ (y.GetHashCode() << 2)) ^ (z.GetHashCode() >> 2)) ^ (w.GetHashCode() >> 1));
    }
    public override bool Equals(object other)
    {
        if (!(other is Vector4))
        {
            return false;
        }
        Vector4 vector = (Vector4)other;
        return (((this.x.Equals(vector.x) && this.y.Equals(vector.y)) && this.z.Equals(vector.z)) && this.w.Equals(vector.w));
    }
    public static Vector4 Normalize(Vector4 a)
    {
        float num = Magnitude(a);
        if (num > 1E-05f)
        {
            return (Vector4)(a / num);
        }
        return zero;
    }
    public void Normalize()
    {
        float num = Magnitude(this);
        if (num > 1E-05f)
        {
            this = (Vector4)(this / num);
        }
        else
        {
            this = zero;
        }
    }
    public Vector4 normalized
    {
        get
        {
            return Normalize(this);
        }
    }
    public override string ToString()
    {
        object[] args = new object[] { this.x, this.y, this.z, this.w };
        return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", args);
    }
    public string ToString(string format)
    {
        object[] args = new object[] { this.x.ToString(format), this.y.ToString(format), this.z.ToString(format), this.w.ToString(format) };
        return string.Format("({0}, {1}, {2}, {3})", args);
    }
    public static float Dot(Vector4 a, Vector4 b)
    {
        return ((((a.x * b.x) + (a.y * b.y)) + (a.z * b.z)) + (a.w * b.w));
    }
    public static Vector4 Project(Vector4 a, Vector4 b)
    {
        return (Vector4)((b * Dot(a, b)) / Dot(b, b));
    }
    public static float Distance(Vector4 a, Vector4 b)
    {
        return Magnitude(a - b);
    }
    public static float Magnitude(Vector4 a)
    {
        return Mathf.Sqrt(Dot(a, a));
    }
    public float magnitude
    {
        get
        {
            return Mathf.Sqrt(Dot(this, this));
        }
    }
    public static float SqrMagnitude(Vector4 a)
    {
        return Dot(a, a);
    }
    public float SqrMagnitude()
    {
        return Dot(this, this);
    }
    public float sqrMagnitude
    {
        get
        {
            return Dot(this, this);
        }
    }
    public static Vector4 Min(Vector4 lhs, Vector4 rhs)
    {
        return new Vector4(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z), Mathf.Min(lhs.w, rhs.w));
    }
    public static Vector4 Max(Vector4 lhs, Vector4 rhs)
    {
        return new Vector4(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z), Mathf.Max(lhs.w, rhs.w));
    }
    public static Vector4 zero
    {
        get
        {
            return new Vector4(0f, 0f, 0f, 0f);
        }
    }
    public static Vector4 one
    {
        get
        {
            return new Vector4(1f, 1f, 1f, 1f);
        }
    }
    public static Vector4 operator +(Vector4 a, Vector4 b)
    {
        return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    }
    public static Vector4 operator -(Vector4 a, Vector4 b)
    {
        return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    }
    public static Vector4 operator -(Vector4 a)
    {
        return new Vector4(-a.x, -a.y, -a.z, -a.w);
    }
    public static Vector4 operator *(Vector4 a, float d)
    {
        return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
    }
    public static Vector4 operator *(float d, Vector4 a)
    {
        return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
    }
    public static Vector4 operator /(Vector4 a, float d)
    {
        return new Vector4(a.x / d, a.y / d, a.z / d, a.w / d);
    }
    public static bool operator ==(Vector4 lhs, Vector4 rhs)
    {
        return (SqrMagnitude(lhs - rhs) < 9.999999E-11f);
    }
    public static bool operator !=(Vector4 lhs, Vector4 rhs)
    {
        return (SqrMagnitude(lhs - rhs) >= 9.999999E-11f);
    }
    public static implicit operator Vector4(Vector3 v)
    {
        return new Vector4(v.x, v.y, v.z, 0f);
    }
    public static implicit operator Vector3(Vector4 v)
    {
        return new Vector3(v.x, v.y, v.z);
    }
    public static implicit operator Vector4(Vector2 v)
    {
        return new Vector4(v.x, v.y, 0f, 0f);
    }
    public static implicit operator Vector2(Vector4 v)
    {
        return new Vector2(v.x, v.y);
    }
}


public delegate void VirtualScreenUpdateFrameEventHandler(object sender, VirtualScreen e);
public delegate void VirtualScreenEndFrameEventHandler(object sender, VirtualScreen e);
public delegate void VirtualScreenBeginUpdateFrameEventHandler(object sender, VirtualScreen e);
public class VirtualScreenEventHandler
{
    public static event VirtualScreenUpdateFrameEventHandler UpdateFrame;
    public static event VirtualScreenEndFrameEventHandler EndFrame;
    public static event VirtualScreenBeginUpdateFrameEventHandler BeginUpdateFrame;
    public static event VirtualScreenBeginUpdateFrameEventHandler StopScreen;

    // Before Clearing the Virtual Screen
    public static void onBeginUpdateFrame(object sender, VirtualScreen e)
    {
        if (BeginUpdateFrame != null)
            BeginUpdateFrame(sender, e);
    }

    // After Clearing the Virtual Screen
    public static void onUpdateFrame(object sender, VirtualScreen e)
    {
        if (UpdateFrame != null)
            UpdateFrame(sender, e);
    }

    // After Rendering the Virtual Screen
    public static void onEndFrame(object sender, VirtualScreen e)
    {
        if (EndFrame != null)
            EndFrame(sender, e);
    }

    // Stop the Virtual Screen
    public static void onStopScreen(object sender, VirtualScreen e)
    {
        if (StopScreen != null)
            StopScreen(sender, e);
    }
}


// To Handle Graphics and Image better..
public class GfxImage
{
    Image m_Image;
    Graphics m_Graphics;

    public GfxImage()
    {
        m_Image = new Bitmap(0, 0); // isnt it 1,1?
        m_Graphics = Graphics.FromImage(m_Image);
    }
    public GfxImage(Vector2 Size)
    {
        m_Image = new Bitmap((int)Size.x, (int)Size.y);
        m_Graphics = Graphics.FromImage(m_Image);
    }
    public GfxImage(Image image)
    {
        m_Image = image;
        m_Graphics = Graphics.FromImage(m_Image);
    }
    public GfxImage(string path)
    {
        m_Image = Image.FromFile(path);
        m_Graphics = Graphics.FromImage(m_Image);
    }


    public Image GetImage()
    {
        m_Graphics.DrawImage(m_Image, 0, 0);
        m_Graphics.Flush();

        return m_Image;
    }
    public Graphics GetGraphics()
    {
        return Graphics.FromImage(m_Image);
    }
}
public struct VirtualScreenInfo
{
    // Screen to Draw
    internal Bitmap Screen;

    // Device to draw the Image
    public IntPtr Device;

    // Screen Width
    public int Width;

    // Screen Height
    public int Height;

    // try FPS Rate
    public int RefreshRate;

    // while isDrawing we Update
    public bool isDrawing;
}
public enum TextAlignment_t
{
    TOP_LEFT,
    TOP_RIGHT,
    TOP_CENTER,

    CENTER_LEFT,
    CENTER_RIGHT,
    CENTER_CENTER,

    BOTTOM_LEFT,
    BOTTOM_RIGHT,
    BOTTOM_CENTER
};
public class VirtualScreen
{
    public VirtualScreenInfo VSInfo;
    internal Thread UpdateThread;
    public Brush ClearColor;

    [DllImport("User32.dll")]
    static extern IntPtr GetDC(IntPtr hwnd);

    [DllImport("User32.dll")]
    static extern void ReleaseDC(IntPtr hWnd, IntPtr hDC);

    public VirtualScreen(IntPtr hwnd, int Width, int Height, int RefreshRate)
    {
        // Initialize the VirtualScreenInfo.
        VSInfo = new VirtualScreenInfo();
        VSInfo.Screen = new Bitmap(Width, Height);
        VSInfo.Device = GetDC(hwnd);
        VSInfo.Width = Width;
        VSInfo.Height = Height;
        VSInfo.RefreshRate = RefreshRate;

        // Clear the Screen.
        Clear();
    }



    public void RunRendererThread()
    {
        // Enable Drawing.
        VSInfo.isDrawing = true;

        // Clear the Screen.
        Clear();

        // Start the Update Thread.
        UpdateThread = new Thread(new ThreadStart(Update));
        UpdateThread.Start();
    }
    public void Resize(int Width, int Height)
    {
        // Stop the Renderer Thread.
        StopRendererThread();

        // Clear the Screen.
        Clear();

        // Edit the VirtualScreenInfo.
        VSInfo.Screen = new Bitmap(Width, Height);
        VSInfo.Width = Width;
        VSInfo.Height = Height;

        // Run the Thread Again.
        RunRendererThread();
    }
    public void StopRendererThread()
    {
        // Stop the While Loop.
        VSInfo.isDrawing = false;

        // For Security we Stop the Update Thread.
        UpdateThread.Abort();

        // Call the Delegate to Stop the screen.
        VirtualScreenEventHandler.onStopScreen(this, this);
    }


    public void Update()
    {
        // Screen get at last cleared but we need to invert it so also the dlg. have to be inverted what get drawn first..
        while (VSInfo.isDrawing)
        {
            // Run the Delegate for Begin Update Frame
            VirtualScreenEventHandler.onBeginUpdateFrame(this, this);

            // Clear the Screen
            Clear();

            // Run the Delegate for Update Frame
            VirtualScreenEventHandler.onUpdateFrame(this, this);

            // Update the Screen.
            Render();

            // Run the Delegate for End Frame
            VirtualScreenEventHandler.onEndFrame(this, this);
        }
        try
        {
            // Release the DC on the End.
            ReleaseDC(IntPtr.Zero, VSInfo.Device);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Update() : Exception");
        }
    }
    public void Render()
    {
        // Check for Zero
        if (VSInfo.Device == IntPtr.Zero)
            return;

        try
        {
            Graphics g = Graphics.FromHdc(VSInfo.Device);
            lock (g)
            {
                lock (VSInfo.Screen)
                {
                    Graphics sg = Graphics.FromImage(VSInfo.Screen);

                    //Color a1 = API.GetPixel(g, 0, 0);
                    //g.DrawLine(Pens.Aqua, 0, 0, 12, 12);

                    //if (API.GetPixel(g, 0, 0) != API.GetPixel(sg, 0, 0))
                    //{
                    g.DrawImage(VSInfo.Screen, 0, 0);
                    //}
                }
            }
            Thread.Sleep(1000 / VSInfo.RefreshRate);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Render() : Exception");
            StopRendererThread();
        }
    }
    public void Clear()
    {
        UI_DrawFilledRectangle(new Vector2(0, 0), new Vector2(VSInfo.Width, VSInfo.Height), ((ClearColor == null) ? Brushes.Black : ClearColor));
    }






    public void UI_DrawText(string Text, Vector2 Position, Font Style, Brush Brush)
    {
        Graphics g = Graphics.FromImage(VSInfo.Screen);
        g.DrawString(Text, Style, Brush, Position.x, Position.y);
        g.Flush();


        Vector2 Size = GetTextInfo(Text, Style);
        //UI_DrawOutLine(Position, Size);
    }
    public void UI_DrawText(string Text, TextAlignment_t TextAlignment, Font Style, Brush Brush)
    {
        UI_DrawText(Text, TextAlignment, new Vector2(0, 0), Style, Brush);
    }
    public void UI_DrawText(string Text, TextAlignment_t TextAlignment, Vector2 Position, Font Style, Brush Brush)
    {
        // Get the Size of the String if it where drawn
        Vector2 strWidth = GetTextInfo(Text, Style);

        switch (TextAlignment)
        {
            case TextAlignment_t.TOP_LEFT:
            UI_DrawText(Text, new Vector2(Position.x, Position.y + strWidth.y), Style, Brush);
            break;
            case TextAlignment_t.TOP_RIGHT:
            UI_DrawText(Text, new Vector2(Position.x + VSInfo.Width - strWidth.x, Position.y + strWidth.y), Style, Brush);
            break;
            case TextAlignment_t.TOP_CENTER:
            UI_DrawText(Text, new Vector2(Position.x + VSInfo.Width / 2 - strWidth.x / 2, Position.y + strWidth.y), Style, Brush);
            break;



            case TextAlignment_t.CENTER_LEFT:
            UI_DrawText(Text, new Vector2(Position.x, Position.y + VSInfo.Height / 2 - strWidth.y / 2), Style, Brush);
            break;
            case TextAlignment_t.CENTER_RIGHT:
            UI_DrawText(Text, new Vector2(Position.x + VSInfo.Width - strWidth.x, Position.y + VSInfo.Height / 2 - strWidth.y / 2), Style, Brush);
            break;
            case TextAlignment_t.CENTER_CENTER:
            UI_DrawText(Text, new Vector2(Position.x + VSInfo.Width / 2 - strWidth.x / 2, Position.y + VSInfo.Height / 2 - strWidth.y / 2), Style, Brush);
            break;


            case TextAlignment_t.BOTTOM_LEFT:
            UI_DrawText(Text, new Vector2(Position.x, Position.y + VSInfo.Height - strWidth.y), Style, Brush);
            break;
            case TextAlignment_t.BOTTOM_RIGHT:
            UI_DrawText(Text, new Vector2(Position.x + VSInfo.Width - strWidth.x, Position.y + VSInfo.Height - strWidth.y), Style, Brush);
            break;
            case TextAlignment_t.BOTTOM_CENTER:
            UI_DrawText(Text, new Vector2(Position.x + VSInfo.Width / 2 - strWidth.x / 2, Position.y + VSInfo.Height - strWidth.y), Style, Brush);
            break;

        }
    }

    public void UI_DrawImage(GfxImage Image, Vector2 Position, Vector2 Size)
    {
        Graphics g = Graphics.FromImage(VSInfo.Screen);
        g.DrawImage(Image.GetImage(), Position.x, Position.y, Size.x, Size.y);
        g.Flush();


        //UI_DrawOutLine(Position, Size);
    }
    public void UI_DrawImage(GfxImage Image, Vector2 Position)
    {
        Image m_image = Image.GetImage();
        UI_DrawImage(Image, Position, new Vector2(m_image.Width, m_image.Height));
    }

    public void UI_DrawLine(Vector2 Position, Vector2 EndPosition, Pen Pen)
    {
        Graphics g = Graphics.FromImage(VSInfo.Screen);
        g.DrawLine(Pen, Position.x, Position.y, EndPosition.x, EndPosition.y);
        g.Flush();
    }
    public void UI_DrawRectangle(Vector2 Position, Vector2 EndPosition, Pen Pen)
    {
        //left to lower left
        UI_DrawLine(new Vector2(Position.x, Position.y), new Vector2(Position.x, Position.y + EndPosition.x), Pens.Red);

        //right to lower right
        UI_DrawLine(new Vector2(Position.x + EndPosition.y, Position.y + EndPosition.y), new Vector2(Position.x + EndPosition.x, Position.y), Pens.Red);

        //left to right top
        UI_DrawLine(new Vector2(Position.x, Position.y), new Vector2(Position.x + EndPosition.x, Position.y), Pens.Red);

        //left to right lower
        UI_DrawLine(new Vector2(Position.x, Position.y + EndPosition.y), new Vector2(Position.x + EndPosition.x, Position.y + EndPosition.y), Pens.Red);
    }
    public void UI_DrawFilledRectangle(Vector2 Position, Vector2 Size, Brush Brush)
    {
        GfxImage m_Image = new GfxImage(Size);
        m_Image.GetGraphics().FillRectangle(Brush, 0, 0, (int)Size.x, (int)Size.y);
        m_Image.GetGraphics().Flush();

        UI_DrawImage(m_Image, Position, Size);
    }

    
    public Vector2 GetTextInfo(string Text, Font Style)
    {
        if (VSInfo.Device == IntPtr.Zero)
            new Vector2(0, 0);

        try
        {
            Graphics g = Graphics.FromHdc(VSInfo.Device);
            SizeF sf = g.MeasureString(Text, Style);
            return new Vector2(sf.Width, sf.Height);
        }
        catch (Exception ex)
        {
            Console.WriteLine("GetTextInfo : Exception");
            //StopRendererThread();
        }
        return new Vector2(0, 0);
    }
}