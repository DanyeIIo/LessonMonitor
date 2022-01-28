using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ConsoleApp1
{
	internal class Program
	{
		private static void Main(string[] args)
		{
            /// my_start

            //string result = "";
            //string result1 = "";
            //string result2 = "";

            //var thread1 = new Thread(() => result = PrintThreadInfo(152312));
            //var thread2 = new Thread(new ThreadStart(() => result1 = PrintThreadInfo(738434)));
            //thread1.Name = "thread1";
            //thread2.Name = "thread2";
            //thread2.IsBackground = true;
            //thread1.Start();
            //thread2.Start();

            //ThreadPool.QueueUserWorkItem((x) => { });
            //         ThreadPool.QueueUserWorkItem<int>((x) => { result2 = PrintThreadInfo(864521); }, 10, true);

            //         Console.WriteLine("Press F to continue");
            //Console.ReadKey();
            //         Console.WriteLine(result);
            //         Console.WriteLine(result1);
            //         Console.WriteLine(result2);
            for (int i = 0; i < 100; i++)
            {
                var worker = new Worker();

                ThreadPool.QueueUserWorkItem((x) => worker.AddNumber());
                ThreadPool.QueueUserWorkItem((x) => worker.AddNumber());
                ThreadPool.QueueUserWorkItem((x) => worker.AddNumber());
                ThreadPool.QueueUserWorkItem((x) => worker.AddNumber());

                Console.WriteLine("Press F to continue");
                Console.ReadKey();

                Console.WriteLine(worker);
            }

            Console.WriteLine("Press F to continue");
			Console.ReadKey();
		}
		//private static object locker = new object();
		public static string PrintThreadInfo(int myThreadId)
		{
			//lock (locker)
			//{
			var processorId = Thread.GetCurrentProcessorId();
			var name = Thread.CurrentThread.Name;
			var threadId = Thread.CurrentThread.ManagedThreadId;
			var priority = Thread.CurrentThread.Priority;
			var threadState = Thread.CurrentThread.ThreadState;
			var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
			var isBackground = Thread.CurrentThread.IsBackground;

			Console.WriteLine("myThreadId: " + myThreadId);
			//Thread.SpinWait(myThreadId); // Блочит поток, будет ждать, не отдаст управление другим потокам или отдаст?

			Console.WriteLine("processorId: " + processorId);
			Console.WriteLine("name: " + name);
			Console.WriteLine("threadId: " + threadId);
			Console.WriteLine("priority: " + priority);
			Console.WriteLine("threadState: " + threadState);
			Console.WriteLine("isThreadPoolThread: " + isThreadPoolThread);
			Console.WriteLine("isBackground: " + isBackground);
			Console.WriteLine();
			//}

			return Guid.NewGuid().ToString();
		}
	}

	public class Worker
	{
		private List<int> _numbers = new List<int>();
		private int _counter = 0;
		private Random _random = new Random();
		private Mutex _mutex = new Mutex(false, "Mutex");
		private AutoResetEvent _autoResetEvent = new AutoResetEvent(true); // true = free
		private Semaphore _semaphore = new Semaphore(1, 1);
		private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

		/// LOCK
		public void AddNumber() // LOCK
        {
            lock (_numbers)
            {
                _numbers.Add(_random.Next());
                _counter++;
            }
        }

        /// MUTEX
        public void AddNumberWithMutex()
        {
            _mutex.WaitOne(); // take ticket
            _numbers.Add(_random.Next());
            _counter++;

            var processorId = Thread.GetCurrentProcessorId();
            var name = Thread.CurrentThread.Name;
            var threadId = Thread.CurrentThread.ManagedThreadId;
            var priority = Thread.CurrentThread.Priority;
            var threadState = Thread.CurrentThread.ThreadState;
            var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
            var isBackground = Thread.CurrentThread.IsBackground;

            Console.WriteLine("processorId: " + processorId);
            Console.WriteLine("name: " + name);
            Console.WriteLine("threadId: " + threadId);
            Console.WriteLine("priority: " + priority);
            Console.WriteLine("threadState: " + threadState);
            Console.WriteLine("isThreadPoolThread: " + isThreadPoolThread);
            Console.WriteLine("isBackground: " + isBackground);
            Console.WriteLine();

            _mutex.ReleaseMutex(); // Clear mutex
        }

        /// AutoResetEvent
        public void AddNumberWithAutoResetEvent()
		{
			_autoResetEvent.WaitOne(); // block thread
			_numbers.Add(_random.Next());
			_counter++;

			var processorId = Thread.GetCurrentProcessorId();
			var name = Thread.CurrentThread.Name;
			var threadId = Thread.CurrentThread.ManagedThreadId;
			var priority = Thread.CurrentThread.Priority;
			var threadState = Thread.CurrentThread.ThreadState;
			var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
			var isBackground = Thread.CurrentThread.IsBackground;

			Console.WriteLine("processorId: " + processorId);
			Console.WriteLine("name: " + name);
			Console.WriteLine("threadId: " + threadId);
			Console.WriteLine("priority: " + priority);
			Console.WriteLine("threadState: " + threadState);
			Console.WriteLine("isThreadPoolThread: " + isThreadPoolThread);
			Console.WriteLine("isBackground: " + isBackground);
			Console.WriteLine();

			_autoResetEvent.Set(); // Clear autoResetEvent
		}

		public void AddNumberWithSemaphore()
		{
			_semaphore.WaitOne();
			_numbers.Add(_random.Next());
			_counter++;

			var processorId = Thread.GetCurrentProcessorId();
			var name = Thread.CurrentThread.Name;
			var threadId = Thread.CurrentThread.ManagedThreadId;
			var priority = Thread.CurrentThread.Priority;
			var threadState = Thread.CurrentThread.ThreadState;
			var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
			var isBackground = Thread.CurrentThread.IsBackground;

			Console.WriteLine("processorId: " + processorId);
			Console.WriteLine("name: " + name);
			Console.WriteLine("threadId: " + threadId);
			Console.WriteLine("priority: " + priority);
			Console.WriteLine("threadState: " + threadState);
			Console.WriteLine("isThreadPoolThread: " + isThreadPoolThread);
			Console.WriteLine("isBackground: " + isBackground);
			Console.WriteLine();

			_semaphore.Release();
		}

		public void AddNumberWithSemaphorSlim()
		{
			_semaphoreSlim.Wait();
			_numbers.Add(_random.Next());
			_counter++;

			var processorId = Thread.GetCurrentProcessorId();
			var name = Thread.CurrentThread.Name;
			var threadId = Thread.CurrentThread.ManagedThreadId;
			var priority = Thread.CurrentThread.Priority;
			var threadState = Thread.CurrentThread.ThreadState;
			var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
			var isBackground = Thread.CurrentThread.IsBackground;

			Console.WriteLine("processorId: " + processorId);
			Console.WriteLine("name: " + name);
			Console.WriteLine("threadId: " + threadId);
			Console.WriteLine("priority: " + priority);
			Console.WriteLine("threadState: " + threadState);
			Console.WriteLine("isThreadPoolThread: " + isThreadPoolThread);
			Console.WriteLine("isBackground: " + isBackground);
			Console.WriteLine();

			_semaphoreSlim.Release();
		}

		public override string ToString()
		{
			return $"counter: {_counter}, numbers: {string.Join(",", _numbers)}";
		}
    }

	public class Worker2
	{
		private List<int> _numbers = new List<int>();
		private int _counter = 0;
		private Random _random = new Random();

		private Mutex _mutex = new Mutex(false, "Mutex");
		private AutoResetEvent _autoResetEvent = new AutoResetEvent(true);
		private Semaphore _semaphore = new Semaphore(1, 1);
		private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

		public void AddNumberWithSemaphorSlim()
		{
			_semaphoreSlim.Wait();
			_numbers.Add(_random.Next());
			_counter++;

			var processorId = Thread.GetCurrentProcessorId();
			var name = Thread.CurrentThread.Name;
			var threadId = Thread.CurrentThread.ManagedThreadId;
			var priority = Thread.CurrentThread.Priority;
			var threadState = Thread.CurrentThread.ThreadState;
			var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
			var isBackground = Thread.CurrentThread.IsBackground;

			Console.WriteLine("processorId: " + processorId);
			Console.WriteLine("name: " + name);
			Console.WriteLine("threadId: " + threadId);
			Console.WriteLine("priority: " + priority);
			Console.WriteLine("threadState: " + threadState);
			Console.WriteLine("isThreadPoolThread: " + isThreadPoolThread);
			Console.WriteLine("isBackground: " + isBackground);
			Console.WriteLine();

			_semaphoreSlim.Release();
		}

		public void AddNumberSemaphore()
		{
			_semaphore.WaitOne();
			_numbers.Add(_random.Next());
			_counter++;

			var processorId = Thread.GetCurrentProcessorId();
			var name = Thread.CurrentThread.Name;
			var threadId = Thread.CurrentThread.ManagedThreadId;
			var priority = Thread.CurrentThread.Priority;
			var threadState = Thread.CurrentThread.ThreadState;
			var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
			var isBackground = Thread.CurrentThread.IsBackground;

			Console.WriteLine("processorId: " + processorId);
			Console.WriteLine("name: " + name);
			Console.WriteLine("threadId: " + threadId);
			Console.WriteLine("priority: " + priority);
			Console.WriteLine("threadState: " + threadState);
			Console.WriteLine("isThreadPoolThread: " + isThreadPoolThread);
			Console.WriteLine("isBackground: " + isBackground);
			Console.WriteLine();

			_semaphore.Release();
		}

		public void AddNumberWithAuthResetEvent()
		{
			_autoResetEvent.WaitOne();
			_numbers.Add(_random.Next());
			_counter++;

			var processorId = Thread.GetCurrentProcessorId();
			var name = Thread.CurrentThread.Name;
			var threadId = Thread.CurrentThread.ManagedThreadId;
			var priority = Thread.CurrentThread.Priority;
			var threadState = Thread.CurrentThread.ThreadState;
			var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
			var isBackground = Thread.CurrentThread.IsBackground;

			Console.WriteLine("processorId: " + processorId);
			Console.WriteLine("name: " + name);
			Console.WriteLine("threadId: " + threadId);
			Console.WriteLine("priority: " + priority);
			Console.WriteLine("threadState: " + threadState);
			Console.WriteLine("isThreadPoolThread: " + isThreadPoolThread);
			Console.WriteLine("isBackground: " + isBackground);
			Console.WriteLine();

			_autoResetEvent.Set();
		}

		public void AddNumberWithLock()
		{
			lock (_numbers)
			{
				_numbers.Add(_random.Next());
				_counter++;
			}

			var processorId = Thread.GetCurrentProcessorId();
			var name = Thread.CurrentThread.Name;
			var threadId = Thread.CurrentThread.ManagedThreadId;
			var priority = Thread.CurrentThread.Priority;
			var threadState = Thread.CurrentThread.ThreadState;
			var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
			var isBackground = Thread.CurrentThread.IsBackground;

			Console.WriteLine("processorId: " + processorId);
			Console.WriteLine("name: " + name);
			Console.WriteLine("threadId: " + threadId);
			Console.WriteLine("priority: " + priority);
			Console.WriteLine("threadState: " + threadState);
			Console.WriteLine("isThreadPoolThread: " + isThreadPoolThread);
			Console.WriteLine("isBackground: " + isBackground);
			Console.WriteLine();
		}

		public void AddNumber()
		{
			_mutex.WaitOne();
			_numbers.Add(_random.Next());
			_counter++;
			_mutex.ReleaseMutex();

			var processorId = Thread.GetCurrentProcessorId();
			var name = Thread.CurrentThread.Name;
			var threadId = Thread.CurrentThread.ManagedThreadId;
			var priority = Thread.CurrentThread.Priority;
			var threadState = Thread.CurrentThread.ThreadState;
			var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
			var isBackground = Thread.CurrentThread.IsBackground;

			Console.WriteLine("processorId: " + processorId);
			Console.WriteLine("name: " + name);
			Console.WriteLine("threadId: " + threadId);
			Console.WriteLine("priority: " + priority);
			Console.WriteLine("threadState: " + threadState);
			Console.WriteLine("isThreadPoolThread: " + isThreadPoolThread);
			Console.WriteLine("isBackground: " + isBackground);
			Console.WriteLine();
		}

		public override string ToString()
		{
			return $"counter: {_counter}, numbers: [{string.Join(", ", _numbers)}]";
		}
	}
}
