using Microsoft.Data.Sqlite;

namespace Securitybot
{
    internal class TaskRepository
    {
        private readonly string connectionString;

        public TaskRepository()
        {
            string databasePath = Path.Combine(AppContext.BaseDirectory, "securitybot_tasks.db");
            connectionString = $"Data Source={databasePath}";
            CreateDatabase();
        }

        public List<TaskItem> GetTasks()
        {
            List<TaskItem> tasks = new List<TaskItem>();

            using SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT Id, Title, Description, ReminderDate, Category, IsComplete
                FROM Tasks
                ORDER BY ReminderDate;";

            using SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                tasks.Add(new TaskItem
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    ReminderDate = DateTime.Parse(reader.GetString(3)),
                    Category = reader.GetString(4),
                    IsComplete = reader.GetInt32(5) == 1
                });
            }

            return tasks;
        }

        public int AddTask(TaskItem task)
        {
            using SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Tasks (Title, Description, ReminderDate, Category, IsComplete)
                VALUES ($title, $description, $reminderDate, $category, $isComplete);
                SELECT last_insert_rowid();";
            command.Parameters.AddWithValue("$title", task.Title);
            command.Parameters.AddWithValue("$description", task.Description);
            command.Parameters.AddWithValue("$reminderDate", task.ReminderDate.ToString("O"));
            command.Parameters.AddWithValue("$category", task.Category);
            command.Parameters.AddWithValue("$isComplete", task.IsComplete ? 1 : 0);

            object? result = command.ExecuteScalar();
            return Convert.ToInt32(result);
        }

        public void MarkTaskComplete(int taskId)
        {
            using SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Tasks
                SET IsComplete = 1
                WHERE Id = $id;";
            command.Parameters.AddWithValue("$id", taskId);
            command.ExecuteNonQuery();
        }

        public void DeleteTask(int taskId)
        {
            using SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                DELETE FROM Tasks
                WHERE Id = $id;";
            command.Parameters.AddWithValue("$id", taskId);
            command.ExecuteNonQuery();
        }

        private void CreateDatabase()
        {
            using SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Tasks (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Description TEXT NOT NULL,
                    ReminderDate TEXT NOT NULL,
                    Category TEXT NOT NULL,
                    IsComplete INTEGER NOT NULL
                );";
            command.ExecuteNonQuery();
        }
    }
}
