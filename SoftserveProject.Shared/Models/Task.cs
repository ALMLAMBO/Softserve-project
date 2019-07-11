﻿using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftserveProject.Shared.Models {
    [FirestoreData]
    class Task {
        public string Id { get; set; }
        [FirestoreProperty]
        public DateTime createdAt { get; set; }
        [FirestoreProperty]
        public string TaskName { get; set; }
        [FirestoreProperty]
        public string Description { get; set; }
    }
}
