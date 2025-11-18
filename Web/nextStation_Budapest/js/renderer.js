// js/renderer.js
import { XMAX, YMAX } from "./stations.js";
import { loadPage } from "../main.js";
import { onStationClicked } from "../components/game.js";
import { getConnectedTrainStationCount, TRAIN_POINTS_TABLE } from "./line-drawing.js";

export function renderGame(container, stationsMap) {

    container.innerHTML = "";

    /* ------------------------------------------------------------------
       CREATE GAME HEADER
    ------------------------------------------------------------------ */
    const header = document.createElement("div");
    header.classList.add("game-header");

    const userName = localStorage.getItem("currentPlayer") || "Player";
    const nameEl = document.createElement("div");
    nameEl.innerHTML = `<strong>${userName}</strong>`;

    const timerEl = document.createElement("div");
    timerEl.classList.add("game-timer");

    let startTime = Number(localStorage.getItem("currentStartTime"));
    if (!startTime) {
        startTime = Date.now();
        localStorage.setItem("currentStartTime", String(startTime));
    }

    function updateTimer() {
        const elapsedSec = Math.floor((Date.now() - startTime) / 1000);
        const h = Math.floor(elapsedSec / 3600);
        const m = Math.floor((elapsedSec % 3600) / 60);
        const s = elapsedSec % 60;
        timerEl.textContent = `${h.toString().padStart(2,'0')}:${m.toString().padStart(2,'0')}:${s.toString().padStart(2,'0')}`;
    }

    if (window.__gameTimer) clearInterval(window.__gameTimer);
    updateTimer();
    window.__gameTimer = setInterval(updateTimer, 1000);

    const exitBtn = document.createElement("button");
    exitBtn.textContent = "Exit";
    exitBtn.classList.add("exit-button");
    exitBtn.onclick = () => {
        if (window.__gameTimer) clearInterval(window.__gameTimer);
        loadPage("menu");
    };

    // Current Line Indicator
    let currentLine = { name: "M1", color: "#f1c40f" };
    try {
        const saved = localStorage.getItem("currentLine");
        if (saved) currentLine = JSON.parse(saved);
    } catch(e){}

    const lineInfo = document.createElement("div");
    lineInfo.style.display = "flex";
    lineInfo.style.alignItems = "center";
    lineInfo.style.gap = "8px";
    lineInfo.innerHTML = `<div style="width:20px;height:20px;background:${currentLine.color};border-radius:4px;"></div> <span>${currentLine.name}</span>`;

    // Card Display
    const cardDisplay = document.createElement("div");
    cardDisplay.classList.add("card-display");
    cardDisplay.textContent = "---";

    const drawBtn = document.createElement("button");
    drawBtn.classList.add("draw-card-button");
    drawBtn.textContent = "Draw Card";
    drawBtn.onclick = () => {
        container.dispatchEvent(new CustomEvent("draw-card", { bubbles: true }));
    };

    header.appendChild(nameEl);
    header.appendChild(timerEl);
    header.appendChild(lineInfo);
    header.appendChild(cardDisplay);
    header.appendChild(drawBtn);
    header.appendChild(exitBtn);

    container.appendChild(header);

    /* ------------------------------------------------------------------
       GAME GRID
    ------------------------------------------------------------------ */
    const gridWrapper = document.createElement("div");
    gridWrapper.classList.add("game-grid-wrapper");

    const grid = document.createElement("div");
    grid.classList.add("station-grid");

    for (let i = 0; i < stationsMap.length; i++) {
        const station = stationsMap[i];
        const cell = document.createElement("div");
        cell.classList.add("grid-cell");
        cell.dataset.pos = i;
        
        const x = i % XMAX;
        const y = Math.floor(i / XMAX);
        cell.dataset.x = x;
        cell.dataset.y = y;

        if (station) {
            cell.classList.add("station");
            cell.dataset.type = station.type;
            
            if (station.train) cell.dataset.train = "true";
            
            const span = document.createElement("span");
            span.textContent = station.type === "?" ? "★" : station.type;
            cell.appendChild(span);

            if (station.lineColor) {
                cell.style.setProperty("--line-color", station.lineColor);
                cell.setAttribute("data-line-color", station.lineColor);
            }
        }

        grid.appendChild(cell);
    }

    grid.addEventListener("click", (e) => {
        const cell = e.target.closest(".grid-cell");
        if (cell) {
            const pos = parseInt(cell.dataset.pos);
            onStationClicked(pos);
        }
    });

    gridWrapper.appendChild(grid);
    container.appendChild(gridWrapper);
}

/* ------------------------------------------------------------------
   MODALS FOR SCORING
------------------------------------------------------------------ */
export function showRoundResultModal(roundIndex, scoreData, onNext) {
    const overlay = document.createElement("div");
    overlay.classList.add("game-modal-overlay");

    const modal = document.createElement("div");
    modal.classList.add("game-modal");

    modal.innerHTML = `
        <h2>Round ${roundIndex} Complete!</h2>
        <div class="score-details">
            <div><span>Districts Touched (PK):</span> <strong>${scoreData.PK}</strong></div>
            <div><span>Max Stations in District (PM):</span> <strong>${scoreData.PM}</strong></div>
            <div><span>Danube Crossings (PD):</span> <strong>${scoreData.PD}</strong></div>
        </div>
        <div class="score-total">Round Score: ${scoreData.total}</div>
        <button id="nextRoundBtn">Next Metro Line</button>
    `;

    overlay.appendChild(modal);
    document.body.appendChild(overlay);

    const btn = modal.querySelector("#nextRoundBtn");
    btn.focus();
    btn.onclick = () => {
        overlay.remove();
        onNext();
    };
}

export function showGameResultModal(scoreData, onFinish) {
    const overlay = document.createElement("div");
    overlay.classList.add("game-modal-overlay");

    const modal = document.createElement("div");
    modal.classList.add("game-modal");

    modal.innerHTML = `
        <h2>Game Over!</h2>
        <p>Here is your final score breakdown:</p>
        <div class="score-details">
            <div><span>Sum of Rounds (ΣFP):</span> <strong>${scoreData.sumFP}</strong></div>
            <div><span>Train Points (PP):</span> <strong>${scoreData.PP}</strong></div>
            <div><span>2-Line Interchanges (P2 x2):</span> <strong>${scoreData.P2 * 2}</strong></div>
            <div><span>3-Line Interchanges (P3 x5):</span> <strong>${scoreData.P3 * 5}</strong></div>
            <div><span>4-Line Interchanges (P4 x9):</span> <strong>${scoreData.P4 * 9}</strong></div>
        </div>
        <div class="score-total">Total Score: ${scoreData.total}</div>
        <button id="finishGameBtn">Back to Menu</button>
    `;

    overlay.appendChild(modal);
    document.body.appendChild(overlay);

    const btn = modal.querySelector("#finishGameBtn");
    btn.focus();
    btn.onclick = () => {
        overlay.remove();
        onFinish();
    };
}