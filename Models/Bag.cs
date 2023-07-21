using System.ComponentModel.DataAnnotations.Schema;

namespace BagAPI.Models
{
    public class Bag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public double Weight { get; set; }
        public int Capacity { get; set; }
    }
}
