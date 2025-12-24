using Xunit;
using Todo.Core;
using System.Linq;

namespace Todo.Core.Tests
{
    public class TodoListTests
    {
        private const string TestFile = "todos.json";
        [Fact]
        public void Add_IncreasesCount()
        {
            var list = new TodoList();
            list.Add("  task  ");
            Assert.Equal(1, list.Count);
            Assert.Equal("task", list.Items.First().Title);
        }

        [Fact]
        public void Remove_ById_Works()
        {
            var list = new TodoList();
            var item = list.Add("a");
            Assert.True(list.Remove(item.Id));
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void Find_ReturnsMatches()
        {
            var list = new TodoList();
            list.Add("Buy milk");
            list.Add("Read book");
            var found = list.Find("buy").ToList();
            Assert.Single(found);
            Assert.Equal("Buy milk", found[0].Title);
        }

        [Fact]
        public void SaveAndLoad_ShouldPreserveTasks()
        {
            // Arrange
            var list = new TodoList();
            list.Add("Task 1");
            list.Add("Task 2");

    
            list.Save(TestFile);
            var loadedList = new TodoList();
            loadedList.Load(TestFile);
 
            Assert.Equal(list.Count, loadedList.Count);
            for (int i = 0; i < list.Count; i++)
            {
                Assert.Equal(list.Items[i].Title, loadedList.Items[i].Title);
                Assert.Equal(list.Items[i].IsDone, loadedList.Items[i].IsDone);
            }

            if (File.Exists(TestFile)) File.Delete(TestFile);
        }
    }
}
 