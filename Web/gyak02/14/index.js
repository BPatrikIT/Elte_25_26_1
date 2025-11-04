function submitForm() {
    event.preventDefault();
    const date = parseFloat(document.getElementById("evszam").value);
    const filteredBooks = library.filter(book => book.ev == date);
    const listPlace = document.getElementById("lista");
    listPlace.textContent = "";
    if (filteredBooks.length <= 0) {
        listPlace.textContent = "Nincs találat";
    } else {
        for (let index = 0; index < filteredBooks.length; index++) {
            
            const element = filteredBooks[index];
            const listItem = document.createElement("li");
            listItem.textContent = `${element.cim} (${element.ev})`;
            listPlace.appendChild(listItem);
        }
    }
}

const submitButton = document.getElementById("listazz");

submitButton.addEventListener("click", submitForm);

const library = [
    {
        szerzo: "J. K. Rowling",
        cim: "Harry Potter és a bölcsek köve",
        ev: 1997,
        kiado: "Bloomsbury",
        isbn: "978-963-07-3456-0"
    },
    {
        szerzo: "George Orwell",
        cim: "1984",
        ev: 1949,
        kiado: "Secker & Warburg",
        isbn: "978-0-452-28423-4"
    },
    {
        szerzo: "Neil Gaiman",
        cim: "Neverwhere",
        ev: 1997,
        kiado: "BBC Books",
        isbn: "978-0-7475-3270-8"
    }
];


















//+feladatok
//A 1990 után megjelent könyvek címei nagybetűsen consol-ra kiírva
//Van-e olyan könyv, aminek a címe 10 karakternél hosszabb, és „9”-essel kezdődik az ISBN-je?
