using AutoMapper;
using NewTodo.Domain.Models;

namespace NewTodo.Application.TodoItems.Models.Mapping
{
    public class DomainToTodoOutputProfile : Profile
    {
        public DomainToTodoOutputProfile()
        {
            CreateMap<TodoItem, TodoOutput>();    
        }
    }
}