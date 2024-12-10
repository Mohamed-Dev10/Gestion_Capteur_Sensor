using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapteurManagement.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
