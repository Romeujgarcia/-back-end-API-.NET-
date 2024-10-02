using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public ICollection<TaskItem>? Tasks { get; set; } // Relacionamento com tarefas
}
