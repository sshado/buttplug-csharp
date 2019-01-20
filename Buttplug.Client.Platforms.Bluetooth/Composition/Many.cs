using System;
using System.Diagnostics;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;

namespace Buttplug.Client.Platforms.Bluetooth.Composition
{
    /// <summary>
    ///     A read-only list of items of a specific type which implements a writable interface
    /// </summary>
    /// <remarks>
    ///     This type carries an <see cref="IList"/> interface but does not support the extension methods.
    ///     We are pretending this has immutability and using the interface for only serialization.
    /// </remarks>
    /// <typeparam name="T">The type of items in the list</typeparam>
    [Serializable]
    [DebuggerDisplay("Count = {Count}")]
    public class Many<T> : IReadOnlyList<T>, IList<T>
    {
        readonly List<T> _items;

        public Many()
        {
            _items = new List<T>();
        }

        internal Many(List<T> items)
        {
            _items = items;
        }

        public IList<T> Write => _items;

        public IEnumerator<T> GetEnumerator() =>
          _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
          GetEnumerator();

        public T this[int index] => _items[index];

        public int Count => _items.Count;
        public bool IsEmpty => Count == 0;
        public bool IsNotEmpty => Count > 0;

        public int IndexOf(T item) =>
          _items.IndexOf(item);

        public bool Contains(T item) =>
          _items.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) =>
          _items.CopyTo(array, arrayIndex);

        //
        // IList
        //

        bool ICollection<T>.IsReadOnly =>
          ((ICollection<T>)_items).IsReadOnly;

        void IList<T>.Insert(int index, T item) =>
          _items.Insert(index, item);

        void IList<T>.RemoveAt(int index) =>
          _items.RemoveAt(index);

        T IList<T>.this[int index]
        {
            get { return _items[index]; }
            set { _items[index] = value; }
        }

        void ICollection<T>.Add(T item) =>
          _items.Add(item);

        void ICollection<T>.Clear() =>
          _items.Clear();

        bool ICollection<T>.Remove(T item) =>
          _items.Remove(item);
    }

    /// <summary>
    /// Extends <see cref="Many{T}"/> and other sequences with various capabilities
    /// </summary>
    [ EditorBrowsable ( EditorBrowsableState.Never ) ]
    public static class ManyExtensions
    {
        public static void AddRange<T>(this IList<T> source, params T[] items) =>
            source.AddRange(items as IEnumerable<T>);

        public static void AddRange<T>(this IList<T> source, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                source.Add(item);
            }
        }

        public static void AddRange<T>(this ISet<T> set, IEnumerable<T> newItems)
        {
            foreach (var newItem in newItems)
            {
                set.Add(newItem);
            }
        }
    }
}
