﻿using System;

namespace Towel.DataStructures
{
    /// <summary>A primitive dynamic sized data structure.</summary>
    /// <typeparam name="T">The type of items to store in the list.</typeparam>
    public interface IList<T> : IDataStructure<T>,
        // Structure Properties
        DataStructure.IAddable<T>,
        DataStructure.ICountable,
        DataStructure.IClearable,
        DataStructure.IEquating<T>
    {
        #region Members

        /// <summary>Removes the first occurence of an item in the list.</summary>
        /// <param name="value">The value to remove the first occurence of.</param>
        void RemoveFirst(T value);
        /// <summary>Removes the first occurence of an item in the list or returns false.</summary>
        /// <param name="value">The value to remove the first occurence of.</param>
        /// <returns>True if the item was found and removed; False if not.</returns>
        bool TryRemoveFirst(T value);
        /// <summary>Removes the first occurence of an item in the list.</summary>
        /// <param name="predicate">The function to determine equality.</param>
        void RemoveFirst(Predicate<T> predicate);
        /// <summary>Removes the first occurence of an item in the list or returns false.</summary>
        /// <param name="predicate">The function to determine equality.</param>
        /// <returns>True if the item was found and removed; False if not.</returns>
        bool TryRemoveFirst(Predicate<T> predicate);
        /// <summary>Removes all occurences of an item in the list.</summary>
        /// <param name="predicate">The function to determine equality.</param>
        void RemoveAll(Predicate<T> predicate);
        /// <summary>Removes all occurences of an item in the list.</summary>
        /// <param name="value">The value to remove all occurences of.</param>
        void RemoveAll(T value);

        #endregion
    }

    /// <summary>Implements a growing, singularly-linked list data structure that inherits InterfaceTraversable.</summary>
    /// <typeparam name="T">The type of objects to be placed in the list.</typeparam>
    [Serializable]
    public class ListLinked<T> : IList<T>
    {
        internal int _count;
        internal Node _head;
        internal Node _tail;
        internal Equate<T> _equate;

        #region Node

        /// <summary>This class just holds the data for each individual node of the list.</summary>
        [Serializable]
        internal class Node
        {
            internal Node Next;
            internal T Value;

            internal Node(T data)
            {
                Value = data;
            }
        }

        #endregion

        #region Constructors

        internal ListLinked(ListLinked<T> listLinked)
        {
            Node head = new Node(listLinked._head.Value);
            Node current = listLinked._head.Next;
            Node current_clone = head;
            while (current != null)
            {
                current_clone.Next = new Node(current.Value);
                current_clone = current_clone.Next;
                current = current.Next;
            }
            _equate = listLinked._equate;
            _head = head;
            _tail = current_clone;
            _count = listLinked._count;
        }

        /// <summary>Creates an instance of a AddableLinked.</summary>
        /// <param name="equate">The equate delegate to be used by the structure.</param>
        /// <runtime>θ(1)</runtime>
        public ListLinked(Equate<T> equate)
        {
            _equate = equate;
            _head = _tail = null;
            _count = 0;
        }

        /// <summary>Creates an instance of a AddableLinked.</summary>
        /// <runtime>θ(1)</runtime>
        public ListLinked() : this(Towel.Equate.Default) { }

        #endregion

        #region Properties

        /// <summary>Returns the number of items in the list.</summary>
        /// <runtime>θ(1)</runtime>
        public int Count => _count;

        /// <summary>Returns the equate delegate being used by the structure.</summary>
        /// <runtime>θ(1)</runtime>
		public Equate<T> Equate => _equate;

        #endregion

        #region Methods

        #region Add

        /// <summary>Adds an item to the list.</summary>
        /// <param name="addition">The item to add to the list.</param>
        /// <runtime>θ(1)</runtime>
        public void Add(T addition)
        {
            if (_tail == null)
            {
                _head = _tail = new Node(addition);
            }
            else
            {
                _tail = _tail.Next = new Node(addition);
            }
            _count++;
        }

