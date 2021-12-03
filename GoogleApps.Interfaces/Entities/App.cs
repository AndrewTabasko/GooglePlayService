using System;
using System.ComponentModel.DataAnnotations;

namespace GoogleApps.Interfaces.Entities
{
    public class App
    {
        public App()
        {
            Guid = Guid.NewGuid();
        }
        [Key]
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string GooglePlayId {get;set;}
        public string Hl { get; set; }
        public string Gl { get; set; }
        public long Downloads { get; set; }

    }
}
