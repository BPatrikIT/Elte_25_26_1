const XMAX = 10;
const YMAX = 10;

/* -----------------------------------------------------------
   LOAD STATIONS.JSON
----------------------------------------------------------- */
export async function loadStationsJson() {
    const response = await fetch("js/data/stations.json");
    const data = await response.json();

    return data.map(st => ({
        id: st.id,
        x: st.x,
        y: st.y,
        pos: st.y * XMAX + st.x,
        type: st.type,
        train: st.train,
        side: st.side,
        district: st.district
    }));
}

/* -----------------------------------------------------------
   LOAD LINES.JSON
----------------------------------------------------------- */
export async function loadLinesJson() {
    const response = await fetch("js/data/lines.json");
    const data = await response.json();

    return data.map(line => ({
        id: line.id,
        name: line.name,
        color: line.color,
        start: line.start
    }));
}

/* -----------------------------------------------------------
   CREATE STATION MAP (array indexed by pos)
----------------------------------------------------------- */
export function createStationMap(stations) {
    const map = [];
    stations.forEach(st => {
        map[st.pos] = st;
    });
    return map;
}

/* -----------------------------------------------------------
   ATTACH LINES TO STATIONS
----------------------------------------------------------- */
export function attachLinesToStations(stationMap, lines) {
    lines.forEach(line => {
        const startPos = line.start;

        stationMap.forEach(station => {
            if (station && station.id === startPos) {
                station.line = line.name;
                station.lineColor = line.color;
                station.lineId = line.id;
            }
        });
    });

    return stationMap;
}

export { XMAX, YMAX };
