import { XMAX, YMAX } from "./stations.js";
import { loadPage } from "../main.js";

export function renderGame(container, stationsMap) {

    container.innerHTML = "";

    /* ------------------------------------------------------------------
       CREATE GAME HEADER (outside grid, above everything)
    ------------------------------------------------------------------ */
    const header = document.createElement("div");
    header.classList.add("game-header");

    const userName = localStorage.getItem("currentPlayer") || "Player";

    const nameEl = document.createElement("div");
    nameEl.innerHTML = `<strong>${userName}</strong>`;

    const timerEl = document.createElement("div");
    timerEl.classList.add("game-timer");

    // read or set timer
    let startTime = Number(localStorage.getItem("gameStartTime"));
    if (!startTime) {
        startTime = Date.now();
        localStorage.setItem("gameStartTime", String(startTime));
    }

    function updateTimer() {
        const elapsedSec = Math.floor((Date.now() - startTime) / 1000);
        const { hours, minutes, seconds } = convertSecondsToHMS(elapsedSec);
        timerEl.textContent = `Time: ${hours}h ${minutes}m ${seconds}s`;
    }

    function convertSecondsToHMS(totalSeconds) {
        const hours = Math.floor(totalSeconds / 3600);
        const minutes = Math.floor((totalSeconds % 3600) / 60);
        const seconds = totalSeconds % 60;

        return { hours, minutes, seconds };
    }

    updateTimer();
    if (window.__nextStationTimerId) clearInterval(window.__nextStationTimerId);
    window.__nextStationTimerId = setInterval(updateTimer, 1000);

    // exit button
    const exitBtn = document.createElement("button");
    exitBtn.textContent = "Exit";
    exitBtn.classList.add("exit-button");

    exitBtn.onclick = () => {
        // stop timer
        if (window.__nextStationTimerId) clearInterval(window.__nextStationTimerId);

        // reset game timer value
        localStorage.removeItem("gameStartTime");

        // navigate using SPA logic
        loadPage("menu");
    };


    // current line indicator
    let currentLine = { name: "None", color: "#888" };
    try {
        const raw = localStorage.getItem("currentLine");
        if (raw) currentLine = JSON.parse(raw);
    } catch {}

    const lineEl = document.createElement("div");
    lineEl.classList.add("current-line");
    lineEl.style.display = "flex";
    lineEl.style.alignItems = "center";
    lineEl.style.gap = "8px";

    const colorBox = document.createElement("span");
    colorBox.style.width = "16px";
    colorBox.style.height = "16px";
    colorBox.style.background = currentLine.color;
    colorBox.style.border = "1px solid #333";
    colorBox.style.borderRadius = "2px";

    const lineName = document.createElement("span");
    lineName.textContent = currentLine.name;

    lineEl.appendChild(colorBox);
    lineEl.appendChild(lineName);

    // add elements to header
    header.appendChild(nameEl);
    header.appendChild(timerEl);
    header.appendChild(lineEl);
    header.appendChild(exitBtn);

    container.appendChild(header);


    /* ------------------------------------------------------------------
       GAME GRID SECTION
    ------------------------------------------------------------------ */

    const gridWrapper = document.createElement("div");
    gridWrapper.classList.add("game-grid-wrapper");

    const grid = document.createElement("div");
    grid.classList.add("station-grid");

    const cellSize = 48;

    for (let y = 0; y < YMAX; y++) {
        for (let x = 0; x < XMAX; x++) {

            const index = y * XMAX + x;
            const station = stationsMap[index];

            const cell = document.createElement("div");
            cell.classList.add("grid-cell");
            cell.dataset.x = x;
            cell.dataset.y = y;

            cell.style.width = `${cellSize}px`;
            cell.style.height = `${cellSize}px`;

            if (station) {
                cell.classList.add("station");
                cell.title = station.type;
                cell.textContent = station.type;

                if (station.lineColor) {
                    cell.style.backgroundColor = station.lineColor;
                    cell.style.color = "#000";  // ensure readable text
                }
            }

            grid.appendChild(cell);
        }
    }

    gridWrapper.appendChild(grid);
    container.appendChild(gridWrapper);
}