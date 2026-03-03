$(document).ready(function () {

    function updateCounts() {
        $("#totalCount").text($("#taskList li").length);
        $("#completedCount").text($("#taskList li.completed").length);
    }

    // Add Task
    $("#addTask").on("click", function () {

        let taskText = $("#taskInput").val().trim();

        if (taskText !== "") {

            let newTask = `
                <li>
                    <span class="task-text">${taskText}</span>
                    <button class="delete-btn">Delete</button>
                </li>
            `;

            $("#taskList").append(newTask);  // append() used
            $("#taskInput").val("");
            updateCounts();
        }
    });

    // Mark as Completed (Event Delegation)
    $("#taskList").on("click", ".task-text", function () {
        $(this).parent().toggleClass("completed");  // toggleClass() used
        updateCounts();
    });

    // Delete Task (Event Delegation)
    $("#taskList").on("click", ".delete-btn", function () {
        $(this).parent().remove();  // remove() used
        updateCounts();
    });

});