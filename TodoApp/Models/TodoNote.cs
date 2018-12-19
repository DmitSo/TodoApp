namespace TodoApp.Models
{
    public class TodoNote
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}
