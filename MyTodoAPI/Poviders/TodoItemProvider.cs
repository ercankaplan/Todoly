using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyTodo.Data.Models.Ef;
using MyTodo.Data.Models.Ef.EfContext;
using MyTodoAPI.Dto;

namespace MyTodoAPI.Poviders
{

    public class TodoItemProvider : ITodoItemProvider
    {
        private readonly TODOLYDbContext dbContext;
        private readonly ILogger<TodoItemProvider> logger;
        private readonly IMapper mapperTodoItemDb2VM;
        private readonly IMapper mapperTodoItemListDb2VM;
        private readonly IMapper mapperTodoItemVM2Db;

        public TodoItemProvider(TODOLYDbContext dbContext, ILogger<TodoItemProvider> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;

            mapperTodoItemDb2VM = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TodoItem, TodoItemDto>();
            }).CreateMapper();


            mapperTodoItemVM2Db = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TodoItemDto, TodoItem>();
            }).CreateMapper();


        }

        public async Task<TodoItem> AddTodoItemAsync(TodoItemDto model)
        {
            try
            {
                TodoItem todo = mapperTodoItemVM2Db.Map<TodoItemDto, TodoItem>(model);


                todo.Id = Guid.NewGuid();

                todo.CreatedTime = DateTime.Now;


                if (!dbContext.Person.Where(o => o.Id == todo.PersonId).Any())
                    throw new ApplicationException("Person not found");


                dbContext.TodoItem.Add(todo);

                await dbContext.SaveChangesAsync();

                return todo;
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.StackTrace);
                return null;
            }
        }

        public async Task<TodoItem> UpdateTodoItemAsync(TodoItemDto model)
        {
            try
            {
                

                TodoItem todoEntity = await dbContext.TodoItem.Where(o => o.Id == model.Id).FirstAsync();

                if (todoEntity == null)
                    throw new ApplicationException("Todo not found");


                if (!dbContext.Person.Where(o => o.Id == todoEntity.PersonId).Any())
                    throw new ApplicationException("Person not found");

                todoEntity.Title = model.Title;
                todoEntity.Description= model.Description;
                todoEntity.CreatedTime = DateTime.Now;
                todoEntity.DueTo = model.DueTo;
                todoEntity.UpdateTime= DateTime.Now;

                dbContext.TodoItem.Update(todoEntity);

                await dbContext.SaveChangesAsync();

                return todoEntity;
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.StackTrace);
                return null;
            }
        }
        public async Task<bool> DeleteTodoItemAsync(Guid id)
        {
            try
            {
                TodoItem todo = await dbContext.TodoItem.Where(o => o.Id == id).FirstAsync();

                if (todo == null)
                    throw new ApplicationException("Todo not found");

                dbContext.TodoItem.Remove(todo);

                await dbContext.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {

                logger?.LogError(ex.StackTrace);
                return false;
            }
        }

        public async Task<TodoItem> GetTodoItemAsync(Guid id)
        {
            try
            {
                TodoItem todo = await dbContext.TodoItem.Where(o => o.Id == id).FirstOrDefaultAsync();

                if (todo == null)
                    throw new ApplicationException("Todo not found");

                return todo;

            }
            catch (Exception ex)
            {

                logger?.LogError(ex.StackTrace);
                return null;
            }
        }

        public async Task<List<TodoItemDto>> GetTodoItemsAsync()
        {
            try
            {
                var response = await dbContext.TodoItem.ToListAsync();

                var result = mapperTodoItemDb2VM.Map<List<TodoItem>, List<TodoItemDto>>(response);

                return result;

            }
            catch (Exception ex)
            {

                logger?.LogError(ex.StackTrace);
                return null;
            }
        }
    }
}
