<?php
    $categories=[
        [
            'id'=>1,'category'=>'Action'
        ],
        [
            'id'=>2,'category'=>'Drama'
        ],
        [
            'id'=>13,'category'=>'Comedy'
        ],
    ];
    ?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    
<form action="" method="post">
    <select name="category" id="Category">
    <?php foreach ($categories as $category): ?>
        <option value="<?php echo $category['id']; ?>"><?php echo $category['category']; ?></option>
    <?php endforeach; ?>
    </select>
        <br><br>
    <?php foreach ($categories as $category): ?>
        <input type="radio" name="category_radio" value="<?php echo $category['id']; ?>" id="cat_<?php echo $category['id']; ?>">
        <label for="cat_<?php echo $category['id']; ?>"><?php  echo $category['category']; ?></label><br>
    <?php endforeach; ?>
        <br><br>
    <?php foreach ($categories as $category): ?>
        <input type="checkbox" name="category_checkbox[]" value="<?php echo $category['id']; ?>" id="cat_cb_<?php echo $category['id']; ?>">
        <label for="cat_cb_<?php echo $category['id']; ?>"><?php  echo $category['category']; ?></label><br>
    <?php endforeach; ?>
    
    <br><br>
    <button>Submit</button>

</form>

</body>
</html>