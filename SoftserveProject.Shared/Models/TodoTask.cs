using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftserveProject.Shared.Models {
    [FirestoreData]
    class TodoTask {
        public string Id { get; set; }
        [FirestoreProperty]
        public DateTime CreatedAt { get; set; }
        [FirestoreProperty]
        public string TaskName { get; set; }
        public string Description { get; set; }
    }
}
