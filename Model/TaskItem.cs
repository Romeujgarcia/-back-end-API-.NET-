public class TaskItem
{
    public int Id { get; set; }
    public string? Title { get; set; }
     public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    
    // Chave estrangeira que se relaciona com o usuário
    public string? UserId { get; set; } // Assumindo que o tipo da chave primária do usuário é string
    public virtual ApplicationUser? User { get; set; } // Propriedade de navegação
}



