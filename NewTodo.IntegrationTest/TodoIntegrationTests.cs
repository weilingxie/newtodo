using System;
using System.Net;
using NewTodo.Application.TodoItems.Models;
using NewTodo.Domain.Constants;
using NewTodo.IntegrationTest.Helpers;
using Newtonsoft.Json;
using RestSharp;
using Xunit;

namespace NewTodo.IntegrationTest
{
    [Trait("Category", "Integration")]
    public class TodoIntegrationTest
    {
        private readonly ServerOptions _serverOptions;
        private const string Resource = "todo";
        private readonly string _userId;
        private readonly string _title;

        public TodoIntegrationTest()
        {
            _serverOptions = TestHelper.GetServerOptions(Environment.CurrentDirectory);
            _userId = Guid.NewGuid().ToString();
            _title = "title1";
        }

        [Fact]
        public void ShouldBeAbleToCreateTodoItem()
        {
            var client = new RestClient($"{_serverOptions.BaseUrl}/{Resource}/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            var body = $"{{\"userId\": \"{_userId}\",\"title\": \"{_title}\"}}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            var response = client.Execute(request);
            var todoOutput = JsonConvert.DeserializeObject<TodoOutput>(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal(Guid.Parse(_userId), todoOutput.UserId);
            Assert.Equal(_title, todoOutput.Title);
            Assert.Equal(TodoState.Todo, todoOutput.State);
        }
    }
}