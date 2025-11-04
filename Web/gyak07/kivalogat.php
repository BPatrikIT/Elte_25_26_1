<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Kivalogatas</title>
</head>
<body>
    <?php
    $limit = 0;
    $numbers = [1, -8, 3, 4, -6, 9];
    
    $res = array_filter($numbers, function($n) use ($limit) {
        return $n < $limit;
    });

    echo "<ul>";
    foreach ($res as $number) {
        echo "<li style=' font-size: 20px;'><strong>" . $number . "</strong></li>";
    }
    echo "</ul>";
    ?>
</body>
</html>