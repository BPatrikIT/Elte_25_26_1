import { loadPage } from "../main.js";

const XMAX = 10;
const YMAX = 10;

export function GameComponent() {

    const container = document.createElement("div");
    container.classList.add("game-container");

    let stationsMap = initializeGame();

    return container;
}

function initializeGame() {
    let stationsMap = initializeStations(10, 10);
    renderGame(stationsMap);

    return stationsMap;
}

function renderGame(stationsMap) {
    const container = document.querySelector(".game-container");
    //if (!container) return;

    // clear previous content
    container.innerHTML = "";

    // grid wrapper
    const grid = document.createElement("div");
    grid.className = "station-grid";
    grid.style.display = "grid";
    grid.style.gridTemplateColumns = `repeat(${XMAX}, ${100 / XMAX}%)`;
    grid.style.gap = "0";
    grid.style.width = "100%";
    grid.style.maxWidth = `${XMAX * 48}px`; // limit full size
    grid.style.margin = "0 auto";

    // create square cells with full borders
    const cellSize = Math.floor(Math.min(48, 480 / Math.max(XMAX, YMAX)));
    for (let y = 0; y < YMAX; y++) {
        for (let x = 0; x < XMAX; x++) {
            const cell = document.createElement("div");
            cell.className = "grid-cell";
            cell.style.boxSizing = "border-box";
            cell.style.width = `${cellSize}px`;
            cell.style.height = `${cellSize}px`;
            cell.style.border = "1px solid #222";
            cell.style.display = "flex";
            cell.style.alignItems = "center";
            cell.style.justifyContent = "center";
            cell.style.fontSize = "12px";
            cell.style.userSelect = "none";
            cell.dataset.x = x;
            cell.dataset.y = y;

            // compute standard linear index (row-major)
            const index = y * XMAX + x;
            const stationObj = stationsMap && stationsMap[index];

            if (stationObj) {
                cell.classList.add("station");
                cell.title = `${stationObj.typeof || stationObj.type || ""}`.trim();
                const mark = document.createElement("div");
                mark.textContent = "🚉";
                mark.style.pointerEvents = "none";
                cell.appendChild(mark);
            }

            grid.appendChild(cell);
        }
    }

    container.appendChild(grid);
}

class station {
    x;
    y;
    pos;
    typeof;
    train;
    side;
    district;
}

function stationPos(x, y) {
    return y * XMAX + x;
}

function createStation(x, y, type, train, side, district) {
    const newStation = new station();
    newStation.x = x;
    newStation.y = y;
    newStation.pos = stationPos(x, y);
    newStation.typeof = type;
    newStation.train = train;
    newStation.side = side;
    newStation.district = district;
    return newStation;
}

function loadStationsJson() {
    const stations = [];
    fetch("js/data/stations.json")
    .then(response => response.json())
    .then(data => {
        data.forEach(stationData => {
            const stationObj = createStation(
                stationData.x,
                stationData.y,
                stationData.type,
                stationData.train,
                stationData.side,
                stationData.district
            );
            stations.push(stationObj);
        });
    });

    return stations;
}

function initializeStations(xMax, yMax) {
    const stations = loadStationsJson();
    const stationsMap = [];
    const maxLength = xMax * yMax;
    
    stations.forEach(element => {
        stationsMap[element.pos] = element;
    });

    return stationsMap;
}
