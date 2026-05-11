namespace ForCSharpTesting.LeetCodeSolutions.Common;

public interface IHeap<T>
{
    void Push(T value);   // добавить элемент
    T Pop();              // извлечь корень (min или max)
    T Peek();             // посмотреть корень без удаления
    int Count { get; }    // количество элементов
}

public class MyHeapMax : IHeap<int>
{
    private int[] _internalArray;
    private int _count;

    public MyHeapMax()
    {
        _internalArray = new int[55];
        _count = 0;
    }

    public MyHeapMax(int capacity)
    {
        _internalArray = new int[capacity];
        _count = 0;
    }

    public static MyHeapMax FromArray(int[] array)
    {
        var heap = new MyHeapMax(array.Length);

        Array.Copy(array, heap._internalArray, array.Length);
        heap._count = array.Length;

        //Console.WriteLine($"init: [{string.Join(", ", heap._internalArray)}]");

        for (int i = heap._internalArray.Length / 2; i >= 0; i--)
        {
            heap.SiftDown(i);
            //Console.WriteLine($"{i}: [{string.Join(", ", heap._internalArray)}]");
        }

        return heap;
    }

    public int Count => _count;

    public int Peek()
    {
        if (_count <= 0) throw new InvalidOperationException("Heap is empty");
        return _internalArray[0];
    }

    public int Pop()
    {
        var result = Peek();

        _internalArray[0] = _internalArray[_count - 1];
        _count--;
        SiftDown(0);

        return result;
    }

    private void SiftDown(int index)
    {
        while (true)
        {
            var maxIndex = index;

            var leftChildIndex = index * 2 + 1;
            var rightChildIndex = index * 2 + 2;

            if (leftChildIndex < _count && _internalArray[leftChildIndex] > _internalArray[maxIndex])
            {
                maxIndex = leftChildIndex;
            }

            if (rightChildIndex < _count && _internalArray[rightChildIndex] > _internalArray[maxIndex])
            {
                maxIndex = rightChildIndex;
            }

            if (index == maxIndex) break;

            (_internalArray[index], _internalArray[maxIndex]) = (_internalArray[maxIndex], _internalArray[index]);
            index = maxIndex;
        }
    }

    public void Push(int value)
    {
        if (_count >= _internalArray.Length)
        {
            var newArray = new int[_internalArray.Length * 2];
            Array.Copy(_internalArray, newArray, _internalArray.Length);

            _internalArray = newArray;
        }

        _internalArray[_count] = value;
        SiftUp(_count);

        _count++;
    }

    private void SiftUp(int index)
    {
        while (index > 0)
        {
            var parentIndex = (index - 1) / 2;
            if (_internalArray[index] > _internalArray[parentIndex])
            {
                (_internalArray[index], _internalArray[parentIndex]) = (_internalArray[parentIndex], _internalArray[index]);
                index = parentIndex;
            }
            else
            {
                break;
            }
        }
    }
}
