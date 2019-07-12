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

            projectId = "softserve-app";
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

                    todoTasks.Add(newTodoTask);
                }

            }

			return todoTasks
                   .OrderBy(x => x.CreatedAt)
                   .ToList();
        }

		public async void AddTodoTask(Task task) {
			CollectionReference collection = 
				firestoreDb.Collection("tasks");

			await collection.AddAsync(task);
		}
    }
}
