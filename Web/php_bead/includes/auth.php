<?php
session_start();

function isLoggedIn() {
    return isset($_SESSION['user']);
}

function currentUser() {
    return $_SESSION['user'] ?? null;
}
