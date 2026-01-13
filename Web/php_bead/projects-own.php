<?php
require_once 'includes/json.php';
require_once 'includes/auth.php';

if (!isLoggedIn()) {
    header('Location: index.php');
    exit;
}

$projects = readJson('data/projects.json');
$categories = readJson('data/categories.json');
$currentUserId = currentUser()['id'];
?>

<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <title>Saját projektek</title>
    <link rel="stylesheet" href="assets/style.css">
</head>
<body>
<div class="container">

<header>
    <h1>Saját nem közzétett projektek</h1>
    <div>
        <a href="index.php">← Vissza a főoldalra</a>
    </div>
</header>

<?php
$hasProjects = false;
foreach ($projects as $p) {
    // Csak a saját projekt, ami nem approved
    if ($p['owner'] === $currentUserId && $p['status'] != 1) {
        $hasProjects = true;
        $categoryName = '';
        foreach ($categories as $c) {
            if ($c['id'] === $p['category']) {
                $categoryName = $c['text'];
                break;
            }
        }
        ?>
        <div class="project">
            <a href="project.php?id=<?= $p['id'] ?>">
                <?= htmlspecialchars($p['title']) ?>
            </a>
            <span class="status">(<?= htmlspecialchars($categoryName) ?> - 
                <?php
                switch ($p['status']) {
                    case 0: echo 'pending'; break;
                    case 2: echo 'rework'; break;
                    case 3: echo 'rejected'; break;
                }
                ?>)
            </span>
        </div>
        <?php
    }
}

if (!$hasProjects) {
    echo '<p>Még nincs nem közzétett projekted.</p>';
}
?>

</div>
</body>
</html>
