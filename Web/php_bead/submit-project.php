<?php
require_once 'includes/json.php';
require_once 'includes/auth.php';

if (!isLoggedIn()) {
    header('Location: index.php');
    exit;
}

$categories = readJson('data/categories.json');
$projects   = readJson('data/projects.json');

$error = '';
$success = '';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $title       = trim($_POST['title'] ?? '');
    $description = trim($_POST['description'] ?? '');
    $category    = (int)($_POST['category'] ?? -1);
    $postal_code = trim($_POST['postal_code'] ?? '');
    $image       = trim($_POST['image'] ?? '');

    // VALIDÁCIÓK
    if (strlen($title) < 10) {
        $error = 'A projekt címe legalább 10 karakter legyen.';
    } elseif (strlen($description) < 150) {
        $error = 'A projekt leírása legalább 150 karakter legyen.';
    } elseif (!in_array($category, array_column($categories, 'id'))) {
        $error = 'Érvénytelen kategória.';
    } else {
        // Irányítószám ellenőrzés teljes pontért
        if ($postal_code !== '1007') {
            if (!preg_match('/^1(0[1-9]|1[0-9]|2[0-3])[1-9]$/', $postal_code)) {
                $error = 'Irányítószám érvénytelen (teljes pontért: 1XXY, XX=01-23, Y=1-9, vagy 1007).';
            }
        }
    }

    if (!$error) {
        $newId = $projects ? max(array_column($projects, 'id')) + 1 : 1;

        // Rövid demo: kategória 0 → approved, mások → pending
        $status = ($category === 0) ? 1 : 0; // 1=approved, 0=pending

        $projects[] = [
            'id' => $newId,
            'status' => $status,
            'title' => $title,
            'description' => $description,
            'category' => $category,
            'postal_code' => $postal_code,
            'image' => $image,
            'owner' => currentUser()['id'],
            'submitted' => date('Y-m-d H:i'),
            'approved' => $status === 1 ? date('Y-m-d H:i') : ''
        ];

        file_put_contents('data/projects.json', json_encode($projects, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE));

        // --- Irányítás a főoldalra siker esetén ---
        header('Location: index.php?success=1');
        exit;
    }
}

?>
<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <title>Új projekt leadása</title>
    <link rel="stylesheet" href="assets/style.css">
</head>
<body>
<div class="container">

<header>
    <h1>Új projekt leadása</h1>
    <div>
        <a href="index.php">← Vissza a főoldalra</a>
    </div>
</header>

<?php if ($error): ?>
    <p class="closed"><?= htmlspecialchars($error) ?></p>
<?php endif; ?>
<?php if ($success): ?>
    <p style="color:green; font-weight:bold; margin-bottom:15px;"><?= htmlspecialchars($success) ?></p>
<?php endif; ?>

<form method="post">
    <label>Projekt címe</label>
    <input type="text" name="title" required value="<?= htmlspecialchars($_POST['title'] ?? '') ?>">

    <label>Leírás</label>
    <textarea name="description" rows="6" required><?= htmlspecialchars($_POST['description'] ?? '') ?></textarea>

    <label>Kategória</label>
    <select name="category" required>
        <?php foreach ($categories as $c): ?>
            <option value="<?= $c['id'] ?>" <?= (($_POST['category'] ?? '') == $c['id']) ? 'selected' : '' ?>>
                <?= htmlspecialchars($c['text']) ?>
            </option>
        <?php endforeach; ?>
    </select>

    <label>Irányítószám</label>
    <input type="text" name="postal_code" required maxlength="4" value="<?= htmlspecialchars($_POST['postal_code'] ?? '') ?>">

    <label>Kép URL (opcionális)</label>
    <input type="text" name="image" value="<?= htmlspecialchars($_POST['image'] ?? '') ?>">

    <button type="submit">Projekt leadása</button>
</form>

</div>
</body>
</html>