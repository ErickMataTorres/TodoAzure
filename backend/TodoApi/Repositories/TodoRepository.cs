using System.Data;
using Dapper;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Repositories;

public class TodoRepository
{
    private readonly Conexion _conexion;

    public TodoRepository(Conexion conexion)
    {
        _conexion = conexion;
    }

    public async Task<IEnumerable<TodoItem>> ConsultarTodosAsync(string? textoBuscar)
    {
        using var conexion = _conexion.CrearConexion();

        return await conexion.QueryAsync<TodoItem>(
            "dbo.spConsultarTodos",
            new { TextoBuscar = textoBuscar },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<TodoItem?> ConsultarTodoAsync(int id)
    {
        using var conexion = _conexion.CrearConexion();

        return await conexion.QuerySingleOrDefaultAsync<TodoItem>(
            "dbo.spConsultarTodo",
            new { Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<TodoItem> GuardarAsync(TodoCrearDto dto)
    {
        using var conexion = _conexion.CrearConexion();

        return await conexion.QuerySingleAsync<TodoItem>(
            "dbo.spGuardarTodo",
            new
            {
                dto.Titulo,
                dto.Descripcion
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<TodoItem?> ActualizarAsync(int id, TodoActualizarDto dto)
    {
        using var conexion = _conexion.CrearConexion();

        return await conexion.QuerySingleOrDefaultAsync<TodoItem>(
            "dbo.spActualizarTodo",
            new
            {
                Id = id,
                dto.Titulo,
                dto.Descripcion
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<TodoItem?> CambiarEstadoAsync(int id, TodoEstadoDto dto)
    {
        using var conexion = _conexion.CrearConexion();

        return await conexion.QuerySingleOrDefaultAsync<TodoItem>(
            "dbo.spCambiarEstadoTodo",
            new
            {
                Id = id,
                dto.Completada
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task BorrarAsync(int id)
    {
        using var conexion = _conexion.CrearConexion();

        await conexion.ExecuteAsync(
            "dbo.spBorrarTodo",
            new { Id = id },
            commandType: CommandType.StoredProcedure);
    }
}