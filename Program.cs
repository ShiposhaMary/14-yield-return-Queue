using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yield_return_Queue

{
    //принимает на вход две последовательности целых чисел
    //    и возвращает последовательность, 
    //    состоящую из попарных сумм их элементов.
    //private static IEnumerable<int> ZipSum(IEnumerable<int> first, IEnumerable<int> second)
    //{
    //    var e1 = first.GetEnumerator();
    //    var e2 = second.GetEnumerator();
    //    while (e1.MoveNext())
    //    {
    //        e2.MoveNext();
    //        yield return e1.Current + e2.Current;
    //    }
    //}







    public class Queue<T> : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            var current = Head;
            while (current!=null )
            {
                yield return current.Value;
                current = current.Next;
            }

        }

        //Не будем останавливаться на этом. Просто всегда пишите так.
        //Это связано с архитектурными особенностями IEnumerable<T>,
        //И требует следующего уровня понимания архитектур
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        QueueItem<T> Head;
        QueueItem<T> tail;

        public bool IsEmpty { get { return Head == null; } }

        public void Enqueue(T value)
        {
            if (IsEmpty)
                tail = Head = new QueueItem<T> { Value = value, Next = null };
            else
            {
                var item = new QueueItem<T> { Value = value, Next = null };
                tail.Next = item;
                tail = item;
            }
        }

        public T Dequeue()
        {
            if (Head == null) throw new InvalidOperationException();
            var result = Head.Value;
            Head = Head.Next;
            if (Head == null)
                tail = null;
            return result;
        }
       
    }

    public class QueueItem<T>
    {
        public T Value { get; set; }
        public QueueItem<T> Next { get; set; }
    }
    class Program
    {
        static void Main()
        {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            foreach (var value in queue)
                Console.WriteLine(value);
        }
    }
}