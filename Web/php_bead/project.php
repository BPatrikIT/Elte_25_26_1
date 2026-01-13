<?php
require_once 'includes/json.php';
require_once 'includes/auth.php';

if (!isset($_GET['id'])) {
    header('Location: index.php');
    exit;
}

$projectId = (int)$_GET['id'];

$projects   = readJson('data/projects.json');
$categories = readJson('data/categories.json');
$users      = readJson('data/users.json');

/* Projekt megkeresése */
$project = null;
foreach ($projects as $p) {
    if ($p['id'] === $projectId) {
        $project = $p;
        break;
    }
}

if (!$project) {
    header('Location: index.php');
    exit;
}

/* Tulajdonos */
$owner = null;
foreach ($users as $u) {
    if ($u['id'] === $project['owner']) {
        $owner = $u;
        break;
    }
}

/* Kategória */
$categoryText = '';
foreach ($categories as $c) {
    if ($c['id'] === $project['category']) {
        $categoryText = $c['text'];
        break;
    }
}

/* Jogosultság ellenőrzés: csak tulajdonos vagy admin férhet hozzá, ha nem approved */
$isOwner = isLoggedIn() && currentUser()['id'] === $project['owner'];
$isAdmin = isLoggedIn() && currentUser()['is_admin'];

if ($project['status'] !== 1 && !$isOwner && !$isAdmin) {
    header('Location: index.php');
    exit;
}

/* Státusz szöveg */
$statusText = match ($project['status']) {
    0 => 'Függőben (pending)',
    1 => 'Közzétéve (approved)',
    2 => 'Elutasítva (rejected)',
    3 => 'Javításra visszaküldve (rework)',
    default => 'Ismeretlen'
};
?>
<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <title><?= htmlspecialchars($project['title']) ?></title>
    <link rel="stylesheet" href="assets/style.css">
</head>
<body>

<div class="container">

<header>
    <h1><?= htmlspecialchars($project['title']) ?></h1>
    <div>
        <?php if ($isOwner || $isAdmin): ?>
            <a href="projects-own.php">← Vissza a saját projektekhez</a>
        <?php else: ?>
            <a href="index.php">← Vissza a főoldalra</a>
        <?php endif; ?>
    </div>
</header>

<section class="category">

    <p><strong>Státusz:</strong> <?= $statusText ?></p>
    <p><strong>Kategória:</strong> <?= htmlspecialchars($categoryText) ?></p>
    <p><strong>Irányítószám:</strong> <?= htmlspecialchars($project['postal_code']) ?></p>
    <p><strong>Projektgazda:</strong> <?= htmlspecialchars($owner['username'] ?? 'Ismeretlen') ?></p>
    <p><strong>Leadás dátuma:</strong> <?= htmlspecialchars($project['submitted']) ?></p>

    <?php if (!empty($project['approved'])): ?>
        <p><strong>Közzététel dátuma:</strong> <?= htmlspecialchars($project['approved']) ?></p>
    <?php endif; ?>

    <?php if (!empty($project['image'])): ?>
        <p>
            <img src="<?= htmlspecialchars($project['image']) ?>" alt="Projekt kép" style="max-width:100%; border-radius:6px;">
        </p>
    <?php endif; ?>

    <hr>

    <p><?= nl2br(htmlspecialchars($project['description'])) ?></p>

</section>

<!-- ADMIN műveletek -->
<?php if ($isAdmin && $project['status'] === 0): ?>
    <section class="category">
        <h2>Admin műveletek</h2>

        <form method="post" action="project-action.php">
            <input type="hidden" name="id" value="<?= $project['id'] ?>">

            <button name="action" value="approve">✔ Jóváhagyás</button>
            <button name="action" value="reject">✖ Elutasítás</button>
            <button name="action" value="rework">✎ Javításra visszaküldés</button>
        </form>
    </section>
<?php endif; ?>

</div>

</body>
</html>