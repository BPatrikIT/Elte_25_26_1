<?php
    $data = [
        ["name" => "Dr. Programo Zoltán", "color" => "#517d81", "schedule" => [9 => 2, 12 => 4]],
        ["name" => "Koaxk Ábel", "color" => "orange", "schedule" => [11 => 1, 13 => 3]],
        ["name" => "Locsolók Anna", "color" => "red", "schedule" => []],
        ["name" => "Trap Pista", "color" => "navy", "schedule" => [10 => 4, 15 => 2]],
        ["name" => "Wincs Eszter", "color" => "hotpink", "schedule" => [9 => 2, 12 => 3, 15 => 1]],
        ["name" => "Zsíros B. Ödön", "color" => "greenyellow", "schedule" => [9 => 8]]
    ];
?>
<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="index.css">
    <title>Projektmenedzsment</title>
</head>
<body>
    <h1>Projektmenedzsment</h1>
    <table>
        <thead>
        <tr>
            <th>Név</th>
            <?php
                for ($hour = 9; $hour <= 16; $hour++) {
                    echo "<th>$hour:00</th>";
                }
            ?>
            <!-- órák -->
        </tr>
        </thead>
        <tbody>
            <?php
            $sumHours = 0;
            // egy sor minden alkalmazottra
            foreach ($data as $employee) {
                echo "<tr>";
                echo "<td>{$employee['name']}</td>";
                for ($hour = 9; $hour <= 16; $hour++) {
                    if (array_key_exists($hour, $employee['schedule'])) {
                        $workHours = $employee['schedule'][$hour];
                        $hour += $employee['schedule'][$hour] - 1; // ugrás a következő szabad órára
                        if (array_key_exists($hour + 1, $employee['schedule'])) {
                             $workHours += $employee['schedule'][$hour + 1];
                             $hour++;
                        }
                        $colspan = $workHours;
                        $color = $employee['color'];
                        echo "<td colspan='$colspan' style='background-color: $color; text-align: center;'></td>";
                        $sumHours += $workHours;
                    } else {
                        echo "<td></td>";
                    }
                }
            }
        ?>
        </tbody>
    </table>
    <b>Összes munkaóra: </b> <?php echo $sumHours; ?> óra
</body>
</html>
