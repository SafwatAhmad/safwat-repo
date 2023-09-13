using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Safwat.Essentials
{
    public class GeneralUtilities
    {
        //Method to convert string to an enum entry
        public static T StringToEnum<T>(string str) where T : Enum => (T)Enum.Parse(typeof(T), str);

        //Method to convert an enum entry to string
        public static string EnumToString<T>(T value) where T : Enum => value.ToString();

        //Method to check if a string is a valid integer
        public static bool IsInteger(string str) => int.TryParse(str, out _);

        //Method to get the maximum value in an array
        public static T MaxValue<T>(T[] array) where T : IComparable<T> => array.Max();

        //Method to get the minimum value in an array
        public static T MinValue<T>(T[] array) where T : IComparable<T> => array.Min();

        //Method to reverse a string
        public static string ReverseString(string str) => new string(str.Reverse().ToArray());

        //Method to convert a list to a comma-separated string
        public static string ListToString<T>(List<T> list) => string.Join(", ", list);
    }

    /////////////////////////////////////////////////
    ////////////////// Object Pool //////////////////
    public interface IPoolable { Action ReturnToPool { get; set; } }

    public class ObjectPool<T> where T : MonoBehaviour, IPoolable
    {
        private T prefab;
        private Queue<T> pool = new();

        public ObjectPool(T prefab) => this.prefab = prefab;

        public T Get()
        {
            if (!pool.TryDequeue(out T obj))
            {
                obj = UnityEngine.Object.Instantiate(prefab);
                obj.ReturnToPool = () => Return(obj);
            }
            return obj;
        }

        public void Return(T obj) => pool.Enqueue(obj);
    }
}