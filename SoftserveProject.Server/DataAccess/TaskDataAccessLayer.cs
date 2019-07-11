using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftserveProject.Server.DataAccess {
    public class TaskDataAccessLayer {
        private string projectId;
        FirestoreDb firestoreDb;

        public TaskDataAccessLayer() {
            string filepath = "C:\\Users\\AAA\\Desktop\\softserve-app-5dab0-90c2a616e393.json";

            Environment.SetEnvironmentVariable(
                "GOOGLE_APPLICATION_CREDENTIALS", filepath);

            projectId = "softserve-app";
            firestoreDb = FirestoreDb.Create(projectId);
        }
    }
}
