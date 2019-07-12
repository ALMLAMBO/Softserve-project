using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftserveProject.Shared.Models;

namespace SoftserveProject.Server.DataAccess {
    public class TaskDataAccessLayer {
        private string projectId;
        private FirestoreDb firestoreDb;

        public TaskDataAccessLayer() {
            string filepath = "C:\\Users\\AAA\\Desktop\\softserve-app-5dab0-90c2a616e393.json";

            Environment.SetEnvironmentVariable(
                "GOOGLE_APPLICATION_CREDENTIALS", filepath);

            projectId = "softserve-app-5dab0";
            firestoreDb = FirestoreDb.Create(projectId);
        }

        public async Task<List<TodoTask>> GetAllTasks() {
            Query todoTaskQuery = firestoreDb.Collection("tasks");
            QuerySnapshot todoTaskQuerySnapshot = await
                todoTaskQuery.GetSnapshotAsync();

            List<TodoTask> todoTasks = new List<TodoTask>();

            foreach (DocumentSnapshot ds in
                    todoTaskQuerySnapshot.Documents) {

                if (ds.Exists) {
                    TodoTask newTodoTask = ds
                        .ConvertTo<TodoTask>();

					newTodoTask.Id = ds.Id;
					newTodoTask.CreatedAt = ds.CreateTime
						.Value.ToDateTime();

					Dictionary<string, object> task = ds.ToDictionary();
					newTodoTask.TaskName = task["TaskName"].ToString();

                    todoTasks.Add(newTodoTask);
                }

            }

			return todoTasks
                   .OrderBy(x => x.CreatedAt)
                   .ToList();
        }

		public async void AddTodoTask(TodoTask task) {
			CollectionReference collection = 
				firestoreDb.Collection("tasks");

			task.CreatedAt = DateTime.SpecifyKind(
				DateTime.Now, 
				DateTimeKind.Utc
			);

			DocumentReference dr = await collection.AddAsync(task);
		}

		public async void UpdateTodoTask(TodoTask task) {
			DocumentReference todoRef = firestoreDb
				.Collection("tasks")
				.Document(task.Id);

			task.CreatedAt = DateTime.SpecifyKind(
				DateTime.Now,
				DateTimeKind.Utc
			);

			await todoRef
				.SetAsync(task, SetOptions.Overwrite);
		}

		public async Task<TodoTask>	GetTodoData(string id) {
			DocumentReference dr = firestoreDb
				.Collection("tasks")
				.Document(id);

			DocumentSnapshot snapshot = await 
				dr.GetSnapshotAsync();

			if(snapshot.Exists) {
				TodoTask task = snapshot
					.ConvertTo<TodoTask>();

				task.Id = snapshot.Id;
				return task;
			}
			else {
				return new TodoTask();
			}
		}

		public async void DeleteTodoTask(string id) {
			DocumentReference document = firestoreDb
				.Collection("tasks")
				.Document(id);

			await document.DeleteAsync();
		}
    }
}
