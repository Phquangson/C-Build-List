using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C__Build_List.Models
{
    public class MyList<T>
    {
        private T[] items;
        private int count;

        public MyList(int capacity = 4)
        {
            if (capacity < 1) capacity = 4;
            items = new T[capacity];
            count = 0;
        }

        public int Count { get { return count; } }

        // Indexer: list[i]
        public T this[int index]
        {
            get { CheckIndex(index); return items[index]; }
            set { CheckIndex(index); items[index] = value; }
        }

        // Thêm vào cuối
        public void Add(T item)
        {
            if (count == items.Length) Resize(items.Length * 2);
            items[count++] = item;
        }

        // Chèn vào vị trí bất kỳ
        public void Insert(int index, T item)
        {
            if (index < 0 || index > count)
                throw new ArgumentOutOfRangeException("index");

            if (count == items.Length) Resize(items.Length * 2);

            for (int i = count; i > index; i--)
                items[i] = items[i - 1];

            items[index] = item;
            count++;
        }

        // Xóa theo vị trí
        public void RemoveAt(int index)
        {
            CheckIndex(index);

            for (int i = index; i < count - 1; i++)
                items[i] = items[i + 1];

            items[--count] = default(T);
        }

        // Xóa theo giá trị (phần tử đầu tiên khớp)
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index < 0) return false;
            RemoveAt(index);
            return true;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < count; i++)
                if (Equals(items[i], item)) return i;
            return -1;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        public void Clear()
        {
            items = new T[4];
            count = 0;
        }

        public T[] ToArray()
        {
            T[] result = new T[count];
            for (int i = 0; i < count; i++) result[i] = items[i];
            return result;
        }

        // Cho phép dùng foreach (không cần implement interface)
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator
        {
            private readonly MyList<T> _list;
            private int _index;

            internal Enumerator(MyList<T> list)
            {
                _list = list;
                _index = -1;
            }

            public T Current { get { return _list.items[_index]; } }

            public bool MoveNext()
            {
                _index++;
                return _index < _list.count;
            }
        }

        private void Resize(int newCapacity)
        {
            T[] newArray = new T[newCapacity];
            for (int i = 0; i < count; i++)
                newArray[i] = items[i];
            items = newArray;
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException("index");
        }
    }
}