        #endregion

        #region Clear

        /// <summary>Resets the list to an empty state.</summary>
        /// <runtime>θ(1)</runtime>
        public void Clear()
        {
            _head = _tail = null;
            _count = 0;
        }

        #endregion

        #region Clone

        /// <summary>Creates a shallow clone of this data structure.</summary>
        /// <returns>A shallow clone of this data structure.</returns>
        /// <runtime>θ(n)</runtime>
        public ListLinked<T> Clone()
        {
            return new ListLinked<T>(this);
        }

        #endregion

        #region Remove

        /// <summary>Removes the first equality by object reference.</summary>
        /// <param name="removal">The reference to the item to remove.</param>
        public void RemoveFirst(T removal)
        {
            RemoveFirst(x => _equate(x, removal));
        }

        /// <summary>Removes the first equality by object reference.</summary>
        /// <param name="predicate">The predicate to determine removal.</param>
        public void RemoveFirst(Predicate<T> predicate)
        {
            if (!TryRemoveFirst(predicate))
            {
                throw new InvalidOperationException("Attempting to remove a non-existing value from a list.");
            }
        }

        /// <summary>Removes all predicated items from the list.</summary>
        /// <param name="predicate">The predicate to determine removal.</param>
        public void RemoveAll(Predicate<T> predicate)
        {
            if (_head == null)
            {
                return;
            }
            if (predicate(_head.Value))
            {
                _head = _head.Next;
                _count--;
            }
            Node listNode = _head;
            while (listNode != null)
            {
                if (listNode.Next == null)
                {
                    break;
                }
                else if (predicate(_head.Value))
                {
                    if (listNode.Next.Equals(_tail))
                    {
                        _tail = listNode;
                    }
                    listNode.Next = listNode.Next.Next;
                }
                listNode = listNode.Next;
            }
        }

        /// <summary>Removes the first equality by object reference.</summary>
        /// <param name="value">The item to remove.</param>
        public void RemoveAll(T value)
        {
            RemoveAll(x => _equate(x, value));
        }

        /// <summary>Trys to remove the first occurence of a value if the value exists.</summary>
        /// <param name="value">The value to remove.</param>
        /// <returns>True if the value was removed. False if the value did not exist.</returns>
        public bool TryRemoveFirst(T value)
        {
            return TryRemoveFirst(x => _equate(x, value));
        }

        /// <summary>Trys to remove the first predicated value if the value exists.</summary>
        /// <param name="predicate">The predicate to determine removal.</param>
        /// <returns>True if the value was removed. False if the value did not exist.</returns>
        public bool TryRemoveFirst(Predicate<T> predicate)
        {
            if (_head == null)
            {
                return false;
            }
            if (predicate(_head.Value))
            {
                _head = _head.Next;
                _count--;
                return true;
            }
            Node listNode = _head;
            while (listNode != null)
            {
                if (listNode.Next == null)
                {
                    return false;
                }
                else if (predicate(_head.Value))
                {
                    if (listNode.Next.Equals(_tail))
                    {
                        _tail = listNode;
                    }
                    listNode.Next = listNode.Next.Next;
                    return true;
                }
                else
                {
                    listNode = listNode.Next;
                }
            }
            return false;
        }

        #endregion

