namespace ForCSharpTesting.LeetCodeSolutions.Common;

/// <summary>
/// https://leetcode.com/problems/design-circular-deque/
/// </summary>
public class MyCircularDeque
{
    private readonly int[] _internalArray;
    private int _startIndex = 0;
    private int _endIndex = 0;

    public int Length { get; set; }
    public int Capacity { get; }


    public MyCircularDeque(int capacity)
    {
        Length = 0;
        Capacity = capacity;
        _internalArray = new int[capacity];
    }

    public bool InsertFront(int value)
    {
        if (Length == Capacity)
        {
            return false;
        }

        var currentIndex = _startIndex - 1;
        if (currentIndex < 0)
        {
            currentIndex = Capacity - 1;
        }

        _internalArray[currentIndex] = value;
        _startIndex = currentIndex;
        Length++;

        return true;
    }

    public bool InsertLast(int value)
    {
        if (Length == Capacity)
        {
            return false;
        }

        var currentIndex = _endIndex + 1;
        if (currentIndex >= Capacity)
        {
            currentIndex = 0;
        }

        _internalArray[_endIndex] = value;
        _endIndex = currentIndex;
        Length++;

        return true;
    }

    public bool DeleteFront()
    {
        if (Length == 0)
        {
            return false;
        }

        var currentIndex = _startIndex + 1;
        if (currentIndex >= Capacity)
        {
            currentIndex = 0;
        }

        _startIndex = currentIndex;
        Length--;

        return true;
    }

    public bool DeleteLast()
    {
        if (Length == 0)
        {
            return false;
        }

        var currentIndex = _endIndex - 1;
        if (currentIndex < 0)
        {
            currentIndex = Capacity - 1;
        }

        _endIndex = currentIndex;
        Length--;

        return true;
    }

    public int GetFront()
    {
        if (Length == 0)
        {
            return -1;
        }

        return _internalArray[_startIndex];
    }

    public int GetRear()
    {
        if (Length == 0)
        {
            return -1;
        }

        var currentIndex = _endIndex - 1;
        if (currentIndex < 0)
        {
            currentIndex = Capacity - 1;
        }

        return _internalArray[currentIndex];
    }

    public bool IsEmpty()
    {
        return Length == 0;
    }

    public bool IsFull()
    {
        return Length == Capacity;
    }
}