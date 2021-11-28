using System;

namespace GoogleApps.Interfaces.Entities
{
    public class App
    {
        public App()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public long Downloads { get; set; }
        public string URL { get; set; }

    }
}
