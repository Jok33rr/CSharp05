using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace NikitchenkoCSharp05
{
    public class ProcessListItem
    {
        public int Id => Process.Id;
        public bool IsActive => Process.Responding;
        public string ProcessName => Process.ProcessName;
        public string Path => Process.MainModule.FileName;
        public bool KeepAlive { get; set; }

        public DateTime LaunchDateTime => Process.StartTime;
        public string User => Process.MachineName;
        public float CPU => Environment.ProcessorCount;
        public long Memory => Process.PrivateMemorySize64 / 1024;
        public int ThreadsCount => Process.Threads.Count;

        public Process Process { get; }
        public string FileName { get; }
        public string Arguments { get; }

        public ProcessListItem(Process process)
        {
            Process = process;
            FileName = process.StartInfo.FileName;
            Arguments = process.StartInfo.Arguments;
        }

        private HashSet<UiModule> _modules;



        public HashSet<UiModule> Modules
        {
            get
            {
                if (_modules == null)
                    RefreshModules();
                return _modules;
            }
        }

        private HashSet<UiThread> _threads;

        public HashSet<UiThread> Threads
        {
            get
            {
                if (_threads == null)
                    RefreshThreads();
                return _threads;
            }
        }



        public void RefreshModules()
        {
            if (_modules == null)
                _modules = new HashSet<UiModule>();
            foreach (ProcessModule m in Process.Modules)
            {
                _modules.Add(new UiModule(m));
            }
        }

        internal void RefreshThreads()
        {
            if (_threads == null)
                _threads = new HashSet<UiThread>();
            foreach (ProcessThread t in Process.Threads)
            {
                _threads.Add(new UiThread(t));
            }
        }

        public override bool Equals(object obj)
        {
            return obj is ProcessListItem another && this.Id == another.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }




    public class UiModule
    {
        private readonly ProcessModule _module;

        public string Name => _module.ModuleName;
        public string Path => _module.FileName;

        internal UiModule([NotNull] ProcessModule module)
        {
            this._module = module;
        }

        public override bool Equals(object obj)
        {
            return obj is UiModule another && this._module.Equals(another._module);
        }

        public override int GetHashCode()
        {
            return _module.GetHashCode();
        }

        private class NotNullAttribute : Attribute
        {
        }
    }

    public class UiThread
    {
        private readonly ProcessThread _thread;

        public int Id => _thread.Id;
        public ThreadState State => _thread.ThreadState;
        public DateTime LaunchDateTime => _thread.StartTime;

        internal UiThread([NotNull] ProcessThread thread)
        {
            this._thread = thread;
        }

        public override bool Equals(object obj)
        {
            return obj is UiThread another && this._thread.Equals(another._thread);
        }

        public override int GetHashCode()
        {
            return _thread.GetHashCode();
        }

        private class NotNullAttribute : Attribute
        {
        }
    }
}



