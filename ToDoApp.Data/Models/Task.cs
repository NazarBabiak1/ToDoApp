

namespace ToDoApp.Data.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int BoardId { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime DueDate { get; set; }
        public int CreatedById { get; set; }
        public int StatusId { get; set;}
        public int AssigneeId { get; set; }

        ///

        public User User { get; set; }
        public Board Board { get; set; }
        public Status Status { get; set; }

    }
}
