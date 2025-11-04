document.addEventListener("click", onClick);

function onClick(event) {
    if (event.target.matches("a")) {
        if (!event.target.href.includes("elte.hu")) {
        event.preventDefault();
        alert("Csak az ELTE honlapjára mutató linkek nyithatók meg.");
        }
    }
}