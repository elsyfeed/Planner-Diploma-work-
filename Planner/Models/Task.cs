    using System.ComponentModel.DataAnnotations;

namespace Planner.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public string? Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM H:mm}")]
        public DateTime Deadline { get; set; }

    }
}
