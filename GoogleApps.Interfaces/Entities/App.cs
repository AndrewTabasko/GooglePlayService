using System;
using System.ComponentModel.DataAnnotations;

namespace GoogleApps.Interfaces.Entities
{
    public class App
    {
        public App()
        {
            guid = Guid.NewGuid();
        }
        [Key]
        public Guid guid { get; set; }
        public string name { get; set; }
        public string googleplayid {get;set;}
        public string hl { get; set; }
        public string gl { get; set; }
        public long downloads { get; set; }

    }
}
