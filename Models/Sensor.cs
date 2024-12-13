using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CapteurManagement.Models
{
    [Table("Sensor")]
    public class Sensor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
