let baseUrl = "https://localhost:44370/api/todotask";
let listContainer = document.getElementById("list");
let addInput = document.getElementById("input-field");
let submitBtn = document.getElementById("subBtn");
let xhttp = new XMLHttpRequest();
let todos = [];

submitBtn.addEventListener("click", function () {
	if (addInput.value != "" && addInput.value != null) {
		postTodo(addInput.value);
		addInput.value = "";
	}
	else {
		alert("Please give me some value!");
	}
});

function renderItems() {
	listContainer.innerHTML = "";
	for (let i = 0; i < todos.length; i++) {
		listContainer.innerHTML += `
			<li class="item">
				<span>${todos[i].taskName}</span>
				<span class="icons-wrapper">
					<img src="../imgs/edit.svg" alt="edit" onclick="editTodo('${todos[i].id}')">
					<img src="../imgs/rubbish-bin.svg" alt="delete" onclick="deleteTodo('${todos[i].id}')">
				</span>
			</li>
		`;
	}
}

function deleteItem(item) {
	let url = baseUrl + "/delete-task/" + item;

	xhttp.onreadystatechange = function () {
		if (this.readyState == 4 && this.status == 200) {
			getTodos();
		}
	};

	xhttp.open("Delete", url, true);
	xhttp.send();
}

function getTodos() {
	let url = baseUrl + "/get-all-tasks";

	xhttp.onreadystatechange = function () {
		if (this.readyState == 4 && this.status == 200) {
			todos = JSON.parse(this.responseText);
			renderItems();
		}
	};

	xhttp.open("GET", url, true);
	xhttp.send();
}

getTodos();