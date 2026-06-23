using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Repositories;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly TodoRepository _repository;

    public TodosController(TodoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> ConsultarTodos([FromQuery] string? textoBuscar = "")
    {
        var todos = await _repository.ConsultarTodosAsync(textoBuscar);
        return Ok(todos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ConsultarTodo(int id)
    {
        var todo = await _repository.ConsultarTodoAsync(id);

        if (todo == null)
            return NotFound(new { mensaje = "No se encontró la tarea." });

        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Guardar([FromBody] TodoCrearDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Titulo))
            return BadRequest(new { mensaje = "El título es obligatorio." });

        var todo = await _repository.GuardarAsync(dto);

        return CreatedAtAction(
            nameof(ConsultarTodo),
            new { id = todo.Id },
            todo);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] TodoActualizarDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Titulo))
            return BadRequest(new { mensaje = "El título es obligatorio." });

        var todo = await _repository.ActualizarAsync(id, dto);

        if (todo == null)
            return NotFound(new { mensaje = "No se encontró la tarea." });

        return Ok(todo);
    }

    [HttpPatch("{id:int}/estado")]
    public async Task<IActionResult> CambiarEstado(int id, [FromBody] TodoEstadoDto dto)
    {
        var todo = await _repository.CambiarEstadoAsync(id, dto);

        if (todo == null)
            return NotFound(new { mensaje = "No se encontró la tarea." });

        return Ok(todo);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Borrar(int id)
    {
        var todo = await _repository.ConsultarTodoAsync(id);

        if (todo == null)
            return NotFound(new { mensaje = "No se encontró la tarea." });

        await _repository.BorrarAsync(id);

        return Ok(new { mensaje = "Tarea eliminada correctamente." });
    }
}