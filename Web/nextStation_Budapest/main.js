import { MenuComponent } from "./components/menu.js";
import { createGameDescription } from "./components/description.js";
import { createAboutUs } from "./components/about.js";
import { GameComponent, initGame } from "./components/game.js";

const app = document.querySelector("#app");

export async function loadPage(page) {
    app.innerHTML = "";

    if (page === "menu") {
        app.appendChild(MenuComponent());
        return;
    }

    if (page === "description") {
        app.appendChild(createGameDescription());
        return;
    }

    if (page === "about") {
        app.appendChild(createAboutUs());
        return;
    }

    if (page === "game") {
        app.appendChild(GameComponent());
        const container = document.querySelector(".game-container");
        initGame(container);
    }
}

// initial load
loadPage("menu");