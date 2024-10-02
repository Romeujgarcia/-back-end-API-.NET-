public class User
{
    public int Id { get; set; }
    public string?  usuario { get; set; }
    public string? senha { get; set; }

   public ICollection<TaskItem>? Tasks { get; set; }  // Relação de navegação com as tarefas 
}
