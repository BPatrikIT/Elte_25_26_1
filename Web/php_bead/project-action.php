<?php
require_once 'includes/json.php';
require_once 'includes/auth.php';

if (!isLoggedIn() || !currentUser()['is_admin']) {
    header('Location: index.php');
    exit;
}

$projects = readJson('data/projects.json');

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $projectId = (int)($_POST['id'] ?? 0);
    $action = $_POST['action'] ?? '';

    foreach ($projects as &$p) {
        if ($p['id'] === $projectId && $p['status'] === 0) { // csak pending
            if ($action === 'approve') {
                $p['status'] = 1; // approved
                $p['approved'] = date('Y-m-d H:i');
            } elseif ($action === 'reject') {
                $p['status'] = 2; // rejected
            }
            break;
        }
    }
    unset($p); // referencia feloldása

    file_put_contents('data/projects.json', json_encode($projects, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE));

    // vissza az admin oldalra
    header('Location: projects-admin.php');
    exit;
}
