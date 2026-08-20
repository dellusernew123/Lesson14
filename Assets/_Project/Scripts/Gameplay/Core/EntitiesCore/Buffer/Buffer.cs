namespace _Project.Scripts.Gameplay.Core.EntitiesCore.Buffer
{
    public class Buffer<T>
    {
        public T[] Items;
        public int Count;

        public Buffer(int initialSize)
        {
            Items = new T[initialSize];
            Count = 0;
        }
    }
}