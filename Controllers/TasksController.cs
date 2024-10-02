using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
[Authorize] // Requer autenticação
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }


    // GET: api/tasks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();

    }

    // post: api/tasks
    [HttpPost]
    public async Task<ActionResult<TaskItem>> CreateTask([FromBody] TaskItem task)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        task.UserId = userId;

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
    }

// PUT: api/tasks/{id}
[HttpPut("{id}")]
public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItemDto taskDto)
{
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    // Verifica se a tarefa existe e pertence ao usuário logado
    var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

    if (task == null)
    {
        return NotFound(); // Retorna 404 se a tarefa não for encontrada ou não pertencer ao usuário
    }

    // Atualiza os dados da tarefa
    task.Title = taskDto.Title;
    task.Description = taskDto.Description;
    task.IsCompleted = taskDto.IsCompleted;

    _context.Tasks.Update(task);
    await _context.SaveChangesAsync();

    return NoContent(); // Retorna 204 (sem conteúdo) para indicar que a operação foi bem-sucedida
}

// DELETE: api/tasks/{id}
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteTask(int id)
{
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    // Verifica se a tarefa existe e pertence ao usuário logado
    var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

    if (task == null)
    {
        return NotFound(); // Retorna 404 se a tarefa não for encontrada ou não pertencer ao usuário
    }

    _context.Tasks.Remove(task);
    await _context.SaveChangesAsync();

    return NoContent(); // Retorna 204 para indicar que a tarefa foi removida
}

}

