<?php
function readJson($path) {
    return json_decode(file_get_contents($path), true) ?? [];
}
