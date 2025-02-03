﻿namespace MyCustomList;
using System;
public class MyList <T>
{
    private T[] _array;
    public int Capacity { get; set; }
    public int Count { get; set; }


    public MyList()
    {
        _array = new T[10];
        Capacity = _array.Length;
        Count = 0;
    }
    public MyList(int capacity)
    {
        _array = new T[capacity];
        Capacity = capacity;
        Count = 0;
    }

    public MyList(int capacity, T element)
    {
        _array = new T[capacity];
        _array[0] = element;
        Capacity = capacity;
        Count = 1;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Capacity: {Capacity}");
        Console.WriteLine($"Count: {Count}");
        for (int i = 0; i < Capacity; i++)
        {
            Console.WriteLine($"_array[{i}]: {_array[i]}");

        }

        Console.WriteLine( );
    }
    public void Add(T element) // добавляем 1 элемент
    {
        if(Count >= Capacity)
        {
            Resize();
        }
        _array[Count] = element;
        Count++;
    }

    public void Add(T[] elements) // добаваяем массив элементов
    {   
        if(Count >= Capacity)
        {
            Resize(elements.Length);
        }

        for (int i = 0; i < elements.Length; i++)
        {
            _array[Count] = elements[i];
            Capacity = _array.Length;
            Count++;
        }

    }

    public void Add(int index, T element) // добавляем элемент в массив без перезаписи
    {
                for (int i = Count; i >= index; i--)
                {
                    if (i + 1 >= _array.Length)
                    {
                        Resize();
                    }
                    _array[i] = _array[i - 1];
                }
                _array[index] = element;
                Count++;
    } 

    public void Add(int index, T[] elements) // добавляем массив элементов в исходный массив без перезаписи
    {
        for (int i = Count; i >= index; i--) // сдвигаем элементы массива направо на длину поступившего массива
        {
            if (i + elements.Length >= _array.Length)
            {
                Resize(elements.Length);
            }

            _array[i + elements.Length] = _array[i];
        }

        for (int j = 0; j < elements.Length; j++) // записываем элементы поступившего массива в исходный
        {
            _array[j + index] = elements[j];
        }
        Count+=elements.Length;
    }



    public T this[int index]
    {
        get
        {
            if(index >= Count || index < 0)
            { 
                throw new IndexOutOfRangeException();
            }
            return _array[index];
        }

        set
        {
            if (index >= Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            _array[index] = value;
        }
    }



    private void Resize()
    {
        int newLength = _array.Length * 2;
        var newArray = new T[newLength];

        Array.Copy(_array, newArray, Count);
        _array = newArray;
        Capacity = _array.Length;
    }

    private void Resize(int arrayLenth) // подумал что нет нужды отправлять сюда массив и доставать длину, если можно сразу отправить длину, не отправляя весь массив
    {
        int newLength = _array.Length * 2 + arrayLenth;
        var newArray = new T[newLength];

        Array.Copy(_array, newArray, Count);
        _array = newArray;
        Capacity = _array.Length;
    }

    private void Copy(T[] sourceArray, T[] destinationArray)
    {
        if(sourceArray.Length > destinationArray.Length)
        {
            throw new ArgumentException();
        }

        for(int i = 0; i < sourceArray.Length; i++)
        {
            destinationArray[i] = sourceArray[i];
        }
    }
    
    public void Sort()
    {
        if (typeof(T) == typeof(int))
        {
            var intArray = _array as int[];
            for (int i = 0; i < Count - 1; i++)
            {
                for (int j = 0; j < Count - i - 1; j++)
                {
                    if (intArray[j] > intArray[j + 1])
                    {
                        int temp = intArray[j];
                        intArray[j] = intArray[j + 1];
                        intArray[j + 1] = temp;
                    }
                }
            }
        }
        else
        {
            throw new InvalidOperationException("Сортировка данного типа не поддерживается");
        }
    }
}