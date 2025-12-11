<?php
session_start();

include("auth.php");
include("utils.php");
include_once('userstorage.php');

$user_storage = new UserStorage();
$auth = new Auth($user_storage);
$auth->logout();
redirect("login.php");