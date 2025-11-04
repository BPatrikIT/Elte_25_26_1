function calculate(r) {
    const radius = parseFloat(r);
    if (isNaN(radius)) {
        resultSpan.textContent = "Kérlek, adj meg egy érvényes számot!";
        rInput.focus();
        rInput.classList.add("error");
        return;
    }
    const circumference = 2 * Math.PI * radius;
    resultSpan.textContent = circumference.toFixed(2);
    rInput.classList.remove("error");    
}

const rInput = document.getElementById("sugarInput");
const resultSpan = document.getElementById("Result");
const submitBtn = document.getElementById("submitBtn");

submitBtn.addEventListener("click", () => calculate(rInput.value));