        #region Stepper And IEnumerable

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> function)
        {
            for (Node looper = _head; looper != null; looper = looper.Next)
            {
                function(looper.Value);
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(StepRef<T> function)
        {
            for (Node looper = _head; looper != null; looper = looper.Next)
            {
                T temp = looper.Value;
                function(ref temp);
                looper.Value = temp;
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepBreak<T> function)
        {
            for (Node looper = _head; looper != null; looper = looper.Next)
            {
                if (function(looper.Value) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
            }
            return StepStatus.Continue;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepRefBreak<T> function)
        {
            for (Node looper = _head; looper != null; looper = looper.Next)
            {
                T temp = looper.Value;
                if (function(ref temp) == StepStatus.Break)
                {
                    looper.Value = temp;
                    return StepStatus.Break;
                }
                looper.Value = temp;
            }
            return StepStatus.Continue;
        }

        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            for (Node looper = _head; looper != null; looper = looper.Next)
            {
                yield return looper.Value;
            }
        }

        System.Collections.Generic.IEnumerator<T>
            System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            for (Node looper = _head; looper != null; looper = looper.Next)
            {
                yield return looper.Value;
            }
        }

        #endregion

        #region ToArray

        /// <summary>Converts the list into a standard array.</summary>
        /// <returns>A standard array of all the items.</returns>
        /// <remarks>Runtime: Towel(n).</remarks>
        public T[] ToArray()
        {
            if (_count == 0)
            {
                return new T[] { };
            }
            T[] array = new T[_count];
            Node node = _head;
            for (int i = 0; i < _count; i++)
            {
                array[i] = node.Value;
                node = node.Next;
            }
            return array;
        }

        #endregion

        #endregion
    }

    /// <summary>A list implemented as a growing array.</summary>
    /// <typeparam name="T">The type of objects to be placed in the list.</typeparam>
    [Serializable]
    public class ListArray<T> : IList<T>
    {
        internal T[] _list;
        internal int _count;
        internal Equate<T> _equate;

        #region Constructor

        /// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
        /// <runtime>O(1)</runtime>
        public ListArray() : this(1, Towel.Equate.Default<T>) { }

        /// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
        /// <param name="expectedCount">The initial and smallest array size allowed by this list.</param>
        /// <runtime>O(1)</runtime>
        public ListArray(int expectedCount) : this(expectedCount, Towel.Equate.Default) { }

        /// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
        /// <param name="expectedCount">The initial and smallest array size allowed by this list.</param>
        /// <param name="equate">The equate delegate to be used by the list.</param>
        /// <runtime>O(1)</runtime>
        public ListArray(int expectedCount, Equate<T> equate)
        {
            if (expectedCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(expectedCount), expectedCount, "!(0 < " + nameof(expectedCount) + ")");
            }
            _equate = equate;
            _list = new T[expectedCount];
            _count = 0;
        }

        internal ListArray(ListArray<T> listArray)
        {
            _list = new T[listArray._list.Length];
            for (int i = 0; i < _list.Length; i++)
            {
                _list[i] = listArray._list[i];
            }
            _equate = listArray._equate;
            _count = listArray._count;
        }

        internal ListArray(T[] list, int count, Equate<T> equate)
        {
            _list = list;
            _count = count;
            _equate = equate;
        }

        #endregion

        #region Properties

        /// <summary>Look-up and set an indexed item in the list.</summary>
        /// <param name="index">The index of the item to get or set.</param>
        /// <returns>The value at the given index.</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > _count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, "!(0 <= " + nameof(index) + " <= " + nameof(Count) + "[" + _count + "])");
                }
                T returnValue = _list[index];
                return returnValue;
            }
            set
            {
                if (index < 0 || index > _count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, "!(0 <= " + nameof(index) + " <= " + nameof(Count) + "[" + _count + "])");
                }
                _list[index] = value;
            }
        }

        /// <summary>Gets the number of items in the list.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public int Count { get { return this._count; } }

        /// <summary>Gets the current capacity of the list.</summary>
        /// <runtime>O(1)</runtime>
        public int CurrentCapacity => _list.Length;
        /// <summary>Gets the equate delegate of the list.</summary>
        /// <runtime>O(1)</runtime>
        public Equate<T> Equate => _equate;

        #endregion

        #region Methods

        #region Add

        /// <summary>Adds an item to the end of the list.</summary>
        /// <param name="addition">The item to be added.</param>
        /// <runtime>O(n) Ω(1) ε(1)</runtime>
        public void Add(T addition)
        {
            if (_count == _list.Length)
            {
                if (_list.Length > int.MaxValue / 2)
                {
                    throw new InvalidOperationException("Your list is so large that it can no longer double itself (int.MaxValue barrier reached).");
                }
                T[] newList = new T[_list.Length * 2];
                _list.CopyTo(newList, 0);
                _list = newList;
            }
            _list[_count++] = addition;
        }

        /// <summary>Adds an item at a given index.</summary>
        /// <param name="addition">The item to be added.</param>
        /// <param name="index">The index to add the item at.</param>
        public void Add(T addition, int index)
        {
            if (_count == _list.Length)
            {
                if (_list.Length > int.MaxValue / 2)
                {
                    throw new InvalidOperationException("Your list is so large that it can no longer double itself (int.MaxValue barrier reached).");
                }
                T[] newList = new T[_list.Length * 2];
                _list.CopyTo(newList, 0);
                _list = newList;
            }
            for (int i = _count; i > index; i--)
            {
                _list[i] = _list[i - 1];
            }
            _list[index] = addition;
            _count++;
        }

        #endregion

        #region Clear

        /// <summary>Empties the list back and reduces it back to its original capacity.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public void Clear()
        {
            _list = new T[1];
            _count = 0;
        }

        #endregion

        #region Clone

        /// <summary>Creates a shallow clone of this data structure.</summary>
        /// <returns>A shallow clone of this data structure.</returns>
        public ListArray<T> Clone()
        {
            return new ListArray<T>(this);
        }

        #endregion

        #region Remove

        /// <summary>Removes the item at a specific index.</summary>
        /// <param name="index">The index of the item to be removed.</param>
        /// <remarks>Runtime: Towel(n - index).</remarks>
        public void Remove(int index)
        {
            RemoveWithoutShrink(index);
            if (_count < _list.Length / 2)
            {
                T[] newList = new T[_list.Length / 2];
                for (int i = 0; i < _count; i++)
                {
                    newList[i] = _list[i];
                }
                _list = newList;
            }
        }

        /// <summary>Removes the item at a specific index.</summary>
        /// <param name="index">The index of the item to be removed.</param>
        /// <remarks>Runtime: Towel(n - index).</remarks>
        public void RemoveWithoutShrink(int index)
        {
            if (index < 0 || index >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "!(0 <= " + nameof(index) + " <= " + nameof(ListArray<T>) + "." + nameof(Count) + ")");
            }
            if (_count < _list.Length / 2)
            {
                T[] newList = new T[_list.Length / 2];
                for (int i = 0; i < _count; i++)
                {
                    newList[i] = _list[i];
                }
                _list = newList;
            }
            for (int i = index; i < _count - 1; i++)
            {
                _list[i] = _list[i + 1];
            }
            _count--;
        }

        /// <summary>Removes all predicated items from the list.</summary>
        /// <param name="predicate">The predicate to determine removals.</param>
        /// <remarks>Runtime: Towel(n).</remarks>
        public void RemoveAll(Predicate<T> predicate)
        {
            RemoveAllWithoutShrink(predicate);
            if (_count < _list.Length / 2)
            {
                T[] newList = new T[_list.Length / 2];
                for (int i = 0; i < _count; i++)
                {
                    newList[i] = _list[i];
                }
                _list = newList;
            }
        }

        /// <summary>Removes all occurences of a given item.</summary>
        /// <param name="value">The item to remove.</param>
        /// <runtime>Θ(n)</runtime>
        public void RemoveAll(T value)
        {
            RemoveAll(x => _equate(x, value));
        }

        /// <summary>Removes all predicated items from the list.</summary>
        /// <param name="predicate">The predicate to determine removals.</param>
        /// <runtime>Θ(n)</runtime>
        public void RemoveAllWithoutShrink(Predicate<T> predicate)
        {
            if (_count == 0)
            {
                return;
            }
            int removed = 0;
            for (int i = 0; i < _count; i++)
            {
                if (predicate(_list[i]))
                {
                    removed++;
                }
                else
                {
                    _list[i - removed] = _list[i];
                }
            }
            _count -= removed;
        }

        /// <summary>Removes the first occurence of a value from the list.</summary>
        /// <param name="removal">The value to remove.</param>
        /// <runtime>O(n), Ω(1)</runtime>
        public void RemoveFirst(T removal)
        {
            RemoveFirst(x => _equate(x, removal));
        }

        /// <summary>Removes the first predicated value from the list.</summary>
        /// <param name="predicate">The predicate to determine removals.</param>
        /// <runtime>O(n), Ω(1)</runtime>
        public void RemoveFirst(Predicate<T> predicate)
        {
            if (!TryRemoveFirst(predicate))
            {
                throw new InvalidOperationException("Attempting to remove a non-existing item from this list.");
            }
        }

        /// <summary>Removes the first occurence of a value from the list without causing the list to shrink.</summary>
        /// <param name="removal">The value to remove.</param>
        /// <runtime>O(n), Ω(1)</runtime>
        public void RemoveFirstWithoutShrink(T removal)
        {
            RemoveFirstWithoutShrink(x => _equate(x, removal));
        }

        /// <summary>Removes the first predicated value from the list wihtout shrinking the list.</summary>
        /// <param name="predicate">The predicate to determine removals.</param>
        /// <runtime>O(n), Ω(1)</runtime>
        public void RemoveFirstWithoutShrink(Predicate<T> predicate)
        {
            if (!TryRemoveFirst(predicate))
            {
                throw new InvalidOperationException("Attempting to remove a non-existing item from this list.");
            }
        }

        /// <summary>Tries to remove the first value if the value exists.</summary>
        /// <param name="value">The value to remove.</param>
        /// <returns>True if the item was found and removed. False if not.</returns>
        public bool TryRemoveFirst(T value)
        {
            return TryRemoveFirst(x => _equate(x, value));
        }

        /// <summary>Tries to remove the first predicated value if the value exists.</summary>
        /// <param name="predicate">The predicate to determine removals.</param>
        /// <returns>True if the item was found and removed. False if not.</returns>
        public bool TryRemoveFirst(Predicate<T> predicate)
        {
            int i;
            for (i = 0; i < _count; i++)
            {
                if (predicate(_list[i]))
                {
                    break;
                }
            }
            if (i == _count)
            {
                return false;
            }
            Remove(i);
            return true;
        }

        #endregion

        #region Stepper And IEnumerable

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> step)
        {
            for (int i = 0; i < _count; i++)
            {
                step(_list[i]);
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        public void Stepper(StepRef<T> step)
        {
            for (int i = 0; i < _count; i++)
            {
                step(ref _list[i]);
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepBreak<T> step)
        {
            for (int i = 0; i < _count; i++)
            {
                if (step(_list[i]) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
            }
            return StepStatus.Continue;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepRefBreak<T> step)
        {
            for (int i = 0; i < _count; i++)
            {
                if (step(ref _list[i]) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
            }
            return StepStatus.Continue;
        }

        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _list[i];
            }
        }

        System.Collections.Generic.IEnumerator<T>
            System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _list[i];
            }
        }

        #endregion

        #region ToArray

        /// <summary>Converts the list array into a standard array.</summary>
        /// <returns>A standard array of all the elements.</returns>
        public T[] ToArray()
        {
            T[] array = new T[_count];
            for (int i = 0; i < _count; i++)
            {
                array[i] = _list[i];
            }
            return array;
        }

        #endregion

        #region Trim

        /// <summary>Resizes this allocation to the current count.</summary>
        public void Trim()
        {
            T[] newList = new T[_count];
            for (int i = 0; i < _count; i++)
            {
                newList[i] = _list[i];
            }
            _list = newList;
        }

        #endregion

        #endregion
    }
}
