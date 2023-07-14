using MessagePack;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Planner.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        [DisplayName("Завдання")]
        public string Name { get; set; }
        [DisplayName("Опис")]
        public string? Description { get; set; }
        [DisplayName("Status")]
        public bool Done { get; set; }
    }
}
