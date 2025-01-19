// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let elememt = document.getElementById("id");

elememt.addEventListener("keyup", () =>
{
    //send request to the back end


    function run() {

        // Creating Our XMLHttpRequest object 
        let xhr = new XMLHttpRequest();

        // Making our connection  
        let url = `https://localhost:7165/Employees/Index?InputSearch={element.value}`;
        xhr.open("Post", url, true);

        // function execute after request is successful 
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                console.log(this.responseText);
            }
        }
        // Sending our request 
        xhr.send();
    }
    run();

}
)