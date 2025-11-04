const button = document.querySelector("#myButton");
const demo = document.querySelector("#demo");
var count = 0;

button.addEventListener("click", sayHello);

function sayHello() {
    count++;
    demo.textContent = "Button clicked " + count + " times!";
}
