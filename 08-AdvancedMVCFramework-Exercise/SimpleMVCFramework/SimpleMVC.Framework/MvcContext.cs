namespace SimpleMVC.Framework
{
    public class MvcContext
    {
        private static MvcContext instance;
        private static readonly object instanceLock = new object();

        private MvcContext() { } //this is going to be singleton

        public static MvcContext Get
        {
            get
            {
                if (instance == null)
                {
                    //this achieves tread safety
                    lock (instanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new MvcContext();
                        }
                    }
                }
                return instance;
            }
        }

        public string AssemblyName { get; set; }

        public string ControllersFolder { get; set; }

        public string ControllersSuffix { get; set; }

        public string ViewsFolder { get; set; }

        public string ModelsFolder { get; set; }

        public string ResourcesFolder { get; set; }
    }

}
