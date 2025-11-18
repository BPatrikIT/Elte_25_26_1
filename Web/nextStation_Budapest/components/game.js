import { loadStationsJson, loadLinesJson, createStationMap, attachLinesToStations } from "../js/stations.js";
import { renderGame } from "../js/renderer.js";

export function GameComponent() {
    const container = document.createElement("div");
    container.classList.add("game-container");
    return container;
}

export async function initGame() {
    const container = document.querySelector("#app");

    // load data
    const stations = await loadStationsJson();
    const lines = await loadLinesJson();

    // build station map
    let stationMap = createStationMap(stations);

    // merge line data
    stationMap = attachLinesToStations(stationMap, lines);

    // render
    renderGame(container, stationMap);
}
