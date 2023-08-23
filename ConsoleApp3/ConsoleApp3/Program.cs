﻿Mutex mutex = new Mutex();

static void ProcessData(object data)
{
    Console.WriteLine($"ProcessData Thread {Thread.CurrentThread.ManagedThreadId} started processing data.");
    Thread.Sleep(5000);
    Console.WriteLine($"ProcessData Thread {Thread.CurrentThread.ManagedThreadId} finished processing data.");
}

static void ParallelProcessing(int[] data, int chunkSize)
{
    for (int i = 0; i < data.Length; i += chunkSize)
    {
        int[] chunk = new int[chunkSize];
        Array.Copy(data, i, chunk, 0, chunkSize);
        Thread thread = new Thread(ProcessData);
        thread.Start(chunk);
    }
}

void UseMutexFunc(string message)
{
    mutex.WaitOne();
    Console.WriteLine($"UseMutexFunc Thread {Thread.CurrentThread.ManagedThreadId} started process with mutex.");
    Thread.Sleep(2000);
    Console.WriteLine($"UseMutexFunc Thread {Thread.CurrentThread.ManagedThreadId} finished process with mutex. {message}");
    mutex.ReleaseMutex();
}

void SynchronizedAccess(int numberOfThreads)
{
    Thread[] threads = new Thread[numberOfThreads];
    for (int i = 0; i < threads.Length; i++)
    {
        threads[i] = new Thread(() => UseMutexFunc($"Message {i}"));
        threads[i].Start();
    }
}

static void Method1()
{
    Console.WriteLine("Thread 1 started");
    Thread.Sleep(2000);
    Console.WriteLine("Thread 1 ended");
}

static void Method2(object obj)
{
    int number = (int)obj;
    Console.WriteLine($"Thread 2 started with number {number}");
    Thread.Sleep(3000);
    Console.WriteLine($"Thread 2 ended with number {number}");
}

static void Method3()
{
    Thread.CurrentThread.Name = "MyThread";
    Console.WriteLine($"Thread {Thread.CurrentThread.Name} started");
    Thread.Sleep(4000);
    Console.WriteLine($"Thread {Thread.CurrentThread.Name} ended");
}

static async Task Method1Async()
{
    Console.WriteLine("Method 1 started");
    await Task.Delay(2000);
    Console.WriteLine("Method 1 ended");
}

static async Task Method2Async()
{
    Console.WriteLine("Method 2 started");
    Task task1 = Task.Delay(3000);
    Task task2 = Task.Delay(2000);
    await Task.WhenAll(task1, task2);
    Console.WriteLine("Method 2 ended");
}

static async Task<int> Method3Async()
{
    Console.WriteLine("Method 3 started");
    await Task.Delay(4000);
    Console.WriteLine("Method 3 ended");
    return 42;
}

void Main()
{
    Thread thread1 = new Thread(Method1);
    Thread thread2 = new Thread(new ParameterizedThreadStart(Method2));
    Thread thread3 = new Thread(Method3);

    thread1.Start();
    thread2.Start(42);
    thread3.Start();

    SynchronizedAccess(5);

    int[] data = new int[1000];
    Random rand = new Random();
    for (int i = 0; i < data.Length; i++)
    {
        data[i] = rand.Next(1, 10);
    }
    ParallelProcessing(data, 100);

    Method1Async().Wait();
    Method2Async().Wait();
    int result = Method3Async().Result;
    Console.WriteLine($"Method 3 returned {result}");
}

Main();