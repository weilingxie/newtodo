using AutoMapper;
using NewTodo.Application.TodoItems.Models;
using NewTodo.Domain.Models;

namespace NewTodo.Mappers
{
    public class DomainToTodoOutputProfile : Profile
    {
        public DomainToTodoOutputProfile()
        {
            CreateMap<TodoItem, TodoOutput>();    
        }
    }
}