using UnityEngine;

namespace _Main.Scripts._Base.SaveSystem
{
    [System.Serializable]
    public class SerializationGrid<T> 
    {
        [SerializeField]
        private int[] array;

        public SerializationGrid(int[,] array)
        {
            var width = array.GetLength(0);
            var height = array.GetLength(1);
            this.array = new int[width * height];
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    this.array[i * height + j] = array[i, j];
                }
            }
        }

        public int[,] ToArray()
        {
            var width = (int) Mathf.Sqrt(array.Length);
            var newArray = new int[width, width];
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    newArray[i, j] = array[i * width + j];
                }
            }
            return newArray;
        }
    }
}